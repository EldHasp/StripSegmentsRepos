using System.Collections.Generic;
using System.Threading.Tasks;

namespace StripSegments
{
    /// <summary>Модель с исходными данными</summary>
    public class StripModel
    {
        /// <summary>Асинхронное получение данных для первой коллекции.</summary>
        /// <param name="range">Диапазон фильтрации.</param>
        /// <returns>Коллекцию элементов прошедших фильтр.</returns>
        public Task<IList<StripSegmentDto>> GetFirstSegmentsAsync(StripSegmentDto range)
         => Task.Factory.StartNew(GetFirstSegments, range);

        /// <summary>Перегрузка для использовании в Task.</summary>
        /// <param name="range">Диапазон фильтрации.
        /// Поступает в типе object, но должен содержать тип StripSegmentDto</param>
        /// <returns>Коллекцию элементов прошедших фильтр.</returns>
        private IList<StripSegmentDto> GetFirstSegments(object range)
            => GetSecondSegments((StripSegmentDto) range);

        /// <summary>Синхронное получение данных для первой коллекции.</summary>
        /// <param name="range">Диапазон фильтрации.</param>
        /// <returns>Коллекцию элементов прошедших фильтр.</returns>
        public IList<StripSegmentDto> GetFirstSegments(StripSegmentDto range)
        {
            // Здесь обработка полученного диапазона и формирование отфильтрованной коллекции.

            // Возврат для примера

            return new StripSegmentDto[]
            {
                new StripSegmentDto(10,20),
                new StripSegmentDto(30,50),
                new StripSegmentDto(60,90)
            };
        }



        /// <summary>Асинхронное получение данных для второй коллекции.</summary>
        /// <param name="range">Диапазон фильтрации.</param>
        /// <returns>Коллекцию элементов прошедших фильтр.</returns>
        public Task< IList<StripSegmentDto>> GetSecondSegmentsAsync(StripSegmentDto range)
            => Task.Factory.StartNew(GetSecondSegments, range);

        /// <summary>Перегрузка для использовании в Task.</summary>
        /// <param name="range">Диапазон фильтрации.
        /// Поступает в типе object, но должен содержать тип StripSegmentDto</param>
        /// <returns>Коллекцию элементов прошедших фильтр.</returns>
        private IList<StripSegmentDto> GetSecondSegments(object range)
            => GetSecondSegments((StripSegmentDto) range);

        /// <summary>Синхронное получение данных для второй коллекции.</summary>
        /// <param name="range">Диапазон фильтрации.</param>
        /// <returns>Коллекцию элементов прошедших фильтр.</returns>
        public IList<StripSegmentDto> GetSecondSegments(StripSegmentDto range)
        {
            // Здесь обработка полученного диапазона и формирование отфильтрованной коллекции.

            // Возврат для примера

            return new StripSegmentDto[]
            {
                new StripSegmentDto(10,20),
                new StripSegmentDto(30,50),
                new StripSegmentDto(60,90)
            };
        }
    }
}
