using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using Livet;
using LookBackHistory.Models.HistoryEntries;
using LookBackHistory.Models.RawMozilla;

namespace LookBackHistory.Models.HistoryCollections
{
	public class FirefoxHistory : IDisposable
	{
		public IEnumerable<FirefoxEntry> History { get; private set; }

		public LivetCompositeDisposable CompositeDisposable { get; } = new LivetCompositeDisposable();

		public async void Load()
		{
			var originalMozPlace = new FileInfo(Environment.GetMozillaHistoryPath());
			var fi = new FileInfo(Environment.LocalFirefoxHistoryFileName);

			if (originalMozPlace.Exists &&
				(!fi.Exists || originalMozPlace.LastWriteTime > fi.LastWriteTime))
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
					var connection = new SQLiteConnection(new SQLiteConnectionStringBuilder
					{
						DataSource = fi.FullName,
					}.ToString());
					CompositeDisposable.Add(connection);

					using (var context = new DataContext(connection))
					{
						History = from h in context.GetTable<moz_historyvisits>()
						          join p in context.GetTable<moz_place>() on h.place_id equals p.id
						          select new FirefoxEntry
						          {
							          FromVisit = h.from_visit,
							          ID = h.id,
							          Title = p.title,
							          Url = p.url,
							          Session = h.session,
							          VisitDate = h.visit_date / 1000,
							          VisitType = h.visit_type,
						          };
					}
				}
				catch (SQLiteException e)
				{
					Console.WriteLine(e);
					MessageBox.Show("SQLiteException!");
				}
			}
		}

		public void Dispose()
		{
			CompositeDisposable.Dispose();
		}
	}
}
