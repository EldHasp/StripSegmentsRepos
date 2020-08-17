using Common;
using Models;
using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace StripSegments
{
    /// <summary>ViewModel - только свойства и реализация команд.</summary>
    public class StripsViewModelProperties : OnPropertyChangedClass
    {
        #region Поля для хранения значений свойств
        private double _step;
        private SegmentDto _size = SegmentDto.Epmty;
        private SegmentDto _range = SegmentDto.Epmty;
        private double _mediumValue;
        private double _mediumMinimum;
        private double _mediumMaximum;
        private double _lengthRange;
        #endregion

        /// <summary>Шаг изменения.</summary>
        public double Step { get => _step; set => SetProperty(ref _step, value); }

        /// <summary>Обший размер.</summary>
        public SegmentDto Size { get => _size; set => SetProperty(ref _size, value); }
        /// <summary>Оторбражаемый диапазон.</summary>
        public SegmentDto Range { get => _range; set => SetProperty(ref _range, value); }

        /// <summary>Коллекция полос.</summary>
        public ObservableCollection<Strip> Strips { get; } = new ObservableCollection<Strip>();

        private RelayActionCommand _nextStepCommand;
        /// <summary>Команда на один шаг вперёд.</summary>
        public RelayActionCommand NextStepCommand => _nextStepCommand
            ?? (_nextStepCommand = new RelayActionCommand(NextStepMetod, NextStepCanMetod));

        private bool NextStepCanMetod()
            => Range.End < Size.End;

        private void NextStepMetod()
        {
            GetStrips(new SegmentDto(Range.Begin + Step, Range.End + Step));
        }

        /// <summary>Метод изменения видимого диапазона. 
        /// Должен переопредляться в производном классе.</summary>
        /// <param name="segmentDto">Новый Диапазон.</param>
        protected virtual void GetStrips(SegmentDto segmentDto) { Range = segmentDto; }

        private RelayActionCommand _prevStepCommand;
        /// <summary>Команда на один шаг назад.</summary>
        public RelayActionCommand PrevStepCommand => _prevStepCommand
            ?? (_prevStepCommand = new RelayActionCommand(PrevStepMetod, PrevStepCanMetod));

        private bool PrevStepCanMetod()
            => Range.Begin > Size.Begin;

        private void PrevStepMetod()
        {
            GetStrips(new SegmentDto(Range.Begin - Step, Range.End - Step));
        }

        /// <summary>Загрузка данных.
        /// Должен переопредляться в производном классе.</summary>
        public virtual void Load() { throw new NotImplementedException(); }

        /// <summary>Среднее значение видимого диапазона.<br/>
        /// Свойство для привязки ScrollBar.Value.</summary>
        public double MediumValue { get => _mediumValue; set => SetProperty(ref _mediumValue, value); }
        /// <summary>Минимальное допустимое среднее значение видимого диапазона.<br/>
        /// Свойство для привязки ScrollBar.Minimum.</summary>
        public double MediumMinimum { get => _mediumMinimum; set => SetProperty(ref _mediumMinimum, value); }
        /// <summary>Среднее значение видимого диапазона.<br/>
        /// Свойство для привязки ScrollBar.Maximum.</summary>
        public double MediumMaximum { get => _mediumMaximum; set => SetProperty(ref _mediumMaximum, value); }
        /// <summary>Длина видимого диапазона.<br/>
        /// Свойство для привязки ScrollBar.ViewportSize.</summary>
        public double LengthRange { get => _lengthRange; set => SetProperty(ref _lengthRange, value); }

        protected override void PropertyNewValue<T>(ref T fieldProperty, T newValue, string propertyName)
        {
            base.PropertyNewValue(ref fieldProperty, newValue, propertyName);

            switch (propertyName)
            {
                case nameof(Range):
                    LengthRange = Range.End - Range.Begin;
                    MediumValue = (Range.Begin + Range.End) * 0.5;
                    break;
                case nameof(Size):
                case nameof(LengthRange):
                    MediumMinimum = Size.Begin + LengthRange * 0.5;
                    MediumMaximum = Size.End - LengthRange * 0.5;
                    break;
                case nameof(MediumValue):
                    if ((Math.Abs(MediumValue - (Range.Begin + Range.End) * 0.5) > 0.1 * LengthRange))
                    {
                        double beg = MediumValue - LengthRange * 0.5;

                        beg = Math.Round(beg / (LengthRange * 0.05), MidpointRounding.AwayFromZero);

                        beg *= LengthRange * 0.05;

                        GetStrips(new SegmentDto(beg, beg + LengthRange));
                    }
                    else if (MediumValue <= Size.Begin + 0.55 * LengthRange)
                        GetStrips(new SegmentDto(Size.Begin, Size.Begin + LengthRange));
                    else if (MediumValue >= Size.End - 0.55 * LengthRange)
                        GetStrips(new SegmentDto(Size.End - LengthRange, Size.End));
                    break;
            }
        }
    }

}
