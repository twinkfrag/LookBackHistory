using System;
using System.Diagnostics;
using Livet;

using LookBackHistory.Models;

namespace LookBackHistory.ViewModels
{
	public class HistoryEntryViewModel : ViewModel
	{
		private HistoryObject entry;

		public HistoryEntryViewModel() { }

		public HistoryEntryViewModel(HistoryObject entry)
		{
			this.entry = entry;

			Title = this.entry.Title;
			Url = this.entry.Url;
			LastAccess = this.entry.LastAccess;
		}

		public string Title { get; set; }

		public string Url { get; set; }

		public DateTime LastAccess { get; set; }

		public void Open()
		{
			Process.Start(Url);
		}
	}
}
