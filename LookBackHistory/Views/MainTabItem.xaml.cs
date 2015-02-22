using System;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;

namespace LookBackHistory.Views
{
	/// <summary>
	/// MainTabItem.xaml の相互作用ロジック
	/// </summary>
	public partial class MainTabItem : UserControl, INotifyPropertyChanged
	{
		public MainTabItem()
		{
			InitializeComponent();
			this.DataContext = this;

#if DEBUG
			TitleSearchText = "C86";
			UrlSearchText = "pixiv";
#endif
		}

		#region TitleSearchText 変更通知プロパティ
		private string _titleSearchText;
		public string TitleSearchText
		{
			get { return _titleSearchText; }
			set
			{
				if (_titleSearchText != value)
				{
					_titleSearchText = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TitleSearchText)));
				}
			}
		}
		#endregion

		#region UrlSearchText 変更通知プロパティ
		private string _urlSearchText;
		public string UrlSearchText
		{
			get { return _urlSearchText; }
			set
			{
				if (_urlSearchText != value)
				{
					_urlSearchText = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UrlSearchText)));
				}
			}
		}
		#endregion

		#region BeginDate 変更通知プロパティ
		private DateTime _beginDate = Utils.OneMonthAgo;
		public DateTime BeginDate
		{
			get { return this._beginDate; }
			set
			{
				_beginDate = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BeginDate)));
			}
		}
		#endregion

		#region EndDate 変更通知プロパティ
		private DateTime _endDate = DateTime.Today;
		public DateTime EndDate
		{
			get { return this._endDate; }
			set
			{
				_endDate = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EndDate)));
			}
		}
		#endregion

		#region IsSearchAble, IsDataLoaded, IsDataLoadable 変更通知プロパティ
		private bool _isDataLoadable = true;
		/// <summary>
		/// IsDataLoaded と同値
		/// </summary>
		public bool IsSearchable
		{
			get { return !this._isDataLoadable; }
		}

		/// <summary>
		/// IsDataLoaded の反転
		/// </summary>
		public bool IsDataLoadable
		{
			get { return this._isDataLoadable; }
		}

		public bool IsDataLoaded
		{
			set
			{
				if (_isDataLoadable == value)
				{
					_isDataLoadable = !value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsDataLoadable)));
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSearchable)));
				}
			}
		}
		#endregion

		public event PropertyChangedEventHandler PropertyChanged;

		public event EventHandler SearchEvent = (s, e) => {	Console.WriteLine("search"); };

        public event EventHandler LoadFirefox = (s, e) => { Console.WriteLine("load firefox event"); };

		public event EventHandler LoadChrome = (s, e) => { Console.WriteLine("load chrome event"); };

		private void LoadFF_Click(object sender, RoutedEventArgs e)
		{
			LoadFirefox?.Invoke(this, null);
		}

		private void LoadCh_Click(object sender, RoutedEventArgs e)
		{
			LoadChrome?.Invoke(this, null);
		}

		private void Search_Click(object sender, RoutedEventArgs e)
		{
			SearchEvent?.Invoke(this, null);
		}
	}
}
