namespace AlvQuestCore
{
    /// <summary>
    /// Эффект реализующий динамическую вариативную игровую логику изменений <see cref="EVariable"/> в <see cref="Parameter"/>
    /// </summary>
    public partial class TriggerParameterModifier : BaseEffect
    {
        /// <summary>
        /// Логический модуль определяющий реагировать ли при срабатывания событий-триггеров
        /// </summary>
        private readonly LogicalModule _triggerlogicalModule;

        /// <summary>
        /// Логический модуль определяющий реагировать ли при срабатывания событий-тиков
        /// </summary>
        private readonly LogicalModule _ticklogicalModule;

        /// <summary>
        /// Длительность эффекта в тиках
        /// </summary>
        private readonly int _duration;

        /// <summary>
        /// Максимальное накапливаемое количество складываний эффекта
        /// </summary>
        private readonly int _maxStack;

        /// <summary>
        /// Список событий, считающихся триггерами:
        /// <br /><see cref="EPlayerType"/> <c>target</c> - цель, у которой нужно отслеживать событие;
        /// <br /><see cref="EEvent"/> <c>type</c> - тип отслеживаемого события.
        /// </summary>
        private readonly List<(EPlayerType target, EEvent type)> _triggerEvents;

        /// <summary>
        /// Список событий, считающихся тиками:
        /// <br /><see cref="EPlayerType"/> <c>target</c> - цель, у которой нужно отслеживать событие;
        /// <br /><see cref="EEvent"/> <c>type</c> - тип отслеживаемого события.
        /// </summary>
        private readonly List<(EPlayerType target, EEvent type)> _tickEvents;

        /// <summary>
        /// Сылки, указывающие как проводить модификации:
        /// <br /><see cref="EPlayerType"/> <c>target</c> - цель воздействия;
        /// <br /><see cref="ECharacteristic"/> <c>characteristic</c> - характеристика воздействия;
        /// <br /><see cref="EDerivative"/> <c>derivative</c> - производная воздействия;
        /// <br /><see cref="EVariable"/> <c>variable</c> - переменная воздействия;
        /// <br /><see cref="double"/> <c>value</c> - величина воздействия.
        /// </summary>
        private readonly List<(EPlayerType target, ECharacteristic characteristic, EDerivative derivative, EVariable variable, double value)> _links;

        /// <summary>
        /// Внутренний список, куда заносятся параметры, на которые будут оказываться воздействия
        /// </summary>
        private readonly List<Parameter> _parameters = new();

        /// <summary>
        /// Флажок, показывающий активен ли эффект в данный момент.
        /// </summary>
        private bool _isActive;

        /// <summary>
        /// Счетчик оставшихся тиков до окончания действия эффекта.
        /// </summary>
        private int _counterTick;

        /// <summary>
        /// Счетчик текущего количества складываний эффекта.
        /// </summary>
        private int _counterStack;

        /// <summary>
        /// Стандартный конструктор <see cref="TriggerParameterModifier"/>
        /// </summary>
        /// <param name="name"> Имя эффекта </param>
        /// <param name="description"> Описание эффекта </param>
        /// <param name="iconName"> Иконка эффекта </param>
        /// <param name="triggerlogicalModule"> Логический модуль определяющий реагировать ли при срабатывания событий-триггеров </param>
        /// <param name="ticklogicalModule"> Логический модуль определяющий реагировать ли при срабатывания событий-тиков </param>
        /// <param name="duration"> Длительность эффекта в тиках</param>
        /// <param name="maxStack"> Максимальное накапливаемое количество складываний эффекта </param>
        /// <param name="links"> Список указывающих ссылок </param>
        /// <param name="triggerEvents"> Список событий, считающихся триггерами </param>
        /// <param name="tickEvents"> Список событий, считающихся тиками </param>
        private TriggerParameterModifier(
            string name,
            string description,
            string iconName,
            LogicalModule triggerlogicalModule,
            LogicalModule ticklogicalModule,
            int duration,
            int maxStack,
            List<(EPlayerType, ECharacteristic, EDerivative, EVariable, double)> links,
            List<(EPlayerType, EEvent)> triggerEvents,
            List<(EPlayerType, EEvent)> tickEvents) : base(name, description, iconName)
        {
            _triggerlogicalModule = triggerlogicalModule;
            _ticklogicalModule = ticklogicalModule;
            _duration = duration;
            _maxStack = maxStack;
            _links = links;
            _triggerEvents = triggerEvents;
            _tickEvents = tickEvents;
        }

