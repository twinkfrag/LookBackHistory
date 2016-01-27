using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using LookBackHistory.Models.HistoryEntries;

namespace LookBackHistory.Models.HistoryCollections
{
	public abstract class HistoryDispatcherBase : IDisposable
	{
		public CompositeDisposable CompositeDisposable { get; } = new CompositeDisposable();

		public IQueryable<Entry> Queryable { get; protected set; }

		public abstract Task<bool> LoadAsync();

		protected async Task<FileInfo> CopyAndGetFileAsync(string originalName, string localName)
		{
			if (string.IsNullOrEmpty(originalName) || string.IsNullOrEmpty(localName)) return null;
			var original = new FileInfo(originalName);
			var fileInfo = new FileInfo(localName);

			if (!original.Exists) return null;
			if (!fileInfo.Exists || fileInfo.LastWriteTime < original.LastWriteTime)
			{
				using (var src = new FileStream(original.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				using (var dest = new FileStream(fileInfo.FullName, FileMode.OpenOrCreate, FileAccess.Write))
				{
					await src.CopyToAsync(dest);
				}
				fileInfo.Refresh();
			}
			return fileInfo;
		}

		public IEnumerable<Entry> Search(string title, string url, DateTime begin, DateTime end)
		{
			if (Queryable == null) throw new InvalidOperationException("Not Loaded");

			var e = from h in Queryable.AsEnumerable()
					where h.LastAccess > begin
					where h.LastAccess < end
					where h.Title?.Contains(title ?? string.Empty) ?? false
					where h.Url?.Contains(url ?? string.Empty) ?? false
					select h;
			return e;
		} 

		public void Dispose()
		{
			CompositeDisposable.Dispose();
		}
	}
}
