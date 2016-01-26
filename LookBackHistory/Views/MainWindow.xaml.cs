using System;
using System.Windows;
using System.Windows.Controls;
using LookBackHistory.ControlsDispatcher;
using LookBackHistory.ViewModels;

namespace LookBackHistory.Views
{
	/* 
	 * ViewModelからの変更通知などの各種イベントを受け取る場合は、PropertyChangedWeakEventListenerや
     * CollectionChangedWeakEventListenerを使うと便利です。独自イベントの場合はLivetWeakEventListenerが使用できます。
     * クローズ時などに、LivetCompositeDisposableに格納した各種イベントリスナをDisposeする事でイベントハンドラの開放が容易に行えます。
     *
     * WeakEventListenerなので明示的に開放せずともメモリリークは起こしませんが、できる限り明示的に開放するようにしましょう。
     */

	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			//mainTabItem.LoadFirefox += (s, e) =>
			//{
			//	((MainTabItemViewModel)mainTabItem.DataContext).IsDataLoaded = historyLoader.LoadFirefox();
			//};
			//mainTabItem.LoadChrome += (s, e) =>
			//{
			//	((MainTabItemViewModel)mainTabItem.DataContext).IsDataLoaded = historyLoader.LoadChrome();
			//};

			//mainTabItem.SearchEvent += MainTabItem_SearchEvent;
		}

		private void MainTabItem_SearchEvent(object sender, EventArgs e)
		{
			//var context = (MainTabItemViewModel)mainTabItem.DataContext;
			//var newtab = new TabItem
			//{
			//	Header =
			//		!string.IsNullOrEmpty(context.TitleSearchText) ? context.TitleSearchText :
			//		!string.IsNullOrEmpty(context.UrlSearchText) ? context.UrlSearchText : "Search",
			//	Content = new SearchTabItem
			//	{
			//		DataContext = new SearchTabItemViewModel(
			//			title: context.TitleSearchText,
			//			url: context.UrlSearchText,
			//			begin: context.BeginDate,
			//			end: context.EndDate)
			//	},
			//};
			//tab.Items.Add(newtab);
			//tab.SelectedItem = newtab;
		}
	}
}
