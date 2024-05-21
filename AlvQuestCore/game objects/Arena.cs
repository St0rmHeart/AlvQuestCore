namespace AlvQuestCore
{
    public class Arena
    {
        #region _____________________ПОЛЯ_____________________
        /// <summary>
        /// 
        /// </summary>
        public CharacterSlot _player { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public CharacterSlot _enemy { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public CharacterSlot ActivePlayer { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public CharacterSlot PassivePlayer { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public TurnSwitch TurnSwitchModule { get; } 
        
        /// <summary>
        /// 
        /// </summary>
        private readonly DamageModule _damageModule;

        /// <summary>
        /// 
        /// </summary>
        public readonly StoneBoard _stoneBoard;
        #endregion

        #region ______________________КОНСТРУКТОР______________________

        /// <summary>
        /// Конструктор Арены, где устанавливается персонаж игрока и персонаж противника,
        /// а так же призводится и полная предварительная настройка и подготовка к сражению
        /// </summary>
        /// <param name="player">Персонаж игрока</param>
        /// <param name="enemy"> персонаж противника</param>
        public Arena(Character player, Character enemy)
        {
            //Установка персонажей игрока противника-компьютера
            _player = new CharacterSlot(player);
            _enemy = new CharacterSlot(enemy);

            //проброска ссылок между всеми объектами
            CharacterSlot[] initArray = [_player, _enemy];
            for (int i = 0; i < initArray.Length; i++)
            {
                var pointer1 = initArray[i];
                var pointer2 = initArray[(i + 1) % 2];

                //установка ссылки на переключателя хода
                pointer1.TurnSwitcherModule = TurnSwitchModule;
                //установка ссылки на модуль урона
                pointer1.DamageModule = _damageModule;
                //установка ссылки на оппонента
                pointer1.CurrentOpponent = pointer2;

                //инициализация всех эффектов в снаряжении персонажа
                /*foreach (Equipment item in pointer1.Character.Equipment.Values)
                {
                    foreach (IEffect effect in item.Effects)
                    {
                        effect.Installation(pointer1, pointer2);
                    }
                }*/
            }
            foreach (CharacterSlot characterSlot in initArray)
            {
                foreach (ECharacteristic characteristic in AlvQuestStatic.CHAR_DER_PAIRS.Keys)
                {
                    characterSlot.Data[characteristic][EDerivative.Value].SetFinalValue();
                }
                foreach (ECharacteristic characteristic in AlvQuestStatic.CHAR_DER_PAIRS.Keys)
                {
                    foreach (EDerivative derivative in AlvQuestStatic.CHAR_DER_PAIRS[characteristic])
                    {
                        (characterSlot.Data[characteristic][derivative] as CommonParameter)?.UpdateA0();
                    }
                }
                (characterSlot.Data[ECharacteristic.Endurance][EDerivative.CurrentHealth] as CurrentCommonParameter).CurrentValue =
                    characterSlot.Data[ECharacteristic.Endurance][EDerivative.MaxHealth].FinalValue; ;
            }

            //У кого из персонажей больше ловкость - тот и начинает ход первым
            var playerDexterity = _player.Data[ECharacteristic.Dexterity][EDerivative.Value].FinalValue;
            var enemyDexterity = _enemy.Data[ECharacteristic.Dexterity][EDerivative.Value].FinalValue;
            if (playerDexterity >= enemyDexterity)
            { ActivePlayer = _player; PassivePlayer = _enemy; }
            else
            { ActivePlayer = _enemy; PassivePlayer = _player; }
            //инициализация эффектов у обоих персонажей
        }
        #endregion

        #region _____________________МЕТОДЫ_____________________

        public void CompleteTurn()
        {
            
        }
        
        public void Installation()
        {
            //проброска ссылок между всеми объектами
            LinksDTO linksDTO = new() {
                CurrentArena = this
            };
            CharacterSlot[] initArray = [_player, _enemy];
            for (int i = 0; i < initArray.Length; i++)
            {
                var pointer1 = initArray[i];
                var pointer2 = initArray[(i + 1) % 2];

                linksDTO.PlayerCharacterSlot = pointer1;
                linksDTO.EnemyCharacterSlot = pointer2;

                //установка ссылки на переключателя хода
                pointer1.TurnSwitcherModule = TurnSwitchModule;
                //установка ссылки на модуль урона
                pointer1.DamageModule = _damageModule;
                //установка ссылки на оппонента
                pointer1.CurrentOpponent = pointer2;
                //инициализация всех эффектов в снаряжении персонажа
                pointer1.Character.Installation(linksDTO);
            }

        }

        #endregion
    }
}
