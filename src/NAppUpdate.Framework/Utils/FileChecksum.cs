using System;
using System.IO;
using System.Security.Cryptography;

namespace NAppUpdate.Framework.Utils
{
	public static class FileChecksum
	{
		public static string GetSHA256Checksum(string filePath)
		{
			using (var stream = File.OpenRead(filePath))
			{
				var sha = new SHA256Managed();
				var checksum = sha.ComputeHash(stream);
				return BitConverter.ToString(checksum).Replace("-", string.Empty);
			}
		}

		public static string GetSHA256Checksum(byte[] fileData)
		{
			var sha = new SHA256Managed();
			var checksum = sha.ComputeHash(fileData);
			return BitConverter.ToString(checksum).Replace("-", string.Empty);
		}
	}
}
