using System.Data.Linq.Mapping;

namespace LookBackHistory.Models.RowChrome
{
	[Table(Name = "urls")]
	class urls
	{
		[Column(Name = "id", IsPrimaryKey = true, DbType = "INT")]
		public long id { get; set; }

		[Column(Name = "url", DbType = "TEXT")]
		public string url { get; set; }

		[Column(Name = "title", DbType = "TEXT")]
		public string title { get; set; }

		[Column(Name = "visit_count", CanBeNull = false, DbType = "INT")]
		public int visit_count { get; set; } = 0;

		[Column(Name = "typed_count", CanBeNull = false, DbType = "INT")]
		public int typed_count { get; set; } = 0;

		[Column(Name = "last_visit_time", CanBeNull = false, DbType ="INT")]
		public long last_visit_time { get; set; }

		[Column(Name = "hidden", CanBeNull = false, DbType ="INT")]
		public int hidden { get; set; } = 0;

		[Column(Name = "favicon_id", CanBeNull = false,DbType ="INT")]
		public int favicon_id { get; set; } = 0;
	}
}
