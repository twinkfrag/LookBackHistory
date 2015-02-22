using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows;
using LookBackHistory.Models;
using LookBackHistory.Models.RowChrome;
using LookBackHistory.Models.RowMozilla;
using static System.Console;

namespace LookBackHistory.ViewModels
{
	public class HistoryLoader
	{
		/// <summary>
		/// 履歴リスト
		/// </summary>
		public IEnumerable<HistoryObject> Histories;

		private SQLiteConnection connection;

		public HistoryLoader()
		{
			App.Current.Exit += (s, e) =>
			{
				try { connection?.Dispose(); }
				catch { WriteLine("Cannot connection Dispose"); }
			};
		}

		/// <summary>
		/// Firefoxの履歴ファイルをコピーして読み込みます。
		/// </summary>
		public bool LoadFirefox()
		{
			WriteLine("Load Firefox");
			var originalMozPlace = new FileInfo(Environment.GetMozillaHistoryPath());
			var fi = new FileInfo(Environment.LocalFirefoxHistoryFileName);

			if (originalMozPlace.Exists &&
				(fi.Exists ? originalMozPlace.LastWriteTime > fi.LastWriteTime : true))
			{
				using (var src = new FileStream(originalMozPlace.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				using (var dest = new FileStream(fi.Name, FileMode.OpenOrCreate, FileAccess.Write))
				{
					src.CopyTo(dest);
				}
				fi.Refresh();
			}

			if (fi.Exists)
			{
				try
				{
					var connectionString = new SQLiteConnectionStringBuilder
					{
						DataSource = fi.FullName
					};

					connection = new SQLiteConnection(connectionString.ToString());
					var context = new DataContext(connection);

					Histories = context.GetTable<moz_historyvisits>().Join(
						context.GetTable<moz_place>(),
						h => h.place_id,
						p => p.id,
						(h, p) => new HistoryMoz()
						{
							FromVisit = h.from_visit,
							ID = h.id,
							Title = p.title,
							Url = p.url,
							Session = h.session,
							VisitDate = h.visit_date / 1000,
							VisitType = h.visit_type,
						});
					return true;
				}
				catch (SQLiteException e)
				{
					WriteLine(e);
					MessageBox.Show("SQLiteException!");
				}
			}
			else
			{
				MessageBox.Show(@"File dose not exist.");
			}
			return false;
		}

		
		/// <summary>
		/// Chromeの履歴ファイルをコピーして読み込みます。
		/// </summary>
		public bool LoadChrome()
		{
			WriteLine("Load Chrome");
			var originalChrPlace = new FileInfo(Environment.GetChromeHistoryPath());
			var fi = new FileInfo(Environment.LocalChromeHistoryFileName);

			if (originalChrPlace.Exists &&
				(fi.Exists ? originalChrPlace.LastWriteTime > fi.LastWriteTime : true))
			{
				using (var src = new FileStream(originalChrPlace.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				using (var dest = new FileStream(fi.Name, FileMode.OpenOrCreate, FileAccess.Write))
				{
					src.CopyTo(dest);
				}
				fi.Refresh();
			}

			if (fi.Exists)
			{
				try
				{
					var connectionString = new SQLiteConnectionStringBuilder
					{
						DataSource = fi.FullName
					};

					connection = new SQLiteConnection(connectionString.ToString());
					var context = new DataContext(connection);

					Histories = context.GetTable<visits>().Join(
						context.GetTable<urls>(),
						v => v.url,
						u => u.id,
						(v, u) => new HistoryChr()
						{
							ID = v.id,
							FaviconID = u.favicon_id,
							FromVisit = v.from_visit,
							Title = u.title,
							Url = u.url,
							VisitCount = u.visit_count,
							VisitTime = v.visit_time / 10,
							VisitDuration = v.visit_duration,
						});

					return true;
				}
				catch (SQLiteException e)
				{
					WriteLine(e);
					MessageBox.Show("SQLiteException!");
				}
			}
			else
			{
				MessageBox.Show(@"File dose not exist.");
			}
			return false;
		}
	}
}
