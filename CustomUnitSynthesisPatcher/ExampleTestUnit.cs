using System;
using System.Collections.Generic;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;



namespace CustomUnitSynthesisPatcher
{
    class ExampleTestUnit
    {
        public static readonly string cardId = CustomUnitSynthesisPatcher.GUID + "_TestUnitCard";
        public static readonly string charId = CustomUnitSynthesisPatcher.GUID + "_TestUnitCharacter";

        private static string characterAssetPath = "test_sign.png";
        private static string cardAssetPath = "test_logo.png";

        public static void Make()
        {
            // CharacterData Information
            // Bulk of information for character once it is in play (i.e. stats, in-train visual asset, etc.)
            // Used in CardDataBuilder
            CharacterDataBuilder testCharacter = new CharacterDataBuilder
            {
                CharacterID = charId,
                Name = "TEST UNIT",
                Size = 2,
                Health = 1233,
                AttackDamage = 456,
                AssetPath = characterAssetPath,
                SubtypeKeys = new List<string> { "SubtypesData_Wurm" }
            };

            // CardData Information
            // Bulk of information for card outside of after being played (i.e. Clan, Rarity, in-hand visual asset and cost, etc.)
            CardDataBuilder card = new CardDataBuilder
            {
                CardID = cardId,
                Name = "TEST TEST UNIT",
                Cost = 1,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Common,
                ClanID = VanillaClanIDs.Hellhorned,

                TargetsRoom = true,
                Targetless = false,
                AssetPath = cardAssetPath,
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.HellhornedBanner, VanillaCardPoolIDs.UnitsAllBanner },

                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = typeof(CardEffectSpawnMonster),
                        TargetMode = TargetMode.DropTargetCharacter,
                        ParamCharacterDataBuilder = testCharacter,
                        EffectStateName = "CardEffectSpawnMonster" // Needed to properly name for current itteration of Trainworks
                    }
                }
            };

            card.BuildAndRegister();
            CustomUnitSynthesisPatcher.Log("Test Unit made.");



            // CardUpgradeData information
            // (Unit Synthesis Information)
            // Contains all information for what this unit's synthesis upgrade will do
            CardUpgradeDataBuilder testUnitSynthesis = new CardUpgradeDataBuilder
            {
                UpgradeTitle = "synthesis_" + charId,
                SourceSynthesisUnit = CustomCharacterManager.GetCharacterDataByID(charId),
                UpgradeDescription = "TEST UNIT SYNTHESIS UPGRADE.",
                UpgradeDescriptionKey = "TestUnit_Synthesis_Upgrade",
                BonusDamage = 333
            };

            CustomUnitSynthesisPatcher.Log($"CardUpgradeDataBuilder {testUnitSynthesis.UpgradeTitle} has CharacterData: {testUnitSynthesis.SourceSynthesisUnit.name}.");

            // Want to check if this can be combined with the CardData builder overall, 
            // or if this does just need to be built separately.
            testUnitSynthesis.Build();
            CustomUnitSynthesisPatcher.Log($"{testCharacter.Name} synthesis made.");
        }
    }
}
