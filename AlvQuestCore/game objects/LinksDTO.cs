namespace AlvQuestCore
{
    public class LinksDTO
    {
        /// <summary>
        /// Ссылка на слот персонажа игрока на текущей арене
        /// </summary>
        public CharacterSlot PlayerCharacterSlot { get; set; }

        /// <summary>
        /// Ссылка на слот персонажа противника на текущей арене
        /// </summary>
        public CharacterSlot EnemyCharacterSlot { get; set; }

        /// <summary>
        /// Ссылка на текущую арену
        /// </summary>
        public Arena CurrentArena { get; set; }
    }
}