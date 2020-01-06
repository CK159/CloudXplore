using System;
using System.IO;
using System.Reflection;

namespace Api.Modules.System
{
	public class ApplicationInfo
	{
		public string ApplicationName { get; private set; }

		public string ApplicationVersion { get; private set; }

		public DateTime ApplicationBuildDate { get; private set; }

		public bool IsDebugBuild { get; private set; }

		public static ApplicationInfo BuildApplicationInfo()
		{
			ApplicationInfo info = new ApplicationInfo
			{
				ApplicationName = "CloudXplore Api",
				ApplicationVersion = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()
					.InformationalVersion,
				//This is not the best - the file properties can be modified by external tooling but there's no default built-in date stamp any more
				ApplicationBuildDate = File.GetLastWriteTime(Assembly.GetExecutingAssembly().Location),
				IsDebugBuild = StaticDebugCheck()
			};
			
			return info;
		}
		
		private static bool StaticDebugCheck()
		{
#if DEBUG
			return true;
#else
		return false;
#endif
		}
	}
}
