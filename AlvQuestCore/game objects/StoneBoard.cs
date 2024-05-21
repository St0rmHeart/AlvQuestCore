using System.Drawing;

namespace AlvQuestCore
{
    public class StoneGridData
    {
        /// <summary>
        /// Список всех комбинаций на сетке камней <see cref='StoneBoard.StoneGrid'/>, где:
        /// <para><see cref='EStoneType'/> - тип камней комбинации,</para>
        /// <para><see cref='int'/> - длина комбинации.</para>
        /// </summary>
        public List<(EStoneType StoneType, int Length)> OnFieldCombinations { get; } = new();

        /// <summary>
        /// Координаты каждого камня комбинаций.
        /// </summary>
        public Dictionary<(int X, int Y), EStoneType> OnFieldCombinationsStones { get; } = new();

        public Dictionary<EStoneType, int> AmountOfCombinedStones = new()
        {
            { EStoneType.Gold, 0 },
            { EStoneType.Experience, 0 },
            { EStoneType.FireStone, 0 },
            { EStoneType.WaterStone, 0 },
            { EStoneType.EarthStone, 0 },
            { EStoneType.AirStone, 0 },
            { EStoneType.Skull, 0 },
        };

        /// <summary>
        /// Сбрасывает все данные по комбинациям в <see cref='StoneGridData'/>.
        /// </summary>
        public void ResetData()
        {
            OnFieldCombinations.Clear();
            OnFieldCombinationsStones.Clear();
            foreach (var stoneType in AmountOfCombinedStones.Keys)
            {
                AmountOfCombinedStones[stoneType] = 0;
            }
        }
    }





    /// <summary>
    /// Доска камней.
    /// </summary>
    public class StoneBoard  
    {

        #region Поля

        /// <summary>
        /// Генератор случайных чисел.
        /// </summary>
        private readonly Random _random = new();

        /// <summary>
        /// Арена, на которой расположена доска камней.
        /// </summary>
        private readonly Arena _arena;

        /// <summary>
        /// Координаты первого выбранного камня
        /// </summary>
        private Point _firstPosition;

        /// <summary>
        /// Кооринаты второго выбранного камня
        /// </summary>
        private Point _secondPosition;

        #endregion

        #region Свойства

        /// <summary>
        /// Статические данные по текущему состояния сетки камней.
        /// </summary>
        public StoneGridData StoneGridData { get; set; }

        /// <summary>
        /// Сетка на которой расположены камни из набора <see cref='EStoneType'/>.
        /// </summary>
        public EStoneType[,] StoneGrid { get; } = new EStoneType[AlvQuestStatic.STONE_GRID_SIZE, AlvQuestStatic.STONE_GRID_SIZE];

        #endregion

        #region Методы
        /// <summary>
        /// Конструктор доски камней
        /// </summary>
        /// <param name="arena"></param>
        public StoneBoard(Arena arena)
        {
            _arena = arena;
        }



        /// <summary>
        /// Установка нового стартового состояния секи камней из набора <see cref='EStoneType'/>.
        /// </summary>
        public void ResetStoneGrid()
        {
            do
            {
                // Генерируем поле камней
                for (int i = 0; i < AlvQuestStatic.STONE_GRID_SIZE; i++)
                {
                    for (int j = 0; j < AlvQuestStatic.STONE_GRID_SIZE; j++)
                    {
                        StoneGrid[i, j] = GetNewStartStone(i, j);
                    }
                }
                // Если поле сгенерировалось так, что на нём нет комбинаций - генерируем заново.
            } while (!CheckCombinationCreationPossibility());
        }

