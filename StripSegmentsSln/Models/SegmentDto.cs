namespace Models
{
    /// <summary>DTO-класс Сегмента Полосы.</summary>
    public class SegmentDto
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
            if (begin < end)
                (Begin, End) = (begin, end);
            else
                (Begin, End) = (end, begin);
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

    }

}
