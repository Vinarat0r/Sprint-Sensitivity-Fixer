using System;
using BepInEx;
using HarmonyLib;
using EFT;
using UnityEngine;

namespace NoSprintSensitivityPenalty
{
    [BepInPlugin("com.vinarator.sprintsensitivity", "NoChangeSensitivityWhileSprinting", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            new Harmony("com.vinarator.sprintsensitivity").PatchAll();
        }
    }

    [HarmonyPatch(typeof(Player), nameof(Player.Rotate), new Type[] { typeof(Vector2), typeof(bool) })]
    public static class PlayerRotatePatch
    {
        [HarmonyPrefix]
        public static void Prefix(ref bool __1)
        {
            __1 = true;
        }
    }
}