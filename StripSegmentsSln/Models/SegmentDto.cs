using System;
using System.Collections;

namespace Models
{
    /// <summary>DTO-класс Сегмента Полосы.</summary>
    public class SegmentDto : IEquatable<SegmentDto>
    {
        /// <summary>Идентификатор.</summary>
        public int Id { get; }
        /// <summary>Начало Сегмента.</summary>
        public double Begin { get; }
        /// <summary>Конец Сегмента.</summary>
        public double End { get; }

        /// <summary>Конструктор с заданием Начала и Конца сегмента.</summary>
        /// <param name="begin">Начало Сегмента.</param>
        /// <param name="end">Конец Сегмента.</param>
        /// <remarks>Свойству Begin присваивается меньшее из begin и end,
        /// Свойству End - большее.</remarks>
        public SegmentDto(double begin, double end)
        {
            if (begin > end)
                (begin, end) = (end, begin);
            Begin = begin;
            End = end;
        }

        /// <summary>Конструктор с заданием всех свойств.</summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="begin">Начало Сегмента.</param>
        /// <param name="end">Конец Сегмента.</param>
        public SegmentDto(int id, double begin, double end)
            : this(begin, end)
            => Id = id;

        public static SegmentDto Epmty { get; }
            = new SegmentDto(default, default);

        public override bool Equals(object obj)
        {
            return Equals(obj as SegmentDto);
        }

        public override int GetHashCode()
        {
            int hashCode = 1903003160;
            hashCode = hashCode * -1521134295 + Begin.GetHashCode();
            hashCode = hashCode * -1521134295 + End.GetHashCode();
            return hashCode;
        }

        public bool Equals(SegmentDto other)
        {
            return other != null &&
                   Begin == other.Begin &&
                   End == other.End;
        }
    }

}
