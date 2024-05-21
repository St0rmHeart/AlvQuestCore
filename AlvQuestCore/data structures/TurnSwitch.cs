using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlvQuestCore
{
    /// <summary>
    /// Класс-обёртка. Переключатель, указываюищий, должен ли закончиться ход ходящего персонажа.
    /// </summary>
    public class TurnSwitch
    {
        /// <summary>
        /// Переключатель, указываюищий, должен ли закончиться ход ходящего персонажа.
        /// </summary>
        public bool IsTurnEnd { get; set; } = false;
    }
}
