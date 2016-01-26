using System;

namespace LookBackHistory.Models.HistoryEntries
{
	public class ChromeEntry : HistoryEntryBase
	{
		/// <summary>
		/// moz_historyvisitsのvisit_dateはSQLite.dllで取得できないので10で割る。
		/// FileTimeの1/100である。
		/// </summary>
		public long VisitTime { get; set; }

		public int FaviconId { get; set; }

		public int VisitCount { get; set; }

		public int FromVisit { get; set; }

		public int VisitDuration { get; set; }

		public override DateTime LastAccess => DateTime.FromFileTime(VisitTime * 100);
	}
}
