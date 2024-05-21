namespace AlvQuestCore
{
    /// <summary>
    /// Абстрактный строитесь с общим функционалом всех экиперуемых объектов.
    /// </summary>
    /// <typeparam name="TBuilder"> Наследующийся строитель </typeparam>
    /// <typeparam name="TProduct"> Создаваемый объект </typeparam>
    /// <typeparam name="TDTO"> DTO версия создаваемого объекта </typeparam>
    public abstract class BEO_Builder<TBuilder, TProduct, TDTO> : BGO_Builder<TBuilder, TProduct, TDTO>
     where TBuilder : BEO_Builder<TBuilder, TProduct, TDTO>, new()
     where TProduct : BaseEquippableObject
     where TDTO : BEO_DTO, new()
    {
        public BEO_Builder() : base() { }

        public TBuilder SetEffect(BaseEffect effect)
        {
            var effectData = effect.GetDTO();
            _objectData.Effects.Add(effectData);
            return this as TBuilder;
        }
        public TBuilder SetRequirement(ECharacteristic characteristic, int requirement)
        {
            _objectData.RequirementsForUse[characteristic] = requirement;
            return this as TBuilder;
        }
        protected override void ResetAdditionalData()
        {
            _objectData.Effects = new();
            _objectData.RequirementsForUse = new();
        }
        protected override void ValidateAdditionalData()
        {
            if (_objectData.Effects.Count == 0) throw new ArgumentException("Не задан ниодин эффект");
        }
    }
}
