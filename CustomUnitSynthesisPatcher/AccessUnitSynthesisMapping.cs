using BepInEx;
using HarmonyLib;
using Trainworks.Managers;
using System;

namespace CustomUnitSynthesisPatcher
{
    class AccessUnitSynthesisMapping
    {
        public static void FindUnitSynthesisMappingInstanceToStub()
        {
            // Gets a reference to AllGameData with Trainworks
            AllGameData testData = ProviderManager.SaveManager.GetAllGameData();
            CustomUnitSynthesisPatcher.Log("Got reference to AllGameData: " + testData.name);

            // Use AllGameData to get access to BalanceData
            BalanceData balanceData = testData.GetBalanceData();
            CustomUnitSynthesisPatcher.Log("Got reference to BalanceData: " + balanceData.name);

            // Use BalanceData to get access to the current instance of the UnitSynthesisMapping
            UnitSynthesisMapping mappingInstance = balanceData.SynthesisMapping;
            if (mappingInstance == null)
            {
                CustomUnitSynthesisPatcher.Log("Failed to find a mapping instance.");
            }
            else
            {
                CustomUnitSynthesisPatcher.Log("Able to find mapping instance: " + mappingInstance.GetID()); // Test to see if this is a different instance
            }

            // Calls CollectMappingData method
            RecallingCollectMappingData.MyTest(mappingInstance);
            CustomUnitSynthesisPatcher.Log("Called CollectMappingData.");
        }
    }

    // Solely exists to allow calling of private method CollectMappingData from within a UnitSynthesisMapping instance
    [HarmonyPatch(typeof(UnitSynthesisMapping), "CollectMappingData", new Type[] { })]
    public class RecallingCollectMappingData
    {
        [HarmonyReversePatch]
        public static void MyTest(object instance)
        {
            // It's a stub so it has no initial content
        }
    }
}
