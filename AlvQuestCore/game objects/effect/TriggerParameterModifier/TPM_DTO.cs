namespace AlvQuestCore
{
    public partial class TriggerParameterModifier
    {
        /// <summary>
        /// DTO версия класса <see cref="TriggerParameterModifier"/>
        /// </summary>
        public class TPM_DTO : BaseEffectDTO
        {
            /// <summary>
            /// Логический модуль определяющий реагировать ли при срабатывания событий-триггеров
            /// </summary>
            public LogicalModule_DTO TriggerLogicalModule_DTO { get; set; }

            /// <summary>
            /// Логический модуль определяющий реагировать ли при срабатывания событий-тиков
            /// </summary>
            public LogicalModule_DTO TickLogicalModule_DTO { get; set; }

            /// <summary>
            /// Длительность эффекта в тиках
            /// </summary>
            public int Duration { get; set; }
            /// <summary>
            /// Максимальное накапливаемое количество складываний эффекта
            /// </summary>
            public int MaxStack { get; set; }

            /// <summary>
            /// Список событий, считающихся триггерами
            /// </summary>
            public List<Dictionary<string, string>> TriggerEvents { get; set; } = new();

            /// <summary>
            /// Список событий, считающихся тиками
            /// </summary>
            public List<Dictionary<string, string>> TickEvents { get; set; } = new();

            /// <summary>
            /// Сылки, указывающие как проводить модификации
            /// </summary>
            public List<Dictionary<string, string>> Links { get; set; } = new();

            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = base.GetHashCode();
                    hashCode ^= TriggerLogicalModule_DTO.GetHashCode();
                    hashCode ^= TickLogicalModule_DTO.GetHashCode();
                    hashCode ^= Duration.GetHashCode();
                    hashCode ^= MaxStack.GetHashCode();
                    hashCode ^= AlvQuestStatic.GetLinkHashCode(TriggerEvents);
                    hashCode ^= AlvQuestStatic.GetLinkHashCode(TickEvents);
                    hashCode ^= AlvQuestStatic.GetLinkHashCode(Links);
                    return hashCode;
                }
            }

            public override TriggerParameterModifier RecreateOriginal()
            {
                return new TriggerParameterModifier(
                    name: BaseData.Name,
                    description: BaseData.Description,
                    iconName: BaseData.Icon,
                    triggerlogicalModule: TriggerLogicalModule_DTO.RecreateLogicalModule(),
                    ticklogicalModule: TickLogicalModule_DTO.RecreateLogicalModule(),
                    duration: Duration,
                    maxStack: MaxStack,
                    links: AlvQuestStatic.DTOConverter.FromDTOImpactLinkList(Links),
                    triggerEvents: AlvQuestStatic.DTOConverter.FromDTOEventLinkList(TriggerEvents),
                    tickEvents: AlvQuestStatic.DTOConverter.FromDTOEventLinkList(TickEvents));
            }
        }
    }
}
