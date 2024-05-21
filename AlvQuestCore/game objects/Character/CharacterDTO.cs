namespace AlvQuestCore
{
    public partial class Character
    {
        /// <summary>
        /// DTO версия <see cref='Character'/>.
        /// </summary>
        public class CharacterDTO : BGO_DTO
        {
            /// <summary>
            /// Уровень.
            /// </summary>
            public int Level { get; set; }

            /// <summary>
            /// Накопленное количество опыта.
            /// </summary>
            public int Xp { get; set; }

            /// <summary>
            /// Накопленное количество золота.
            /// </summary>
            public int Gold { get; set; }

            /// <summary>
            /// Количество неиспользованных очков характеристик.
            /// </summary>
            public int CharPoints { get; set; }

            /// <summary>
            /// Базовые значения характеристик.
            /// </summary>
            public Dictionary<ECharacteristic, int> Characteristics { get; set; }

            /// <summary>
            /// Используемые перки.
            /// </summary>
            public List<Perk.PerkDTO> Perks { get; set; }

            /// <summary>
            /// Используемое снаряжение.
            /// </summary>
            public Dictionary<EBodyPart, Equipment.EquipmentDTO> Equipment { get; set; }

            /// <summary>
            /// Используемые заклинания.
            /// </summary>
            public List<Spell.SpellDTO> Spells { get; set; }

            public override Character RecreateOriginal()
            {
                var character = new Character(
                    name: BaseData.Name,
                    description: BaseData.Description,
                    iconName: BaseData.Icon)
                {
                    Level = Level,
                    Xp = Xp,
                    Gold = Gold,
                    CharPoints = CharPoints,
                    _characteristics = new Dictionary<ECharacteristic, int>(Characteristics),
                    _perks = Perks.Select(perkDTO => perkDTO.RecreateOriginal()).ToList(),
                    _equipment = Equipment.ToDictionary(kv => kv.Key, kv => kv.Value.RecreateOriginal()),
                    _spells = Spells.Select(spellDTO => spellDTO.RecreateOriginal()).ToList()
                };
                return character;
            }
        }
    }
}
