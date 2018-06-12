using System;
using System.Collections.Generic;
using System.Reflection;
using NAppUpdate.Framework;
using NAppUpdate.Framework.Tasks;

namespace NAppUpdate.SampleApp
{
	public class UpdateTaskHelper
	{
		private readonly UpdateManager _manager;

		public IList<UpdateTaskInfo> TaskListInfo { get; private set; }
		public string CurrentVersion { get; private set; }
		public string UpdateDescription { get; private set; }
		public string UpdateFileName { get; private set; }
		public string UpdateVersion { get; private set; }
		public DateTime? UpdateDate { get; private set; }
		public long UpdateFileSize { get; private set; }

		public UpdateTaskHelper()
		{
			_manager = UpdateManager.Instance;
			GetUpdateTaskInfo();
			CurrentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
		}

		public IList<UpdateTaskInfo> GetUpdateTaskInfo()
		{
			var taskListInfo = new List<UpdateTaskInfo>();
			foreach (var task in _manager.Tasks)
			{
				var fileTask = task as FileUpdateTask;
				if (fileTask == null) continue;

				UpdateDescription = fileTask.Description;
			}
			TaskListInfo = taskListInfo;
			return taskListInfo;
		}
	}
}
