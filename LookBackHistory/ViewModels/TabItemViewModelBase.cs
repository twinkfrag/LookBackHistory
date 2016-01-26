using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using LookBackHistory.Models;

namespace LookBackHistory.ViewModels
{
	public abstract class TabItemViewModelBase : ViewModel
	{
		public virtual string HeaderTitle { get; }
	}
}
