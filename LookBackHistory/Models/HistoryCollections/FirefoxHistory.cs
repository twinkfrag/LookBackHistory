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
using LookBackHistory.Models.RawMozilla;

namespace LookBackHistory.Models.HistoryCollections
{
	public class FirefoxHistory : HistoryDipatcherBase
	{
		public override async Task LoadAsync()
		{
			var originalMozPlace = new FileInfo(Environment.GetMozillaHistoryPath());
			var fi = new FileInfo(Environment.LocalFirefoxHistoryFileName);

			if (originalMozPlace.Exists &&
				(!fi.Exists || originalMozPlace.LastWriteTime > fi.LastWriteTime))
			{
				using (var src = new FileStream(originalMozPlace.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				using (var dest = new FileStream(fi.Name, FileMode.OpenOrCreate, FileAccess.Write))
				{
					await src.CopyToAsync(dest);
				}
				fi.Refresh();
			}

			if (!fi.Exists) return;

			try
			{
				var connection = new SQLiteConnection(new SQLiteConnectionStringBuilder
				{
					DataSource = fi.FullName,
				}.ToString());
				CompositeDisposable.Add(connection);

				using (var context = new DataContext(connection))
				{
					Queryable = from h in context.GetTable<moz_historyvisits>()
								join p in context.GetTable<moz_place>() on h.place_id equals p.id
								select new Entry
								{
									FromVisitId = h.from_visit,
									Id = h.id,
									Title = p.title,
									Url = p.url,
									RawTime = h.visit_date / 1000,
									RawTimeMode = Entry.TimeMode.Unix,
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
}
