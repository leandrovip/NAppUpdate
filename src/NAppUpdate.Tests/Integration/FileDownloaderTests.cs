using Microsoft.VisualStudio.TestTools.UnitTesting;
using NAppUpdate.Framework.Utils;

namespace NAppUpdate.Tests.Integration
{
	[TestClass]
	public class FileDownloaderTests
	{
		[TestMethod]
		public void Should_be_able_to_download_a_small_file_from_the_internet()
		{
			var fileDownloader = new FileDownloader("http://www.google.co.uk/intl/en_uk/images/logo.gif");

			var fileData = fileDownloader.Download();

			Assert.IsTrue(fileData.Length > 0);
		}
	}
}
