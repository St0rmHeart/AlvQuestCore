using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AlvQuestCore
{
    /// <summary>
    /// Абстрактный предок любого параметра, содержащий общие черты потомков.
    /// </summary>
    public abstract class Parameter
    {
        #region _____________________ПОЛЯ_____________________
        //Массив переменных для рассчета FinalValue, переменные массива соотносятся с перменными в рассчетной формуле конечного значения парамтра.
        protected double[] _variables = new double[] { 0, 1, 0, 1, 0, 1, 0 };

        //Итоговое значение параметра
        public double FinalValue { get; protected set; }
        #endregion


        #region _____________________МЕТОДЫ_____________________
        /// <summary>
        /// Произвести перерассчет итогового значения производной
        /// </summary>
        public virtual void SetFinalValue()
        {
            FinalValue = (((_variables[0] * _variables[1] + _variables[2]) * _variables[3] + _variables[4]) * _variables[5] + _variables[6]).Round();
        }

        /// <summary>
        /// Изменить значение одной из переменных, участвующих в рассчете финального значения производной.
        /// </summary>
        /// <param name="variable">Имя переменной.</param>
        /// <param name="value">Значение, на которое производится изменение.</param>
        public virtual void ChangeVariable(EVariable variable, double value)
        {
            if (variable == EVariable.None || variable == EVariable.A0)
            {
                throw new ArgumentOutOfRangeException("Значение " + nameof(variable) + " недопустимо.");
            }

            var index = (int)variable - 1;
            var newValue = _variables[index] + value;
            _variables[index] = newValue.Round();
            SetFinalValue();
        }
        #endregion
    }
}