        /// <summary>
        /// Возвращает новый камень для очередной клетки во время работы <see cref='ResetStoneGrid()'/>.
        /// </summary>n
        /// <param name="x">X координата камня в сетке <see cref='StoneGrid'/>.</param>
        /// <param name="y">Y координата камня в сетке <see cref='StoneGrid'/>.</param>
        /// <returns>Тип очередного камня из набора <see cref='EStoneType'/>,
        /// гарантирующий отсутвие комбинаций на стартовом состоянии <see cref='StoneGrid'/>.</returns>
        private EStoneType GetNewStartStone(int x, int y)
        {
            var newStone  = GetRandomStone();
            while ((y > 1 && newStone == StoneGrid[x, y - 1] && StoneGrid[x, y] == StoneGrid[x, y - 2]) ||
                   (x > 1 && newStone == StoneGrid[x - 1, y] && StoneGrid[x, y] == StoneGrid[x - 2, y]))
            {
                newStone = GetRandomStone();
            }
            return newStone;
        }

        /// <summary>
        /// Возвращает случайный камень из набора <see cref='EStoneType'/>.
        /// </summary>
        /// <returns>Тип камня.</returns>
        private EStoneType GetRandomStone()
        {
            return (EStoneType)_random.Next(1, 8);
        }

        /// <summary>
        /// Точка входа в подсистему доски камней. Реализует клик игрока по камню.
        /// </summary>
        /// <param name="x">X координата выбранного камня в сетке <see cref='StoneGrid'/>.</param>
        /// <param name="y">Y координата выбранного камня в сетке <see cref='StoneGrid'/>.</param>
        public void StoneClick(int x, int y)
        {
            // Координаты новой выбранной точки
            var newPoint = new Point(x, y);
            // Если первая точка не была выбрана.
            if (_firstPosition.X == -1)
            {
                // Выбранная точка становится первой
                _firstPosition = newPoint;
            } // Если новая точка отлична от первой 
            else if (newPoint != _firstPosition)
            {
                // Вычисляем дистанцию между камнями
                var distance =
                    Math.Sqrt(Math.Pow(_firstPosition.X - newPoint.X, 2) + Math.Pow(_firstPosition.Y - newPoint.Y, 2));
                //Если камни соседние 
                if (distance == 1)
                {
                    // Выбранная точка становится второй и начинается процесс реагирования доски камней.
                    _secondPosition = newPoint;
                    SwapStones(_firstPosition.X, _firstPosition.Y, _secondPosition.X, _secondPosition.Y, StoneGrid);
                    ResetPoints();
                    ExecuteStoneSwappingTurn();
                }
                else
                {
                    //Выбранная точка становится новой первой
                    _firstPosition = newPoint;
                }
            }
        }

        /// <summary>
        /// Обмен двух соседних камней местами.
        /// </summary>
        /// <param name="x1">X координата первого камня.</param>
        /// <param name="y1">Y координата первого камня.</param>
        /// <param name="x2">X координата второго камня.</param>
        /// <param name="y2">Y координата второго камня.</param>
        /// <param name="stoneGrid">Сетка, на которой расположены камни</param>
        private static void SwapStones(int x1, int y1, int x2, int y2, EStoneType[,] stoneGrid)
        {
            (stoneGrid[x1, y1], stoneGrid[x2, y2]) = (stoneGrid[x2, y2], stoneGrid[x1, y1]);
        }

        /// <summary>
        /// Сброс запомненных координат выбранных камней.
        /// </summary>
        private void ResetPoints()
        {
            _firstPosition = new Point(-1, -1);
            _secondPosition = new Point(-1, -1);
        }

        /// <summary>
        /// Реализация хода активного игрока, когда он меняет местами два соседних камня.
        /// </summary>
        private void ExecuteStoneSwappingTurn()
        {
            //Вызываем событие о совершении хода путём обмена двух соседних камней активным игроком
            _arena.ActivePlayer.SendStoneSwappingTurnNotification();
            // Пока на доске существует хотя бы одна комбинация:
            while (TryFindStoneCombinations())
            {
                CheckForTurnContinuation();
                _arena.ActivePlayer.AbsorbDestroyedStones(StoneGridData.AmountOfCombinedStones);
                DestroyCombinedStones();
                StonesFreeFall();
            }
            if (_arena.TurnSwitchModule.IsTurnEnd)
            {
                _arena.CompleteTurn();
            }
            if (!CheckCombinationCreationPossibility())
            {
                ResetStoneGrid();
            }
        }

