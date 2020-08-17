using System;
using System.Windows;
using System.Windows.Input;

namespace AttachedProperties
{
	/// <summary>Class with Attachable Property for bound RoutedCommands</summary>
	public class RoutedCommandBindings : Freezable
	{
		/// <summary>Getting the RoutedCommand Collection</summary>
		/// <param name="obj">The object to which the property is attached</param>
		/// <returns>RoutedCommandBinding Collection</returns>
		/// <exception cref="obj">Thrown when not a UIElement or ContentElement</exception>
		public static RoutedCommandBindingCollection GetRoutedCommandBindings(DependencyObject obj)
		{
			RoutedCommandBindingCollection routedCollection = (RoutedCommandBindingCollection)obj.GetValue(RoutedCommandBindingCollectionProperty);
			if (routedCollection == null)
			{
				CommandBindingCollection commandCollection;
				if (obj is UIElement element)
					commandCollection = element.CommandBindings;
				else if (obj is ContentElement content)
					commandCollection = content.CommandBindings;
				else
					throw new ArgumentException("There must be an UIElement or ContentElement", nameof(obj));

				obj.SetValue(RoutedCommandBindingCollectionProperty, routedCollection = new RoutedCommandBindingCollection(commandCollection));
			}

			return routedCollection;
		}

        protected override Freezable CreateInstanceCore() => new RoutedCommandBindings();

        // Using a DependencyProperty as the backing store for RoutedCommandBindingCollection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RoutedCommandBindingCollectionProperty =
			DependencyProperty.RegisterAttached("ShadowRoutedCommandBindings", typeof(RoutedCommandBindingCollection), typeof(RoutedCommandBindings), new PropertyMetadata(null));

	}
}
