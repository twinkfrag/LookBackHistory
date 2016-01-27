using System;

namespace LookBackHistory.Utils
{
	public static class DateTimeEx
	{
		private static readonly DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);

		public static DateTime GetDateTime(long epochMilliSecond)
		{
			return unixEpoch.AddMilliseconds(epochMilliSecond);
		}

		private static readonly TimeSpan oneWeek = new TimeSpan(7, 0, 0, 0);
		
		private static readonly TimeSpan oneDay = new TimeSpan(1, 0, 0, 0);

		public static DateTime OneWeekAgo => DateTime.Today - oneWeek;

		public static DateTime Tomorrow => DateTime.Today + oneDay;

		public static DateTime OneMonthAgo => DateTime.Today - new TimeSpan(31, 0, 0, 0);
	}
}
