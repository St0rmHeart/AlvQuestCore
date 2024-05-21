using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlvQuestCore
{
    /// <summary>
    /// Универсальный класс с полем - двойным словарём - набором производных характеристик - и инструментами взаимодействия
    /// </summary>
    public partial class DerivativesEnumeration
    {
        #region _____________________ПОЛЯ_____________________
        /// <summary>
        /// Структура: Характеристика <see cref="ECharacteristic"/>, её производная <see cref="EDerivative"/>, значение, представляемое <see cref="Parameter"/>
        /// </summary>
        private readonly Dictionary<ECharacteristic, Dictionary<EDerivative, Parameter>> _statList = new Dictionary<ECharacteristic, Dictionary<EDerivative, Parameter>>();
        #endregion

        #region _____________________КОНСТРУКТОР_____________________
        /// <summary>
        /// Конструктор перечисления всех производных всех характеристик
        /// </summary>
        /// <param name="character"></param>
        public DerivativesEnumeration(Character character)
        {
            //словарь для хранения ссылок на все ValueParameter
            var valueParametersValues = new Dictionary<ECharacteristic, ValueParameter >();
            //Создаём все ValueParameter в _statList
            foreach (ECharacteristic characteristic in AlvQuestStatic.CHAR_DER_PAIRS.Keys)
            {
                _statList.Add(characteristic, new Dictionary<EDerivative, Parameter>());
                var newValueParameter = new ValueParameter(character[characteristic]);
                _statList[characteristic].Add(EDerivative.Value, newValueParameter);
                valueParametersValues.Add(characteristic, newValueParameter);
            }

            //Далее создаём в словаре все CurrentParameter, CommonParameter и MaxCommonParameter

            foreach (ECharacteristic characteristic in AlvQuestStatic.CHAR_DER_PAIRS.Keys)
            {
                //для каждой характеристки находим все производные, для котрых нужно создать Parameter;
                List<EDerivative> derList = new List<EDerivative>(AlvQuestStatic.CHAR_DER_PAIRS[characteristic]);
                derList.Remove(EDerivative.Value);
                derList.Remove(EDerivative.MaxMana);
                derList.Remove(EDerivative.MaxHealth);

                foreach (EDerivative derivative in derList)
                {
                    switch (derivative)
                    {
                        case EDerivative.CurrentMana:
                            var currentMana = new CurrentCommonParameter(valueParametersValues, characteristic, EDerivative.CurrentMana);
                            var maxMana = new MaxCommonParameter(valueParametersValues, characteristic, EDerivative.MaxMana, currentMana);
                            _statList[characteristic].Add(EDerivative.MaxMana, maxMana);
                            _statList[characteristic].Add(EDerivative.CurrentMana, currentMana);
                            break;
                        case EDerivative.CurrentHealth:
                            var currentHealth = new CurrentCommonParameter(valueParametersValues, ECharacteristic.Endurance, EDerivative.CurrentHealth);
                            var maxHealth = new MaxCommonParameter(valueParametersValues, ECharacteristic.Endurance, EDerivative.MaxHealth, currentHealth);
                            _statList[characteristic].Add(EDerivative.MaxHealth, maxHealth);
                            _statList[characteristic].Add(EDerivative.CurrentHealth, currentHealth);
                            break;
                        default:
                            _statList[characteristic].Add(derivative, new CommonParameter(valueParametersValues, characteristic, derivative));
                            break;
                    }
                }
            }
        }
        #endregion

        #region ______________________СВОЙСТВА______________________
        /// <summary>
        /// Итератор, дающий доступ к производным указанной характеристики
        /// </summary>
        /// <param name="characteristic">Характеристика, производные которой будут возвращены</param>
        /// <returns>Словарь производных указанной характеристики</returns>
        public Dictionary<EDerivative, Parameter> this[ECharacteristic characteristic]
        {
            get { return _statList[characteristic]; }
        }
        #endregion
    }
}
