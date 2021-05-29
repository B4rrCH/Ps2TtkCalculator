using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Ps2TtkCalculator.Shared.Dto
{
    public class ItemList
    {
        [JsonPropertyName("item_list")]
        public List<Item> Items { get; set; }

        [JsonPropertyName("returned")]
        public int Returned { get; set; }
    }
}