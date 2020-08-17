using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Input;

namespace AttachedProperties
{
	/// <summary>Collection for RoutedCommandBinding</summary>
	public class RoutedCommandBindingCollection : FreezableCollection<RoutedCommandBinding>
	{
		/// <summary>Linked CommandBindingCollection</summary>
		public CommandBindingCollection CommandBindingCollection { get; }

		/// <summary>The only constructor</summary>
		/// <param name="commandBindingCollection">Linked CommandBindingCollection</param>
		/// <exception cref="commandBindingCollection">Thrown when null</exception>
		public RoutedCommandBindingCollection(CommandBindingCollection commandBindingCollection)
		{
			CommandBindingCollection = commandBindingCollection ?? throw new ArgumentNullException(nameof(commandBindingCollection));
			INotifyCollectionChanged notifyCollection = this;
			notifyCollection.CollectionChanged += CollectionChanged;
		}

		private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.OldItems?.Count > 0)
				foreach (RoutedCommandBinding commandBinding in e.OldItems)
					CommandBindingCollection.Remove(commandBinding.CommandBinding);

			if (e.NewItems?.Count > 0)
				foreach (RoutedCommandBinding commandBinding in e.NewItems)
					CommandBindingCollection.Add(commandBinding.CommandBinding);
		}

	}
}
