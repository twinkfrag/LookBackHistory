﻿using System;
using LookBackHistory.Utils;

namespace LookBackHistory.Models.HistoryEntries
{
	public class FirefoxEntry : Entry
	{
		public long FromVisit { get; set; }

		public long PlaceId { get; set; }

		/// <summary>
		/// moz_historyvisitsのvisit_dateはエポックナノ秒で桁が大きすぎて溢れるので1000で割ってミリ秒にすること
		/// </summary>
		public long VisitDate { get; set; }

		public long VisitType { get; set; }

		public long Session { get; set; }

		public override DateTime LastAccess => DateTimeEx.FromUnixEpoch(VisitDate);
	}
}