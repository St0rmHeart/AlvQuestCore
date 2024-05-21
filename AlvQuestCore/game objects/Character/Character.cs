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
        private Dictionary<ECharacteristic, int> _characteristics = new()
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
            get { return _characteristics[characteristic]; }
        }

        /// <summary>
        /// Используемые перки.
        /// </summary>
        private List<Perk> _perks = new();

        /// <summary>
        /// Используемое снаряжение.
        /// </summary>
        private Dictionary<EBodyPart, Equipment> _equipment = new();

        /// <summary>
        /// Итератор по снаряжению персонажа. 
        /// </summary>
        /// <param name="bodyPart"> <see cref='EBodyPart'/> слот снаряжения </param>
        /// <returns><see cref='Equipment'/> объект снаряжения, экиперованный в указанном слоте или <see cref='null'/>, если слот пуст </returns>
        public Equipment this[EBodyPart bodyPart]
        {
            //возвращает предмет снаряжения, либо null если в указанной ячейке ничего не одето
            get
            {
                if (_equipment.TryGetValue(bodyPart, out Equipment value))
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
        private List<Spell> _spells = new();

        /// <summary>
        /// Базовый конструктор персонажа.
        /// </summary>
        /// <param name="name"> Имя персонажа </param>
        /// <param name="description">Описание персонажа </param>
        /// <param name="iconName"> Иконка персонажа</param>
        private Character(string name, string description, string iconName) : base(name, description, iconName) { }

        public override void Installation(LinksDTO linksDTO)
        {
            foreach (Equipment item in _equipment.Values)
            {
                linksDTO.CurrentEquipment = item;
                item.Installation(linksDTO);
            }

            foreach (Perk perk in _perks)
            {
                linksDTO.CurrentPerk = perk;
                perk.Installation(linksDTO);
            }

            foreach (Spell spell in _spells)
            {
                linksDTO.CurrentSpell = spell;
                spell.Installation(linksDTO);
            }

            //throw new NotImplementedException();
        }
        public override void Uninstallation()
        {
            throw new NotImplementedException();
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
                _characteristics = new Dictionary<ECharacteristic, int>(_characteristics),
                _perks = _perks.Select(perk => perk.Clone()).ToList(),
                _equipment = _equipment.ToDictionary(kv => kv.Key, kv => kv.Value.Clone()),
                _spells = _spells.Select(spell => spell.Clone()).ToList()
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
                Characteristics = _characteristics,
                Perks = _perks.Select(perk => perk.GetDTO()).ToList(),
                Equipment = _equipment.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.GetDTO()),
                Spells = _spells.Select(spell => spell.GetDTO()).ToList()
            };
            return characterDTO;
        }
    }
}
