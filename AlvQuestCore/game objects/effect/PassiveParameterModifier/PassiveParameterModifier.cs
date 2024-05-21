namespace AlvQuestCore
{
    /// <summary>
    /// Эффект, единожды модифицирующий набор указанных переменных в указанных параметрах в начале сражения.
    /// </summary>
    public partial class PassiveParameterModifier : BaseEffect
    {
        /// <summary>
        /// Сылки, указывающие как проводить модификации:
        /// <br /><see cref="EPlayerType"/> <c>target</c> - цель воздействия;
        /// <br /><see cref="ECharacteristic"/> <c>characteristic</c> - характеристика воздействия;
        /// <br /><see cref="EDerivative"/> <c>derivative</c> - производная воздействия;
        /// <br /><see cref="EVariable"/> <c>variable</c> - переменная воздействия;
        /// <br /><see cref="double"/> <c>value</c> - величина воздействия.
        /// </summary>
        private readonly List<(EPlayerType target, ECharacteristic characteristic, EDerivative derivative, EVariable variable, double value)> _links;

        /// <summary>
        /// Сандартный конструктор <see cref="PassiveParameterModifier"/>.
        /// </summary>
        /// <param name="name"> Имя эффекта </param>
        /// <param name="description"> Название эффекта </param>
        /// <param name="iconName"> Иконка эффекта </param>
        /// <param name="links"> Список указывающих ссылок </param>
        private PassiveParameterModifier(
            string name,
            string description,
            string iconName,
            List<(EPlayerType, ECharacteristic, EDerivative, EVariable, double)> links) : base(name, description, iconName)
        {
            _links = links;
        }

        public override void Installation(LinksDTO linksDTO)
        {
            CharacterSlot owner = linksDTO.PlayerCharacterSlot;
            CharacterSlot enemy = linksDTO.EnemyCharacterSlot;
            for (int i = 0; i < _links.Count; i++)
            {
                var link = _links[i];
                var target = (link.target == EPlayerType.Self) ? owner : enemy;
                var currentParameter = target.Data[link.characteristic][link.derivative];
                currentParameter.ChangeVariable(link.variable, link.value);
            }
        }

        public override void Uninstallation()
        {
            //никаких действий не требуется, так как объект не формирует никаких связей в методе Installation
        }

        public override PassiveParameterModifier Clone()
        {
            return new PassiveParameterModifier(
                name: Name,
                description: Description,
                iconName: Icon,
                new List<(EPlayerType, ECharacteristic, EDerivative, EVariable, double)>(_links));
        }

        public override PPM_DTO GetDTO()
        {
            var dto = new PPM_DTO
            {
                BaseData = GetBaseData(),
                Links = AlvQuestStatic.DTOConverter.ToDTOImpactLinkList(_links)
            };
            return dto;
        }
    }
}
