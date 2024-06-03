using System.Drawing;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;

namespace AlvQuestCore
{
    /// <summary>
    /// Класс, в хором хранится вся информация, определющая персонажа
    /// </summary>
    public partial class Character : BaseGameObject
    {
        /// <summary>
        /// Уровень.
        /// </summary>
        public int Level { get; private set; } = 1;

        /// <summary>
        /// Накопленное количество опыта.
        /// </summary>
        public int Xp { get; set; } = 0;

        /// <summary>
        /// Количество неиспользованных очков характеристик.
        /// </summary>
        public int CharPoints { get; private set; } = 0;

        /// <summary>
        /// Накопленное количество золота.
        /// </summary>
        public int Gold { get; set; } = 0;

        /// <summary>
        /// Базовые значения характеристик.
        /// </summary>
        public Dictionary<ECharacteristic, int> Characteristics { get; private set; } = new()
        {
            {ECharacteristic.Strength, 0},
            {ECharacteristic.Endurance, 0},
            {ECharacteristic.Dexterity, 0},
            {ECharacteristic.Fire, 0},
            {ECharacteristic.Water, 0},
            {ECharacteristic.Air, 0},
            {ECharacteristic.Earth, 0},
        };

        /// <summary>
        /// Итератор по характеристикам персонажа. 
        /// </summary>
        /// <param name="characteristic"> <see cref='ECharacteristic'/> характеристика </param>
        /// <returns> <see cref='int'/> значение указанной характеристики. </returns>
        public int this[ECharacteristic characteristic]
        {
            get { return Characteristics[characteristic]; }
        }

        /// <summary>
        /// Используемые перки.
        /// </summary>
        public List<Perk> Perks { get; private set; } = new();

        /// <summary>
        /// Используемое снаряжение.
        /// </summary>
        public Dictionary<EBodyPart, Equipment> Equipments { get; private set; } = new();

        /// <summary>
        /// Итератор по снаряжению персонажа. 
        /// </summary>
        /// <param name="bodyPart"> <see cref='EBodyPart'/> слот снаряжения </param>
        /// <returns><see cref='Equipment'/> объект снаряжения, экиперованный в указанном слоте или <see cref='null'/>, если слот пуст </returns>
        public Equipment this[EBodyPart bodyPart]
        {
            //возвращает предмет саряжения, либо null если в указанной ячейке ничего не одето
            get
            {
                if (Equipments.TryGetValue(bodyPart, out Equipment value))
                {
                    return value;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Используемые заклинания.
        /// </summary>
        public List<Spell> Spells { get; private set; } = new();

        /// <summary>
        /// Базовый конструктор персонажа.
        /// </summary>
        /// <param name="name"> Имя персонажа </param>
        /// <param name="description">Описание персонажа </param>
        /// <param name="iconName"> Иконка персонажа</param>
        private Character(string name, string description, string iconName) : base(name, description, iconName) { }

        public override void Installation(LinksDTO linksDTO)
        {
            foreach (Equipment item in Equipments.Values)
            {
                item.Installation(linksDTO);
            }

            foreach (Perk perk in Perks)
            {
                perk.Installation(linksDTO);
            }

            foreach (Spell spell in Spells)
            {
                spell.Installation(linksDTO);
            }
        }
        public override void Uninstallation()
        {
            foreach (Equipment item in Equipments.Values)
            {
                item.Uninstallation();
            }

            foreach (Perk perk in Perks)
            {
                perk.Uninstallation();
            }

            foreach (Spell spell in Spells)
            {
                spell.Uninstallation();
            }
        }
        public override Character Clone()
        {
            var character = new Character(
                name: Name,
                description: Description,
                iconName: Icon
            )
            {
                Level = Level,
                Xp = Xp,
                Gold = Gold,
                CharPoints = CharPoints,
                Characteristics = new Dictionary<ECharacteristic, int>(Characteristics),
                Perks = Perks.Select(perk => perk.Clone()).ToList(),
                Equipments = Equipments.ToDictionary(kv => kv.Key, kv => kv.Value.Clone()),
                Spells = Spells.Select(spell => spell.Clone()).ToList()
            };

            return character;
        }
        public override CharacterDTO GetDTO()
        {
            var characterDTO = new CharacterDTO
            {
                BaseData = GetBaseData(),
                Level = Level,
                Xp = Xp,
                Gold = Gold,
                CharPoints = CharPoints,
                Characteristics = Characteristics,
                Perks = Perks.Select(perk => perk.GetDTO()).ToList(),
                Equipment = Equipments.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.GetDTO()),
                Spells = Spells.Select(spell => spell.GetDTO()).ToList()
            };
            return characterDTO;
        }
    }
}
