namespace AlvQuestCore
{
    /// <summary>
    /// Стандартный экиперуемый объект
    /// </summary>
    public abstract class BaseEquippableObject : BaseGameObject
    {
        /// <summary>
        /// Список эффектов, реализующий данный объект
        /// </summary>
        public List<BaseEffect> Effects { get; }

        /// <summary>
        /// Минимальные значения характеристик, которыми должен обладать персонаж для экиперовки объекта
        /// </summary>
        public Dictionary<ECharacteristic, int> RequirementsForUse { get; }

        /// <summary>
        /// Стандартный конструктор экиперуемого объекта
        /// </summary>
        /// <param name="name"> Название объекта </param>
        /// <param name="description"> Описание объекта </param>
        /// <param name="icon"> Иконка объекта</param>
        /// <param name="effects"> Список эффектов, реализующий данный объект </param>
        /// <param name="requirementsForUse"> Минимальные значения характеристик, которыми должен обладать персонаж для экиперовки объекта </param>
        protected BaseEquippableObject(
            string name,
            string description,
            string icon,
            List<BaseEffect> effects,
            Dictionary<ECharacteristic, int> requirementsForUse) : base(name, description, icon)
        {
            Effects = effects;
            RequirementsForUse = requirementsForUse;
        }

        public override void Installation(LinksDTO linksDTO)
        {
            Effects.ForEach(effect => effect.Installation(linksDTO));
        }

        public override void Uninstallation()
        {
            Effects.ForEach(effect => effect.Uninstallation());
        }

        public abstract override BaseEquippableObject Clone();

        public abstract override BEO_DTO GetDTO();
    }
}
