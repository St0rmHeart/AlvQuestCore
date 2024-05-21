namespace AlvQuestCore
{
    public class LinksDTO
    {
        public CharacterSlot PlayerCharacterSlot { get; set; }
        public CharacterSlot EnemyCharacterSlot { get; set; }
        public Arena CurrentArena { get; set; }
        public Equipment CurrentEquipment {  get; set; }
        public Perk CurrentPerk { get; set; }
        public Spell CurrentSpell { get; set; }
    }
}