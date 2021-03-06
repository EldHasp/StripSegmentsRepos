﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Common
{
	/// <summary>Базовый класс с реализацией INPC </summary>
	public abstract class OnPropertyChangedClass : INotifyPropertyChanged
	{
		#region Событие PropertyChanged
		/// <summary>Событие для извещения об изменения свойства</summary>
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		#region Методы вызова события PropertyChanged
		/// <summary>Метод для вызова события извещения об изменении свойства</summary>
		/// <param name="propertyName">Изменившееся свойство</param>
		public void OnPropertyChanged([CallerMemberName]string propertyName = "")
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		/// <summary>Метод для вызова события извещения об изменении списка свойств</summary>
		/// <param name="propList">Список имён свойств</param>
		public void OnPropertyChanged(IEnumerable<string> propList)
		{
			foreach (string propertyName in propList.Where(name => !string.IsNullOrWhiteSpace(name)))
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		/// <summary>Метод для вызова события извещения об изменении перечня свойств</summary>
		/// <param name="propList">Список имён свойств</param>
		public void OnPropertyChanged(params string[] propList)
		{
			foreach (string propertyName in propList.Where(name => !string.IsNullOrWhiteSpace(name)))
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		/// <summary>Метод для вызова события извещения об изменении всех свойств</summary>
		/// <param name="propList">Список свойств</param>
		public void OnAllPropertyChanged()
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));

		/// <summary>Перегрузка используемая для "сквозного проброса" 
		/// события PropertyChanged</summary>
		/// <param name="sender">Не используется</param>
		/// <param name="e">Параметры от прослушиваемого события</param>
		/// <remarks> Данный метод используется в случае получения свойствами свои значений из
		/// другого экземпляра с INPC.
		/// Пример:</remarks>
		/// <code>
		/// public class ViewModel : OnPropertyChangedClass
		/// {
		///     INotifyPropertyChanged Other;
		///     public int Number
		///     {
		///         get => Other.Number;
		///         set => Other.Number = value;
		///     }
		///
		///     public ViewModel(INotifyPropertyChanged other)
		///     {
		///         Other = other;
		///         Other.PropertyChanged += OnPropertyChanged;
		///     }
		/// }
		/// </code>
		public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
			 => PropertyChanged?.Invoke(this, e);



		#endregion

		#region Виртуальные защищённые методы для изменения значений свойств
		/// <summary>Виртуальный метод определяющий изменения в значении поля значения свойства</summary>
		/// <param name="fieldProperty">Ссылка на поле значения свойства</param>
		/// <param name="newValue">Новое значение</param>
		/// <param name="propertyName">Название свойства</param>
		protected virtual void SetProperty<T>(ref T fieldProperty, T newValue, [CallerMemberName]string propertyName = "")
		{
			if ((fieldProperty != null && !fieldProperty.Equals(newValue)) || (fieldProperty == null && newValue != null))
				PropertyNewValue(ref fieldProperty, newValue, propertyName);
		}

		/// <summary>Виртуальный метод изменяющий значение поля значения свойства</summary>
		/// <param name="fieldProperty">Ссылка на поле значения свойства</param>
		/// <param name="newValue">Новое значение</param>
		/// <param name="propertyName">Название свойства</param>
		protected virtual void PropertyNewValue<T>(ref T fieldProperty, T newValue, string propertyName)
		{
			fieldProperty = newValue;
			OnPropertyChanged(propertyName);
		}
		#endregion
	}
	/// <summary>Базовый класс с реализацией INPC </summary>
	public abstract class BaseINPC : INotifyPropertyChanged
	{
		private readonly Dictionary<string, object> PropertyValues
			= new Dictionary<string, object>();

		#region Событие PropertyChanged
		/// <summary>Событие для извещения об изменения свойства</summary>
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		#region Методы вызова события PropertyChanged
		/// <summary>Метод для вызова события извещения об изменении свойства</summary>
		/// <param name="propertyName">Изменившееся свойство</param>
		public void OnPropertyChanged([CallerMemberName]string propertyName = "")
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		/// <summary>Метод для вызова события извещения об изменении списка свойств</summary>
		/// <param name="propList">Список имён свойств</param>
		public void OnPropertyChanged(IEnumerable<string> propList)
		{
			foreach (string propertyName in propList.Where(name => !string.IsNullOrWhiteSpace(name)))
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		/// <summary>Метод для вызова события извещения об изменении перечня свойств</summary>
		/// <param name="propList">Список имён свойств</param>
		public void OnPropertyChanged(params string[] propList)
		{
			foreach (string propertyName in propList.Where(name => !string.IsNullOrWhiteSpace(name)))
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		/// <summary>Метод для вызова события извещения об изменении всех свойств</summary>
		/// <param name="propList">Список свойств</param>
		public void OnAllPropertyChanged()
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));

		/// <summary>Перегрузка используемая для "сквозного проброса" 
		/// события PropertyChanged</summary>
		/// <param name="sender">Не используется</param>
		/// <param name="e">Параметры от прослушиваемого события</param>
		/// <remarks> Данный метод используется в случае получения свойствами свои значений из
		/// другого экземпляра с INPC.
		/// Пример:
		/// <code>
		/// public class ViewModel : OnPropertyChangedClass
		/// {
		///     INotifyPropertyChanged Other;
		///     public int Number
		///     {
		///         get => Other.Number;
		///         set => Other.Number = value;
		///     }
		///
		///     public ViewModel(INotifyPropertyChanged other)
		///     {
		///         Other = other;
		///         Other.PropertyChanged += OnPropertyChanged;
		///     }
		/// }
		/// </code>
		/// </remarks>
		public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
			 => PropertyChanged?.Invoke(this, e);



		#endregion

		#region Виртуальные защищённые методы для изменения значений свойств
		/// <summary>Виртуальный метод определяющий изменения в значении поля значения свойства</summary>
		/// <param name="newValue">Новое значение</param>
		/// <param name="propertyName">Название свойства</param>
		protected virtual void SetValue<T>(T newValue, [CallerMemberName]string propertyName = "")
		{
			if (string.IsNullOrWhiteSpace(propertyName))
				throw new ArgumentNullException(nameof(propertyName));

			T oldValue;
			if (PropertyValues.TryGetValue(propertyName, out object _oldValue))
				oldValue = (T)_oldValue;
			else
				PropertyValues.Add(propertyName, oldValue = default);

			if ((oldValue != null && !oldValue.Equals(newValue)) || (oldValue == null && newValue != null))
				OnValueChange(oldValue, newValue, propertyName);
		}

		/// <summary>Виртуальный метод изменяющий значение поля значения свойства</summary>
		/// <param name="oldValue">Ссылка на поле значения свойства</param>
		/// <param name="newValue">Новое значение</param>
		/// <param name="propertyName">Название свойства</param>
		protected virtual void OnValueChange<T>(T oldValue, T newValue, string propertyName)
		{
			PropertyValues[propertyName] = newValue;
			OnPropertyChanged(propertyName);
		}

		protected virtual T GetValue<T>(T defaultValue = default, [CallerMemberName]string propertyName = "")
		{
			T value;
			if (PropertyValues.TryGetValue(propertyName, out object _value))
				value = (T)_value;
			else
				PropertyValues.Add(propertyName, value = defaultValue);

			return value;
		}
		#endregion
	}

}
