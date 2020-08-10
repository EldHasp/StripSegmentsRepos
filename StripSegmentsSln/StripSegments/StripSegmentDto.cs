using System;

namespace StripSegments
{
    /// <summary>DTO-класс Сегмента Полосы.</summary>
    public class StripSegmentDto : IEquatable<StripSegmentDto>
    {
        /// <summary>Начало Сегмента.</summary>
        public double Begin { get; }
        /// <summary>Конец Сегмента.</summary>
        public double End { get; }

        /// <summary>Конструктор с заданием всех свойств.</summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        public StripSegmentDto(double begin, double end)
        {
            Begin = begin;
            End = end;
        }

        public static StripSegmentDto Epmty { get; }
            = new StripSegmentDto(default, default);

        public override bool Equals(object obj)
        {
            return Equals(obj as StripSegmentDto);
        }

        public override int GetHashCode()
        {
            int hashCode = 1903003160;
            hashCode = hashCode * -1521134295 + Begin.GetHashCode();
            hashCode = hashCode * -1521134295 + End.GetHashCode();
            return hashCode;
        }

        public bool Equals(StripSegmentDto other)
        {
            return other != null &&
                   Begin == other.Begin &&
                   End == other.End;
        }
    }
}
