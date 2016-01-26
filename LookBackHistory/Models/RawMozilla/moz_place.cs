using System.Data.Linq.Mapping;
// ReSharper disable InconsistentNaming
#pragma warning disable 0649

namespace LookBackHistory.Models.RawMozilla
{
	[Table(Name = "moz_places")]
	public class moz_place
	{
		[Column(Name = "id", IsPrimaryKey = true, DbType = "INT")]
		public long id;

		[Column(Name = "url", DbType = "TEXT")]
		public string url;

		[Column(Name = "title", DbType = "TEXT")]
		public string title;

		[Column(Name = "frecency", DbType = "INT")]
		public int frecency;

	}
}
