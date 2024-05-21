namespace AlvQuestCore
{
    public partial class PassiveParameterModifier
    {
        /// <summary>
        /// DTO версия класса <see cref="PassiveParameterModifier"/>.
        /// </summary>
        public class PPM_DTO : BaseEffectDTO
        {
            /// <summary>
            /// Список ссылок-кортежей, преобразованных в словари, описывающий все модификации, производимыe объектом
            /// </summary>
            public List<Dictionary<string, string>> Links { get; set; }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = base.GetHashCode();
                    hashCode ^= AlvQuestStatic.GetLinkHashCode(Links);
                    return hashCode;
                }
            }

            public override PassiveParameterModifier RecreateOriginal()
            {
                return new PassiveParameterModifier(
                        name: BaseData.Name,
                        description: BaseData.Description,
                        iconName: BaseData.Icon,
                        links: AlvQuestStatic.DTOConverter.FromDTOImpactLinkList(Links));
            }
        }
    }
}
