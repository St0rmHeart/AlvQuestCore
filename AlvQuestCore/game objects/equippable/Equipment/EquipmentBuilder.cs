namespace AlvQuestCore
{
    public partial class Equipment
    {
        /// <summary>
        /// Класс-строитель предметов снаряжения
        /// </summary>
        public class EquipmentBuilder : BEO_Builder<EquipmentBuilder, Equipment, EquipmentDTO>
        {
            public EquipmentBuilder() : base() { }
            /// <summary>
            /// Устанавливает часть тела, на которую одевается снаряжение
            /// </summary>
            /// <param name="bodyPart"> Часть Тела</param>
            /// <returns>Экземпляр строителя для реализации fluent интерфейса</returns>
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
