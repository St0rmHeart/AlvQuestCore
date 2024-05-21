namespace AlvQuestCore
{
    /// <summary>
    /// Перк, определяющий какую-то особенность персонажа
    /// </summary>
    public partial class Perk : BaseEquippableObject
    {
        /// <summary>
        /// Стандартный конструктор перков 
        /// </summary>
        /// <param name="name"> Название перка </param>
        /// <param name="description"> Описание перка </param>
        /// <param name="icon"> Иконка перка </param>
        /// <param name="effects"> Список эффектов, реализующий данный объект </param>
        /// <param name="requirementsForUse"> Минимальные значения характеристик, которыми должен обладать персонаж для экиперовки объекта </param>
        private Perk(
            string name,
            string description,
            string icon, List<BaseEffect> effects,
            Dictionary<ECharacteristic, int> requirementsForUse)
            : base(name, description, icon, effects, requirementsForUse)
        {

        }

        public override Perk Clone()
        {
            return new Perk(
                name: Name,
                description: Description,
                icon: Icon,
                effects: Effects.Select(effect => effect.Clone()).ToList(),
                requirementsForUse: new Dictionary<ECharacteristic, int>(RequirementsForUse));
        }

        public override PerkDTO GetDTO()
        {
            var dto = new PerkDTO
            {
                BaseData = GetBaseData(),
                Effects = Effects.Select(effect => effect.GetDTO()).ToList(),
                RequirementsForUse = new Dictionary<ECharacteristic, int>(RequirementsForUse),
            };
            return dto;
        }
    }
}
