using Microsoft.AspNetCore.Components;

namespace Ps2TtkCalculator.Shared.Model
{
    public class Player
    {
        private Weapon weapon = new();
        private Target target = new();
        private Shooter shooter = new();
        private EventCallback propertiesChanged;

        public Weapon Weapon
        {
            get => weapon;
            set
            {
                if (weapon != value)
                {
                    weapon = value;
                    weapon.PropertiesChanged = this.PropertiesChanged;
                    PropertiesChanged.InvokeAsync();
                }
            }
        }
        public Target Target
        {
            get => target;
            set
            {
                if (target != value)
                {
                    target = value;
                    target.PropertiesChanged = this.PropertiesChanged;
                    PropertiesChanged.InvokeAsync();
                }
            }
        }
        public Shooter Shooter
        {
            get => shooter;
            set
            {
                if (shooter != value)
                {
                    shooter = value;
                    shooter.PropertiesChanged = this.PropertiesChanged;
                    PropertiesChanged.InvokeAsync();
                }
            }
        }
        public EventCallback PropertiesChanged
        {
            get => propertiesChanged;
            set
            {
                propertiesChanged = value;
                Weapon.PropertiesChanged = value;
                Target.PropertiesChanged = value;
                Shooter.PropertiesChanged = value;
            }
        }
    }
}