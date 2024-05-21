using System.Text.Json.Serialization;

namespace AlvQuestCore
{
    //абстрактный предок всех логических модулей
    public abstract class LogicalModule
    {
        protected CharacterSlot _owner;
        protected CharacterSlot _enemy;
        //StepExecution, Death,
        protected EEvent? _simpleData;
        //DeltaFireMana, DeltaWaterMana, DeltaAirMana, DeltaEarthMana, DeltaXP, DeltaHP, DeltaGold
        protected (EEvent eEvent, double value)? _deltaData;
        //DamageEmitting, DamageAccepting, DamageBlocking, DamageTaking,
        protected (EEvent eEvent, EDamageType damageType, double value)? _damageData;
   
        public void Installation(CharacterSlot owner, CharacterSlot enemy)
        {
            _owner = owner;
            _enemy = enemy;
        }
        public void Uninstallation()
        {
            _owner = null;
            _enemy = null;
        }
        public void SetData(EEvent arg)
        {
            _simpleData = arg;
            _deltaData = null;
            _damageData = null;
        }
        public void SetData((EEvent eEvent, double args) arg)
        {
            _simpleData = null;
            _deltaData = arg;
            _damageData = null;
        }
        public void SetData((EEvent eEvent, EDamageType damageType, double value) arg)
        {
            _simpleData = null;
            _deltaData = null;
            _damageData = arg;
        }
        public abstract bool Result();
        public abstract LogicalModule Clone();
        public abstract LogicalModule_DTO GetDTO();
    }

    [JsonDerivedType(typeof(LM_CONSTANT_TRUE_DTO), typeDiscriminator: "CONSTANT_TRUE")]
    [JsonDerivedType(typeof(LM_01_deltaThreshold_DTO), typeDiscriminator: "deltaThreshold")]
    [JsonDerivedType(typeof(LM_02_damageThreshold_DTO), typeDiscriminator: "damageThreshold")]
    public abstract class LogicalModule_DTO
    {
        public abstract LogicalModule RecreateLogicalModule();
    }

    /// <summary>
    /// Модуль, всегда возвращающий True
    /// </summary>
    public class LM_CONSTANT_TRUE : LogicalModule
    {
        public override bool Result()
        {
            return true;
        }
        public override LogicalModule Clone()
        {
            return new LM_CONSTANT_TRUE();
        }
        public override LM_CONSTANT_TRUE_DTO GetDTO()
        {
            return new LM_CONSTANT_TRUE_DTO();
        }
    }
    public class LM_CONSTANT_TRUE_DTO : LogicalModule_DTO
    {
        public override LogicalModule RecreateLogicalModule()
        {
            return new LM_CONSTANT_TRUE();
        }
    }


    /// <summary>
    /// _deltaData должен содержать value больше, чем _threshold. Иначе возвращается False
    /// </summary>
    public class LM_01_deltaThreshold : LogicalModule
    {
        private readonly double _threshold;
        public LM_01_deltaThreshold(double threshold)
        {
            _threshold = threshold;
        }
        public override bool Result()
        {
            return _deltaData?.value >= _threshold;
        }
        public override LogicalModule Clone()
        {
            return new LM_01_deltaThreshold(_threshold);
        }
        public override LogicalModule_DTO GetDTO()
        {
            var dto = new LM_01_deltaThreshold_DTO
            {
                Threshold = _threshold
            };
            return dto;
        }
    }
    public class LM_01_deltaThreshold_DTO : LogicalModule_DTO
    {
        public double Threshold { get; set; }
        public override LogicalModule RecreateLogicalModule()
        {
            return new LM_01_deltaThreshold(Threshold);
        }
    }

    /// <summary>
    /// _deltaData должен содержать value больше, чем _threshold. Иначе возвращается False
    /// </summary>
    public class LM_02_damageThreshold : LogicalModule
    {
        private readonly EDamageType _damageType;
        private readonly double _threshold;
        public LM_02_damageThreshold(EDamageType damageType, double threshold)
        {
            _damageType = damageType; _threshold = threshold;
        }
        public override bool Result()
        {
            return _damageData?.damageType == _damageType && _damageData?.value >= _threshold;
        }
        public override LogicalModule Clone()
        {
            return new LM_02_damageThreshold(_damageType, _threshold);
        }
        public override LogicalModule_DTO GetDTO()
        {
            var dto = new LM_02_damageThreshold_DTO
            {
                DamageType = _damageType,
                Threshold = _threshold
            };
            return dto;
        }
    }
    public class LM_02_damageThreshold_DTO : LogicalModule_DTO
    {
        public EDamageType DamageType { get; set; }
        public double Threshold { get; set; }
        public LM_02_damageThreshold_DTO() { }
        public override LogicalModule RecreateLogicalModule()
        {
            return new LM_02_damageThreshold(DamageType, Threshold);
        }
    }
}
