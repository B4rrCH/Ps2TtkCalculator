
using Microsoft.AspNetCore.Components;

namespace Ps2TtkCalculator.Shared.Model
{
    public class Shooter
    {
        private double acc = 1.0;
        private double hsr = 0.0;

        public double Acc
        {
            get => acc;
            set
            {
                if (acc != value)
                {
                    acc = value;
                    PropertiesChanged.InvokeAsync();
                }
            }
        }
        public double Hsr
        {
            get => hsr;
            set
            {
                if (hsr != value)
                {
                    hsr = value;
                    PropertiesChanged.InvokeAsync();
                }
            }
        }
        public EventCallback PropertiesChanged { get; set; }
    }
}