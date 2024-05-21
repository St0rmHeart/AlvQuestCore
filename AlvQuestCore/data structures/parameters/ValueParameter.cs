using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlvQuestCore
{
    /// <summary>
    /// Параметр, используемый для оперирования только с <see cref="EDerivative.Value"/> у каждой из <see cref="ECharacteristic"/>.
    /// Имеет возможность подписывать на событие своего изменения <see cref="CommonParameter"/>
    /// </summary>
    public class ValueParameter : Parameter
    {
        #region _____________________EVENT_____________________
        /// <summary>
        /// Событие изменеия значения поля <see cref="Parameter.FinalValue"/> у <see cref="ValueParameter"/>.
        /// </summary>
        public event EventHandler ValueDerivativeUpdate;
        #endregion

        #region ______________________КОНСТРУКТОР______________________
        /// <summary>
        /// Конструктор <see cref="ValueParameter"/>, просто присваивающий в <see cref="EVariable.A0"/> значение какой-либо характеристики <see cref="Character"/>.
        /// </summary>
        /// <param name="baseValue">Значение характеристики.</param>
        public ValueParameter(double baseValue)
        {
            _variables[0] = baseValue;
        }
        #endregion

        #region _____________________МЕТОДЫ_____________________
        /// <summary>
        /// Реализует изменение любой из переменных для рассчета <see cref="Parameter.FinalValue"/> с изменением всех зависимых <see cref="CommonParameter"/>.
        /// </summary>
        /// <param name="variable">Имя переменой, которую нужно изменить.</param>
        /// <param name="value">Величина изменения.</param>
        /// <exception cref="ArgumentOutOfRangeException">В случае если переданы невозможные значения <see cref="EVariable.None"/> или <see cref="EVariable.A0"/></exception>
        public override void ChangeVariable(EVariable variable, double value)
        {
            if (variable == EVariable.None || variable == EVariable.A0)
            {
                throw new ArgumentOutOfRangeException("Значение " + nameof(variable) + " недопустимо.");
            }
            var index = (int)variable - 1;
            var newValue = _variables[index] + value;
            _variables[index] = newValue.Round();
            SetFinalValue();
            ValueDerivativeUpdate?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}