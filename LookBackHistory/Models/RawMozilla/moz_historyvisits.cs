using System.Data.Linq.Mapping;
// ReSharper disable InconsistentNaming

namespace LookBackHistory.Models.RawMozilla
{
	[Table(Name = "moz_historyvisits")]
	public class moz_historyvisits
	{
		[Column(Name = "id", IsPrimaryKey = true, DbType = "INT")]
		public long id;

		[Column(Name = "from_visit", DbType = "INT")]
		public long from_visit;

		[Column(Name = "place_id", DbType = "INT")]
		public long place_id;

		[Column(Name = "visit_date", DbType = "INT")]
		public long visit_date;

		[Column(Name = "visit_type", DbType = "INT")]
		public long visit_type;

		[Column(Name = "session", DbType = "INT")]
		public long session;

	}
}
