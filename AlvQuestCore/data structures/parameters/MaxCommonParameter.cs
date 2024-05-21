using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlvQuestCore
{
    /// <summary>
    /// Параметр, используемый для оперирования с <see cref="EDerivative.MaxHealth"/> и <see cref="EDerivative.MaxMana"/>.
    /// При уменьшении своего значения, при необходимости уменьшает значение соответсвующего <see cref="CurrentCommonParameter"/>.
    /// </summary>
    public class MaxCommonParameter : CommonParameter
    {
        private readonly CurrentCommonParameter _currentCommonParameter;

        public MaxCommonParameter(
            Dictionary<ECharacteristic, ValueParameter> derivativeValueValues, 
            ECharacteristic characteristic, 
            EDerivative derivative, 
            CurrentCommonParameter currentCommonParameter)
            : base(derivativeValueValues, characteristic, derivative) 
        {
            _currentCommonParameter = currentCommonParameter;
        }
        
        public override void SetFinalValue()
        {
            FinalValue = (((_variables[0] * _variables[1] + _variables[2]) * _variables[3] + _variables[4]) * _variables[5] + _variables[6]).Round();
            if (_currentCommonParameter.CurrentValue > FinalValue)
            {
                _currentCommonParameter.CurrentValue = FinalValue;
            }
        }
    }
}
