using Microsoft.AspNetCore.Components;

namespace Ps2TtkCalculator.Shared.Model
{
    public class Target
    {
        private int maxHp = 1000;
        private double resistanceHeadshot = 0.0;
        private double resistanceBodyshot = 0.0;

        public int MaxHp
        {
            get => maxHp;
            set
            {
                if (maxHp != value)
                {
                    maxHp = value;
                    PropertiesChanged.InvokeAsync();
                }
            }
        }
        public double ResistanceHeadshot
        {
            get => resistanceHeadshot;
            set
            {
                if (resistanceHeadshot != value)
                {
                    resistanceHeadshot = value;
                    PropertiesChanged.InvokeAsync();
                }
            }
        }
        public double ResistanceBodyshot
        {
            get => resistanceBodyshot;
            set
            {
                if (resistanceBodyshot != value)
                {
                    resistanceBodyshot = value;
                    PropertiesChanged.InvokeAsync();
                }
            }
        }
        public EventCallback PropertiesChanged { get; set; }
    }
}