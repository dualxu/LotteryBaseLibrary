using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Tools.Utilities
{
	[Flags]
	public enum MoveFileFlag : int
	{
		/// <summary>
		/// Perform a default move funtion.
		/// </summary>
		None				= 0x00000000,
		/// <summary>
		/// If the target file exists, the move function will replace it.
		/// </summary>
		ReplaceExisting     = 0x00000001,
		/// <summary>
		/// If the file is to be moved to a different volume, 
		/// the function simulates the move by using the CopyFile and DeleteFile functions. 
		/// </summary>
		CopyAllowed         = 0x00000002,
		/// <summary>
		/// The system does not move the file until the operating system is restarted. 
		/// The system moves the file immediately after AUTOCHK is executed, but before 
		/// creating any paging files. Consequently, this parameter enables the function 
		/// to delete paging files from previous startups. 
		/// </summary>
		DelayUntilReboot    = 0x00000004,
		/// <summary>
		/// The function does not return until the file has actually been moved on the disk. 
		/// </summary>
		WriteThrough        = 0x00000008,
		/// <summary>
		/// Reserved for future use.
		/// </summary>
		CreateHardLink      = 0x00000010,
		/// <summary>
		/// The function fails if the source file is a link source, but the file cannot be tracked after the move. This situation can occur if the destination is a volume formatted with the FAT file system.
		/// </summary>
		FailIfNotTrackable	= 0x00000020,
	}

	/// <summary>
	/// FileOperationAdvance 的摘要说明。
	/// </summary>
	public class FileOperationAdvance
	{
		static FileOperationAdvance()
		{
			
		}
		[DllImport("KERNEL32.DLL")]
		private static extern bool MoveFileEx( 
			string lpExistingFileName, 
			string lpNewFileName, 
			long dwFlags );

		public static bool MoveFile( string existingFileName, string newFileName, MoveFileFlag flags)
		{
			return MoveFileEx( existingFileName, newFileName, (int)flags );
		}

		/// <summary>
		/// Deletes a folder. If the folder cannot be deleted at the time this method is called,
		/// the deletion operation is delayed until the next system boot.
		/// </summary>
		/// <param name="folderPath">The directory to be removed</param>
		public static void DestroyFolder( string folderPath )
		{
			try
			{
				if ( Directory.Exists( folderPath) )
				{
					Directory.Delete( folderPath, true );
				}
			}
			catch( Exception )
			{
				// If we couldn't remove the files, postpone it to the next system reboot
				if ( Directory.Exists( folderPath) )
				{
					MoveFile(
						folderPath,
						null,
						MoveFileFlag.DelayUntilReboot );
				}
			}
		}

		/// <summary>
		/// Deletes a file. If the file cannot be deleted at the time this method is called,
		/// the deletion operation is delayed until the next system boot.
		/// </summary>
		/// <param name="filePath">The file to be removed</param>
		public static void DestroyFile( string filePath )
		{
			try
			{
				if ( File.Exists( filePath ) )
				{
					File.Delete( filePath );
				}
			}
			catch
			{
				if ( File.Exists( filePath ) )
				{
					MoveFile(
						filePath,
						null,
						MoveFileFlag.DelayUntilReboot );
				}
			}
		}


		/// <summary>
		/// Returns the path to the newer version of the .NET Framework installed on the system.
		/// </summary>
		/// <returns>A string containig the full path to the newer .Net Framework location</returns>
		public static string GetLatestDotNetFrameworkPath()
		{
			Version latestVersion = null;
			string fwkPath = Path.GetFullPath( Path.Combine( Environment.SystemDirectory, @"..\Microsoft.NET\Framework" ) );
			foreach(string path in Directory.GetDirectories( fwkPath, "v*" ) )
			{
				string candidateVersion = Path.GetFileName( path ).TrimStart( 'v' );
				try
				{
					Version curVersion = new Version( candidateVersion );
					if ( latestVersion == null || ( latestVersion != null && latestVersion < curVersion ) )
					{
						latestVersion = curVersion;
					}
				}
				catch {}
			}

			return  Path.Combine( fwkPath, "v" + latestVersion.ToString() );
		}

		/// <summary>
		/// Creates a new temporary folder under the system temp folder
		/// and returns its full pathname.
		/// </summary>
		/// <returns>The full temp path string.</returns>
		public static string CreateTemporaryFolder()
		{
			return Path.Combine( Path.GetTempPath(), Path.GetFileNameWithoutExtension( Path.GetTempFileName() ) );
		}
		


	}
}
