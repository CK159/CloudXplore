using System;

namespace Api.Modules.System
{
	public class ApplicationInfoModel
	{
		public string ApplicationName { get; set; }

		public string ApplicationVersion { get; set; }

		public DateTime ApplicationBuildDate { get; set; }

		public bool IsDebugBuild { get; set; }

		public string EnvironmentName { get; set; }

		public bool DebuggerAttached { get; set; }

		public MemoryStatModel Memory { get; set; }
	}

	public class MemoryStatModel
	{
		/// <summary>
		/// Managed memory allocated by GC
		/// </summary>
		public double GcAllocatedMemoryMb { get; set; }

		/// <summary>
		/// .NET Process working set memory
		/// </summary>
		public double ProcessWorkingSetMemoryMb { get; set; }
	}

	public class RunGcModel
	{
		public MemoryStatModel BeforeGc { get; set; }

		public MemoryStatModel AfterGc { get; set; }

		public MemoryStatModel ReclaimedMemory { get; set; }
	}
}
