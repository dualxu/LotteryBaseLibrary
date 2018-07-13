using System;
using System.IO;

namespace Tools.Utilities
{
	/// <summary>
	/// FileOperation 的摘要说明。
	/// </summary>
	public class FileOperation
	{
		static FileOperation()
		{
		}

		public static void CopyDirectory( string sourcePath, string destinationPath )
		{
			CopyDirectory( sourcePath, destinationPath, true );
		}

		public static void CopyDirectory( string sourcePath, string destinationPath, bool overwrite )
		{
			CopyDirRecurse( sourcePath, destinationPath, destinationPath, overwrite );
		}
		private static void CopyDirRecurse( string sourcePath, string destinationPath, string originalDestination, bool overwrite )
		{
			//  ensure terminal backslash
			sourcePath = FileOperation.AppendTerminalBackslash( sourcePath );
			destinationPath = FileOperation.AppendTerminalBackslash( destinationPath );

			if ( !Directory.Exists( destinationPath ) )
			{
				Directory.CreateDirectory( destinationPath );
			}

			//  get dir info which may be file or dir info object
			DirectoryInfo dirInfo = new DirectoryInfo( sourcePath );

			string destFileName = null;

			foreach( FileSystemInfo fsi in dirInfo.GetFileSystemInfos() )
			{
				if ( fsi is FileInfo )
				{
					destFileName = Path.Combine( destinationPath, fsi.Name );

					//  if file object just copy when overwrite is allowed
					if ( File.Exists( destFileName ) )
					{
						if ( overwrite )
						{
							File.Copy( fsi.FullName, destFileName, true );
						}
					}
					else
					{
						File.Copy( fsi.FullName, destFileName );
					}
				}
				else if( fsi is DirectoryInfo)
				{
					// avoid this recursion path, otherwise copying directories as child directories
					// would be an endless recursion (up to an stack-overflow exception).
					if ( fsi.FullName != originalDestination )
					{
						//  must be a directory, create destination sub-folder and recurse to copy files
						//Directory.CreateDirectory( destinationPath + fsi.Name );
						CopyDirRecurse( fsi.FullName, destinationPath + fsi.Name, originalDestination, overwrite );
					}
				}
				else
				{
					//do nothing
				}
			}
		}
		public static string AppendTerminalBackslash( string path )
		{
			if( path.IndexOf( Path.DirectorySeparatorChar, path.Length - 1 ) == -1 )
			{
				return path + Path.DirectorySeparatorChar;
			}
			else
			{
				return path;
			}
		}


	}
}
