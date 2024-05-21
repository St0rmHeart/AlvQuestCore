namespace AlvQuestCore
{
    public partial class PassiveParameterModifier
    {
        /// <summary>
        /// Класс-строитель объектов класса <see cref="PassiveParameterModifier"/>
        /// </summary>
        public class PPM_Builder : BaseEffectBuilder<PPM_Builder, PassiveParameterModifier, PPM_DTO>
        {
            /// <summary>
            /// Добавить указывающую ссылку в настройки строителя.
            /// </summary>
            /// <param name="target"> цель воздействия </param>
            /// <param name="characteristic"> характеристика воздействия </param>
            /// <param name="derivative"> производная воздействия </param>
            /// <param name="variable"> переменная воздействия </param>
            /// <param name="value"> величина воздействия </param>
            /// <returns></returns>
            public PPM_Builder SetLink(EPlayerType target, ECharacteristic characteristic, EDerivative derivative, EVariable variable, double value)
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

            protected override void ResetAdditionalData()
            {
                _objectData.Links = new();
            }

            protected override void ValidateAdditionalData()
            {
                if (_objectData.Links.Count == 0) throw new ArgumentException("Отсутствует ссылка.");
            }
        }
    }
}
