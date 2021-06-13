using System.ComponentModel;

namespace Ps2TtkCalculator.Shared.Model
{
    public enum Faction
    {
        [Description("Nanite Systems")]
        NaniteSystems = 0,
        [Description("Vanu Sovergnity")]
        VanuSovergnity = 1,
        [Description("New Conglomerate")]
        NewConglomerate = 2,
        [Description("Terran Republic")]
        TerranRepublic = 3,
        [Description("Nanite Systems Operatives")]
        NaniteSystemsOperatives = 4
    }
}