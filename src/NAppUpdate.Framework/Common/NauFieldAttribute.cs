using System;
using System.Diagnostics;

namespace NAppUpdate.Framework.Common
{
	[AttributeUsage(AttributeTargets.Property)]
	public class NauFieldAttribute : Attribute
	{
		public NauFieldAttribute(string alias, string description, bool isRequired)
		{
			Alias = alias;
			Description = description;
			IsRequired = isRequired;
		}

		public string Alias { [DebuggerStepThrough] get; }

		public string Description { [DebuggerStepThrough] get; }

		public bool IsRequired { [DebuggerStepThrough] get; }
	}
}
