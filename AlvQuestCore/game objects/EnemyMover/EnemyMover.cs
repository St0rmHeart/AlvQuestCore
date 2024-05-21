using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlvQuestCore
{
    public class EnemyMover
    {
        #region Поля

        private List<ECharacteristic> _priority = [];

        private Arena _arena { get; }
        
        private StoneBoard _stoneBoard { get; }
        #endregion

        #region Свойства



        #endregion

        #region Методы
        public EnemyMover(Arena arena, StoneBoard stoneBoard)
        {
            _arena = arena;
            _stoneBoard = stoneBoard;
        }
        public Move FindBestMove()
        {
            FormPriority();
            var moveList = _stoneBoard.WriteCombinationCreationPossibility();
            foreach (var move in moveList)
            {
                move.Score = CalculateMoveScore(move);
            }
            return moveList.MaxBy(move => move.Score);
            //_stoneBoard.MakeMove(moveList.MaxBy(move => move.Score));
        }

        private void FormPriority()
        {
            var statsList = new List<(ECharacteristic characteristic, double value)>
            {
                (ECharacteristic.Strength, _arena._enemy.Data[ECharacteristic.Strength][EDerivative.Value].FinalValue),
                (ECharacteristic.Dexterity, _arena._enemy.Data[ECharacteristic.Strength][EDerivative.Value].FinalValue),
                (ECharacteristic.Endurance, _arena._enemy.Data[ECharacteristic.Strength][EDerivative.Value].FinalValue),
                (ECharacteristic.Water, _arena._enemy.Data[ECharacteristic.Strength][EDerivative.Value].FinalValue),
                (ECharacteristic.Fire, _arena._enemy.Data[ECharacteristic.Strength][EDerivative.Value].FinalValue),
                (ECharacteristic.Air, _arena._enemy.Data[ECharacteristic.Strength][EDerivative.Value].FinalValue),
                (ECharacteristic.Earth, _arena._enemy.Data[ECharacteristic.Strength][EDerivative.Value].FinalValue)
            };
            var sorterdStatsList = statsList.OrderBy(s => s.value);
            foreach (var stat in sorterdStatsList)
            {
                _priority.Add(stat.characteristic);
            }
        }
        private int CalculateMoveScore(Move move)
        {
            int score = 0;
            _stoneBoard.TryFindStoneCombinations();
            var combinationsBeforeMove = _stoneBoard.StoneGridData.OnFieldCombinations;

            _stoneBoard.ExecuteFakeStoneSwappingTurn(move);
            var moveResult = _stoneBoard.StoneGridData.OnFieldCombinations;
            foreach (var combination in moveResult)
            {
                score += 15 * (int)(_priority.IndexOf((ECharacteristic)combination.StoneType)*((combination.Length-2)*1.5));
            }
            var combinationsAfterMove = _stoneBoard.StoneGridData.OnFieldCombinations;

            List<(EStoneType StoneType, int Length)> newCombinations = [];
            foreach (var combination in combinationsAfterMove)
            {
                if (!combinationsBeforeMove.Contains(combination))
                {
                    newCombinations.Add(combination);
                    score += 7 * (int)(_priority.IndexOf((ECharacteristic)combination.StoneType) * ((combination.Length - 2) * 1.5));
                }
            }

            List<(EStoneType StoneType, int Length)> lostCombinations = [];
            foreach (var combination in combinationsBeforeMove)
            {
                if (!combinationsAfterMove.Contains(combination))
                {
                    lostCombinations.Add(combination);
                    score -= 7 * (int)(_priority.IndexOf((ECharacteristic)combination.StoneType) * ((combination.Length - 2) * 1.5));
                }
            }

            _stoneBoard.StoneGridData.ResetData();
            return score;
        }
        #endregion
    }
}
