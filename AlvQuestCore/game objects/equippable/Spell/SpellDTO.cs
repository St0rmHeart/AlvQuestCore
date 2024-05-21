namespace AlvQuestCore
{
    public partial class Spell
    {
        /// <summary>
        /// DTO версия <see cref='Spell'/>.
        /// </summary>
        public class SpellDTO : BEO_DTO
        {
            /// <summary>
            /// 
            /// </summary>
            public Dictionary<EManaType, double> ManaCost { get; set; }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override Spell RecreateOriginal()
            {
                return new Spell(
                name: BaseData.Name,
                description: BaseData.Description,
                icon: BaseData.Icon,
                effects: new List<BaseEffect>(Effects.Select(effect => effect.RecreateOriginal()).ToList()),
                requirementsForUse: new Dictionary<ECharacteristic, int>(RequirementsForUse),
                manaCost: new Dictionary<EManaType, double>(ManaCost));
            }
        }
    }
}
