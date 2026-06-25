using System;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using EFT;
using UnityEngine;

namespace NoSprintSensitivityPenalty
{
    [BepInPlugin("com.vinarator.sprintsensitivity", "NoChangeSensitivityWhileSprinting", "1.0.1")]
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
        private static readonly FieldInfo getSensitivityField = AccessTools.Field(typeof(Player), "GetSensitivity");

        [HarmonyPrefix]
        public static void Prefix(Player __instance, ref Vector2 __0, ref bool __1)
        {
            if (__instance.MovementContext != null && __instance.MovementContext.IsSprintEnabled)
            {
                __1 = true;
                if (getSensitivityField != null)
                {
                    var getSensitivity = getSensitivityField.GetValue(__instance) as Func<float>;
                    if (getSensitivity != null)
                    {
                        float sensitivity = getSensitivity();
                        __0.x *= sensitivity;
                        __0.y *= sensitivity;
                    }
                }
            }
        }
    }
}