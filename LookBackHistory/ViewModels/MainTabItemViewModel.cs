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

namespace LookBackHistory.ViewModels
{
	public class MainTabItemViewModel : ViewModel, ITabItem
	{
		public void Initialize()
		{
		}

		public string HeaderTitle => "Main";


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
		private DateTime _BeginDate = Utils.OneMonthAgo;

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
		private DateTime _EndDate = DateTime.Today;

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
					_LoadFirefoxCommand = new ViewModelCommand(LoadFirefox, CanLoadFirefox);
				}
				return _LoadFirefoxCommand;
			}
		}

		public bool CanLoadFirefox() => !IsDataLoaded;

		public void LoadFirefox()
		{
			IsDataLoaded = true;
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
					_LoadChromeCommand = new ViewModelCommand(LoadChrome, CanLoadChrome);
				}
				return _LoadChromeCommand;
			}
		}

		public bool CanLoadChrome() => !IsDataLoaded;

		public void LoadChrome()
		{
			IsDataLoaded = true;
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
					_SearchCommand = new ViewModelCommand(Search, CanSearch);
				}
				return _SearchCommand;
			}
		}

		public bool CanSearch() => IsDataLoaded;

		public void Search()
		{
			MainWindowViewModel.Instance.TabItems.Add(new SearchTabItemViewModel(
				title: TitleSearchText,
				url: UrlSearchText,
				begin: BeginDate,
				end: EndDate));
		}
		#endregion

	}
}
