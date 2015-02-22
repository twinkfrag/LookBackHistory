using System;
using System.Windows;
using System.Windows.Controls;
using LookBackHistory.ViewModels;

namespace LookBackHistory.Views
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			historyLoader = new HistoryLoader();
			mainTabItem.LoadFirefox += (s, e) =>
			{
				mainTabItem.IsDataLoaded = historyLoader.LoadFirefox();
			};
			mainTabItem.LoadChrome += (s, e) =>
			{
				mainTabItem.IsDataLoaded = historyLoader.LoadChrome();
			};

			mainTabItem.SearchEvent += MainTabItem_SearchEvent;
		}

		private void MainTabItem_SearchEvent(object sender, EventArgs e)
		{
			var newtab = new TabItem()
			{
				Header = 
					!string.IsNullOrEmpty(mainTabItem.TitleSearchText) ? mainTabItem.TitleSearchText : 
					!string.IsNullOrEmpty(mainTabItem.UrlSearchText) ? mainTabItem.UrlSearchText :  "Search",
				Content = new SearchTabItem()
				{
					TitleSearchString = mainTabItem.TitleSearchText,
					UrlSearchString = mainTabItem.UrlSearchText,
					BeginDate = mainTabItem.BeginDate,
					EndDate = mainTabItem.EndDate,
				},
			};
			tab.Items.Add(newtab);
			tab.SelectedItem = newtab;
		}

		public static HistoryLoader historyLoader;
	}
}
