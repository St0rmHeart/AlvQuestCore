using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AlvQuestCore
{
    /// <summary>
    /// Структура хранения всей информации для каждого из двух игроков, необходимой для "сражения"
    /// </summary>
    public class CharacterSlot
    {
        #region EVENT - события для отслеживания изменений персонажа
        /// <summary>
        /// Срабатывает, когда персонаж любым способом создает на доске комбинацию из 3-5 камней в ряд.
        /// <br />
        /// <br /><see cref="EStoneType"/> <c>stoneType</c> - тип скомбинированных камней;
        /// <br /><see cref="int"/> <c>count</c> - количество камней в комбинации.  
        /// </summary>
        public event EventHandler<(EStoneType StoneType, int Count)> StoneCombining;



        /// <summary>
        /// Срабатывает, когда персонаж любым способом уничтожает на доске камни какого-либо типа.
        /// <br />
        /// <br /><see cref="EStoneType"/> <c>stoneType</c> - тип уничтоженных камней;
        /// <br /><see cref="int"/> <c>count</c> - количество уничтоженных камней.  
        /// </summary>
        public event EventHandler<(EStoneType StoneType, int Count)> StoneDestruction;



        /// <summary>
        /// Срабатывает, когда персонаж поглощает камни и получает эффект их поглощения с учетом параметра TerminationMult.
        /// <br />
        /// <br /><see cref="EStoneType"/> <c>stoneType</c> - тип поглощенных камней;
        /// <br /><see cref="double"/> <c>value</c> - величина эффекта поглощенных камней. 
        /// </summary>
        public event EventHandler<(EStoneType StoneType, double Value)> StoneAbsorption;



        /// <summary>
        /// Срабатывает при использовании персонажем любого заклинания.
        /// </summary>
        public event EventHandler SpellCasting;



        /// <summary>
        /// Срабатывает при перемещении персонажем двух соседних камней.
        /// </summary>
        public event EventHandler StoneSwap;



        /// <summary>
        /// Срабатывает при использовании любого заклинания или перемещении камней на доске.
        /// </summary>
        public event EventHandler ActionExecution;



        /// <summary>
        /// Срабатывает когда ход персонажа завершается.
        /// </summary>
        public event EventHandler TurnExecution;



        /// <summary>
        /// Срабатывает когда здоровье персонажа опускается до нуля.
        /// </summary>
        public event EventHandler Death;



        /// <summary>
        /// Срабатывает когда персонаж испускает урон.
        /// <br />
        /// <br /><see cref="EDamageType"/> <c>damageType </c> - тип испускаемого урона;
        /// <br /><see cref="double"/> <c>value</c> - величина урона. 
        /// </summary>
        public event EventHandler<(EDamageType DamageType, double Value)> DamageEmitting;



        /// <summary>
        /// Срабатывает когда персонаж принимает урон.
        /// <br />
        /// <br /><see cref="EDamageType"/> <c>damageType </c> - тип принимаемого урона;
        /// <br /><see cref="double"/> <c>value</c> - величина урона. 
        /// </summary>
        public event EventHandler<(EDamageType DamageType, double Value)> DamageAccepting;



        /// <summary>
        /// Срабатывает когда персонаж блокирует урон.
        /// <br />
        /// <br /><see cref="EDamageType"/> <c>damageType </c> - тип блокируемого урона;
        /// <br /><see cref="double"/> <c>value</c> - величина урона. 
        /// </summary>
        public event EventHandler<(EDamageType DamageType, double Value)> DamageBlocking;



        /// <summary>
        /// Срабатывает когда персонаж получает урон.
        /// <br />
        /// <br /><see cref="EDamageType"/> <c>damageType </c> - тип получаемого урона;
        /// <br /><see cref="double"/> <c>value</c> - величина урона. 
        /// </summary>
        public event EventHandler<(EDamageType DamageType, double Value)> DamageTaking;



        /// <summary>
        /// Срабатывает когда у персонажа меняется количество опыта.
        /// <br />
        /// <br /><see cref="double"/> <c>value</c> - величина изменения.
        /// </summary>
        public event EventHandler<double> DeltaXP;



        /// <summary>
        /// Срабатывает когда у персонажа меняется количество здоровья.
        /// <br />
        /// <br /><see cref="double"/> <c>value</c> - величина изменения.
        /// </summary>
        public event EventHandler<double> DeltaHP;



        /// <summary>
        /// Срабатывает когда у персонажа меняется количество золота.
        /// <br />
        /// <br /><see cref="double"/> <c>value</c> - величина изменения.
        /// </summary>
        public event EventHandler<double> DeltaGold;



        /// <summary>
        /// Срабатывает когда у персонажа меняется количество какой-либо маны.
        /// <br />
        /// <br /><see cref="EManaType"/> <c>manaType</c> - тип изменяемой маны.
        /// <br /><see cref="double"/> <c>value</c> - величина изменения.
        /// </summary>
        public event EventHandler<(EManaType ManaType, double Value)> DeltaMP;
        #endregion 

        #region EVENT INVOKE - методы вызова событий.
        /// <summary>
        /// Вызов события <see cref="StoneCombining"/>
        /// <br /> Срабатывает, когда персонаж любым способом создает на доске комбинацию из 3-5 камней в ряд.
        /// </summary>
        /// <param name="stoneType"> Тип скомбинированных камней </param>
        /// <param name="count"> Количество камней в комбинации</param>
        private void InvokeStoneCombining(EStoneType stoneType, int count)
        {
            StoneCombining?.Invoke(this,(stoneType, count));
        }



        /// <summary>
        /// Вызов события <see cref="StoneDestruction"/>
        /// <br /> Срабатывает, когда персонаж любым способом уничтожает на доске камни какого-либо типа.
        /// </summary>
        /// <param name="stoneType"> Тип уничтоженных камней </param>
        /// <param name="count"> Количество уничтоженных камней </param>
        private void InvokeStoneDestruction(EStoneType stoneType, int count)
        {
            StoneDestruction?.Invoke(this, (stoneType, count));
        }



        /// <summary>
        /// Вызов события <see cref="StoneAbsorption"/>
        /// <br /> Срабатывает, когда персонаж поглощает камни и получает эффект их поглощения с учетом параметра TerminationMult.
        /// </summary>
        /// <param name="stoneType"> Тип поглощенных камней </param>
        /// <param name="value"> Количество поглощенных камней </param>
        private void InvokeStoneAbsorption(EStoneType stoneType, double value)
        {
            StoneAbsorption?.Invoke(this, (stoneType, value));
        }



        /// <summary>
        /// Вызов события <see cref="SpellCasting"/>
        /// <br /> Срабатывает при использовании персонажем любого заклинания.
        /// </summary>
        private void InvokeSpellCasting()
        {
            InvokeActionExecution();
            SpellCasting?.Invoke(this, EventArgs.Empty);
        }



        /// <summary>
        /// Вызов события <see cref="StoneSwap"/>
        /// <br /> Срабатывает при перемещении персонажем двух соседних камней.
        /// </summary>
        private void InvokeStoneSwapping()
        {
            InvokeActionExecution();
            StoneSwap?.Invoke(this, EventArgs.Empty);
        }



        /// <summary>
        /// Вызов события <see cref="ActionExecution"/>
        /// <br /> Срабатывает при использовании любого заклинания или перемещении камней на доске.
        /// </summary>
        private void InvokeActionExecution()
        {
            ActionExecution?.Invoke(this, EventArgs.Empty);
        }



        /// <summary>
        /// Вызов события <see cref="TurnExecution"/>
        /// <br /> Срабатывает когда ход персонажа завершается.
        /// </summary>
        private void InvokeTurnExecution()
        {
            TurnExecution?.Invoke(this, EventArgs.Empty);
        }



        /// <summary>
        /// Вызов события <see cref="Death"/>
        /// <br /> Срабатывает когда здоровье персонажа опускается до нуля.
        /// </summary>
        private void InvokeDeath()
        {
            Death?.Invoke(this, EventArgs.Empty);
        }



        /// <summary>
        /// Вызов события <see cref="DamageEmitting"/>
        /// <br /> Срабатывает когда персонаж испускает урон.
        /// </summary>
        /// <param name="damageType"> Тип испускаемого урона </param>
        /// <param name="value"> Величина урона </param>
        private void InvokeDamageEmitting(EDamageType damageType, double value)
        {
            DamageEmitting?.Invoke(this, (damageType, value));
        }



        /// <summary>
        /// Вызов события <see cref="DamageAccepting"/>
        /// <br /> Срабатывает когда персонаж принимает урон.
        /// </summary>
        /// <param name="damageType"> Тип принимаемого урона </param>
        /// <param name="value"> Величина урона </param>
        private void InvokeDamageAccepting(EDamageType damageType, double value)
        {
            DamageAccepting?.Invoke(this, (damageType, value));
        }



        /// <summary>
        /// Вызов события <see cref="DamageBlocking"/>
        /// <br /> Срабатывает когда персонаж блокирует урон.
        /// </summary>
        /// <param name="damageType"> Тип блокируемого урона </param>
        /// <param name="value"> Величина урона </param>
        private void InvokeDamageBlocking(EDamageType damageType, double value)
        {
            DamageBlocking?.Invoke(this, (damageType, value));
        }



        /// <summary>
        /// Вызов события <see cref="DamageTaking"/>
        /// <br /> Срабатывает когда персонаж принимает урон.
        /// </summary>
        /// <param name="damageType"> Тип получаемого урона </param>
        /// <param name="value"> Величина урона </param>
        private void InvokeDamageTaking(EDamageType damageType, double value)
        {
            DamageTaking?.Invoke(this, (damageType, value));
        }



        /// <summary>
        /// Вызов события <see cref="DeltaXP"/>
        /// <br /> Срабатывает когда у персонажа меняется количество опыта.
        /// </summary>
        /// <param name="value"> Величина изменения </param>
        private void InvokeDeltaXP(double value)
        {
            DeltaXP?.Invoke(this, value);
        }



        /// <summary>
        /// Вызов события <see cref="DeltaHP"/>
        /// <br /> Срабатывает когда у персонажа меняется количество здоровья.
        /// </summary>
        /// <param name="value"> Величина изменения </param>
        private void InvokeDeltaHP(double value)
        {
            DeltaHP?.Invoke(this, value);
        }



        /// <summary>
        /// Вызов события <see cref="DeltaGold"/>
        /// <br /> Срабатывает когда у персонажа меняется количество золота.
        /// </summary>
        /// <param name="value"> Величина изменения </param>
        private void InvokeDeltaGold(double value)
        {
            DeltaGold?.Invoke(this, value);
        }



        /// <summary>
        /// Вызов события <see cref="DeltaAnyMana"/>
        /// <br /> Срабатывает когда у персонажа меняется количество какой-либо маны.
        /// </summary>
        /// <param name="value"> Величина изменения </param>
        private void InvokeDeltaMana(EManaType manatype, double value)
        {
            DeltaMP?.Invoke(this, (manatype, value));
        }
        #endregion

        #region ПОЛЯ
        /// <summary>
        /// Генератор случайных чисел.
        /// </summary>
        private readonly Random _random = new();



        /// <summary>
        /// Флажок - переключатель хода
        /// </summary>
        public TurnSwitch TurnSwitcherModule { get; set; }



        /// <summary>
        /// Модуль боевой системы, через который персонажи наносят и плучают урон
        /// </summary>
        public DamageModule DamageModule { get; set; }

        /// <summary>
        /// Доска камней
        /// </summary>
        public StoneBoard StoneBoard { get; set; }


        /// <summary>
        /// Сcылка на текущего оппонента в сражении
        /// </summary>
        public CharacterSlot CurrentOpponent { get; set; }


        /// <summary>
        /// Персонаж, представляемый данным <see cref="CharacterSlot"/>
        /// </summary>
        public Character Character { get; private set; }


    
        /// <summary>
        /// Массив параметров персонажа
        /// </summary>
        public DerivativesEnumeration Data { get; private set; }
        #endregion

        #region ______________________КОНСТРУКТОР______________________
        /// <summary>
        /// Стандартный конструктор <see cref="CharacterSlot"/>.
        /// </summary>
        /// <param name="character"></param>
        public CharacterSlot(Character character)
        {
            //установка персонажа
            Character = character;

            //Рассчет всех производных параметров персонажа
            Data = new DerivativesEnumeration(character);
        }
        #endregion

        #region ______________________СВОЙСТВА______________________
        /// <summary>
        /// Количество здоровья персонажа. 
        /// </summary>
        public double Health
        {
            get
            {
                if (Data[ECharacteristic.Endurance][EDerivative.CurrentHealth] is CurrentCommonParameter current)
                    return current.CurrentValue;
                else 
                    throw new InvalidOperationException("Некорректный класс по адресу.");
            }
            private set
            {
                if (Data[ECharacteristic.Endurance][EDerivative.CurrentHealth] is CurrentCommonParameter current)
                    current.CurrentValue = value;
                else
                    throw new InvalidOperationException("Некорректный класс по адресу.");
            }
        }



        /// <summary>
        /// Количество опыта персонажа.
        /// </summary>
        public int Xp
        {
            get { return Character.Xp; }
            private set { Character.Xp = value; }
        }



        /// <summary>
        /// Количество опыта персонажа.
        /// </summary>
        public int Gold
        {
            get { return Character.Gold; }
            private set { Character.Gold = value; }
        }



        /// <summary>
        /// Количество маны огня персонажа.
        /// </summary>
        public double ManaFire
        {
            get
            {
                if (Data[ECharacteristic.Fire][EDerivative.CurrentMana] is CurrentCommonParameter current)
                    return current.CurrentValue;
                else
                    throw new InvalidOperationException("Некорректный класс по адресу.");
            }
            private set
            {
                if (Data[ECharacteristic.Fire][EDerivative.CurrentMana] is CurrentCommonParameter current)
                    current.CurrentValue = value;
                else
                    throw new InvalidOperationException("Некорректный класс по адресу.");
            }
        }



        /// <summary>
        /// Количество маны воды персонажа.
        /// </summary>
        public double ManaWater
        {
            get
            {
                if (Data[ECharacteristic.Water][EDerivative.CurrentMana] is CurrentCommonParameter current)
                    return current.CurrentValue;
                else
                    throw new InvalidOperationException("Некорректный класс по адресу.");
            }
            private set
            {
                if (Data[ECharacteristic.Water][EDerivative.CurrentMana] is CurrentCommonParameter current)
                    current.CurrentValue = value;
                else
                    throw new InvalidOperationException("Некорректный класс по адресу.");
            }
        }



        /// <summary>
        /// Количество маны земли персонажа.
        /// </summary>
        public double ManaEarth
        {
            get
            {
                if (Data[ECharacteristic.Earth][EDerivative.CurrentMana] is CurrentCommonParameter current)
                    return current.CurrentValue;
                else
                    throw new InvalidOperationException("Некорректный класс по адресу.");
            }
            private set
            {
                if (Data[ECharacteristic.Earth][EDerivative.CurrentMana] is CurrentCommonParameter current)
                    current.CurrentValue = value;
                else
                    throw new InvalidOperationException("Некорректный класс по адресу.");
            }
        }



        /// <summary>
        /// Количество маны воздуха персонажа.
        /// </summary>
        public double ManaAir
        {
            get
            {
                if (Data[ECharacteristic.Air][EDerivative.CurrentMana] is CurrentCommonParameter current)
                    return current.CurrentValue;
                else
                    throw new InvalidOperationException("Некорректный класс по адресу.");
            }
            private set
            {
                if (Data[ECharacteristic.Air][EDerivative.CurrentMana] is CurrentCommonParameter current)
                    current.CurrentValue = value;
                else
                    throw new InvalidOperationException("Некорректный класс по адресу.");
            }
        }



        /// <summary>
        /// Имя персонажа.
        /// </summary>
        public string Name
        {
            get { return Character.Name; }
        }
        #endregion

        #region _____________________МЕТОДЫ_____________________
        /// <summary>
        /// Вычисляет, получает ли персонаж право продолжить ход в результате создания комбинаций на доске камней.
        /// </summary>
        /// <param name="stoneGridData"> Состояние доски камней </param>
        /// <returns><see cref="bool"/> <c>true</c>, если персонаж создал комбинацию 4 или 5 в ряд или сработал шанс доп. хода, иначе <see cref="bool"/> <c>false</c>.</returns>
        public bool IsAdditionalTurnReceivedFromCombinations(List<(EStoneType StoneType, int Lenth)> onFieldCombinations)
        {
            // Разбираем каждую комбинацию на доске
            foreach (var combination in onFieldCombinations)
            {
                InvokeStoneCombining(combination.StoneType, combination.Lenth);
                // Комбинации 4 и 5 в ряд продлевают ход гарантированно
                if(combination.Lenth > 3)
                {
                    return true;
                }

                // Рассматриваем комбинацию из трёх камней
                var characteristic = (ECharacteristic)combination.StoneType;
                var addTurnChance = Data[characteristic][EDerivative.AddTurnChance].FinalValue;

                if (addTurnChance > _random.NextDouble())
                {
                    return true;
                }
            }
            return false;
        }



        /// <summary>
        /// Реализует все эффекты поглощения набора уничтоженных камней
        /// </summary>
        /// <param name="stoneGridData"></param>
        public void AbsorbDestroyedStones(Dictionary<EStoneType, int> combinedStones)
        {
            foreach (var stoneGroup in combinedStones)
            {
                EStoneType stoneGroupType = stoneGroup.Key;
                int stoneGroupAmount = stoneGroup.Value;

                InvokeStoneDestruction(stoneGroupType, stoneGroupAmount);

                var characteristic = (ECharacteristic)stoneGroupType;
                var terminationMult = Data[characteristic][EDerivative.TerminationMult].FinalValue;
                var absobtionResult = (stoneGroupAmount * terminationMult).Round();

                switch (stoneGroupType)
                {
                    case EStoneType.Skull:
                        DamageModule.AddAttack(this, CurrentOpponent, EDamageType.PhysicalDamage, absobtionResult, true, true, true);
                        break;

                    case EStoneType.Gold:
                        ChangeGold_WithNotification(absobtionResult);
                        break;

                    case EStoneType.Experience:
                        ChangeXP_WithNotification(absobtionResult);
                        break;

                    case EStoneType.FireStone:
                    case EStoneType.WaterStone:
                    case EStoneType.EarthStone:
                    case EStoneType.AirStone:
                        var manaType = (EManaType)stoneGroupType;
                        ChangeMP_WithNotification(manaType, absobtionResult);
                        break;
                }

                InvokeStoneAbsorption(stoneGroupType, absobtionResult);
            }
        }



        /// <summary>
        /// Обработать изменение очков здоровья у персонажа.
        /// </summary>
        /// <param name="delta">Значение, на которое нужно изменить очки опыта.</param>
        /// <returns>Фактическое измененеие очков здоровья.</returns>
        public double ChangeHP(double delta)
        {
            double maxHP = Data[ECharacteristic.Endurance][EDerivative.MaxHealth].FinalValue;
            double currentHP = Health;

            if (maxHP == currentHP && delta > 0)
            {
                return 0;
            }

            double newHp = (currentHP + delta).Round();

            if (newHp > maxHP)
            {
                Health = maxHP;
                return (maxHP - currentHP).Round();
            }
            else if (newHp < 0)
            {
                Health = 0;
                return -currentHP;
            }
            else
            {
                Health = newHp;
                return delta;
            }
        }



        /// <summary>
        /// Обработать изменение опыта у персонажа.
        /// </summary>
        /// <param name="value">Значение, на которое нужно изменить опыт.</param>
        /// <returns>Фактическое изменение опыта.</returns>
        public int ChangeXP(double value)
        {
            int currentLevel = Character.Level;
            int currentMinimum = AlvQuestStatic.levelBoundaries[currentLevel - 2];
            int delta = (int)Math.Round(value);
            int oldXp = Xp;
            int newXp = oldXp + delta;
            if (newXp < currentMinimum)
            {
                Xp = currentMinimum;
                return currentMinimum - oldXp;
            }
            else
            {
                Character.Xp+=delta;
                return delta;
            }
        }



        /// <summary>
        /// Обработать изменение золота у персонажа.
        /// </summary>
        /// <param name="value">Значение, на которое нужно изменить золото.</param>
        /// <returns>Фактическое изменение золота.</returns>
        public int ChangeGold(double value)
        {
            int delta = (int)Math.Round(value);
            int oldGold = Gold;
            int newGold = oldGold + delta;
            if (newGold < 0)
            {
                Gold = 0;
                return -oldGold;
            }
            else
            {
                Gold += delta;
                return delta;
            }
        }



        /// <summary>
        /// Обработать изменение маны определённого типа у персонажа.
        /// </summary>
        /// <param name="characteristic">Характеристика мастерства стихии.</param>
        /// <param name="delta">На сколько нужно попытаться изменить количество маны.</param>
        /// <returns>Фактическое измененеие маны</returns>
        public double ChangeMP(EManaType manaType, double delta)
        {
            // Связанная с данным типом маны характеристика
            var characteristic = (ECharacteristic)manaType;

            // Параметр, хранящий данную ману
            var currentParameter = Data[characteristic][EDerivative.CurrentMana] as CurrentCommonParameter;

            // Текущее количество маны
            double currentMana = currentParameter.CurrentValue;

            // Максимально допустимое количество маны
            double maxMana = Data[characteristic][EDerivative.MaxMana].FinalValue;

            // Обработка ситуаций, когда изменение количества маны невозможно
            if ((maxMana == currentMana && delta > 0) || (currentMana == 0 && delta < 0))
            {
                return 0;
            }

            // В иных случаях - вычисляем, насколько изменился запас маны
            double newMana = (currentMana + delta).Round();

            // Устанавливаем максимум, если новое значение больше MaxMana
            if (newMana > maxMana)
            {
                currentParameter.CurrentValue = maxMana;
                return (maxMana - currentMana).Round();
            }
            // Устанавливаем CurrentMana в 0, если новое значение меньше 0
            else if (newMana < 0)
            {
                currentParameter.CurrentValue = 0;
                return -currentMana;
            }
            // В остальных случаях устанавливаем CurrentMana равным newMana
            else
            {
                currentParameter.CurrentValue = newMana;
                return delta;
            }
        }



        /// <summary>
        /// Обработать изменение золота у персонажа и вызвать уведомление с фактическим значением изменения.
        /// </summary>
        /// <param name="value">Значение, на которое нужно изменить золото.</param>
        public void ChangeGold_WithNotification(double value)
        {
            int actualDelta = ChangeGold(value);
            if (actualDelta != 0)
            {
                InvokeDeltaGold(actualDelta);
            }
        }



        /// <summary>
        /// Обработать изменение маны определённого типа у персонажа и вызвать уведомление с фактическим значением изменения.
        /// </summary>
        /// <param name="characteristic">Характеристика мастерства стихии.</param>
        /// <param name="delta">На сколько нужно попытаться изменить количество маны.</param>
        public void ChangeMP_WithNotification(EManaType manaType, double delta)
        {
            double actualDelta = ChangeMP(manaType, delta);
            if (actualDelta != 0)
            {
                InvokeDeltaMana(manaType, actualDelta);
            }
        }



        /// <summary>
        /// Обработать изменение очков опыта у персонажа и вызвать уведомление с фактическим значением изменения, если оно было.
        /// </summary>
        /// <param name="value">Значение, на которое нужно изменить очки опыта.</param>
        public void ChangeXP_WithNotification(double value)
        {
            int actualDelta = ChangeXP(value);
            if (actualDelta != 0)
            {
                InvokeDeltaXP(actualDelta);
            }
        }



        /// <summary>
        /// Обработать изменение очков здоровья у персонажа и вызвать уведомление с фактическим значением изменения.
        /// </summary>
        /// <param name="delta">Значение, на которое нужно изменить очки опыта.</param>
        public void ChangeHP_WithNotification(double delta)
        {
            double actualDelta = ChangeHP(delta);
            if (actualDelta != 0)
            {
                InvokeDeltaHP(actualDelta);
            }
        }



        /// <summary>
        /// Вызвать уведомелление об ипускании владельцем value едениц damageType урона.
        /// </summary>
        /// <param name="damageType">Тип испускаемого урона.</param>
        /// <param name="value">Количество урона.</param>
        public void SendEmitDamageNotification(EDamageType damageType, double value)
        {
            if(value > 0)
            {
                InvokeDamageEmitting(damageType, value);
            }
            
        }



        /// <summary>
        /// Вызвать уведомелление о блокировании владельцем value едениц damageType урона.
        /// </summary>
        /// <param name="damageType">Тип блокируевомого урона.</param>
        /// <param name="value">Количество урона.</param>
        public void SendBlockDamageNotification(EDamageType damageType, double value)
        {
            if (value > 0)
            {
                InvokeDamageBlocking(damageType, value);
            }
        }



        /// <summary>
        /// Вызвать уведомелление о принятии владельцем value едениц damageType урона.
        /// </summary>
        /// <param name="damageType">Тип принимаемого урона.</param>
        /// <param name="value">Количество урона</param>
        public void SendAcceptDamageNotification(EDamageType damageType, double value)
        {
            if (value > 0)
            {
                InvokeDamageAccepting(damageType, value);
            }
        }



        /// <summary>
        /// Вызвать уведомелление о получении владельцем value едениц damageType урона.
        /// </summary>
        /// <param name="damageType">Тип получаемого урона.</param>
        /// <param name="value">Количество урона.</param>
        public void SendTakeDamageNotification(EDamageType damageType, double value)
        {
            if (value > 0)
            {
                InvokeDamageTaking(damageType, value);
            }
        }



        /// <summary>
        /// Вызвать уведомелление о совершении хода, пуём переменещения двух соседних камней.
        /// </summary>
        public void SendStoneSwappingTurnNotification()
        {
            TurnSwitcherModule.IsTurnEnd = true;
            InvokeStoneSwapping();
        }

        public void SendTurnEndNotification()
        {
            InvokeTurnExecution();
        }
        #endregion
    }
}
