using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows;
using LookBackHistory.Models.HistoryCollections;
using LookBackHistory.Models.HistoryEntries;
using LookBackHistory.Models.RawChrome;
using LookBackHistory.Models.RawMozilla;

namespace LookBackHistory.ControlsDispatcher
{
	public class HistoryLoader
	{
		/// <summary>
		/// 履歴リスト
		/// </summary>
		public IEnumerable<HistoryEntryBase> History { get; private set; }

		public static HistoryLoader Instance { get; } = new HistoryLoader();

		private HistoryLoader()
		{
		}

		/// <summary>
		/// Firefoxの履歴ファイルをコピーして読み込みます。
		/// </summary>
		public void LoadFirefox()
		{
			Console.WriteLine("Load Firefox");
			var h = new FirefoxHistory();
			h.Load();
			History = h.History;
		}


		/// <summary>
		/// Chromeの履歴ファイルをコピーして読み込みます。
		/// </summary>
		public void LoadChrome()
		{
			Console.WriteLine("Load Chrome");
			var h = new ChromeHistory();
			h.Load();
			History = h.History;
		}
	}
}
