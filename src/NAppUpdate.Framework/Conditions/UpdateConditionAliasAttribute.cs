using System;
using System.Diagnostics;

namespace NAppUpdate.Framework.Conditions
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public class UpdateConditionAliasAttribute : Attribute
	{
		public UpdateConditionAliasAttribute(string alias)
		{
			Alias = alias;
		}

		public string Alias { [DebuggerStepThrough] get; }
	}
}
