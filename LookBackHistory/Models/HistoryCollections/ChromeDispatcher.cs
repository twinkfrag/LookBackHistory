using System;
using System.Data.Linq;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using LookBackHistory.Models.HistoryEntries;
using LookBackHistory.Models.RawChrome;
using Reactive.Bindings.Extensions;

namespace LookBackHistory.Models.HistoryCollections
{
	public class ChromeDispatcher : HistoryDipatcherBase
	{
		public override async Task<bool> LoadAsync()
		{
			var fileInfo =
				await CopyAndGetFileAsync(Environment.GetChromeHistoryPath(), Environment.LocalChromeHistoryFileName);
			if (!(fileInfo?.Exists ?? false)) return false;

			try
			{
				var connection = new SQLiteConnection(new SQLiteConnectionStringBuilder
				{
					DataSource = fileInfo.FullName
				}.ToString()).AddTo(this.CompositeDisposable);

				using (var context = new DataContext(connection))
				{
					Queryable = from v in context.GetTable<visits>()
								join u in context.GetTable<urls>() on v.url equals u.id
								select new Entry
								{
									Id = v.id,
									FromVisitId = v.from_visit,
									Title = u.title,
									Url = u.url,
									Count = u.visit_count,
									RawTime = v.visit_time / 10,
									RawTimeMode = Entry.TimeMode.FileTimeCenti,
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
