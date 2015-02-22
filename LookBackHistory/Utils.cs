using System;

namespace LookBackHistory
{
	public static class Utils
	{
		private static readonly DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);

		public static DateTime GetDateTime(long epochMilliSecond)
		{
			return UNIX_EPOCH.AddMilliseconds(epochMilliSecond);
		}

		private static readonly TimeSpan ONE_WEEK = new TimeSpan(7, 0, 0, 0);

		public static DateTime OneWeekAgo => DateTime.Today - ONE_WEEK;

		public static DateTime OneMonthAgo => DateTime.Today - new TimeSpan(31, 0, 0, 0);
	}
}
