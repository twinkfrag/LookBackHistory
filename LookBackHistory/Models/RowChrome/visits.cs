using System.Data.Linq.Mapping;

namespace LookBackHistory.Models.RowChrome
{
	[Table(Name = "visits")]
	class visits
	{
		[Column(Name = "id", IsPrimaryKey = true, DbType = "INT")]
		public long id { get; set; }

		[Column(Name = "url", CanBeNull = false, DbType = "INT")]
		public long url { get; set; }

		[Column(Name = "visit_time", CanBeNull = false, DbType = "INT")]
		public long visit_time { get; set; }

		[Column(Name = "from_visit", DbType = "INT")]
		public int from_visit { get; set; }

		[Column(Name = "transition", DbType = "INT", CanBeNull = false)]
		public long transition { get; set; } = 0;

		[Column(Name = "segment_id", DbType = "INT")]
		public int segment_id { get; set; }

		[Column(Name = "visit_duration", DbType = "INT", CanBeNull = false)]
		public int visit_duration { get; set; } = 0;
	}
}
