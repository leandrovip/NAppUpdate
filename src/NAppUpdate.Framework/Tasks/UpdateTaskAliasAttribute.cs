using System;
using System.Diagnostics;

namespace NAppUpdate.Framework.Tasks
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public class UpdateTaskAliasAttribute : Attribute
	{
		public UpdateTaskAliasAttribute(string alias)
		{
			Alias = alias;
		}

		public string Alias { [DebuggerStepThrough] get; }
	}
}
