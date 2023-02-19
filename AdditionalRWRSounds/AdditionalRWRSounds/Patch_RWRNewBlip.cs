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
            Debug.Log($"Running RWR Prefix {__instance.newContactBlip.name}");
            Main.instance.oldClip = __instance.newContactBlip;
            if (radarActor.role == Actor.Roles.Air)
            {
                __instance.newContactBlip = Main.instance.newAirContactBlip;
                Debug.Log($"Blip updated to {__instance.newContactBlip.name}");
            }
        }
        public static void Postfix(DashRWR __instance, Actor radarActor)
        {
            __instance.newContactBlip = Main.instance.oldClip;
            Debug.Log($"Clip changed back to default {__instance.newContactBlip.name}");
        }
    }
}