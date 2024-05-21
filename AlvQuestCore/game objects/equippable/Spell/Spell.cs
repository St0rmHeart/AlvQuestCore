using System.Xml.Linq;

namespace AlvQuestCore
{
    /// <summary>
    /// Заклинания, которыми пользуются персонажи 
    /// </summary>
    public partial class Spell : BaseEquippableObject
    {
        /// <summary>
        /// Количество маны, тратящееся за использование заклинания
        /// </summary>
        public Dictionary<EManaType, double> ManaCost {  get; private set; }

        /// <summary>
        /// Стандартный конструктор заклинаний 
        /// </summary>
        /// <param name="name"> Название заклинания </param>
        /// <param name="description"> Описание заклинания </param>
        /// <param name="icon"> Иконка заклинания </param>
        /// <param name="effects"> Список эффектов, реализующий данный объект </param>
        /// <param name="requirementsForUse"> Минимальные значения характеристик, которыми должен обладать персонаж для экиперовки объекта </param>
        /// <param name="manaCost"> Количество маны, тратящееся за использование заклинания </param>
        public Spell(
            string name,
            string description,
            string icon, List<BaseEffect> effects,
            Dictionary<ECharacteristic, int> requirementsForUse,
            Dictionary<EManaType, double> manaCost)
            : base(name, description, icon, effects, requirementsForUse)
        {
            ManaCost = manaCost;
        }

        public override Spell Clone()
        {
            return new Spell(
                name: Name,
                description: Description,
                icon: Icon,
                effects: Effects.Select(effect => effect.Clone()).ToList(),
                requirementsForUse: new Dictionary<ECharacteristic, int>(RequirementsForUse),
                manaCost: new Dictionary<EManaType, double>(ManaCost));
        }

        public override SpellDTO GetDTO()
        {
            var dto = new SpellDTO
            {
                BaseData = GetBaseData(),
                Effects = Effects.Select(effect => effect.GetDTO()).ToList(),
                RequirementsForUse = new Dictionary<ECharacteristic, int>(RequirementsForUse),
            };
            return dto;
        }
    }
}