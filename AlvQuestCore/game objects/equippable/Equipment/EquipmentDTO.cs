namespace AlvQuestCore
{
    public partial class Equipment
    {
        /// <summary>
        /// DTO версия класса <see cref="Equipment"/>
        /// </summary>
        public class EquipmentDTO : BEO_DTO
        {
            /// <summary>
            /// Слот к которому относится данный объект снаряжения 
            /// </summary>
            public EBodyPart BodyPart { get; set; }

            public override Equipment RecreateOriginal()
            {
                return new Equipment(
                        name: BaseData.Name,
                        description: BaseData.Description,
                        icon: BaseData.Icon,
                        effects: new List<BaseEffect>(Effects.Select(effect => effect.RecreateOriginal()).ToList()),
                        requirementsForUse: new Dictionary<ECharacteristic, int>(RequirementsForUse),
                        bodyPart: BodyPart);
            }
        }
    }
}
