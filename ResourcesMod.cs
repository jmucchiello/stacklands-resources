using HarmonyLib;
using System;
using System.Collections;
using UnityEngine;

namespace ResourcesModNS
{
    public class ResourcesMod : Mod
    {
        public static ResourcesMod instance;

        public override void Ready()
        {
            instance = this;
            Logger.Log("Ready!");
        }

        public void Awake()
        {
            Harmony.PatchAll();
        }

        public void Log(string message)
        {
            Logger.Log(message);
        }
    }
}