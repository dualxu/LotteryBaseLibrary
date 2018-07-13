using System;
using System.Data;

namespace updater.DataModels
{
	/// <summary>
	/// FileListData 的摘要说明。
	/// </summary>
	[Serializable]
	public class FileListData : DataSet
	{
		public const string mTableName = "FileListTable";
		public const string mFileName = "FileName";
		public const string mFileLength = "FileLength";
		public const string mOtherRemark = "OtherRemark";
		public const string mDestDir = "DestDirectory";

		public FileListData()
		{
			this.DataSetName = "FileListData";
			this.Tables.Clear();

			DataTable table = new DataTable(mTableName);
			table.Columns.Add(mFileName,typeof(string));
			table.Columns.Add(mFileLength,typeof(long));
			table.Columns.Add(mOtherRemark,typeof(string));
			table.Columns.Add(mDestDir,typeof(string));

			this.Tables.Add(table);
		}
	}
}
