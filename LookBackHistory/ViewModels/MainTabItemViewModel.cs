using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;
using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using LookBackHistory.Models;
using LookBackHistory.Models.HistoryCollections;
using LookBackHistory.Utils;
using Reactive.Bindings.Extensions;

namespace LookBackHistory.ViewModels
{
	public class MainTabItemViewModel : ViewModel, ITabItem
	{
		public void Initialize()
		{
		}

		public string HeaderTitle => "Main";

		private CompositeCommands CompositeCommands { get; } = new CompositeCommands();

		private HistoryDispatcherBase dispatcher;

		#region TitleSearchText変更通知プロパティ
		private string _TitleSearchText;

		public string TitleSearchText
		{
			get
			{ return _TitleSearchText; }
			set
			{
				if (_TitleSearchText == value)
					return;
				_TitleSearchText = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region UrlSearchText変更通知プロパティ
		private string _UrlSearchText;

		public string UrlSearchText
		{
			get
			{ return _UrlSearchText; }
			set
			{
				if (_UrlSearchText == value)
					return;
				_UrlSearchText = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region BeginDate変更通知プロパティ
		private DateTime _BeginDate = DateTimeEx.OneMonthAgo;

		public DateTime BeginDate
		{
			get
			{ return _BeginDate; }
			set
			{
				if (_BeginDate == value)
					return;
				_BeginDate = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region EndDate変更通知プロパティ
		private DateTime _EndDate = DateTimeEx.Tomorrow;

		public DateTime EndDate
		{
			get
			{ return _EndDate; }
			set
			{
				if (_EndDate == value)
					return;
				_EndDate = value;
				RaisePropertyChanged();
			}
		}
		#endregion


		#region IsDataLoaded変更通知プロパティ
		private bool _IsDataLoaded;

		public bool IsDataLoaded
		{
			get
			{ return _IsDataLoaded; }
			set
			{ 
				if (_IsDataLoaded == value)
					return;
				_IsDataLoaded = value;
				RaisePropertyChanged();
				CompositeCommands.RaiseCanExtecuteChangedAll();
			}
		}
		#endregion


		#region LoadFirefoxCommand
		private ViewModelCommand _LoadFirefoxCommand;

		public ViewModelCommand LoadFirefoxCommand
		{
			get
			{
				if (_LoadFirefoxCommand == null)
				{
					_LoadFirefoxCommand = new ViewModelCommand(LoadFirefox, CanLoadFirefox).AddTo(CompositeCommands);
				}
				return _LoadFirefoxCommand;
			}
		}

		public bool CanLoadFirefox() => !IsDataLoaded;

		public async void LoadFirefox()
		{
			dispatcher?.Dispose();
			dispatcher = new FirefoxDispatcher().AddTo(this.CompositeDisposable);
			IsDataLoaded = await dispatcher.LoadAsync();
		}
		#endregion


		#region LoadChromeCommand
		private ViewModelCommand _LoadChromeCommand;

		public ViewModelCommand LoadChromeCommand
		{
			get
			{
				if (_LoadChromeCommand == null)
				{
					_LoadChromeCommand = new ViewModelCommand(LoadChrome, CanLoadChrome).AddTo(CompositeCommands);
				}
				return _LoadChromeCommand;
			}
		}

		public bool CanLoadChrome() => !IsDataLoaded;

		public async void LoadChrome()
		{
			dispatcher?.Dispose();
			dispatcher = new ChromeDispatcher().AddTo(this.CompositeDisposable);
			IsDataLoaded = await dispatcher.LoadAsync();
		}
		#endregion


		#region SearchCommand
		private ViewModelCommand _SearchCommand;

		public ViewModelCommand SearchCommand
		{
			get
			{
				if (_SearchCommand == null)
				{
					_SearchCommand = new ViewModelCommand(Search, CanSearch).AddTo(CompositeCommands);
				}
				return _SearchCommand;
			}
		}

		public bool CanSearch() => IsDataLoaded;

		public void Search()
		{
			var q = dispatcher.Search(TitleSearchText, UrlSearchText, BeginDate, EndDate);
			var header = TitleSearchText.GetOrDefault(UrlSearchText).GetOrDefault("Search");

			MainWindowViewModel.Instance.TabItems.Add(new SearchTabItemViewModel(q, header));

			dispatcher.Dispose();
		}
		#endregion

	}
}
