using System.Data.Linq.Mapping;

namespace LookBackHistory.Models.RowMozilla
{
	[Table(Name = "moz_places")]
	public class moz_place
	{
		[Column(Name = "id", IsPrimaryKey = true, DbType = "INT")]
		public long id { get; set; }

		[Column(Name = "url", DbType = "TEXT")]
		public string url { get; set; }

		[Column(Name = "title", DbType = "TEXT")]
		public string title { get; set; }

		[Column(Name = "frecency", DbType = "INT")]
		public int frecency { get; set; }

	}
}
