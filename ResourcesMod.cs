using HarmonyLib;
using System;
using System.Collections;
using UnityEngine;

namespace ResourcesModNS
{
    public class ResourcesMod : Mod
    {
        public static ModLogger L;

//        private static ConfigEntry<bool> includeMetals;
//        private void awake(){}
        public override void Ready()
        {
            L = Logger;
            Logger.Log("Ready!");
        }

        public void Awake()
        {
            Harmony.PatchAll();
        }
    }
}