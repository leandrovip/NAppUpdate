using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Principal;

namespace NAppUpdate.Framework.Utils
{
	public static class FileSystem
	{
		public static void CreateDirectoryStructure(string path)
		{
			CreateDirectoryStructure(path, true);
		}

		public static void CreateDirectoryStructure(string path, bool pathIncludeFile)
		{
			var paths = path.Split(Path.DirectorySeparatorChar);

			// ignore the last split because its the filename
			var loopCount = paths.Length;
			if (pathIncludeFile)
				loopCount--;

			for (var ix = 0; ix < loopCount; ix++)
			{
				var newPath = paths[0] + @"\";
				for (var add = 1; add <= ix; add++)
					newPath = Path.Combine(newPath, paths[add]);
				if (!Directory.Exists(newPath))
					Directory.CreateDirectory(newPath);
			}
		}

		/// <summary>
		///     Safely delete a folder recuresively
		///     See http://stackoverflow.com/questions/329355/cannot-delete-directory-with-directory-deletepath-true/329502#329502
		/// </summary>
		/// <param name="targetDir">Folder path to delete</param>
		public static void DeleteDirectory(string targetDir)
		{
			var files = Directory.GetFiles(targetDir);
			var dirs = Directory.GetDirectories(targetDir);

			foreach (var file in files)
			{
				File.SetAttributes(file, FileAttributes.Normal);
				File.Delete(file);
			}

			foreach (var dir in dirs)
				DeleteDirectory(dir);

			File.SetAttributes(targetDir, FileAttributes.Normal);
			Directory.Delete(targetDir, false);
		}

		public static IEnumerable<string> GetFiles(string path, string searchPattern, SearchOption searchOption)
		{
			var searchPatterns = searchPattern.Split('|');
			var files = new List<string>();
			foreach (var sp in searchPatterns)
				files.AddRange(Directory.GetFiles(path, sp, searchOption));
			return files;
		}

		/// <summary>
		///     Returns true if read/write lock exists on the file, otherwise false
		///     From http://stackoverflow.com/a/937558
		/// </summary>
		/// <param name="file">The file to check for a lock</param>
		/// <returns></returns>
		public static bool IsFileLocked(FileInfo file)
		{
			FileStream stream = null;

			try
			{
				stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
			}
			catch (IOException)
			{
				//the file is unavailable because it is:
				//still being written to
				//or being processed by another thread
				//or does not exist (has already been processed)
				return true;
			}
			finally
			{
				if (stream != null)
					stream.Close();
			}

			//file is not locked
			return false;
		}

		public static void CopyAccessControl(FileInfo src, FileInfo dst)
		{
			var fs = src.GetAccessControl();

			var hasInheritanceRules = fs.GetAccessRules(false, true, typeof(SecurityIdentifier)).Count > 0;
			if (hasInheritanceRules)
				fs.SetAccessRuleProtection(false, false);
			else
				fs.SetAccessRuleProtection(true, true);

			dst.SetAccessControl(fs);
		}

		public static string GetFullPath(string localPath)
		{
			var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
			return Path.Combine(currentDirectory, localPath);
		}
	}
}
