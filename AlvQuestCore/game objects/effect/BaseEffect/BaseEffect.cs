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

        /// <summary>
        /// Ссылки на текущую арену и на слоты ее игроков
        /// </summary>
        protected LinksDTO LinksDTO;

        public override void Installation(LinksDTO linksDTO)
        {
            LinksDTO = linksDTO;
            ConcreteInstallation(linksDTO);
        }

        /// <summary>
        /// Конкретная реализация установки связей объекта
        /// </summary>
        /// <param name="linksDTO"></param>
        public abstract void ConcreteInstallation(LinksDTO linksDTO);

        public abstract override BaseEffect Clone();

        public abstract override BaseEffectDTO GetDTO();
    }
}