        /// <summary>
        /// Пробует записать в <see cref='StoneGridData'/> все комбинации камней и координаты составляющих их камней в сетке <see cref='StoneGrid'/>.
        /// </summary>
        /// <returns><see cref="bool"/> <c>true</c>, если была записана хотя бы одна комбинация, иначе <see cref="bool"/> <c>false</c>.</returns>
        public bool TryFindStoneCombinations()
        {
            StoneGridData.ResetData();
            bool IsAnyMatches = false;
            for (int i = 0; i < AlvQuestStatic.STONE_GRID_SIZE; i++)
            {
                for (int j = 0; j < AlvQuestStatic.STONE_GRID_SIZE; j++)
                {
                    if (StoneGridData.OnFieldCombinationsStones.ContainsKey((i, j)))
                    {
                        if (TryMarkHorizontalCombination(i, j) || TryMarkVerticalCombination(i, j))
                        {
                            IsAnyMatches = true;
                        }
                    }
                }
            }
            return IsAnyMatches;
        }

        /// <summary>
        /// Пробует записать горизонтальную комбинацию, в которой участвует указанный камень.
        /// </summary>
        /// <param name="x">X координата выбранного камня в сетке <see cref='StoneGrid'/>.</param>
        /// <param name="y">Y координата выбранного камня в сетке <see cref='StoneGrid'/>.</param>
        /// <returns><see cref="bool"/> <c>true</c>, если была записана комбинация, иначе <see cref="bool"/> <c>false</c>.</returns>
        private bool TryMarkHorizontalCombination(int x, int y)
        {
            EStoneType value = StoneGrid[x, y];
            if (value == EStoneType.None)
            {
                return false;
            }
            int offsetX;
            int leftBorderX = x;
            int rightBorderX = x;

            // Проверяем камни слева
            offsetX = x - 1;
            while (offsetX >= 0 && StoneGrid[offsetX, y] == value)
            {
                leftBorderX = offsetX;
                offsetX--;
            }
            // Проверяем камни справа
            offsetX = x + 1;
            while (offsetX < AlvQuestStatic.STONE_GRID_SIZE && StoneGrid[offsetX, y] == value)
            {
                rightBorderX = offsetX;
                offsetX++;
            }

            var combinationLength = rightBorderX - leftBorderX + 1;
            
            // Если была встречена комбинация
            if (combinationLength > 2)
            {
                // Записываем, что была встречена комбинация типа value длины combinationLenth
                StoneGridData.OnFieldCombinations.Add((value, combinationLength));
                // Записываем координаты камней, участвующих в комбинации
                for (int i = leftBorderX; i < rightBorderX + 1; i++)
                {
                    // Если данный камень успешно записан:
                    if (StoneGridData.OnFieldCombinationsStones.TryAdd((i, y), value))
                    {
                        // Увеличиваем счетчик камней данного типа
                        StoneGridData.AmountOfCombinedStones[value]++;
                    }
                }
                return true;
            }
            return false;
        }
        private static bool CheckHorizontalMatch(int x, int y, EStoneType[,] stoneGrid)
        {
            EStoneType value = stoneGrid[x, y];
            int offsetX;
            int leftBorderX = x;
            int rightBorderX = x;

            // Проверяем камни слева
            offsetX = x - 1;
            while (offsetX >= 0 && stoneGrid[offsetX, y] == value)
            {
                leftBorderX = offsetX;
                offsetX--;
            }
            // Проверяем камни справа
            offsetX = x + 1;
            while (offsetX < AlvQuestStatic.STONE_GRID_SIZE && stoneGrid[offsetX, y] == value)
            {
                rightBorderX = offsetX;
                offsetX++;
            }

            var combinationLenth = rightBorderX - leftBorderX + 1;
            return combinationLenth > 2;
        }

