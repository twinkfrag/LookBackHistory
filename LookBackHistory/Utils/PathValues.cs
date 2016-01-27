using System;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;

namespace LookBackHistory.Utils
{
	/// <summary>
	/// システム環境
	/// </summary>
	public static class PathValues
	{
		public static string RoamingApplicationData =>
			Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

		public static string LocalApplicationData =>
			Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

		private static string GlobalFirefoxHistoryFileName => "places.sqlite";

		public static string LocalFirefoxHistoryFileName => "FFHistory.sqlite3";

		public static string GetMozillaHistoryPath()
		{
			var firefoxProfileDir = new DirectoryInfo(Path.Combine(RoamingApplicationData, @"Mozilla\Firefox\Profiles"));
			try
			{
				return firefoxProfileDir.GetDirectories().Single().GetFiles(GlobalFirefoxHistoryFileName).Single().FullName;
			}
			catch (Exception exc) when (exc is DirectoryNotFoundException ||
					exc is NullReferenceException ||
					exc is InvalidOperationException)
			{
				MessageBox.Show(GlobalFirefoxHistoryFileName + "を選択してください", "Firefox履歴ファイルが見つかりません");
				var ofd = new OpenFileDialog
				{
					InitialDirectory = firefoxProfileDir.FullName,
					FileName = GlobalFirefoxHistoryFileName,
					Filter = "|" + GlobalFirefoxHistoryFileName
				};
				if (ofd.ShowDialog() == true)
				{
					return ofd.FileName;
				}
			}
			catch (Exception exc)
			{
				Console.WriteLine(exc);
				MessageBox.Show("Unexpected Exception");
			}
			return string.Empty;
		}

		private static string GlobalChromeHistoryFileName => "History";

		public static string LocalChromeHistoryFileName => "ChHistory.sqlite3";

		public static string GetChromeHistoryPath()
		{
			var chromeProfileDir = new DirectoryInfo(Path.Combine(LocalApplicationData, @"Google\Chrome\User Data"));

			try
			{
				return chromeProfileDir.GetDirectories("Default").First().GetFiles(GlobalChromeHistoryFileName).Single().FullName;
			}
			catch (Exception exc)
			{
				Console.WriteLine(exc);
				MessageBox.Show("Unexpected Exception");
			}
			return string.Empty;
		}
	}
}

