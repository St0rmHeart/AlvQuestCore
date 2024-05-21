using System.Runtime.InteropServices;

namespace AlvQuestCore
{
    public partial class Spell
    {
        public class SpellBuilder : BEO_Builder<SpellBuilder, Spell, SpellDTO>
        {
            public SpellBuilder() : base() { }
            public SpellBuilder SetManaCost(EManaType manaType, double value)
            {
                _objectData.ManaCost[manaType] = value;
                return this;
            }
            protected override void ResetAdditionalData()
            {
                base.ResetAdditionalData();
                _objectData.ManaCost = new();
            }
            protected override void ValidateAdditionalData()
            {
                base.ValidateAdditionalData();
                if (_objectData.ManaCost.Count == 0) throw new ArgumentException("Не задана стоимость маны.");
            }
        }
    }
}
