using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlvQuestCore
{
    /// <summary>
    /// Калькулятор производных
    /// </summary>
    public abstract class CalculatorA0
    {
        #region _________________________ПОЛЯ_________________________
        protected readonly Dictionary<ECharacteristic, ValueParameter> _derivativeValueValues;
        #endregion

        #region ______________________КОНСТРУКТОР______________________
        protected CalculatorA0(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
        {
            _derivativeValueValues = derivativeValueValues;
        }
        #endregion

        #region _____________________МЕТОДЫ_____________________
        protected double GetCharacteristicValue(ECharacteristic characteristic)
        {
            return _derivativeValueValues[characteristic].FinalValue;
        }

        /// <summary>
        /// Вычислят актуальное значение <see cref="EVariable.A0"/> указанного <see cref="CommonParameter"/> с учетом актуальных значений всех <see cref="ValueParameter"/>.
        /// </summary>
        /// <param name="derivative">Параметр, значение <see cref="EVariable.A0"/> которого необходимо рассчитать</param>
        /// <returns>Значение <see cref="EVariable.A0"/> переменной.</returns>
        public static CalculatorA0 GetModule(ECharacteristic characteristic, EDerivative derivative, Dictionary<ECharacteristic, ValueParameter> dVV)
        {
            //указание на подключение нужного модуля вычисления
            return (characteristic) switch
            {
                ECharacteristic.Strength => (derivative) switch
                {
                    EDerivative.TerminationMult => new StrengthTerminationMultModule(dVV),
                    EDerivative.AddTurnChance => new StrengthAddTurnChanceModule(dVV),
                    EDerivative.Resistance => new StrengthResistanceModule(dVV),
                    _ => throw new NotImplementedException(),
                },
                ECharacteristic.Dexterity => (derivative) switch
                {
                    EDerivative.TerminationMult => new DexterityTerminationMultModule(dVV),
                    EDerivative.AddTurnChance => new DexterityAddTurnChanceModule(dVV),
                    _ => throw new NotImplementedException(),
                },
                ECharacteristic.Endurance => (derivative) switch
                {
                    EDerivative.TerminationMult => new EnduranceTerminationMultModule(dVV),
                    EDerivative.AddTurnChance => new EnduranceAddTurnChanceModule(dVV),
                    EDerivative.MaxHealth => new EnduranceMaxHealthModule(dVV),
                    EDerivative.CurrentHealth => new EnduranceMaxHealthModule(dVV),
                    _ => throw new NotImplementedException(),
                },
                ECharacteristic.Fire => (derivative) switch
                {
                    EDerivative.TerminationMult => new FireTerminationMultModule(dVV),
                    EDerivative.AddTurnChance => new FireAddTurnChanceModule(dVV),
                    EDerivative.MaxMana => new FireMaxManaModule(dVV),
                    EDerivative.CurrentMana => new FireCurrentManaModule(dVV),
                    EDerivative.Resistance => new FireResistanceModule(dVV),
                    _ => throw new NotImplementedException(),
                },
                ECharacteristic.Water => (derivative) switch
                {
                    EDerivative.TerminationMult => new WaterTerminationMultModule(dVV),
                    EDerivative.AddTurnChance => new WaterAddTurnChanceModule(dVV),
                    EDerivative.MaxMana => new WaterMaxManaModule(dVV),
                    EDerivative.CurrentMana => new WaterCurrentManaModule(dVV),
                    EDerivative.Resistance => new WaterResistanceModule(dVV),
                    _ => throw new NotImplementedException(),
                },
                ECharacteristic.Air => (derivative) switch
                {
                    EDerivative.TerminationMult => new AirTerminationMultModule(dVV),
                    EDerivative.AddTurnChance => new AirAddTurnChanceModule(dVV),
                    EDerivative.MaxMana => new AirMaxManaModule(dVV),
                    EDerivative.CurrentMana => new AirCurrentManaModule(dVV),
                    EDerivative.Resistance => new AirResistanceModule(dVV),
                    _ => throw new NotImplementedException(),
                },
                ECharacteristic.Earth => (derivative) switch
                {
                    EDerivative.TerminationMult => new EarthTerminationMultModule(dVV),
                    EDerivative.AddTurnChance => new EarthAddTurnChanceModule(dVV),
                    EDerivative.MaxMana => new EarthMaxManaModule(dVV),
                    EDerivative.CurrentMana => new EarthCurrentManaModule(dVV),
                    EDerivative.Resistance => new EarthResistanceModule(dVV),
                    _ => throw new NotImplementedException(),
                },
                _ => throw new NotImplementedException(),
            };
        }

        /// <summary>
        ///Производит рассчет <see cref="EVariable.A0"/> для <see cref="CalculateNewA0(CommonParameter)"/>.
        ///Метод переопределён для каждого уникального <see cref="CommonParameter"/>.
        /// </summary>
        public abstract double CalculateA0();
        #endregion
    }





    #region Потомки DerivativesCalculator
    /// <summary>
    /// Производит рассчет <see cref="EVariable.A0"/> параметра MaxHealth
    /// </summary>
    public class EnduranceMaxHealthModule : CalculatorA0
    {
        public EnduranceMaxHealthModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues) : base(derivativeValueValues) { }
        public override double CalculateA0()
        {
            double charValue = GetCharacteristicValue(ECharacteristic.Endurance);
            charValue = 20 + 1.2 * charValue + Math.Pow(charValue, 0.9) * 2;
            return charValue.Round();
        }
    }

    /// <summary>
    /// Производит стандартный рассчет <see cref="EVariable.A0"/> для производной <see cref="EDerivative.AddTurnChance"/>
    /// </summary>
    public abstract class UniversalAddTurnChanceModule : CalculatorA0
    {
        public UniversalAddTurnChanceModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues) : base(derivativeValueValues) { }
        protected ECharacteristic characteristic;
        public override double CalculateA0()
        {
            double charValue = GetCharacteristicValue(characteristic);
            charValue = Math.Pow(charValue, 0.6) / 100;
            return charValue.Round();
        }
    }
    public class StrengthAddTurnChanceModule : UniversalAddTurnChanceModule
    {
        public StrengthAddTurnChanceModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
            : base(derivativeValueValues) { characteristic = ECharacteristic.Strength; }
    }
    public class DexterityAddTurnChanceModule : UniversalAddTurnChanceModule
    {
        public DexterityAddTurnChanceModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
            : base(derivativeValueValues) { characteristic = ECharacteristic.Dexterity; }
    }
    public class EnduranceAddTurnChanceModule : UniversalAddTurnChanceModule
    {
        public EnduranceAddTurnChanceModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
            : base(derivativeValueValues) { characteristic = ECharacteristic.Endurance; }
    }
    public class FireAddTurnChanceModule : UniversalAddTurnChanceModule
    {
        public FireAddTurnChanceModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
            : base(derivativeValueValues) { characteristic = ECharacteristic.Fire; }
    }
    public class WaterAddTurnChanceModule : UniversalAddTurnChanceModule
    {
        public WaterAddTurnChanceModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
            : base(derivativeValueValues) { characteristic = ECharacteristic.Water; }
    }
    public class AirAddTurnChanceModule : UniversalAddTurnChanceModule
    {
        public AirAddTurnChanceModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
            : base(derivativeValueValues) { characteristic = ECharacteristic.Air; }
    }
    public class EarthAddTurnChanceModule : UniversalAddTurnChanceModule
    {
        public EarthAddTurnChanceModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
            : base(derivativeValueValues) { characteristic = ECharacteristic.Earth; }
    }

    /// <summary>
    /// Производит стандартный рассчет <see cref="EVariable.A0"/> для производной <see cref="EDerivative.TerminationMult"/>
    /// </summary>
    public abstract class UniversalTerminationMultModule : CalculatorA0
    {
        public UniversalTerminationMultModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues) : base(derivativeValueValues) { }
        protected ECharacteristic characteristic;
        public override double CalculateA0()
        {
            double charValue = GetCharacteristicValue(characteristic);
            charValue = 1 + Math.Pow(charValue, 0.93) / 100;
            return charValue.Round();
        }
    }
    public class StrengthTerminationMultModule : UniversalTerminationMultModule
    {
        public StrengthTerminationMultModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
            : base(derivativeValueValues) { characteristic = ECharacteristic.Strength; }
    }
    public class DexterityTerminationMultModule : UniversalTerminationMultModule
    {
        public DexterityTerminationMultModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
            : base(derivativeValueValues) { characteristic = ECharacteristic.Dexterity; }
    }
    public class EnduranceTerminationMultModule : UniversalTerminationMultModule
    {
        public EnduranceTerminationMultModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
            : base(derivativeValueValues) { characteristic = ECharacteristic.Endurance; }
    }
    public class FireTerminationMultModule : UniversalTerminationMultModule
    {
        public FireTerminationMultModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
            : base(derivativeValueValues) { characteristic = ECharacteristic.Fire; }
    }
    public class WaterTerminationMultModule : UniversalTerminationMultModule
    {
        public WaterTerminationMultModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
            : base(derivativeValueValues) { characteristic = ECharacteristic.Water; }
    }
    public class AirTerminationMultModule : UniversalTerminationMultModule
    {
        public AirTerminationMultModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
            : base(derivativeValueValues) { characteristic = ECharacteristic.Air; }
    }
    public class EarthTerminationMultModule : UniversalTerminationMultModule
    {
        public EarthTerminationMultModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
            : base(derivativeValueValues) { characteristic = ECharacteristic.Earth; }
    }

    /// <summary>
    /// Производит стандартный рассчет <see cref="EVariable.A0"/> для производной <see cref="EDerivative.Resistance"/>
    /// </summary>
    public abstract class UniversalResistanceModule : CalculatorA0
    {
        public UniversalResistanceModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues) : base(derivativeValueValues) { }
        protected ECharacteristic characteristic;
        public override double CalculateA0()
        {
            double charValue = GetCharacteristicValue(characteristic);
            charValue = Math.Pow(charValue, 0.5) / 100;
            return charValue.Round();
        }
    }
    public class StrengthResistanceModule : UniversalResistanceModule
    {
        public StrengthResistanceModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
            : base(derivativeValueValues) { characteristic = ECharacteristic.Strength; }
    }
    public class FireResistanceModule : UniversalResistanceModule
    {
        public FireResistanceModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
            : base(derivativeValueValues) { characteristic = ECharacteristic.Fire; }
    }
    public class WaterResistanceModule : UniversalResistanceModule
    {
        public WaterResistanceModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
            : base(derivativeValueValues) { characteristic = ECharacteristic.Water; }
    }
    public class AirResistanceModule : UniversalResistanceModule
    {
        public AirResistanceModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
            : base(derivativeValueValues) { characteristic = ECharacteristic.Air; }
    }
    public class EarthResistanceModule : UniversalResistanceModule
    {
        public EarthResistanceModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
            : base(derivativeValueValues) { characteristic = ECharacteristic.Earth; }
    }

    /// <summary>
    /// Производит стандартный рассчет <see cref="EVariable.A0"/> для производной <see cref="EDerivative.MaxMana"/>
    /// </summary>
    public abstract class UniversalMaxManaModule : CalculatorA0
    {
        public UniversalMaxManaModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues) : base(derivativeValueValues) { }
        protected ECharacteristic characteristic;
        public override double CalculateA0()
        {
            double charValue = GetCharacteristicValue(characteristic);
            charValue = 15 + charValue + Math.Pow(charValue, 0.5);
            return charValue.Round();
        }
    }
    public class FireMaxManaModule : UniversalMaxManaModule
    {
        public FireMaxManaModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
            : base(derivativeValueValues) { characteristic = ECharacteristic.Fire; }
    }
    public class WaterMaxManaModule : UniversalMaxManaModule
    {
        public WaterMaxManaModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
            : base(derivativeValueValues) { characteristic = ECharacteristic.Water; }
    }
    public class AirMaxManaModule : UniversalMaxManaModule
    {
        public AirMaxManaModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
            : base(derivativeValueValues) { characteristic = ECharacteristic.Air; }
    }
    public class EarthMaxManaModule : UniversalMaxManaModule
    {
        public EarthMaxManaModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
            : base(derivativeValueValues) { characteristic = ECharacteristic.Earth; }
    }

    /// <summary>
    /// Производит стандартный рассчет <see cref="EVariable.A0"/> для производной <see cref="EDerivative.CurrentMana"/>
    /// </summary>
    public abstract class UniversalCurrentManaModule : CalculatorA0
    {
        public UniversalCurrentManaModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues) : base(derivativeValueValues) { }
        protected ECharacteristic characteristic;
        public override double CalculateA0()
        {
            double charValue = GetCharacteristicValue(characteristic);
            charValue = charValue / 10 + Math.Pow(charValue, 0.6);
            return charValue.Round();
        }
    }
    public class FireCurrentManaModule : UniversalCurrentManaModule
    {
        public FireCurrentManaModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
            : base(derivativeValueValues) { characteristic = ECharacteristic.Fire; }
    }
    public class WaterCurrentManaModule : UniversalCurrentManaModule
    {
        public WaterCurrentManaModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
            : base(derivativeValueValues) { characteristic = ECharacteristic.Water; }
    }
    public class AirCurrentManaModule : UniversalCurrentManaModule
    {
        public AirCurrentManaModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
            : base(derivativeValueValues) { characteristic = ECharacteristic.Air; }
    }
    public class EarthCurrentManaModule : UniversalCurrentManaModule
    {
        public EarthCurrentManaModule(Dictionary<ECharacteristic, ValueParameter> derivativeValueValues)
            : base(derivativeValueValues) { characteristic = ECharacteristic.Earth; }
    }
    #endregion
}