        /// <summary>
        /// Пробует записать вертикальную комбинацию, в которой участвует указанный камень.
        /// </summary>
        /// <param name="x">X координата выбранного камня в сетке <see cref='StoneGrid'/>.</param>
        /// <param name="y">Y координата выбранного камня в сетке <see cref='StoneGrid'/>.</param>
        /// <returns><see cref="bool"/> <c>true</c>, если была записана комбинация, иначе <see cref="bool"/> <c>false</c>.</returns>
        private bool TryMarkVerticalCombination(int x, int y)
        {
            EStoneType value = StoneGrid[x, y];
            if (value == EStoneType.None)
            {
                return false;
            }
            int offsetY;
            int leftBorderY = y;
            int rightBorderY = y;

            // Проверяем камни сверху
            offsetY = y - 1;
            while (offsetY >= 0 && StoneGrid[x, offsetY] == value)
            {
                leftBorderY = offsetY;
                offsetY--;
            }
            // Проверяем камни снизу
            offsetY = y + 1;
            while (offsetY < AlvQuestStatic.STONE_GRID_SIZE && StoneGrid[x, offsetY] == value)
            {
                rightBorderY = offsetY;
                offsetY++;
            }

            var combinationLength = rightBorderY - leftBorderY + 1;

            // Если была встречена комбинация
            if (combinationLength > 2)
            {
                //записываем, что была встречена комбинация типа value длины combinationLenth
                StoneGridData.OnFieldCombinations.Add((value, combinationLength));
                //записываем координаты камней, участвующих в комбинации
                for (int i = leftBorderY; i < rightBorderY + 1; i++)
                {
                    // Если данный камень успешно записан:
                    if (StoneGridData.OnFieldCombinationsStones.TryAdd((x, i), value))
                    {
                        // Увеличиваем счетчик камней данного типа
                        StoneGridData.AmountOfCombinedStones[value]++;
                    }
                }
                return true;
            }
            return false;
        }
        private static bool CheckVerticalMatch(int x, int y, EStoneType[,] stoneGrid)
        {
            EStoneType value = stoneGrid[x, y];
            int offsetY;
            int leftBorderY = y;
            int rightBorderY = y;

            // Проверяем камни сверху
            offsetY = y - 1;
            while (offsetY >= 0 && stoneGrid[x, offsetY] == value)
            {
                leftBorderY = offsetY;
                offsetY--;
            }
            // Проверяем камни снизу
            offsetY = y + 1;
            while (offsetY < AlvQuestStatic.STONE_GRID_SIZE && stoneGrid[x, offsetY] == value)
            {
                rightBorderY = offsetY;
                offsetY++;
            }

            var combinationLength = rightBorderY - leftBorderY + 1;

            // Если была встречена комбинация
            return combinationLength > 2;
        }



        /// <summary>
        /// Реализует механику продления хода активного игрока в результате создания комбинаций.
        /// </summary>
        private void CheckForTurnContinuation()
        {
            // Если персонаж ещё не получил возможности продолжить ход
            if (_arena.TurnSwitchModule.IsTurnEnd)
            {
                // Если при этом совмещение какой-либо комбинации даёт ему право продолжить хода
                if (_arena.ActivePlayer.IsAdditionalTurnReceivedFromCombinations(StoneGridData.OnFieldCombinations))
                {
                    // Отменяем завершение хода
                    _arena.TurnSwitchModule.IsTurnEnd = false;
                }
            }
        }

        /// <summary>
        /// Помечает все камни, являющиеся элементами комбинаций, как уничтоженные.
        /// </summary>
        private void DestroyCombinedStones()
        {
            foreach (var (x, y) in StoneGridData.OnFieldCombinationsStones.Keys)
            {
                StoneGrid[x,y] = EStoneType.None;
            }
        }

