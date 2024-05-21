namespace AlvQuestCore
{
    /// <summary>
    /// Абстрактный предок всех строителей игровых объектов, содержащий общий функционал.
    /// </summary>
    /// <typeparam name="TBuilder"> Наследующийся строитель </typeparam>
    /// <typeparam name="TProduct"> Создаваемый объект </typeparam>
    /// <typeparam name="TDTO"> DTO версия создаваемого объекта </typeparam>
    public abstract class BGO_Builder<TBuilder, TProduct, TDTO>
        where TBuilder : BGO_Builder<TBuilder, TProduct, TDTO>
        where TProduct : BaseGameObject
        where TDTO : BGO_DTO, new()
    {
        /// <summary>
        /// DTO версия создаваемого объекта, в которую кешируются все настройки строителя.
        /// </summary>
        protected TDTO _objectData;

        public BGO_Builder()
        {
            ResetData();
        }

        /// <summary>
        /// Пересоздаёт строителя с
        /// </summary>
        /// <returns> Экземлер строителя для реализации fluent интерфейса.</returns>
        public TBuilder Reset()
        {
            ResetData();
            return this as TBuilder;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void ResetData()
        {
            _objectData = new();
            _objectData.BaseData = new();
            ResetAdditionalData();
        }

        /// <summary>
        /// 
        /// </summary>
        protected abstract void ResetAdditionalData();

        /// <summary>
        /// Установка настроек строителя из готового DTO объекта.
        /// </summary>
        /// <param name="dto"> DTO версия объекта </param>
        /// <returns> Экземлер строителя для реализации fluent интерфейса.</returns>
        public TBuilder InstallDTO(TDTO dto)
        {
            _objectData = dto;
            return this as TBuilder;
        }

        /// <summary>
        /// Устанавливает настройку "Имя объекта".
        /// </summary>
        /// <param name="name">Имя объекта</param>
        /// <returns> Экземлер строителя для реализации fluent интерфейса.</returns>
        public TBuilder SetName(string name)
        {
            _objectData.BaseData.Name = name;
            return this as TBuilder;
        }

        /// <summary>
        /// Устанавливает настройку "Описание объекта".
        /// </summary>
        /// <param name="Description"> Описание объекта </param>
        /// <returns> Экземлер строителя для реализации fluent интерфейса.</returns>
        public TBuilder SetDescription(string Description)
        {
            _objectData.BaseData.Description = Description;
            return this as TBuilder;
        }

        /// <summary>
        /// Устанавливает настройку "Иконка объекта".
        /// </summary>
        /// <param name="iconName">Иконка объекта</param>
        /// <returns> Экземлер строителя для реализации fluent интерфейса.</returns>
        public TBuilder SetIcon(string iconName)
        {
            _objectData.BaseData.Icon = iconName;
            return this as TBuilder;
        }

        /// <summary>
        /// Проверяет правильность заполения основных данных
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        protected void ValidateData()
        {
            var baseData = _objectData.BaseData;
            if (baseData.Name == null || baseData.Name == "") throw new ArgumentException("Отсутствует название.");
            if (baseData.Description == null || baseData.Description == "") throw new ArgumentException("Отсутствует описание.");
            if (baseData.Icon == null) throw new ArgumentException("Отсутствует иконка.");
            ValidateAdditionalData();
        }

        /// <summary>
        /// Проверяет правильность заполения дополнительных данных.
        /// </summary>
        protected abstract void ValidateAdditionalData();

        /// <summary>
        /// Строит экземпляр создаваемого объекта по настройкам строителя.
        /// </summary>
        /// <returns> Экземпляр создаваемого объекта</returns>
        public TProduct Build()
        {
            ValidateData();
            return _objectData.RecreateOriginal() as TProduct;
        }

        /// <summary>
        /// Возвращает DTO с версию создаваемого объекта с настройками строителя.
        /// </summary>
        /// <returns>DTO с версия создаваемого объекта</returns>
        public TDTO GetDTO()
        {
            ValidateData();
            return _objectData;
        }
        
    }
    

}
