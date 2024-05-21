using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlvQuestCore
{
    /// <summary>
    /// Виды ивентов Арены
    /// </summary>
    public enum EEvent
    {
        Constant = -1,
        None = 0,
        //Виды ивентов
        StepExecution,
        Death,

        DamageEmitting,
        DamageAccepting,
        DamageBlocking,
        DamageTaking,

        DeltaFireMana,
        DeltaWaterMana,
        DeltaAirMana,
        DeltaEarthMana,

        DeltaXP,
        DeltaHP,
        DeltaGold,
    }
    public enum EDamageType
    {
        None = 0,
        //Виды урона
        PhysicalDamage = ECharacteristic.Strength,
        FireDamage = ECharacteristic.Fire,
        WaterDamage = ECharacteristic.Water,
        AirDamage = ECharacteristic.Air,
        EarthDamage = ECharacteristic.Earth,
    }
    /// <summary>
    /// Виды камней на игровой доске
    /// </summary>
    public enum EStoneType
    {
        None = 0,
        //Виды камней
        Skull = ECharacteristic.Strength,
        Gold = ECharacteristic.Dexterity,
        Experience = ECharacteristic.Endurance,
        FireStone = ECharacteristic.Fire,
        WaterStone = ECharacteristic.Water,
        AirStone = ECharacteristic.Air,
        EarthStone = ECharacteristic.Earth,
    }
    /// <summary>
    /// Указатели для адресации к игроку или противнику игрока
    /// </summary>
    public enum EPlayerType
    {
        None = 0,
        //виды указателей
        Self,
        Enemy,
    }
    /// <summary>
    /// Все характеристики, необходимые для описания параметров персонажа, снаряжения, перков, заклинаний, эффектов и всего прочего
    /// </summary>
    public enum ECharacteristic
    {
        None = 0,
        //характеристики персонажа
        Strength,       //сила
        Dexterity,      //ловкость
        Endurance,      //выносливость
        Fire,           //мастерство огня
        Water,          //мастерство воды
        Air,            //мастерство воздуха
        Earth,          //мастерство земли
    }
    /// <summary>
    /// Всевозможное производные параметры от каждой <see cref="ECharacteristic"/>.
    /// у КАЖДОЙ <see cref="ECharacteristic"/> может быть СВОЙ набор из <see cref="EDerivative"/>
    /// </summary>
    public enum EDerivative
    {
        None = 0,
        //возможные производные характеристик
        Value,          //значение характеристики
        MaxMana,        //максимальный запас маны
        CurrentMana,    //текущий(стартовый) запас маны
        TerminationMult,//мультипликатор эффекта уничтожения камня, связанного с характеристикой производной 
        AddTurnChance,  //шанс доп хода при уничтожении камня, связанного с характеристикой производной
        Resistance,     //сопротивления урону, связанному с этой характеристикой
        MaxHealth,      //максимальный запас здоровья
        CurrentHealth,  //текущий(стартовый) запас здоровья
    }
    /// <summary>
    /// 6 "слотов" в в которые можно одевать снаряжение соответсвующего типа
    /// </summary>
    public enum EBodyPart
    {
        None = 0,
        //Набор слолов под снаряжение 
        Head,           //голова
        Body,           //тело
        Hands,          //руки
        Feet,           //ноги
        Weapon,         //оружие
        Extra,          //экста
    }
    public enum EVariable
    {
        None = 0,
        //Набор переменных для формулы кончеого значения производной
        A0,
        B1,
        B2,
        C1,
        C2,
        D1,
        D2,
    }
    public enum  EManaType
    {
        None = 0,
        //Виды маны в игре
        FireStone = ECharacteristic.Fire,
        WaterStone = ECharacteristic.Water,
        AirStone = ECharacteristic.Air,
        EarthStone = ECharacteristic.Earth,
    }

}
