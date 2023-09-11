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
                TooltipTerm = "resourcesmod_tweak_goldmine_tooltip",
            });
        }

        private ConfigEntry<bool> modifyGoldMine;

        public override void Ready()
        {
            Config.OnSave += MinorBalanceTweaks;
            WorldManager.instance.actionTimeBases.Add(new ActionTimeBase((ActionTimeParams p) =>
                p.villager.Id == Cards.lumberjack && p.baseCard.CardsInStackMatchingPredicate(card => card.Id == Cards.wood || card.Id == Cards.stick).Any(), 0.5f));
            MinorBalanceTweaks();
            Logger.Log("Ready!");
        }

        public void MinorBalanceTweaks()
        {
            CardData cardData = WorldManager.instance.GameDataLoader.GetCardFromId(Cards.gold_mine);
            if (cardData is CombatableHarvestable ch)
            {
                CardBag bag = ch.MyCardBag;
                CardChance cc = bag.Chances.FirstOrDefault(x => x.Id == Cards.gold_ore);
                cc.Chance = modifyGoldMine.Value ? 3 : 1;
                float num = bag.Chances.Sum(x => (float)x.Chance);
                foreach (CardChance x in bag.Chances)
                {
                    x.PercentageChance = x.Chance / num;
                }
                Log($"Goldmine {cc.Chance}, {cc.PercentageChance}");
            }
        }
    }
}