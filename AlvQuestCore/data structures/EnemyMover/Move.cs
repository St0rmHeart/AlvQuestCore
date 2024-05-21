using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlvQuestCore
{
    public class Move
    {
        public int X { get; set; }
        public int Y { get; set; }
        public EMoveDirection MoveDirection { get; set; }
        public int Score { get; set; }
        public Move(int x, int y, EMoveDirection moveDirection)
        {
            X = x;
            Y = y;
            MoveDirection = moveDirection;
        }
    }
}
