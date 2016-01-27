using System;
using System.Data.Linq;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using LookBackHistory.Models.HistoryEntries;
using LookBackHistory.Models.RawMozilla;
using Reactive.Bindings.Extensions;

namespace LookBackHistory.Models.HistoryCollections
{
	public class FirefoxDispatcher : HistoryDipatcherBase
	{
		public override async Task<bool> LoadAsync()
		{
			var fileInfo =
				await CopyAndGetFileAsync(Environment.GetMozillaHistoryPath(), Environment.LocalFirefoxHistoryFileName);
			if (!(fileInfo?.Exists ?? false)) return false;

			try
			{
				var connection = new SQLiteConnection(new SQLiteConnectionStringBuilder
				{
					DataSource = fileInfo.FullName,
				}.ToString()).AddTo(this.CompositeDisposable);

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

				return Queryable != null;
			}
			catch (SQLiteException e)
			{
				Console.WriteLine(e);
				MessageBox.Show("SQLiteException!");
				return false;
			}
		}
	}
}
