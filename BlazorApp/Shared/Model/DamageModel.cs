using Microsoft.AspNetCore.Components;

namespace Ps2TtkCalculator.Shared.Model
{
    public class DamageModel
    {
        private int minDamageRange_m = 65;
        private int maxDamageRange_m = 10;
        private int minDamage = 112;
        private int maxDamage = 143;
        public int MaxDamage
        {
            get => maxDamage;
            set
            {
                if (maxDamage != value)
                {
                    maxDamage = value;
                    PropertiesChanged.InvokeAsync();
                }
            }
        }
        public int MinDamage
        {
            get => minDamage;
            set
            {
                if (minDamage != value)
                {
                    minDamage = value;
                    PropertiesChanged.InvokeAsync();
                }
            }
        }
        public int MaxDamageRange_m
        {
            get => maxDamageRange_m;
            set
            {
                if (maxDamageRange_m != value)
                {
                    maxDamageRange_m = value;
                    PropertiesChanged.InvokeAsync();
                }
            }
        }
        public int MinDamageRange_m
        {
            get => minDamageRange_m;
            set
            {
                if (minDamageRange_m != value)
                {
                    minDamageRange_m = value;
                    PropertiesChanged.InvokeAsync();
                }
            }
        }
        public EventCallback PropertiesChanged { get; set; }
    }
}