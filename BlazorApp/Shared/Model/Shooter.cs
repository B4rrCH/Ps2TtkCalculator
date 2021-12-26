
using Microsoft.AspNetCore.Components;

namespace Ps2TtkCalculator.Shared.Model
{
    public class Shooter
    {
        private double accuracy = 1.0;
        private double headshotRatio = 0.0;

        public double Accuracy
        {
            get => accuracy;
            set
            {
                if (accuracy != value)
                {
                    accuracy = value;
                    PropertiesChanged.InvokeAsync();
                }
            }
        }
        public double HeadshotRatio
        {
            get => headshotRatio;
            set
            {
                if (headshotRatio != value)
                {
                    headshotRatio = value;
                    PropertiesChanged.InvokeAsync();
                }
            }
        }
        public EventCallback PropertiesChanged { get; set; }
    }
}