namespace AlvQuestCore
{
    public partial class Perk
    {
        public class PerkBuilder : BEO_Builder<PerkBuilder, Perk, PerkDTO>
        {
            public PerkBuilder() : base() { }
            protected override void ResetAdditionalData()
            {
                base.ResetAdditionalData();
            }
            protected override void ValidateAdditionalData()
            {
                base.ValidateAdditionalData();
            }
        }
    }
}
