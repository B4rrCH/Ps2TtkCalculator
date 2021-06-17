using System.ComponentModel;

namespace Ps2TtkCalculator.Shared.Model
{
    enum ResistanceType
    {
        [Description("Normal (0%)")]
        Normal,
        [Description("Nanoweave Armor (20% body-only)")]
        NanoWeaveArmor,
        [Description("Symbiote (20%)")]
        Symbiote,
        [Description("ResistShield (35%)")]
        ReistShield
    }
}