        #region Реализация функционала потомка
        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="enemy"></param>
        /// <param name="triggerEvent"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void SubscribeTrigger(CharacterSlot owner, CharacterSlot enemy, (EPlayerType target, EEvent type) triggerEvent)
        {
            var target = (triggerEvent.target == EPlayerType.Self) ? owner : enemy;
            switch (triggerEvent.type)
            {
                default: throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="enemy"></param>
        /// <param name="tickEvent"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void SubscribeTick(CharacterSlot owner, CharacterSlot enemy, (EPlayerType target, EEvent type) tickEvent)
        {
            var target = (tickEvent.target == EPlayerType.Self) ? owner : enemy;
            switch (tickEvent.type)
            {
                default: throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Activation()
        {
            _isActive = true;
            _counterTick = _duration;
            if (_counterStack < _maxStack)
            {
                for (int i = 0; i < _parameters.Count; i++)
                {
                    if (_parameters[i] is CurrentCommonParameter parameter)
                        parameter.CurrentValue += _links[i].value;
                    else
                        _parameters[i].ChangeVariable(_links[i].variable, _links[i].value);
                }
                _counterStack++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Activation(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Activation(object sender, (EDamageType DamageType, double Value) arg)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Activation(object sender, (EEvent Event, double Args) arg)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Tick()
        {
            if (_counterTick > 1)
            {
                _counterTick--;
            }
            else
            {
                for (int i = 0; i < _parameters.Count; i++)
                {
                    _parameters[i].ChangeVariable(_links[i].variable, -(_links[i].value) * _counterStack);
                }
                _isActive = false;
                _counterStack = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        public void Tick(object sender, EEvent arg)
        {
            _ticklogicalModule.SetData(arg);
            if (_isActive && _ticklogicalModule.Result()) Tick();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        public void Tick(object sender, (EEvent eEvent, EDamageType damageType, double value) arg)
        {
            _ticklogicalModule.SetData(arg);
            if (_isActive && _ticklogicalModule.Result()) Tick();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        public void Tick(object sender, (EEvent eEvent, double args) arg)
        {
            _ticklogicalModule.SetData(arg);
            if (_isActive && _ticklogicalModule.Result()) Tick();
        }
        #endregion

        public override void Installation(LinksDTO linksDTO)
        {
            _isActive = false;

            CharacterSlot owner = linksDTO.PlayerCharacterSlot;
            CharacterSlot enemy = linksDTO.EnemyCharacterSlot;

            for (int i = 0; i < _links.Count; i++)
            {
                var link = _links[i];
                var target = (link.target == EPlayerType.Self) ? owner : enemy;
                var currentParameter = target.Data[link.characteristic][link.derivative];
                _parameters.Add(currentParameter);
            }
            foreach (var triggerEvent in _triggerEvents)
            {
                SubscribeTrigger(owner, enemy, triggerEvent);
            }
            foreach (var tickEvent in _tickEvents)
            {
                SubscribeTick(owner, enemy, tickEvent);
            }
            _triggerlogicalModule.Installation(owner, enemy);
            _ticklogicalModule.Installation(owner, enemy);
        }

        public override void Uninstallation()
        {
            _parameters.Clear();
            _triggerlogicalModule.Uninstallation();
            _ticklogicalModule.Uninstallation();
        }
        

        public override TPM_DTO GetDTO()
        {
            var dto = new TPM_DTO
            {
                BaseData = GetBaseData(),
                TriggerLogicalModule_DTO = _triggerlogicalModule.GetDTO(),
                TickLogicalModule_DTO = _ticklogicalModule.GetDTO(),
                Duration = _duration,
                MaxStack = _maxStack,
                Links = AlvQuestStatic.DTOConverter.ToDTOImpactLinkList(_links),
                TriggerEvents = AlvQuestStatic.DTOConverter.ToDTOEventLinkList(_triggerEvents),
                TickEvents = AlvQuestStatic.DTOConverter.ToDTOEventLinkList(_tickEvents)
            };
            return dto;
        }

        public override TriggerParameterModifier Clone()
        {
            return new TriggerParameterModifier(
                name: Name,
                description: Description,
                iconName: Icon,
                triggerlogicalModule: _triggerlogicalModule.Clone(),
                ticklogicalModule: _ticklogicalModule.Clone(),
                duration: _duration,
                maxStack: _maxStack,
                links: new List<(EPlayerType, ECharacteristic, EDerivative, EVariable, double)>(_links),
                triggerEvents: new List<(EPlayerType, EEvent)>(_triggerEvents),
                tickEvents: new List<(EPlayerType, EEvent)>(_tickEvents));
        }
    }
}
