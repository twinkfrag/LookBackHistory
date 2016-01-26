using System;

namespace LookBackHistory.Models
{
	public class HistoryObject
	{
		public virtual long ID { get; set; }

		public virtual string Url { get; set; }

		public virtual string Title { get; set; }

		public virtual DateTime LastAccess { get; set; }

		public virtual string DataText { get; set; }

		public virtual dynamic DataDynamic { get; set; }

		public override string ToString()
		{
			return $"{Title} : {Url} : {LastAccess:yyyymmdd}";
		}
	}
}
