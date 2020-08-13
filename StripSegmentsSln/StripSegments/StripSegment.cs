﻿using Common;
using Models;
using System;

namespace StripSegments
{
    /// <summary>Класс Сегмента Полосы.</summary>
    public class StripSegment : OnPropertyChangedClass, ICloneable<StripSegment>, IDto<SegmentDto>
    {
        #region Поля, хранящие значения одноимённых свойств
        private double _begin;
        private double _end;
        private double _length;
        private SegmentDto _dto;
        #endregion

        /// <summary>Начало Сегмента.</summary>
        public double Begin { get => _begin; set => SetProperty(ref _begin, value); }
        /// <summary>Конец Сегмента.</summary>
        public double End { get => _end; set => SetProperty(ref _end, value); }
        /// <summary>Длина Сегмента.</summary>
        public double Length { get => _length; set => SetProperty(ref _length, value); }

        protected override void PropertyNewValue<T>(ref T fieldProperty, T newValue, string propertyName)
        {
            base.PropertyNewValue(ref fieldProperty, newValue, propertyName);

            // Пересчёт значений свойств при изменении одного их них.
            switch (propertyName)
            {
                // Если изменяется Начало или Конец, то пересчитывается Длина.
                case nameof(End):
                case nameof(Begin):
                    Length = End - Begin;
                    break;

                // Если изменяется длина, то пересчитывается Конец.
                case nameof(Length):
                    End = Begin + Length;
                    break;

                default:
                    break;
            }
        }

        #region Реализация интерфейса ICloneable<StripSegment>
        public StripSegment Clone() => (StripSegment)((ICloneable)this).Clone();
        object ICloneable.Clone() => MemberwiseClone();
        #endregion

        #region Реализация интерфейса IDto<StripSegmentDto>
        public SegmentDto Dto { get => _dto; private set => SetProperty(ref _dto, value); }
        public void SetDto(SegmentDto newDto) => CopyFrom(Dto = newDto);

        #region Реализация интерфейса ICopy<StripSegmentDto>
        public SegmentDto Copy() => new SegmentDto(Begin, End);

        public void CopyFrom(SegmentDto dto) => (Begin, End) = (dto.Begin, dto.End);

        public void CopyTo(SegmentDto obj)
        {
            throw new NotImplementedException();
        }

        #endregion
        #endregion
    }
}
