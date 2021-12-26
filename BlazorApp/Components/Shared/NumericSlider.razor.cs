using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Ps2TtkCalculator.Components.Shared
{
    public partial class NumericSlider<T>
    {
        private T _value;

        [Parameter]
        public T Value { 
            get => _value; 
            set
            {
                if (EqualityComparer<T>.Default.Equals(value, _value))
                {
                    return;
                }
                _value = value;
                ValueChanged.InvokeAsync(_value);
            }
        }

        [Parameter]
        public EventCallback<T> ValueChanged { get; set; }

        [Parameter]
        public RenderFragment Icon { get; set; }

        [Parameter]
        public T Min { get; set; }

        [Parameter]
        public T Max { get; set; }

        [Parameter]
        public T Step { get; set; }

        [Parameter]
        public string Unit { get; set; }

        [Parameter]
        public string Label { get; set; }
    }
}
