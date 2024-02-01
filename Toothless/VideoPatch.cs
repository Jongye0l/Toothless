using System;
using HarmonyLib;
using MonsterLove.StateMachine;

namespace Toothless {
    [HarmonyPatch]
    public class VideoPatch {
        [HarmonyPatch(typeof(StateBehaviour), "ChangeState", typeof(Enum))]
        [HarmonyPostfix]
        public static void OnChangeState(Enum newState) {
            if((States) newState == States.Fail2) VideoPlayerController.Show();
            else VideoPlayerController.Hide();
        }
        
        [HarmonyPatch(typeof(scrController), "TogglePauseGame")]
        [HarmonyPostfix]
        public static void ExitPlay() {
            VideoPlayerController.Hide();
        }
    }
}