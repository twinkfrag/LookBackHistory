using System.Collections.Generic;
using Livet.Commands;

namespace LookBackHistory.Utils
{
	public class CompositeCommands : ICollection<ViewModelCommand>
	{
		private readonly List<ViewModelCommand> commands;

		public CompositeCommands()
		{
			commands = new List<ViewModelCommand>();
		}

		public CompositeCommands(IEnumerable<ViewModelCommand> enumerable)
		{
			commands = new List<ViewModelCommand>(enumerable);
		}

		public void RaiseCanExtecuteChangedAll()
		{
			foreach (var command in commands)
			{
				command.RaiseCanExecuteChanged();
			}
		}

		#region impletement interface

		public IEnumerator<ViewModelCommand> GetEnumerator() => commands.GetEnumerator();

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		public void Add(ViewModelCommand item) => commands.Add(item);

		public void Clear() => commands.Clear();

		public bool Contains(ViewModelCommand item) => commands.Contains(item);

		public void CopyTo(ViewModelCommand[] array, int arrayIndex) => commands.CopyTo(array, arrayIndex);

		public bool Remove(ViewModelCommand item) => commands.Remove(item);

		public int Count => commands.Count;

		public bool IsReadOnly => false;

		#endregion
	}

	public static class LivetCommandEx
	{
		public static ViewModelCommand AddTo(this ViewModelCommand command, ICollection<ViewModelCommand> collection)
		{
			collection.Add(command);
			return command;
		}
	}
}
