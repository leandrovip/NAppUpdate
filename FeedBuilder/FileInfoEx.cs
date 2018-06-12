using System;
using System.Diagnostics;
using System.IO;
using NAppUpdate.Framework.Utils;

namespace FeedBuilder
{
	public class FileInfoEx
	{
		public FileInfo FileInfo { get; }

		public string FileVersion { get; }

		public string Hash { get; }

		public string RelativeName { get; private set; }

		public FileInfoEx(string fileName, int rootDirLength)
		{
			FileInfo = new FileInfo(fileName);
			var verInfo = FileVersionInfo.GetVersionInfo(fileName);
			if (FileVersion == null)
				FileVersion =
					new Version(verInfo.FileMajorPart, verInfo.FileMinorPart, verInfo.FileBuildPart, verInfo.FilePrivatePart)
						.ToString();
			Hash = FileChecksum.GetSHA256Checksum(fileName);

			RelativeName = fileName.Substring(rootDirLength);
		}
	}
}
