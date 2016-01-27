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

		public static DateTime OneWeekAgo => DateTime.Today - TimeSpan.FromDays(7);

		public static DateTime Tomorrow => (DateTime.Today + TimeSpan.FromDays(1)).Date;

		public static DateTime OneMonthAgo => DateTime.Today - TimeSpan.FromDays(31);
	}
}
