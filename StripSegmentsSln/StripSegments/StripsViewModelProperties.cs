using Common;
using Models;
using System;
using System.Collections.ObjectModel;

namespace StripSegments
{
    /// <summary>ViewModel - только свойства и реализация команд.</summary>
    public class StripsViewModelProperties : OnPropertyChangedClass
    {
        #region Поля для хранения значений свойств
        private double _step;
        #endregion

        /// <summary>Шаг изменения.</summary>
        public double Step { get => _step; set => SetProperty(ref _step, value); }

        /// <summary>Обший размер.</summary>
        public Segment Size { get; } = new Segment();

        /// <summary>Оторбражаемый диапазон.</summary>
        public Segment Range { get ; } = new Segment();

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
        protected virtual void GetStrips(SegmentDto segmentDto) { throw new NotImplementedException(); }

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
    }

}
