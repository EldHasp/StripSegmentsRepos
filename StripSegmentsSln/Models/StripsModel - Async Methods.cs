using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>Модель с исходными данными</summary>
    public partial class StripsModel
    {

        /// <summary>Асинхронное  получение всех Полос с Сегментами из заданного диапазона.</summary>
        /// <param name="range">Диапазон фильтрации Сегментов.</param>
        /// <returns>Выполняемую задачу результатом которой является неизменяемая коллекция
        /// всех Полос с Сегментами из заданного диапазона.</returns>
        public Task<IReadOnlyCollection<StripDto>> GetStripsAsync(SegmentDto range)
            => Task.Factory.StartNew(GetStrips, range);

        /// <summary>Перегрузка для использовании в Task.</summary>
        /// <param name="range">Дипазон фильтрации Сегментов.</param>
        /// <returns>Неизменяемую коллекцию всех Полос с Сегментами из заданного диапазона.</returns>
        public IReadOnlyCollection<StripDto> GetStrips(object range)
            => GetStrips((SegmentDto)range);

        /// <summary>Асинхронная загрузка данных.</summary>
        /// <returns>Выполняемую задачу с загрузкой данных.</returns>
        public Task LoadAsync() => Task.Run(Load);

    }

}
