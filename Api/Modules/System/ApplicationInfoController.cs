using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.System
{
	[ApiController]
	[Route("api/[controller]")]
	public class ApplicationInfoController : ControllerBase
	{
		private ApplicationInfo appInfo;
		private IWebHostEnvironment hostingEnv;

		public ApplicationInfoController(ApplicationInfo _appInfo, IWebHostEnvironment _hostingEnv)
		{
			appInfo = _appInfo;
			hostingEnv = _hostingEnv;
		}
		
		[HttpGet]
		public ApplicationInfoModel Get()
		{
			ApplicationInfoModel model = new ApplicationInfoModel
			{
				ApplicationName = appInfo.ApplicationName,
				ApplicationVersion = appInfo.ApplicationVersion,
				ApplicationBuildDate = appInfo.ApplicationBuildDate,
				IsDebugBuild = appInfo.IsDebugBuild,
				EnvironmentName = hostingEnv.EnvironmentName,
				DebuggerAttached = Debugger.IsAttached,
				Memory = GetInternalMemoryStats()
			};


			return model;
		}
		
		[HttpGet("MemoryStats")]
		public MemoryStatModel GetMemoryStats()
		{
			return GetInternalMemoryStats();
		}

		[HttpPost("RunGc")]
		public RunGcModel RunGc()
		{
			RunGcModel model = new RunGcModel();
			
			model.BeforeGc = GetInternalMemoryStats();
			GC.Collect();
			model.AfterGc = GetInternalMemoryStats();
			
			model.ReclaimedMemory = new MemoryStatModel
			{
				GcAllocatedMemoryMb = model.BeforeGc.GcAllocatedMemoryMb - model.AfterGc.GcAllocatedMemoryMb,
				ProcessWorkingSetMemoryMb = model.BeforeGc.ProcessWorkingSetMemoryMb - model.AfterGc.ProcessWorkingSetMemoryMb
			};
			
			return model;
		}

		private MemoryStatModel GetInternalMemoryStats()
		{
			return new MemoryStatModel
			{
				GcAllocatedMemoryMb = Math.Round(GC.GetTotalMemory(false) / 1048576d, 2),
				ProcessWorkingSetMemoryMb = Math.Round(Process.GetCurrentProcess().WorkingSet64 / 1048576d, 2)
			};
		}
	}
}
