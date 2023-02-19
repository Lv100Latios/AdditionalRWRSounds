using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Harmony;
using UnityEngine.Networking;

namespace AdditionalRWRSounds
{
    public class Main : VTOLMOD
    {
        public AudioClip oldClip;
        public AudioClip newAirContactBlip;
        public static Main instance;
        // This method is run once, when the Mod Loader is done initialising this game object
        public override void ModLoaded()
        {
            //This is an event the VTOLAPI calls when the game is done loading a scene
            VTOLAPI.SceneLoaded += SceneLoaded;
            base.ModLoaded();
            var harmonyInstance = HarmonyInstance.Create("Alice.RWRAirBlip");
            harmonyInstance.PatchAll();
            instance = this;
            StartCoroutine(LoadAudioFile());
        }
        IEnumerator LoadAudioFile()
        {
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip($"{ModFolder}\\newAirContactBlip.wav", AudioType.WAV))
            {
                yield return www.Send();

                if (www.isNetworkError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    newAirContactBlip = DownloadHandlerAudioClip.GetContent(www);
                }
            }
        }
        //This function is called every time a scene is loaded. this behaviour is defined in Awake().
        private void SceneLoaded(VTOLScenes scene)
        {
            //If you want something to happen in only one (or more) scenes, this is where you define it.

            //For example, lets say you're making a mod which only does something in the ready room and the loading scene. This is how your code could look:
            switch (scene)
            {
                case VTOLScenes.ReadyRoom:
                    //Add your ready room code here
                    break;
                case VTOLScenes.LoadingScene:
                    //Add your loading scene code here
                    break;
            }
        }
    }
}