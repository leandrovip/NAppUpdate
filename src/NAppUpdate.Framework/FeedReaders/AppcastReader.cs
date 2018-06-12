using System.Collections.Generic;
using System.Xml;
using NAppUpdate.Framework.Conditions;
using NAppUpdate.Framework.Tasks;

namespace NAppUpdate.Framework.FeedReaders
{
	public class AppcastReader : IUpdateFeedReader
	{
		// http://learn.adobe.com/wiki/display/ADCdocs/Appcasting+RSS

		#region IUpdateFeedReader Members

		public IList<IUpdateTask> Read(string feed)
		{
			var doc = new XmlDocument();
			doc.LoadXml(feed);
			var nl = doc.SelectNodes("/rss/channel/item");

			var ret = new List<IUpdateTask>();

			foreach (XmlNode n in nl)
			{
				var task = new FileUpdateTask();
				task.Description = n["description"].InnerText;
				task.UpdateTo = n["enclosure"].Attributes["url"].Value;

				var cnd = new FileVersionCondition();
				cnd.Version = n["appcast:version"].InnerText;
				if (task.UpdateConditions == null) task.UpdateConditions = new BooleanCondition();
				task.UpdateConditions.AddCondition(cnd, BooleanCondition.ConditionType.AND);

				ret.Add(task);
			}

			return ret;
		}

		#endregion
	}
}
