using System;
using System.Runtime.InteropServices;

namespace PashRuntime {
    public class OSInfo {
        
        [DllImport ("libc")]
		static extern int uname (IntPtr buf);
        
        public static bool IsRunningMac ()
		{
			IntPtr buf = IntPtr.Zero;
			try {
				buf = Marshal.AllocHGlobal (8192);
				// This is a hacktastic way of getting sysname from uname ()
				if (uname (buf) == 0) {
					string os = Marshal.PtrToStringAnsi (buf);
					if (os == "Darwin")
						return true;
				}
			} catch {
			} finally {
				if (buf != IntPtr.Zero)
					Marshal.FreeHGlobal (buf);
			}
			return false;
		}
        
        public static bool IsRunningUnix() => Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX;
        
        public static readonly bool OS_UNIX = IsRunningUnix();
        
        public static bool IsRunningLinux() => OS_UNIX && !IsRunningMac();
        
        public static readonly bool OS_LINUX = IsRunningLinux();
        
        public static readonly bool OS_OSX = OS_UNIX && IsRunningMac();
        
        public static readonly bool OS_WINDOWS = Environment.OSVersion.Platform == PlatformID.Win32Windows;
    }
}
