using System.Data.Linq.Mapping;

namespace LookBackHistory.Models.RowMozilla
{
	[Table(Name = "moz_historyvisits")]
	public class moz_historyvisits
	{
		[Column(Name = "id", IsPrimaryKey = true, DbType = "INT")]
		public long id { get; set; }

		[Column(Name = "from_visit", DbType = "INT")]
		public long from_visit { get; set; }

		[Column(Name = "place_id", DbType = "INT")]
		public long place_id { get; set; }

		[Column(Name = "visit_date", DbType = "INT")]
		public long visit_date { get; set; }

		[Column(Name = "visit_type", DbType = "INT")]
		public long visit_type { get; set; }

		[Column(Name = "session", DbType = "INT")]
		public long session { get; set; }

	}
}
