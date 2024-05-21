namespace AlvQuestCore
{
    public partial class Perk
    {
        /// <summary>
        /// DTO версия <see cref='Perk'/>.
        /// </summary>
        public class PerkDTO : BEO_DTO
        {
            public override Perk RecreateOriginal()
            {
                return new Perk(
                name: BaseData.Name,
                description: BaseData.Description,
                icon: BaseData.Icon,
                effects: new List<BaseEffect>(Effects.Select(effect => effect.RecreateOriginal()).ToList()),
                requirementsForUse: new Dictionary<ECharacteristic, int>(RequirementsForUse));
            }
        }
    }
}
