using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using LookBackHistory.Models.HistoryEntries;

namespace LookBackHistory.Models.HistoryCollections
{
	public abstract class HistoryDipatcherBase<T> : IDisposable where T : HistoryEntryBase
	{
		public CompositeDisposable CompositeDisposable { get; } = new CompositeDisposable();

		public IQueryable<T> Queryable { get; protected set; }

		public abstract Task LoadAsync();

		public IEnumerable<T> Search(string title, string url, DateTime begin, DateTime end)
		{
			if (Queryable == null) throw new InvalidOperationException("Not Loaded");

			var e = from h in Queryable
					where h.LastAccess > begin
					where h.LastAccess < end
					where h.Title.Contains(title)
					where h.Url.Contains(url)
					select h;
			return e.AsEnumerable();
		} 

		public void Dispose()
		{
			CompositeDisposable.Dispose();
		}
	}
}
