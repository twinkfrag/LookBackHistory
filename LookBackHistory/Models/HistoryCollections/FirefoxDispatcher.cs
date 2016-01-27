using System;
using System.Data.Linq;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using LookBackHistory.Models.HistoryEntries;
using LookBackHistory.Models.RawMozilla;
using LookBackHistory.Utils;
using Reactive.Bindings.Extensions;
using Environment = LookBackHistory.Utils.Environment;

namespace LookBackHistory.Models.HistoryCollections
{
	public class FirefoxDispatcher : HistoryDispatcherBase
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

				var context = new DataContext(connection).AddTo(CompositeDisposable);

				Queryable = from h in context.GetTable<moz_historyvisits>()
							join p in context.GetTable<moz_place>() on h.place_id equals p.id
							select new Entry
							{
								FromVisitId = h.from_visit,
								Id = h.id,
								Title = p.title,
								Url = p.url,
								//FileTimeSecond = DateTimeEx.FileTimeFromUnixEpoch(h.visit_date / 1000), 
								//LastAccess = DateTimeEx.FromUnixEpoch(h.visit_date / 1000),
							};

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
