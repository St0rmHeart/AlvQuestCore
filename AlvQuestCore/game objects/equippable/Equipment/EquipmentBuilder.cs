namespace AlvQuestCore
{
    public partial class Equipment
    {
        public class EquipmentBuilder : BEO_Builder<EquipmentBuilder, Equipment, EquipmentDTO>
        {
            public EquipmentBuilder() : base() { }

            public EquipmentBuilder SetEBodyPart(EBodyPart bodyPart)
            {
                _objectData.BodyPart = bodyPart;
                return this;
            }
            protected override void ResetAdditionalData()
            {
                base.ResetAdditionalData();
                _objectData.BodyPart = EBodyPart.None;
            }
            protected override void ValidateAdditionalData()
            {
                base.ValidateAdditionalData();
                if (_objectData.BodyPart == EBodyPart.None) throw new ArgumentException("Не задан слот тела.");
            }
        }
    }
}
