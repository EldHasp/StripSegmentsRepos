namespace Common
{
    /// <summary>Интерфейс связи с DTO контейнером.</summary>
    /// <typeparam name="T">DTO тип.</typeparam>
    public interface IDto<T> : ICopy<T>
    {
        /// <summary>Ссылка на DTO контейнер.</summary>
        T Dto { get; }

        /// <summary>Установка в свойство Dto нового экземпляра Dto.</summary>
        /// <param name="newDto">Новый экземпляр Dto.</param>
        void SetDto(T newDto);
    }
}
