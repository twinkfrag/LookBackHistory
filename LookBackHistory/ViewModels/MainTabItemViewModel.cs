﻿using System;
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
	public class MainTabItemViewModel : ViewModel
	{
		/* コマンド、プロパティの定義にはそれぞれ 
         * 
         *  lvcom   : ViewModelCommand
         *  lvcomn  : ViewModelCommand(CanExecute無)
         *  llcom   : ListenerCommand(パラメータ有のコマンド)
         *  llcomn  : ListenerCommand(パラメータ有のコマンド・CanExecute無)
         *  lprop   : 変更通知プロパティ(.NET4.5ではlpropn)
         *  
         * を使用してください。
         * 
         * Modelが十分にリッチであるならコマンドにこだわる必要はありません。
         * View側のコードビハインドを使用しないMVVMパターンの実装を行う場合でも、ViewModelにメソッドを定義し、
         * LivetCallMethodActionなどから直接メソッドを呼び出してください。
         * 
         * ViewModelのコマンドを呼び出せるLivetのすべてのビヘイビア・トリガー・アクションは
         * 同様に直接ViewModelのメソッドを呼び出し可能です。
         */

		/* ViewModelからViewを操作したい場合は、View側のコードビハインド無で処理を行いたい場合は
         * Messengerプロパティからメッセージ(各種InteractionMessage)を発信する事を検討してください。
         */

		/* Modelからの変更通知などの各種イベントを受け取る場合は、PropertyChangedEventListenerや
         * CollectionChangedEventListenerを使うと便利です。各種ListenerはViewModelに定義されている
         * CompositeDisposableプロパティ(LivetCompositeDisposable型)に格納しておく事でイベント解放を容易に行えます。
         * 
         * ReactiveExtensionsなどを併用する場合は、ReactiveExtensionsのCompositeDisposableを
         * ViewModelのCompositeDisposableプロパティに格納しておくのを推奨します。
         * 
         * LivetのWindowテンプレートではViewのウィンドウが閉じる際にDataContextDisposeActionが動作するようになっており、
         * ViewModelのDisposeが呼ばれCompositeDisposableプロパティに格納されたすべてのIDisposable型のインスタンスが解放されます。
         * 
         * ViewModelを使いまわしたい時などは、ViewからDataContextDisposeActionを取り除くか、発動のタイミングをずらす事で対応可能です。
         */

		/* UIDispatcherを操作する場合は、DispatcherHelperのメソッドを操作してください。
         * UIDispatcher自体はApp.xaml.csでインスタンスを確保してあります。
         * 
         * LivetのViewModelではプロパティ変更通知(RaisePropertyChanged)やDispatcherCollectionを使ったコレクション変更通知は
         * 自動的にUIDispatcher上での通知に変換されます。変更通知に際してUIDispatcherを操作する必要はありません。
         */

		public void Initialize()
		{
#if DEBUG
			TitleSearchText = "C86";
			UrlSearchText = "pixiv";
#endif
		}


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
		private DateTime _BeginDate;

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
		private DateTime _EndDate;

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

		#region IsSearchAble, IsDataLoaded, IsDataLoadable 変更通知プロパティ
		private bool _IsDataLoadable = true;
		/// <summary>
		/// IsDataLoaded と同値
		/// </summary>
		public bool IsSearchable => !_IsDataLoadable;

		/// <summary>
		/// IsDataLoaded の反転
		/// </summary>
		public bool IsDataLoadable => _IsDataLoadable;

		public bool IsDataLoaded
		{
			set
			{
				if (_IsDataLoadable == value)
				{
					_IsDataLoadable = !value;
					// ReSharper disable ExplicitCallerInfoArgument
					RaisePropertyChanged(nameof(IsDataLoadable));
					RaisePropertyChanged(nameof(IsSearchable));
					// ReSharper restore ExplicitCallerInfoArgument
				}
			}
		}
		#endregion
	}
}