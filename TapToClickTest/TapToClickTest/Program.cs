using System;

namespace TapToClickTest
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            if (Environment.OSVersion.Platform == PlatformID.MacOSX) {
				string env = Environment.GetEnvironmentVariable("DYLD_LIBRARY_PATH");
                Environment.SetEnvironmentVariable("DYLD_LIBRARY_PATH", env + ":./osx");
            }

            using (var g = new TestGame()) {
                g.Run();
            }
        }
    }
}
