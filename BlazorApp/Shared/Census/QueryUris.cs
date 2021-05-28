using System;
using System.Linq;
using Ps2TtkCalculator.Shared.Model;

namespace Ps2TtkCalculator.Shared.Census
{
    public static class QueryUris
    {
        private static readonly Uri baseCensusUri = new Uri("https://census.daybreakgames.com/s:b4rr/get/ps2:v2/");

        public static Uri GetWeaponListRequestUri()
        {
            string baseFilter = "item?item_category_id=" + string.Join(",", Enum.GetValues<WeaponCategory>().Cast<int>());
            string langFilter = "c:lang=en";
            string lengthFilter = "c:limit=1000";
            string showFilter = "c:show=item_id,item_category_id,name,faction_id,image_path";

            string fullFilter = $"{baseFilter}&{langFilter}&{lengthFilter}&{showFilter}";

            return new Uri(baseCensusUri, fullFilter);
        }

        public static Uri GetWeaponFullRequestUri(int itemId)
        {
            string baseFilter = $"item?item_id={itemId}";
            string langFilter = "c:lang=en";
            string lengthFilter = "c:limit=1000";
            string showFilter = "c:show=item_id,item_category_id,name,faction_id,image_path";
            string baseQuery = $"{baseFilter}&{langFilter}&{lengthFilter}&{showFilter}";

            string fireMode = "c:join=fire_mode^show:fire_mode_id'pellets_per_shot'muzzle_velocity^inject_at:fire_mode";
            string fireMode2 = "(fire_mode_2^show:max_damage'max_damage_range'min_damage'min_damage_range'damage_head_multiplier'fire_refire_ms^inject_at:fire_mode_2";
            string fireGroup = "fire_group_to_fire_mode^on:fire_mode_id^to:fire_mode_id(fire_group^on:fire_group_id^to:fire_group_id^show:chamber_duration_ms";
            string ammo = "c:join = weapon_datasheet^show:clip_size^inject_at:ammo";

            string deepQuery = $"{fireMode}({fireMode2},{fireGroup})&{ammo}";
            string fullQuery = $"{baseQuery}&{deepQuery}";

            return new Uri(baseCensusUri, fullQuery);
        }
    }
}
