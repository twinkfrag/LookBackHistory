using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Livet;
using LookBackHistory.Models.HistoryEntries;
using LookBackHistory.Models.RawChrome;

namespace LookBackHistory.Models.HistoryCollections
{
	class ChromeHistory : IDisposable
	{
		public IEnumerable<ChromeEntry> History { get; private set; }

		public LivetCompositeDisposable CompositeDisposable { get; } = new LivetCompositeDisposable();

		public async void Load()
		{
			var originalChrPlace = new FileInfo(Environment.GetChromeHistoryPath());
			var fi = new FileInfo(Environment.LocalChromeHistoryFileName);

			if (originalChrPlace.Exists &&
				(!fi.Exists || originalChrPlace.LastWriteTime > fi.LastWriteTime))
			{
				using (var src = new FileStream(originalChrPlace.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				using (var dest = new FileStream(fi.Name, FileMode.OpenOrCreate, FileAccess.Write))
				{
					src.CopyTo(dest);
				}
				fi.Refresh();
			}

			if (!fi.Exists) return;

			try
			{
				var connection = new SQLiteConnection(new SQLiteConnectionStringBuilder
				{
					DataSource = fi.FullName
				}.ToString());
				using (var context = new DataContext(connection))
				{

					History = from v in context.GetTable<visits>()
					          join u in context.GetTable<urls>() on v.url equals u.id
					          select new ChromeEntry
					          {
						          ID = v.id,
						          FaviconId = u.favicon_id,
						          FromVisit = v.from_visit,
						          Title = u.title,
						          Url = u.url,
						          VisitCount = u.visit_count,
						          VisitTime = v.visit_time / 10,
						          VisitDuration = v.visit_duration,
					          };
				}
			}
			catch (SQLiteException e)
			{
				Console.WriteLine(e);
				MessageBox.Show("SQLiteException!");
			}
		}

		public void Dispose()
		{
			CompositeDisposable.Dispose();
		}
	}
}
