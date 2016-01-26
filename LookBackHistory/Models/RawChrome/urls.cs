using System.Data.Linq.Mapping;
// ReSharper disable InconsistentNaming
#pragma warning disable 0649

namespace LookBackHistory.Models.RawChrome
{
	[Table(Name = "urls")]
	internal class urls
	{
		[Column(Name = "id", IsPrimaryKey = true, DbType = "INT")]
		public long id;

		[Column(Name = "url", DbType = "TEXT")]
		public string url;

		[Column(Name = "title", DbType = "TEXT")]
		public string title;

		[Column(Name = "visit_count", CanBeNull = false, DbType = "INT")]
		public int visit_count;

		[Column(Name = "typed_count", CanBeNull = false, DbType = "INT")]
		public int typed_count;

		[Column(Name = "last_visit_time", CanBeNull = false, DbType = "INT")]
		public long last_visit_time;

		[Column(Name = "hidden", CanBeNull = false, DbType = "INT")]
		public int hidden;

		[Column(Name = "favicon_id", CanBeNull = false, DbType = "INT")]
		public int favicon_id;
	}
}
