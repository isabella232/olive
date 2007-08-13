//
// AssemblyInfo.cs
//
// Author:
//   Iain McCoy (iain@mccoy.id.au)
//   Andreas Nahr (ClassDevelopment@A-SoftTech.com)
//
// (C) 2003 Ximian, Inc.  http://www.ximian.com
// (C) 2004 Novell (http://www.novell.com)
//
// this file based on mcs/class/Mono.Data.SqlClient/Assembly/AssemblyInfo.cs

using System;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyVersion ("0.0.0.0")]

[assembly: AssemblyDelaySign (true)]
#if NET_2_1
[assembly: AssemblyKeyFile ("../msfinal.pub")]
#else
// For our desktop use, use the mono.pub key.
[assembly: AssemblyKeyFile ("../mono.pub")]
#endif
