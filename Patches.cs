using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using System.Collections;
using UnityEngine;


namespace ResourcesModNS
{
    /// <summary>
    /// Add flint to the list of cards a smelter can have on it.
    /// </summary>
    [HarmonyPatch(typeof(Smelter),"CanHaveCard")]
    public static class PatcheSmelter
    {
        static bool Prefix(Smelter __instance, ref bool __result, CardData otherCard)
        {
            if (otherCard.Id == Cards.flint || otherCard.Id == Cards.charcoal)
            {
                __result = true;
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(Well), "CanHaveCard")]
    public static class PatchWell
    {
        static bool Prefix(Well __instance, ref bool __result, CardData otherCard)
        {
            if (otherCard.Id == Cards.water || otherCard.Id == Cards.bottle_of_water || otherCard.Id == Cards.empty_bottle) // || industrial.Contains(__instance.Id) && industrial.Contains(otherCard.Id))
            {
                __result = true;
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(WishingWell), "CanHaveCard")]
    public static class PatchWishingWell
    {
        static bool Prefix(WishingWell __instance, ref bool __result, CardData otherCard)
        {
            if (otherCard.Id == Cards.water || otherCard.Id == Cards.bottle_of_water) // || industrial.Contains(__instance.Id) && industrial.Contains(otherCard.Id))
            {
                __result = true;
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(Building), "CanHaveCard")]
    public static class PatchStorage
    {
        static readonly string[] storage = new string[] { Cards.shed, Cards.lighthouse, Cards.warehouse };
        //static readonly string[] industrial = new string[] { Cards.quarry, Cards.lumbercamp, Cards.sand_quarry, Cards.gold_mine,
        //                                                     Cards.brickyard, Cards.sawmill, Cards.smelter, Cards.smithy };  // doesn't work...

        static bool Prefix(Building __instance, ref bool __result, CardData otherCard)
        {
            if (storage.Contains(__instance.Id) && storage.Contains(otherCard.Id)) // || industrial.Contains(__instance.Id) && industrial.Contains(otherCard.Id))
            {
                __result = true;
                return false;
            }
            return true;
        }
    }
}
