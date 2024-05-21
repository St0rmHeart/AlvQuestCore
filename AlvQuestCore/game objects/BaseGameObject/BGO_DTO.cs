namespace AlvQuestCore
{
    /// <summary>
    /// Базовый Data Transfer Object класс для всех игровых объектов. 
    /// </summary>
    public abstract class BGO_DTO
    {
        /// <summary>
        /// Общие поля всех игровых сущностей 
        /// </summary>
        public BaseData BaseData { get; set; } = new();

        /// <summary>
        /// Вычисляет хэш код в зависимости от содержимых данных.
        /// </summary>
        /// <returns><see cref="int"/> Хэш код конкретного объекта.</returns>
        public override int GetHashCode()
        {
            return BaseData.GetHashCode();
        }

        /// <summary>
        /// Воссоздаёт оригинальную версию объекта.
        /// </summary>
        /// <returns></returns>
        public abstract BaseGameObject RecreateOriginal();
    }
}
