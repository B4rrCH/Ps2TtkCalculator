using System.ComponentModel;

namespace Ps2TtkCalculator.Shared.Model
{
    enum HealthType
    {
        [Description("Normal (1000)")]
        Normal,
        [Description("NMG/Adren Shield (1425)")]
        NanomeshGenerator,
        [Description("Infiltrator (900)")]
        Infiltrator
    }
}
