using HarmonyLib;
using System;
using System.Collections;
using UnityEngine;

namespace ResourcesModNS
{
    public class ResourcesMod : Mod
    {
        public static ResourcesMod instance;

        public static void Log(string msg) => instance.Logger.Log(msg);
        public static void LogError(string msg) => instance.Logger.LogError(msg);

        public void Awake()
        {
            instance = this;
            Harmony.PatchAll();
            modifyGoldMine = new ConfigEntry<bool>("resourcesmod_config_goldmine", Config, true, new ConfigUI()
            {
                NameTerm = "resourcesmod_tweak_goldmine",
                TooltipTerm = "resourcesmod_tweak_goldmin_tooltip",
            });
        }

        private ConfigEntry<bool> modifyGoldMine;

        public override void Ready()
        {
            Config.OnSave += MinorBalanceTweaks;
            WorldManager.instance.actionTimeBases.Add(new ActionTimeBase((ActionTimeParams p) =>
                p.villager.Id == Cards.lumberjack && p.baseCard.CardsInStackMatchingPredicate(card => card.Id == Cards.wood || card.Id == Cards.stick).Any(), 0.5f));
            Logger.Log("Ready!");
        }

        public void MinorBalanceTweaks()
        {
            CardData cardData = WorldManager.instance.GameDataLoader.GetCardFromId(Cards.gold_mine);
            if (cardData is Harvestable h)
            {
                h.MyCardBag.Chances.FirstOrDefault(x => x.Id == Cards.gold_ore).Chance = modifyGoldMine.Value ? 3 : 1;
            }
        }
    }
}