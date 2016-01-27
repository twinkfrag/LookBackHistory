using System;
using LookBackHistory.Utils;

namespace LookBackHistory.Models.HistoryEntries
{
	public class Entry
	{
		public long Id { get; set; }

		public string Url { get; set; }

		public string Title { get; set; }

		public long FileTimeSecond { get; set; }

		public virtual DateTime LastAccess { get; set; }

		public int Count { get; set; }

		public long FromVisitId { get; set; }

		public string DataText { get; set; }

		public dynamic DataDynamic { get; set; }

		public override string ToString()
		{
			return $"{Title} : {Url} : {LastAccess:yyMMdd HHmmss}";
		}
	}
}
