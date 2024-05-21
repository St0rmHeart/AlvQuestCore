namespace AlvQuestCore
{
    /// <summary>
    /// Абстрактный строитесь с общим функционалом всех эффектов.
    /// </summary>
    /// <typeparam name="TBuilder"> Наследующийся строитель </typeparam>
    /// <typeparam name="TProduct"> Создаваемый объект </typeparam>
    /// <typeparam name="TDTO"> DTO версия создаваемого объекта </typeparam>
    public abstract class BaseEffectBuilder<TBuilder, TProduct, TDTO> : BGO_Builder<TBuilder, TProduct, TDTO>
    where TBuilder : BaseEffectBuilder<TBuilder, TProduct, TDTO>
    where TProduct : BaseEffect
    where TDTO : BaseEffectDTO, new()
    {
        public BaseEffectBuilder() : base() { }
    }
}
