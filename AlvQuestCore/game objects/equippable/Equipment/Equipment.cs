namespace AlvQuestCore
{
    /// <summary>
    /// Предмет снаряжения, который можно одеть в соответсвующий ему слот
    /// </summary>
    public partial class Equipment : BaseEquippableObject
    { 
        /// <summary>
        /// Слот к которому относится данный объект снаряжения 
        /// </summary>
        public EBodyPart BodyPart { get; }

        /// <summary>
        /// Стандартный конструктор снаряжения
        /// </summary>
        /// <param name="name"> Название </param>
        /// <param name="description"> Описание </param>
        /// <param name="icon"> Иконка </param>
        /// <param name="effects"> Список эффектов, реализующий данный объект </param>
        /// <param name="requirementsForUse"> Минимальные значения характеристик, которыми должен обладать персонаж для экиперовки объекта </param>
        /// <param name="bodyPart"> Слот к которому относится данный объект снаряжения </param>
        private Equipment(
            string name,
            string description,
            string icon,
            List<BaseEffect> effects,
            Dictionary<ECharacteristic, int> requirementsForUse,
            EBodyPart bodyPart) : base(name, description, icon, effects, requirementsForUse)
        {
            BodyPart = bodyPart;
        }

        public override Equipment Clone()
        {
            return new Equipment(
                name: Name,
                description: Description,
                icon: Icon,
                effects: Effects.Select(effect => effect.Clone()).ToList(),
                requirementsForUse: new Dictionary<ECharacteristic, int>(RequirementsForUse),
                bodyPart: BodyPart);
        }

        public override EquipmentDTO GetDTO()
        {
            var dto = new EquipmentDTO
            {
                BaseData = GetBaseData(),
                Effects = Effects.Select(effect => effect.GetDTO()).ToList(),
                RequirementsForUse = new Dictionary<ECharacteristic, int>(RequirementsForUse),
                BodyPart = BodyPart
            };
            return dto;
        }
    }
}
