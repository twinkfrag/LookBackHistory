using System;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;

namespace LookBackHistory.Views
{
	/// <summary>
	/// MainTabItem.xaml の相互作用ロジック
	/// </summary>
	public partial class MainTabItem : UserControl
	{
		public MainTabItem()
		{
			InitializeComponent();
		}

		public event EventHandler SearchEvent = (s, e) => { Console.WriteLine("search"); };

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
