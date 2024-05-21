using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlvQuestCore
{
    public class DamageModule
    {
        #region _____________________ПОЛЯ_____________________

        private bool _isActive;
        private int _counter;

        private EDamageType _attackerDamageType;
        public double AttackerDamageMultiplier {  get; set; }
        public double AttackerDamageSummand { get; set; }
        public double DefenderDamageMultiplier { get; set; }
        public double DefenderDamageSummand { get; set; }


        private readonly List<(
            CharacterSlot attacker,
            CharacterSlot defender,
            (EDamageType type, double value, bool isblockable, bool isAttackerReact, bool isDefenderReact) damageData)> _attacksList = new();

        #endregion

        #region ______________________КОНСТРУКТОР______________________

        public DamageModule()
        {
            Reset();
            _isActive = false;
            _counter = 0;
            _attacksList.Clear();
        }

        #endregion

        #region _____________________МЕТОДЫ_____________________

        private void Reset()
        {
            _attackerDamageType = EDamageType.None;
            AttackerDamageMultiplier = 1;
            AttackerDamageSummand = 0;
            DefenderDamageMultiplier = 1;
            DefenderDamageSummand = 0;
        }
        public void AddAttack(
            CharacterSlot attacker,
            CharacterSlot defender,
            EDamageType damageType,
            double damageBaseValue,
            bool isblockable,
            bool isAttackerReact,
            bool isDefenderReact)
        {
            _attacksList.Add((attacker, defender, (damageType, damageBaseValue, isblockable, isAttackerReact, isDefenderReact)));
            if (!_isActive)
            {
                InitAttack();
            }
        }
        private void InitAttack()
        {
            _isActive = true;
            for (_counter = 0; _counter < _attacksList.Count; _counter++)
            {
                //установка всех стартовых параметров
                CharacterSlot attacker = _attacksList[_counter].attacker;
                CharacterSlot defender = _attacksList[_counter].defender;
                _attackerDamageType = _attacksList[_counter].damageData.type;
                var attackerDamageBaseValue = _attacksList[_counter].damageData.value;
                AttackerDamageMultiplier = 1;
                AttackerDamageSummand = 0;

                if (_attacksList[_counter].damageData.isAttackerReact)
                {
                    //запускаме ивент на испускание базового урона у атакующего пенрсонажа
                    attacker.SendEmitDamageNotification(_attackerDamageType, attackerDamageBaseValue);
                    //принимают винальные значения поля:
                    //AttackerDamageMultiplier - на сколько относительно базовго значения должен измениться испускаемый урон 
                    //AttackerDamageSummand - на сколько должен абсолютно должен измениться испускаемый урон
                    //_AttackdamageType должно содержать окончательное значение элемента урона
                }

                //вычисляем итоговое значение урона, который должен получить защищающийся персонаж
                double defenderAcceptedDamage = (attackerDamageBaseValue * AttackerDamageMultiplier + AttackerDamageSummand).Round();

                if (_attacksList[_counter].damageData.isblockable)
                {
                    //выясняем сопротивление к урону данного типа у защищающегося персонажа
                    double defenderResistance = defender.Data[(ECharacteristic)(int)_attackerDamageType][EDerivative.Resistance].FinalValue;
                    //вычсляем заблокированный урон
                    double defenderBlockedDamage = defenderAcceptedDamage * defenderResistance;
                    //вычисляем принимаемый урон
                    defenderAcceptedDamage = (defenderAcceptedDamage - defenderBlockedDamage).Round();
                    //запускаем ивент на блокирование урона у защищающегося персонажа
                    defender.SendBlockDamageNotification(_attackerDamageType, defenderBlockedDamage);
                }

                if (_attacksList[_counter].damageData.isDefenderReact)
                {
                    //запускаем ивент на принятие урона у защищающегося персонажа
                    defender.SendAcceptDamageNotification(_attackerDamageType, defenderAcceptedDamage);
                    //по итогу должны быть заполнены поля:
                    //DefenderDamageMultiplier - на сколько относительно базовго значения должен измениться принимаемый урон 
                    //DefenderDamageSummand - на сколько должен абсолютно должен измениться принимаемый урон

                    //вычисляем итоговое значение урона, который получает защищающийся персонаж
                    defenderAcceptedDamage = (defenderAcceptedDamage * DefenderDamageMultiplier + DefenderDamageSummand).Round();
                    //запускаем ивент на получение урона у защищающегося персонажа
                    defender.SendTakeDamageNotification(_attackerDamageType, defenderAcceptedDamage);
                }
                else
                {
                    //запускаем ивент на изменение здоровья у защищающегося персонажа
                    defender.ChangeHP_WithNotification(-defenderAcceptedDamage);
                }
                Reset();
            }
            _isActive = false;
            _counter = 0;
            _attacksList.Clear();
        }
        #endregion
    }
}
