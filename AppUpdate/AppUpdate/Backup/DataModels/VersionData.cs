using System;
using System.Data;

namespace updater.DataModels
{
	/// <summary>
	/// VersionInfo 的摘要说明。
	/// </summary>
	[Serializable]
	public class VersionData : DataSet
	{
		public const string mTableName = "vertable";
		public const string mExeName = "ExeName";
		public const string mFullVersion = "FullVersion";
		public const string mFirst = "First";
		public const string mSecond = "Second";
		public const string mThird = "Third";
		public const string mForth = "Forth";

		public VersionData()
		{
			this.DataSetName = "VersionData";
			this.Tables.Clear();

			DataTable table = new DataTable(mTableName);
			table.Columns.Add(mExeName,typeof(string));
			table.Columns.Add(mFullVersion,typeof(string));
			table.Columns.Add(mFirst,typeof(int));
			table.Columns.Add(mSecond,typeof(int));
			table.Columns.Add(mThird,typeof(int));
			table.Columns.Add(mForth,typeof(int));
			this.Tables.Add(table);
		}
	}
}
