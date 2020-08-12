using System.Collections.Generic;
using System.Linq;

namespace Models
{
    /// <summary>Модель с исходными данными</summary>
    public partial class StripsModel
    {

        /// <summary>Синхронное получение всех Полос с Сегментами из заданного диапазона.</summary>
        /// <param name="range">Диапазон фильтрации Сегментов.</param>
        /// <returns>Неизменяемую коллекцию всех Полос с Сегментами из заданного диапазона.</returns>
        public IReadOnlyCollection<StripDto> GetStrips(SegmentDto range)
        {

            // Запоминание диапазона запроса.
            Range.begin = range.Begin;
            Range.end = range.End;

            // Создание списка полос.
            List<StripDto> list = new List<StripDto>(Strips.Count);
            foreach (StripXml strip in Strips)
                list.Add(new StripDto
                (
                    strip.id,
                    strip.name,
                    // Вызов метода фильтрующего Сегменты.
                    GetSegments(Range, strip.segments),
                    range
                ));

            // Возврат неизменяемой оболочки списка.
            return list.AsReadOnly();
        }

        /// <summary>Статический метод фильтрации Сегментов.</summary>
        /// <param name="range">Диапазон фильтрации.</param>
        /// <param name="segments">Последовательность Сегментов.</param>
        /// <returns></returns>
        public static IReadOnlyCollection<SegmentDto> GetSegments(SegmentXml range, IEnumerable<SegmentXml> segments)
        {
            // Получение Сегментов котрые начинаются до заданного 
            // дииапазона, но заканчиваются внутри него.
            var listMin = segments
                .OrderBy(sg => sg.end)
                .SkipWhile(sg => sg.end <= range.begin)
                .OrderBy(sg => sg.begin)
                .TakeWhile(sg => sg.begin < range.begin);

            // Получение Сегментов начинающихся внутри заданного Диапазона.
            var listMax = segments
                .OrderBy(sg => sg.begin)
                .SkipWhile(sg => sg.begin < range.begin)
                .TakeWhile(sg => sg.begin < range.end);

            // Соединение полученных последовательностей,
            // преобразование в последовательность типа SegmentDto,
            // сортировка по началу Сегмента,
            // получение списка и его неизменяемой оболочки.
            return listMin.Concat(listMax)
                .Select(sg => CopyToDto(sg))
                .OrderBy(sg => sg.Begin)
                .ToList().AsReadOnly();
        }

        // Получение Диапазона последнего запроса.
        public SegmentDto GetRange() => CopyToDto(Range);

        // Получение SegmentDto из SegmentXml
        public static SegmentDto CopyToDto(SegmentXml segment)
            => new SegmentDto(segment.id, segment.begin, segment.end);
    }

}
