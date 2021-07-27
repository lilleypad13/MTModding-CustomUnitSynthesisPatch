using BepInEx;
using HarmonyLib;
using Steamworks;
using System.Collections.Generic;
using Trainworks.Interfaces;
using Trainworks.Managers;

namespace CustomUnitSynthesisPatcher
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess("MonsterTrain.exe")]
    [BepInProcess("MtLinkHandler.exe")]
    [BepInDependency("tools.modding.trainworks")]
    public class CustomUnitSynthesisPatcher : BaseUnityPlugin, IInitializable
    {
        public static CustomUnitSynthesisPatcher Instance { get; private set; }

        public const string GUID = "test.modding.unitsynthesis";
        public const string NAME = "Custom Unit Synthesis Patcher";
        public const string VERSION = "1.0.0";

        private void Awake()
        {
            Instance = this;

            var harmony = new Harmony(GUID);
            harmony.PatchAll();
            Logger.LogInfo($"{NAME} has patched.");
        }

        public void Initialize()
        {
            Logger.LogInfo($"{NAME} initialization began.");

            // Make sure to add CharacterData and CardUpgradeData BEFORE calling CollectMappingData
            ExampleTestUnit.Make();
            ExampleTestCakeUnit.BuildFastFoodCharacter();

            // Calls Monster Train's CollectMappingData method located in UnitSynthesisMapping
            AccessUnitSynthesisMapping.FindUnitSynthesisMappingInstanceToStub();

            Logger.LogInfo($"{NAME} finished initialization.");
        }

        public static void Log(string message)
        {
            Instance.Logger.LogInfo(message);
        }
    }


}
