using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Models
{
    /// <summary>DTO-класс Полосы.</summary>
    public class StripDto
    {
        /// <summary>Идентификатор.</summary>
        public int Id { get; }

        /// <summary>Имя Полосы.</summary>
        public string Name { get; }

        /// <summary>Сегменты Полосы.</summary>
        public IReadOnlyList<SegmentDto> Segments { get; }

        /// <summary>Диапазон полосы.</summary>
        public SegmentDto Range { get; }

        public StripDto(int id, string name, IEnumerable<SegmentDto> segments, SegmentDto range)
            : this(name, segments, range)
            => Id = id;

        public StripDto(string name, IEnumerable<SegmentDto> segments, SegmentDto range)
        {
            Name = name;
            Segments = segments.ToList().AsReadOnly();
            Range = range;
        }
    }

}