        /// <summary>
        /// Реализует падение камней вниз, а так же выпадение новых камней в освободившиеся верхние ячейки.
        /// </summary>
        private void StonesFreeFall()
        {
            // Идём по столбцам слева на право
            for (int i = 0; i < AlvQuestStatic.STONE_GRID_SIZE; i++)
            {
                // Новое состояние столбца
                // currentColumn[0] соответствует самому нижнему камню столбца
                EStoneType[] currentColumn = new EStoneType[AlvQuestStatic.STONE_GRID_SIZE];
                // Счетчик существующих в столбце камней
                int counter = 0;
                // Идём по столбцу снизу вверх
                for (int j = AlvQuestStatic.STONE_GRID_SIZE - 1; j >= 0; j--)
                {
                    var currentStone = StoneGrid[j, i];
                    // "Прижимаем" все существующие камни вниз столбца
                    if (currentStone != EStoneType.None)
                    {
                        currentColumn[counter] = currentStone;
                        counter++;
                    }
                    // Генерируем новые случайные камни в освободившиеся верхние ячейки
                    for (int k = counter; k < AlvQuestStatic.STONE_GRID_SIZE; k++)
                    {
                        currentColumn[k] = GetRandomStone();
                    }
                }
            }
        }

        /// <summary>
        /// Проверяет наличие хотя бы одного возможного хода для составления комбинации на поле.</summary>
        /// <returns><see cref="bool"/> <c>true</c>, если существует хотя бы однин ход, иначе <see cref="bool"/> <c>false</c>.</returns>
        private bool CheckCombinationCreationPossibility()
        {
            // Инициализируем копию сетки камней
            EStoneType[,] stoneGridCopy = new EStoneType[AlvQuestStatic.STONE_GRID_SIZE, AlvQuestStatic.STONE_GRID_SIZE];

            // Копируем расположение камней
            for (int i = 0; i < AlvQuestStatic.STONE_GRID_SIZE; i++)
            {
                for (int j = 0; j < AlvQuestStatic.STONE_GRID_SIZE; j++)
                {
                    stoneGridCopy[i, j] = StoneGrid[i, j];
                }
            }

            // Координаты смещений для получения соседей камня
            int[] offsetX = [-1, 0, 0, 1];
            int[] offsetY = [0, 1, -1, 0];

            // Выбираем камни в шахматном порядке.
            for (int i = 0; i < AlvQuestStatic.STONE_GRID_SIZE; i++)
            {
                for (int j = i % 2; j < AlvQuestStatic.STONE_GRID_SIZE; j += 2)
                {
                    // Для каждого возможного соседа:
                    for (int k = 0; k < 4; k++)
                    {
                        // Вычисляем координаты
                        int x = i + offsetX[k];
                        int y = j + offsetY[k];

                        // Если координаты не выходят за границы сетки:
                        if(x >= 0 && x < AlvQuestStatic.STONE_GRID_SIZE &&
                           y >= 0 && y < AlvQuestStatic.STONE_GRID_SIZE)
                        {
                            // Меянем местами 
                            SwapStones(i, j, x, y, stoneGridCopy);

                            // Проверяем, образовалась ли хотя бы одна комбинация
                            if(CheckHorizontalMatch(i, j, stoneGridCopy) || 
                               CheckVerticalMatch(i, j, stoneGridCopy) ||
                               CheckHorizontalMatch(x, y, stoneGridCopy) ||
                               CheckVerticalMatch(x, y, stoneGridCopy))
                            {
                                // Если да - заканчиваем работу метода 
                                return  true;
                            }
                            // Меняем камни обратно
                            SwapStones(i, j, x, y, stoneGridCopy);
                        }
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Записывает в выходной список все возможные хода для составления комбинации на поле.</summary>
        /// <returns>Список <see cref="Move"/>, выполнение которых приводит к появлению комбинации.</returns>
        public List<Move> WriteCombinationCreationPossibility()
        {
            List<Move> output = [];
            // Инициализируем копию сетки камней
            EStoneType[,] stoneGridCopy = new EStoneType[AlvQuestStatic.STONE_GRID_SIZE, AlvQuestStatic.STONE_GRID_SIZE];

            // Копируем расположение камней
            for (int i = 0; i < AlvQuestStatic.STONE_GRID_SIZE; i++)
            {
                for (int j = 0; j < AlvQuestStatic.STONE_GRID_SIZE; j++)
                {
                    stoneGridCopy[i, j] = StoneGrid[i, j];
                }
            }

            // Координаты смещений для получения соседей камня
            int[] offsetX = [-1, 0, 0, 1];
            int[] offsetY = [0, 1, -1, 0];

            // Выбираем камни в шахматном порядке.
            for (int i = 0; i < AlvQuestStatic.STONE_GRID_SIZE; i++)
            {
                for (int j = i % 2; j < AlvQuestStatic.STONE_GRID_SIZE; j += 2)
                {
                    // Для каждого возможного соседа:
                    for (int k = 0; k < 4; k++)
                    {
                        // Вычисляем координаты
                        int x = i + offsetX[k];
                        int y = j + offsetY[k];

                        // Если координаты не выходят за границы сетки:
                        if (x >= 0 && x < AlvQuestStatic.STONE_GRID_SIZE &&
                           y >= 0 && y < AlvQuestStatic.STONE_GRID_SIZE)
                        {
                            // Меянем местами 
                            SwapStones(i, j, x, y, stoneGridCopy);

                            // Проверяем, образовалась ли хотя бы одна комбинация
                            if (CheckHorizontalMatch(i, j, stoneGridCopy) ||
                               CheckVerticalMatch(i, j, stoneGridCopy) ||
                               CheckHorizontalMatch(x, y, stoneGridCopy) ||
                               CheckVerticalMatch(x, y, stoneGridCopy))
                            {
                                // Если да - записываем ход в выходной список
                                output.Add(new Move(i, j, (EMoveDirection)k));
                            }
                            // Меняем камни обратно
                            SwapStones(i, j, x, y, stoneGridCopy);
                        }
                    }
                }
            }
            return output;
        }
        private static void SwapStones(Move move, EStoneType[,] stoneGrid)
        {
            int x1 = move.X;
            int y1 = move.Y;
            (int, int)[] offsets = [(-1, 0),(0,1),(0,-1),(1,0)];
            (stoneGrid[x1, y1], stoneGrid[offsets[(int)move.MoveDirection].Item1, offsets[(int)move.MoveDirection].Item2]) = (stoneGrid[offsets[(int)move.MoveDirection].Item1, offsets[(int)move.MoveDirection].Item2], stoneGrid[x1, y1]);
        }

        public void ExecuteFakeStoneSwappingTurn(Move move)
        {
            StoneGridData.ResetData();
            SwapStones(move, StoneGrid);
            bool turnContinuation = false;
            // Пока на доске существует хотя бы одна комбинация:
            while (TryFindStoneCombinations())
            {
                foreach (var combination in StoneGridData.OnFieldCombinations)
                {
                    if (combination.Length > 3)
                    {
                        turnContinuation = true;
                        break;
                    }
                }
                if (turnContinuation)
                {
                    DestroyCombinedStones();
                    StonesFreeFallWithoutReplacement();
                    turnContinuation = false;
                }
            }
        }

        private void StonesFreeFallWithoutReplacement()
        {
            // Идём по столбцам слева на право
            for (int i = 0; i < AlvQuestStatic.STONE_GRID_SIZE; i++)
            {
                // Новое состояние столбца
                // currentColumn[0] соответствует самому нижнему камню столбца
                EStoneType[] currentColumn = new EStoneType[AlvQuestStatic.STONE_GRID_SIZE];
                // Счетчик существующих в столбце камней
                int counter = 0;
                // Идём по столбцу снизу вверх
                for (int j = AlvQuestStatic.STONE_GRID_SIZE - 1; j >= 0; j--)
                {
                    var currentStone = StoneGrid[j, i];
                    // "Прижимаем" все существующие камни вниз столбца
                    if (currentStone != EStoneType.None)
                    {
                        currentColumn[counter] = currentStone;
                        counter++;
                    }
                }
            }
        }
        #endregion
    }
}
