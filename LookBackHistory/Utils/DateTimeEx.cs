using System;

namespace LookBackHistory.Utils
{
	public static class DateTimeEx
	{
		private static readonly DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);

		private static readonly DateTime zero = DateTime.FromFileTime(0L);

		public static DateTime FromUnixEpoch(long epochMilliSecond)
		{
			return unixEpoch.AddMilliseconds(epochMilliSecond);
		}

		public static long ToUnixEpoch(this DateTime dateTime)
		{
			return (dateTime - unixEpoch).Ticks / 10;
		}

		public static DateTime FromFileTimeSec(long fileTimeSec)
		{
			return zero.AddMilliseconds(fileTimeSec * 1000);
		}

		public static long FileTimeFromUnixEpoch(long epochMillisecond)
		{
			return FromUnixEpoch(epochMillisecond).ToFileTime() / 1000;
		}

		private static readonly TimeSpan oneWeek = new TimeSpan(7, 0, 0, 0);
		
		private static readonly TimeSpan oneDay = new TimeSpan(1, 0, 0, 0);

		public static DateTime OneWeekAgo => DateTime.Today - oneWeek;

		public static DateTime Tomorrow => DateTime.Today + oneDay;

		public static DateTime OneMonthAgo => DateTime.Today - new TimeSpan(31, 0, 0, 0);
	}
}
