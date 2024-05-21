namespace AlvQuestCore
{
    /// <summary>
    /// DTO версия класса <see cref="BaseEquippableObject"/>
    /// </summary>
    public abstract class BEO_DTO : BGO_DTO
    {
        /// <summary>
        /// Список эффектов, реализующий данный объект
        /// </summary>
        public List<BaseEffectDTO> Effects { get; set; }

        /// <summary>
        /// Минимальные значения характеристик, которыми должен обладать персонаж для экиперовки объекта
        /// </summary>
        public Dictionary<ECharacteristic, int> RequirementsForUse { get; set; }

        public abstract override BaseEquippableObject RecreateOriginal();
    }
}
