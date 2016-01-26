using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LookBackHistory.ViewModels;

namespace LookBackHistory.Views
{
	/// <summary>
	/// SearchTabItem.xaml の相互作用ロジック
	/// </summary>
	public partial class SearchTabItem : UserControl
	{
		public SearchTabItem()
		{
			InitializeComponent();
		}

		private void Item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var item = (this.DataContext as SearchTabItemViewModel)?.SelectedItem;
			if (item == null) return;

			Process.Start(item.Url);
		}
	}
}
