﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;
using LookBackHistory.ControlsDispatcher;
using LookBackHistory.Models;
using LookBackHistory.Views;

namespace LookBackHistory.ViewModels
{
	public class SearchTabItemViewModel : ViewModel, ITabItem
	{
		public SearchTabItemViewModel()
		{
			HeaderTitle = "Search";
		}

		public SearchTabItemViewModel(string title, string url, DateTime begin, DateTime end)
		{
			History = HistoryLoader.Instance.History.Where(x =>
				(x.Title?.Contains(title ?? string.Empty) ?? false) &&
					(x.LastAccess > begin) &&
					(x.LastAccess < end) &&
					(x.Url?.Contains(url ?? string.Empty) ?? false))
				.Select(x => new HistoryEntryViewModel(x))
				.ToArray();

			HeaderTitle = !string.IsNullOrEmpty(title) ? title :
						  !string.IsNullOrEmpty(url) ? url : "Search";
		}

		public string HeaderTitle { get; }

		#region History 変更通知プロパティ
		private HistoryEntryViewModel[] _History;

		public HistoryEntryViewModel[] History
		{
			get
			{ return _History; }
			set
			{
				if (_History == value)
					return;
				_History = value;
				RaisePropertyChanged();
			}
		}
		#endregion


		public HistoryEntryViewModel SelectedItem { get; set; }
	}
}
