namespace AlvQuestCore
{
    /// <summary>
    /// Базовый класс для всех эффектов
    /// </summary>
    public abstract class BaseEffect : BaseGameObject
    {
        /// <summary>
        /// Стандартный конструктор эффекта
        /// </summary>
        /// <param name="name"> Название эффекта </param>
        /// <param name="description"> Описание эффекта </param>
        /// <param name="iconName"> Иконка эффекта </param>
        protected BaseEffect(string name, string description, string iconName) : base(name, description, iconName) { }

        public abstract override BaseEffect Clone();

        public abstract override BaseEffectDTO GetDTO();
    }
}