using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlvQuestCore
{
    /// <summary>
    /// Параметр, используемый для оперирования с <see cref="EDerivative.CurrentHealth"/> и <see cref="EDerivative.CurrentMana"/>
    /// </summary>
    public class CurrentCommonParameter : CommonParameter
    {
        /// <param name="derivativeValueValues">Ссылки на все <see cref="ValueParameter"/> персонажа.</param>
        /// <param name="characteristic">Характеристика <see cref="ECharacteristic"/>, к корторой относится данный <see cref="CommonParameter"/></param>
        /// <param name="derivative">Производная <see cref="EDerivative"/>, к корторой относится данный <see cref="CommonParameter"/></param>
        public CurrentCommonParameter(
            Dictionary<ECharacteristic, ValueParameter> derivativeValueValues,
            ECharacteristic characteristic,
            EDerivative derivative) : base (derivativeValueValues, characteristic, derivative) 
        { }

        #region ______________________СВОЙСТВА______________________
        //Геттер и сеттер для "Current" производых
        public double CurrentValue { get { return FinalValue; } set { FinalValue = value; } }
        #endregion
    }
}
