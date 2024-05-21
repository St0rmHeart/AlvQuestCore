using System.Text.Json.Serialization;

namespace AlvQuestCore
{
    /// <summary>
    /// Базовый DTO класс для всех DTO версий эффектов
    /// </summary>
    [JsonDerivedType(typeof(PassiveParameterModifier.PPM_DTO), typeDiscriminator: "PPM_DTO")]
    [JsonDerivedType(typeof(TriggerParameterModifier.TPM_DTO), typeDiscriminator: "TPM_DTO")]
    public abstract class BaseEffectDTO : BGO_DTO
    {
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public abstract override BaseEffect RecreateOriginal();
    }
}
