using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace NAppUpdate.Updater
{
	public class ArgumentsParser
	{
		public bool HasArgs { get; private set; }
		public string ProcessName { get; private set; }
		public bool ShowConsole { get; private set; }
		public bool Log { get; private set; }
		public string CallingApp { get; set; }

		private static ArgumentsParser _instance;
		protected ArgumentsParser() { }

		public static ArgumentsParser Get()
		{
			return _instance ?? (_instance = new ArgumentsParser());
		}

		public ArgumentsParser(string[] args)
		{
			Parse(args);
		}

		public void ParseCommandLineArgs()
		{
			Parse(Environment.GetCommandLineArgs());
		}

		public void Parse(string[] args)
		{
			for (var i = 0; i < args.Length; i++)
			{
				var arg = args[i];

				// Skip any args that are our own executable (first arg should be this).
				// In Visual Studio, the arg will be the VS host starter instead of
				// actually ourself.
				if (arg.Equals(Assembly.GetEntryAssembly().Location, StringComparison.InvariantCultureIgnoreCase)
				    || arg.EndsWith(".vshost.exe", StringComparison.InvariantCultureIgnoreCase))
				{
					CallingApp = arg.ToLower().Replace(".vshost.exe", string.Empty);
					continue;
				}

				arg = CleanArg(arg);
				if (arg == "log")
				{
					Log = true;
					HasArgs = true;
				}
				else if (arg == "showconsole")
				{
					ShowConsole = true;
					HasArgs = true;
				}
				else if (ProcessName == null)
				{
					// if we don't already have the processname set, assume this is it
					ProcessName = args[i];
				}
				else
				{
					Console.WriteLine("Unrecognized arg '{0}'", arg);
				}
			}
		}

		private static string CleanArg(string arg)
		{
			const string pattern1 = "^(.*)([=,:](true|0))";
			arg = arg.ToLower();
			if (arg.StartsWith("-") || arg.StartsWith("/"))
				arg = arg.Substring(1);
			var r = new Regex(pattern1);
			arg = r.Replace(arg, "{$1}");
			return arg;
		}
	}
}
