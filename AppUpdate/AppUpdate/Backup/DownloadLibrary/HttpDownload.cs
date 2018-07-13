using System;
using System.IO;
using System.Net;
using System.Threading;

namespace Tools.DownloadLibrary
{
	public class HttpFileDownloader : IDisposable
	{
		private WaitHandle cancelEvent;

		// to adjust how many bytes are read from the url at a time,
		// simply change this constant:
		private const int downloadBlockSize = 4096;

		public HttpFileDownloader()
		{
			// make all WebRequests use the same Proxy info that IE uses
			GlobalProxySelection.Select = WebProxy.GetDefaultProxy();
			GlobalProxySelection.Select.Credentials = CredentialCache.DefaultCredentials;
		}

		public event DownloadProgressHandler ProgressChanged;
		public event DownloadProgressHandler StateChanged;

		public bool Download(string url, string file, WaitHandle cancelEvent)
		{
			DownloadData data = null;

			try
			{
				this.cancelEvent = cancelEvent;

				// exit on cancel
				if(HasUserCancelled())
				{
					return false;
				}

				// get download details
				data = DownloadData.Create(url, file);

				// send the new download state
				RaiseStateChanged(data.DownloadState);

				// create the download buffer
				byte[] buffer = new byte[downloadBlockSize];

				int readCount;

				// update how many bytes have already been read
				long totalDownloaded = data.StartPoint;

				bool bcanceled = false;
				// read a block of bytes and get the number of bytes read
				while((int)(readCount = data.DownloadStream.Read(buffer, 0, downloadBlockSize)) > 0)
				{
					// break on cancel
					if(HasUserCancelled())
					{
						bcanceled = true;
						break;
					}

					// update total bytes read
					totalDownloaded += readCount;

					// send progress info
					if(data.IsProgressKnown)
						RaiseProgressChanged(totalDownloaded, data.FileSize,file);

					// save block to end of file
					SaveToFile(buffer, readCount, file);

					// break on cancel
					if(HasUserCancelled())
					{
						bcanceled = true;
						break;
					}
				}

				// send 100% completion if url size is known and user hasn't cancelled
				if(!HasUserCancelled() && data.IsProgressKnown)
				{
					RaiseProgressChanged(data.FileSize, data.FileSize,file);
				}
				return !bcanceled;
			}
			finally
			{
				if(data != null)
					data.Close();
			}
		}
		private void SaveToFile(byte[] buffer, int count, string fileName)
		{
			FileStream f = null;

			try
			{
				f = File.Open(fileName, FileMode.Append, FileAccess.Write);
				f.Write(buffer, 0, count);
			}
			finally
			{
				if(f != null)
					f.Close();
			}
		}
		private void RaiseStateChanged(string state)
		{
			if(this.StateChanged != null)
				this.StateChanged(this, new DownloadEventArgs(state));
		}
		private void RaiseProgressChanged(long current, long target, string filefullname)
		{
			int percent = (int) ((((double) current) / target) * 100);
			if(this.ProgressChanged != null)
				this.ProgressChanged(this, new DownloadEventArgs(percent,filefullname));
		}
		
		private bool HasUserCancelled()
		{
			return (this.cancelEvent != null && this.cancelEvent.WaitOne(0, false));
		}
		public void Dispose()
		{
			this.cancelEvent = null;
		}
	}

	public class DownloadData
	{
		public static DownloadData Create(string url, string file)
		{
			bool progressKnown;
			bool resume = false;
			long urlSize = GetFileSize(url, out progressKnown);
			long startPoint = 0;
			HttpWebRequest req = GetRequest(url);

			if(progressKnown && File.Exists(file))
			{
				startPoint = new FileInfo(file).Length;
				if(startPoint != urlSize)
				{
					resume = true;
					req.AddRange((int) startPoint);
				}
			}
			else if(File.Exists(file))
				File.Delete(file);

			HttpWebResponse response = (HttpWebResponse) req.GetResponse();

			if(resume && response.StatusCode == HttpStatusCode.OK)
			{
				File.Delete(file);
				startPoint = 0;
			}

			return new DownloadData(response, urlSize, startPoint, progressKnown);
		}

		private static long GetFileSize(string url, out bool progressKnown)
		{
			HttpWebResponse response = null;
			long size = -1;

			try
			{
				response = (HttpWebResponse) GetRequest(url).GetResponse();

				size = response.ContentLength;

				if(size == -1)
					progressKnown = false;
				else
					progressKnown = true;
			}
			finally
			{
				if(response != null)
					response.Close();
			}

			return size;
		}

		private static HttpWebRequest GetRequest(string url)
		{
			HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
			request.Credentials = CredentialCache.DefaultCredentials;
			return request;
		}

		private HttpWebResponse response;
		private Stream stream;
		private long size;
		private long start;
		private bool progressKnown;

		private DownloadData(HttpWebResponse response, long size, long start, bool progressKnown)
		{
			this.response = response;
			this.size = size;
			this.start = start;
			this.stream = null;
			this.progressKnown = progressKnown;
		}

		public Stream DownloadStream
		{
			get
			{
				if(this.start == this.size)
					return Stream.Null;
				if(this.stream == null)
					this.stream = this.response.GetResponseStream();
				return this.stream;
			}
		}

		public long FileSize
		{
			get
			{
				return this.size;
			}
		}

		public long StartPoint
		{
			get
			{
				return this.start;
			}
		}

		public string DownloadState
		{
			get
			{
				return this.response.StatusCode.ToString();
			}
		}

		public bool IsProgressKnown
		{
			get
			{
				return this.progressKnown;
			}
		}

		public void Close()
		{
			this.response.Close();
		}
	}
}
