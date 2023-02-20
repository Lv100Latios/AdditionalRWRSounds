using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Harmony;
using static TreePopulator;
using UnityEngine;

namespace AdditionalRWRSounds
{
    [HarmonyPatch(typeof(DashRWR), nameof(DashRWR.RadarPing))]
    public class Patch_RWRNewBlip
    {
        public static void Prefix(DashRWR __instance, Actor radarActor)
        {
            Main.instance.oldClip = __instance.newContactBlip;
            if (radarActor.role == Actor.Roles.Air)
            {
                __instance.newContactBlip = Main.instance.newAirContactBlip;
                Debug.Log($"Blip updated to Air blip.");
            }
        }
        public static void Postfix(DashRWR __instance)
        {
            if (__instance.newContactBlip != Main.instance.oldClip)
            {
                __instance.newContactBlip = Main.instance.oldClip;
                Debug.Log($"Blip changed back to default.");
            }
        }
    }
}