using System;
using System.Net;

namespace Tools.DownloadLibrary
{
	/// <summary>
	/// DownloadHelper 的摘要说明。
	/// </summary>
	public class DownloadHelper
	{
		private DownloadHelper()
		{
		}

		static DownloadHelper()
		{
		}

		public static long GetFileSize(string url)
		{
			HttpWebResponse response = null;
			long size = -1;

			try
			{
				response = (HttpWebResponse) GetRequest(url).GetResponse();
				size = response.ContentLength;
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

	}
}
