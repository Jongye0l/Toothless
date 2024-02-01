using System.IO;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using UnityModManagerNet;

namespace Toothless {
    public class Main {
        public static UnityModManager.ModEntry.ModLogger Logger;
        public static UnityModManager.ModEntry ModEntry;
        private static Harmony _harmony;
        public static bool Enabled;
        private static Assembly _assembly;
        public static AssetBundle AssetBundle;

        public static void Setup(UnityModManager.ModEntry modEntry) {
            Logger = modEntry.Logger;
            ModEntry = modEntry;
            modEntry.OnToggle = OnToggle;
            _assembly = Assembly.GetExecutingAssembly();
            AssetBundle = AssetBundle.LoadFromFile(Path.Combine(modEntry.Path, "shaderbundle"));
        }

        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool value) {
            Enabled = value;
            if(value) {
                _harmony = new Harmony(modEntry.Info.Id);
                _harmony.PatchAll(_assembly);
            } else _harmony.UnpatchAll(modEntry.Info.Id);
            return true;
        }
    }
}