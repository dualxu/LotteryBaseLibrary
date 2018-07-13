using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace Tools.ZipLibrary
{
	/// <summary>
	/// ZipHelper 的摘要说明。
	/// </summary>
	public class ZipHelper
	{
		static ZipHelper()
		{
		}
		
		public static bool ZipDirectory(string directoryToZip,string zipedFile, bool incSubDir)
		{
			if(!Directory.Exists(directoryToZip))
			{
				return false;
			}

			try
			{
				FastZip fz = new FastZip();
				fz.CreateEmptyDirectories = true;
				fz.CreateZip(zipedFile, directoryToZip, incSubDir, "");
				fz = null;
			}
			catch
			{
				return false;
			}

			return true;
		}


		public static bool UnZipFile(string zipedFile, string destDirectory, bool incSubDir)
		{
			if(!File.Exists(zipedFile))
			{
				return false;
			}
			try
			{
				FastZip fz = new FastZip();
				fz.CreateEmptyDirectories = true;
				fz.ExtractZip(zipedFile,destDirectory,"");
			}
			catch
			{
				return false;
			}

			return true;
		}


		// ////////////////////////

		public static bool ZipDirectory(string directoryToZip,string zipedFile)
		{
			return ZipDirectory(directoryToZip,zipedFile,true);
		}

		public static bool UnZipFile(string zipedFile, string destDirectory)
		{
			return UnZipFile(zipedFile,destDirectory,true);
		}


		/*
		public static bool ZipDirectory(string directoryToZip,string zipedFile)
		{		
			string errMsg = "";
			if(!Directory.Exists(directoryToZip))
			{
				errMsg = "目录不存在";
				return false;
			}

			try
			{
				string[] filenames = Directory.GetFiles(directoryToZip);

				using ( ZipOutputStream zipstream = new ZipOutputStream( File.Create(zipedFile) ) ) 
				{

					zipstream.SetLevel(6); // 0 - store only to 9 - means best compression

					byte[] buffer = new byte[4096];
	
					foreach (string file in filenames) 
					{					
						// Using GetFileName makes the result compatible with XP
						// as the resulting path is not absolute.
						ZipEntry entry = new ZipEntry(Path.GetFileName(file));
		
						// Setup the entry data as required.
		
						// Crc and size are handled by the library for seakable streams
						// so no need to do them here.

						// Could also use the last write time or similar for the file.
						entry.DateTime = DateTime.Now;
						zipstream.PutNextEntry(entry);
		
						using ( FileStream fs = File.OpenRead(file) ) 
						{		
							// Using a fixed size buffer here makes no noticeable difference for output
							// but keeps a lid on memory usage.
							int sourceBytes;
							do 
							{
								sourceBytes = fs.Read(buffer, 0, buffer.Length);
								zipstream.Write(buffer, 0, sourceBytes);
							} while ( sourceBytes > 0 );
						}
					}
	
					// Finish/Close arent needed strictly as the using statement does this automatically
	
					// Finish is important to ensure trailing information for a Zip file is appended.  Without this
					// the created file would be invalid.
					zipstream.Finish();
	
					// Close is important to wrap things up and unlock the file.
					zipstream.Close();
				}
			}
			catch(Exception ex)
			{
				errMsg = ex.Message;
				return false;

				// No need to rethrow the exception as for our purposes its handled.
			}
			

			return true;
		}

		public static bool UnZipFile(string zipFile, string destDirectory)
		{
			if(zipFile.Length == 0) return false;
			if(!File.Exists(zipFile)) return false;

			using (ZipInputStream s = new ZipInputStream( File.OpenRead(zipFile) ) ) 
			{		
				ZipEntry theEntry;
				while ((theEntry = s.GetNextEntry()) != null) 
				{				
					string directoryName = destDirectory;
					string fileName      = Path.GetFileName(theEntry.Name);
				
					// create directory
					if ( directoryName.Length > 0 ) 
					{
						Directory.CreateDirectory(directoryName);
					}
				
					if (fileName != String.Empty) 
					{
						using (FileStream streamWriter = File.Create(Path.Combine(directoryName,theEntry.Name))) 
						{
					
							int size = 2048;
							byte[] data = new byte[2048];
							while (true) 
							{
								size = s.Read(data, 0, data.Length);
								if (size > 0) 
								{
									streamWriter.Write(data, 0, size);
								} 
								else 
								{
									break;
								}
							}
						}
					}
				}
			}

			return true;
		}
		*/

	}
}
