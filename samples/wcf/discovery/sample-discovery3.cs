using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Discovery;

public class Test
{
	public static void Main ()
	{
		RunCodeUnderDiscoveryHost (new Uri ("http://localhost:37564"), new Uri ("http://localhost:4989"));
	}

	static void RunCodeUnderDiscoveryHost (Uri serviceUri, Uri aHostUri)
	{
		// announcement service
		var abinding = new CustomBinding (new HttpTransportBindingElement ());
		var aAddress = new EndpointAddress (aHostUri);
		var aEndpoint = new AnnouncementEndpoint (abinding, aAddress);
		
		// discovery service
		var dEndpoint = new UdpDiscoveryEndpoint (DiscoveryVersion.WSDiscovery11, new Uri ("soap.udp://239.255.255.250:3802/"));
		// Without this, .NET rejects the host as if it had no service.
		dEndpoint.IsSystemEndpoint = false;
		var ib = new InspectionBehavior ();
		ib.ReplySending += delegate (ref Message msg, object o) {
			var mb = msg.CreateBufferedCopy (0x10000);
			msg = mb.CreateMessage ();
			Console.Error.WriteLine (mb.CreateMessage ());
			};
		ib.RequestReceived += delegate (ref Message msg, IClientChannel channel, InstanceContext ctx) {
			var mb = msg.CreateBufferedCopy (0x10000);
			msg = mb.CreateMessage ();
			Console.Error.WriteLine (mb.CreateMessage ());
			return null;
			};
		ib.ReplyReceived += delegate (ref Message msg, object o) {
			var mb = msg.CreateBufferedCopy (0x10000);
			msg = mb.CreateMessage ();
			Console.Error.WriteLine (mb.CreateMessage ());
			};
		ib.RequestSending += delegate (ref Message msg, IClientChannel channel) {
			var mb = msg.CreateBufferedCopy (0x10000);
			msg = mb.CreateMessage ();
			Console.Error.WriteLine (mb.CreateMessage ());
			return null;
			};

		dEndpoint.Behaviors.Add (ib);
		aEndpoint.Behaviors.Add (ib);

		// it internally hosts an AnnouncementService
		using (var inst = new AnnouncementBoundDiscoveryService (aEndpoint)) {
			var host = new ServiceHost (inst);
			host.AddServiceEndpoint (dEndpoint);
			host.Description.Behaviors.Find<ServiceDebugBehavior> ()
				.IncludeExceptionDetailInFaults = true;
			host.Open ();
			Console.WriteLine ("Type [CR] to quit...");
			Console.ReadLine ();
			host.Close ();
		}
	}
}


