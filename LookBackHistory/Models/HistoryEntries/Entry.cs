using System;

namespace LookBackHistory.Models.HistoryEntries
{
	public class Entry
	{
		public long Id { get; set; }

		public string Url { get; set; }

		public string Title { get; set; }

		internal long RawTime { private get; set; }

		internal TimeMode RawTimeMode { private get; set; }

		public DateTime LastAccess
		{
			get
			{
				switch (RawTimeMode)
				{
					case TimeMode.Unix:
						return Utils.GetDateTime(RawTime);
					case TimeMode.FileTimeCenti:
						return DateTime.FromFileTime(RawTime * 100);
					default:
						throw new NotSupportedException(nameof(RawTimeMode));
				}
			}
		}

		public int Count { get; set; }

		public long FromVisitId { get; set; }

		public string DataText { get; set; }

		public dynamic DataDynamic { get; set; }

		public override string ToString()
		{
			return $"{Title} : {Url} : {LastAccess:yyMMdd HHmmss}";
		}

		public enum TimeMode { Unix, FileTimeCenti }
	}
}
