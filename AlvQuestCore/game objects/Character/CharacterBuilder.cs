namespace AlvQuestCore
{
    public partial class Character
    {
        /// <summary>
        /// 
        /// </summary>
        public class CharacterBuilder : BGO_Builder<CharacterBuilder, Character, CharacterDTO>
        {
            public CharacterBuilder() : base() { }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="level"></param>
            /// <returns></returns>
            public CharacterBuilder SetLevel(int level)
            {
                _objectData.Level = level;
                return this;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="xp"></param>
            /// <returns></returns>
            public CharacterBuilder SetXp(int xp)
            {
                _objectData.Xp = xp;
                return this;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="gold"></param>
            /// <returns></returns>
            public CharacterBuilder SetGold(int gold)
            {
                _objectData.Gold = gold;
                return this;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="charPoints"></param>
            /// <returns></returns>
            public CharacterBuilder SetCharPoints(int charPoints)
            {
                _objectData.CharPoints = charPoints;
                return this;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="characteristic"></param>
            /// <param name="value"></param>
            /// <returns></returns>
            public CharacterBuilder SetCharacteristic(ECharacteristic characteristic, int value)
            {
                _objectData.Characteristics[characteristic] = value;
                return this;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="perk"></param>
            /// <returns></returns>
            public CharacterBuilder SetPerk(Perk perk)
            {
                _objectData.Perks.Add(perk.GetDTO());
                return this;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="equipment"></param>
            /// <returns></returns>
            public CharacterBuilder SetEquipment(Equipment equipment)
            {
                _objectData.Equipment[equipment.BodyPart] = equipment.GetDTO();
                return this;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="spell"></param>
            /// <returns></returns>
            public CharacterBuilder SetSpell(Spell spell)
            {
                _objectData.Spells.Add(spell.GetDTO());
                return this;
            }

            /// <summary>
            /// 
            /// </summary>
            protected override void ResetAdditionalData()
            {
                _objectData.Level = 1;
                _objectData.Xp = 0;
                _objectData.Gold = 0;
                _objectData.CharPoints = 0;
                _objectData.Characteristics = new()
                {
                    {ECharacteristic.Strength, 0},
                    {ECharacteristic.Endurance, 0},
                    {ECharacteristic.Dexterity, 0},
                    {ECharacteristic.Fire, 0},
                    {ECharacteristic.Water, 0},
                    {ECharacteristic.Air, 0},
                    {ECharacteristic.Earth, 0},
                };
                _objectData.Perks = new();
                _objectData.Equipment = new();
                _objectData.Spells = new();
            }

            protected override void ValidateAdditionalData()
            {
                // Так как все параметры, устанавливаемые в ResetAdditionalData() являются возможными - валидация не требуется
            }
        }
    }
}
