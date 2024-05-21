namespace AlvQuestCore
{
    public partial class TriggerParameterModifier
    {
        /// <summary>
        /// Класс-строитель объектов класса <see cref="PassiveParameterModifier"/>
        /// </summary>
        public class TPM_Builder : BaseEffectBuilder<TPM_Builder, TriggerParameterModifier, TPM_DTO>
        {
            /// <summary>
            /// Устанавливает триггерный логический модуль
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public TPM_Builder SetTriggerlogicalModule(LogicalModule value)
            {
                _objectData.TriggerLogicalModule_DTO = value.GetDTO();
                return this;
            }

            /// <summary>
            /// Устанавливает тиковый логический модуль
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public TPM_Builder SetTicklogicalModule(LogicalModule value)
            {
                _objectData.TickLogicalModule_DTO = value.GetDTO();
                return this;
            }

            /// <summary>
            /// Устанавливает длительность
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public TPM_Builder SetDuration(int value)
            {
                _objectData.Duration = value;
                return this;
            }

            /// <summary>
            /// Устанавливает максимальное количество сложений
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public TPM_Builder SetMaxStack(int value)
            {
                _objectData.MaxStack = value;
                return this;
            }

            /// <summary>
            /// Добавляет указывающую ссылку в настройки строителя.
            /// </summary>
            /// <param name="target"> цель воздействия </param>
            /// <param name="characteristic"> характеристика воздействия </param>
            /// <param name="derivative"> производная воздействия </param>
            /// <param name="variable"> переменная воздействия </param>
            /// <param name="value"> величина воздействия </param>
            /// <returns></returns>
            public TPM_Builder SetLink(EPlayerType target, ECharacteristic characteristic, EDerivative derivative, EVariable variable, double value)
            {
                if (target == EPlayerType.None || characteristic == ECharacteristic.None || derivative == EDerivative.None || variable == EVariable.None || value == 0)
                {
                    throw new ArgumentException("Ссылка некорректно заполнена");
                }
                if (!AlvQuestStatic.CHAR_DER_PAIRS[characteristic].Contains(derivative))
                {
                    throw new ArgumentException("Невозможная ссылка. У " + nameof(characteristic) + " нет производной " + nameof(derivative) + ".");
                }
                var newLink = (target, characteristic, derivative, variable, value);
                var newDTOLink = AlvQuestStatic.DTOConverter.ToDTOImpactLink(newLink);
                bool dictionaryExists = _objectData.Links.Any(d => d.OrderBy(kvp => kvp.Key).SequenceEqual(newDTOLink.OrderBy(kvp => kvp.Key)));
                if (dictionaryExists) throw new ArgumentException("Указанная ссылка уже существует.");
                _objectData.Links.Add(newDTOLink);
                return this;
            }

            /// <summary>
            /// Устанавливает отслеживаемое событие, которое будет считаться триггером
            /// </summary>
            /// <param name="target"> цель, у которой нужно отслеживать событие </param>
            /// <param name="triggerEvent"> тип отслеживаемого события </param>
            /// <returns></returns>
            /// <exception cref="ArgumentException"></exception>
            public TPM_Builder SetTriggerEvent(EPlayerType target, EEvent triggerEvent)
            {
                if (target == EPlayerType.None || triggerEvent == EEvent.None) throw new ArgumentException("Значение None недопустимо");
                var newLink = (target, triggerEvent);
                var newDTOLink = AlvQuestStatic.DTOConverter.ToDTOEventLink(newLink);
                bool dictionaryExists = _objectData.TriggerEvents.Any(d => d.OrderBy(kvp => kvp.Key).SequenceEqual(newDTOLink.OrderBy(kvp => kvp.Key)));
                if (dictionaryExists) throw new ArgumentException("Указанная ссылка уже существует.");
                _objectData.TriggerEvents.Add(newDTOLink);
                return this;
            }

            /// <summary>
            /// Устанавливает отслеживаемое событие, которое будет считаться тиком
            /// </summary>
            /// <param name="target"> цель, у которой нужно отслеживать событие </param>
            /// <param name="tickEvent"> тип отслеживаемого события </param>
            /// <returns></returns>
            /// <exception cref="ArgumentException"></exception>
            public TPM_Builder SetTickEventt(EPlayerType target, EEvent tickEvent)
            {
                if (target == EPlayerType.None || tickEvent == EEvent.None) throw new ArgumentException("Значение None недопустимо");
                var newLink = (target, tickEvent);
                var newDTOLink = AlvQuestStatic.DTOConverter.ToDTOEventLink(newLink);
                bool dictionaryExists = _objectData.TickEvents.Any(d => d.OrderBy(kvp => kvp.Key).SequenceEqual(newDTOLink.OrderBy(kvp => kvp.Key)));
                if (dictionaryExists) throw new ArgumentException("Указанная ссылка уже существует.");
                _objectData.TickEvents.Add(newDTOLink);
                return this;
            }
            protected override void ResetAdditionalData()
            {
                _objectData.TriggerLogicalModule_DTO = null;
                _objectData.TickLogicalModule_DTO = null;
                _objectData.Duration = 0;
                _objectData.MaxStack = 0;
                _objectData.Links = new();
                _objectData.TickEvents = new();
                _objectData.TriggerEvents = new();
            }
            protected override void ValidateAdditionalData()
            {
                if (_objectData.TriggerLogicalModule_DTO == null) throw new ArgumentException("Не указан логический модуль триггера");
                if (_objectData.TickLogicalModule_DTO == null) throw new ArgumentException("Не указан логический модуль тика");
                if (_objectData.Duration == 0) throw new ArgumentException("Не указана длительность");
                if (_objectData.MaxStack == 0) throw new ArgumentException("Не указан максимальный стак");
                if (_objectData.Links.Count == 0) throw new ArgumentException("Не указана ссылка");
                if (_objectData.TriggerEvents.Count == 0) throw new ArgumentException("Не указан ивент-триггер");
                if (_objectData.TickEvents.Count == 0) throw new ArgumentException("Не указан ивент-тик");
            }
        }
    }
}
