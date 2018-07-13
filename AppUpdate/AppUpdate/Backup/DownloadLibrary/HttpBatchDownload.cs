using System;
using System.IO;
using System.Threading;

namespace Tools.DownloadLibrary
{
	public class HttpBatchDownloader : IDisposable
	{
		private ManualResetEvent cancelEvent;
		private int downloadCount;
		private int totalDownloads;

		public HttpBatchDownloader()
		{
		}

		public event DownloadProgressHandler StateChanged;
		public event DownloadProgressHandler CurrentProgressChanged;
		public event DownloadProgressHandler TotalProgressChanged;
		public event DownloadProgressHandler FileChanged;
		public event DownloadProgressHandler FileCompleted;

		public void Download(string[] urls, string destinationFolder, ManualResetEvent cancelEvent)
		{
			this.cancelEvent = cancelEvent;
			this.downloadCount = 0;
			this.totalDownloads = urls.Length;

			foreach(string url in urls)
			{
				// break out if a cancellation has occurred
				if(HasUserCancelled())
					break;

				//add begin
				currentUrl = url;
				//add end

				// create the destination path using the destination folder and the url
				string fileName = Path.GetFileName(url);
				string destPath = Path.Combine(destinationFolder, Path.GetFileName(url));

				// send the new filename back to the owner class
				if(this.FileChanged != null)
					this.FileChanged(this, new DownloadEventArgs(fileName));

				// download the file
				bool bsinglesucc = false;	//no cancel
				using(HttpFileDownloader dL = new HttpFileDownloader())
				{
					dL.StateChanged += new DownloadProgressHandler(dL_StateChanged);
					dL.ProgressChanged += new DownloadProgressHandler(dL_ProgressChanged);
					bsinglesucc = dL.Download(url, destPath, this.cancelEvent);
				}

				this.downloadCount ++;
				if(bsinglesucc)
				{
					RaiseFileCompleted(fileName);	//only file name,no path
				}

				//add begin
				if( (UrlsInfo != null) && (UrlsInfo.Length > 0) )
				{
					foreach(BatchUrlInfo bui in UrlsInfo)
					{
						if(currentUrl == bui.url)
						{
							bui.downloaded = true;
							break;
						}
					}
				}
				//add end

			}

			// send a 100% complete message if the user hasn't cancelled
			if(!HasUserCancelled())
			 dL_ProgressChanged(this, new DownloadEventArgs(100,""));
		}

		//add begin
		//single file download completely
		private void RaiseFileCompleted(string filenameonly)	
		{
			if(this.FileCompleted != null)
			{
				this.FileCompleted(this,new DownloadEventArgs(filenameonly));
			}
		}
		//add end

		public void Download(string destinationFolder, ManualResetEvent cancelEvent)
		{
			string[] urls = new string[UrlsInfo.Length];
			for(int i=0;i<urls.Length;i++)
			{
				urls[i] = UrlsInfo[i].url;
			}
			this.Download(urls,destinationFolder,cancelEvent);
		}

		private bool HasUserCancelled()
		{
			return (this.cancelEvent != null && this.cancelEvent.WaitOne(0, false));
		}

		private void dL_StateChanged(object sender, DownloadEventArgs e)
		{
			if(this.StateChanged != null)
				this.StateChanged(this, e);
		}

		private void dL_ProgressChanged(object sender, DownloadEventArgs e)
		{
			// resend the progress info
			if(this.CurrentProgressChanged != null)
				this.CurrentProgressChanged(this, e);

			int percent = 0;
			if( (UrlsInfo==null) || (UrlsInfo.Length==0) )
			{
				// calculate total progress
				double percentForEach = (double) 100 / this.totalDownloads;
				percent = (int) ((((double) this.downloadCount) / this.totalDownloads) * 100);
				percent += (int) (percentForEach * ((double) e.PercentDone / 100));
				if(percent > 100)
					percent = 100;
			}
			else
			{
				double totalvalue = 0;
				double downloadedvalue = 0;
				foreach(BatchUrlInfo bui in UrlsInfo)
				{
					if(bui.downloaded)
					{
						downloadedvalue += (double)bui.length/1000;
					}
					if(bui.url == currentUrl)
					{
						downloadedvalue += (double)(bui.length * e.PercentDone/100) /1000.0;
					}
					totalvalue += (double)bui.length/1000.0;
				}
				if(totalvalue > 0)
				{
					percent = (int)(((double)downloadedvalue/totalvalue)*100);

					if(percent>100) percent = 100;
				}
			}

			// send total progress info
			if(this.TotalProgressChanged != null)
				this.TotalProgressChanged(this, new DownloadEventArgs(percent,""));
		}

		public void Dispose()
		{
			this.cancelEvent = null;
		}


		// //////////////////////////////

		public BatchUrlInfo[] UrlsInfo;
		private string currentUrl = "";
	}

	public class BatchUrlInfo
	{
		public string url = "";
		public int length = 0;
		public bool downloaded = false;	//falseŒ¥œ¬‘ÿ£¨true“—œ¬‘ÿ
		
	}

}