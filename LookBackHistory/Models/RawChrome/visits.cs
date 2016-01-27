using System.Data.Linq.Mapping;
// ReSharper disable InconsistentNaming
#pragma warning disable 0649

namespace LookBackHistory.Models.RawChrome
{
	[Table(Name = "visits")]
	public class visits
	{
		[Column(Name = "id", IsPrimaryKey = true, DbType = "INT")]
		public long id;

		[Column(Name = "url", CanBeNull = false, DbType = "INT")]
		public long url;

		[Column(Name = "visit_time", CanBeNull = false, DbType = "INT")]
		public long visit_time;

		[Column(Name = "from_visit", DbType = "INT")]
		public int from_visit;

		[Column(Name = "transition", DbType = "INT", CanBeNull = false)]
		public long transition;

		[Column(Name = "segment_id", DbType = "INT")]
		public int segment_id;

		[Column(Name = "visit_duration", DbType = "INT", CanBeNull = false)]
		public int visit_duration;
	}
}
