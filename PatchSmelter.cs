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
    public static class PatchSmelter
    {
        static bool Prefix(Smelter __instance, ref bool __result, CardData otherCard)
        {
            if (otherCard.Id == Cards.flint || otherCard.Id == Cards.charcoal)
            {
//                ResourcesMod.instance.Log("Flint is welcome here."); // don't do this. It gets called alot as soon as you pick up the flint, even from across the board
                __result = true;
                return false;
            }
            return true;
        }
    }
}
