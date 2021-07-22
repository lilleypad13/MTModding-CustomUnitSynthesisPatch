using System;
using System.Collections.Generic;
using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

namespace CustomUnitSynthesisPatcher
{
    class ExampleTestCakeUnit
    {
        public static void BuildFastFoodCharacter()
        {
            CharacterData fastfoodcharacter = new CharacterDataBuilder
            {

                CharacterID = "Sweetkin_Unit_FastFood",
                Name = "Fast Food",
                NameKey = "Card_XXXIV",
                Size = 1,
                Health = 1,
                AttackDamage = 0,
                AssetPath = "cakeCharacter.png", // EDIT
                SubtypeKeys = new List<string> { "SubtypesData_Wurm" }, // EDIT
                PriorityDraw = false,
                TriggerBuilders = new List<CharacterTriggerDataBuilder>
                    {
                        new CharacterTriggerDataBuilder
                        {
                            Trigger = CharacterTriggerData.Trigger.OnEaten,
                            Description = "Eater gains Quick",
                            DescriptionKey = "Mon_XIV",
                            EffectBuilders = new List<CardEffectDataBuilder>
                            {
                                new CardEffectDataBuilder
                                {
                                    EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
                                    TargetMode = TargetMode.LastFeederCharacter,
                                    TargetTeamType = Team.Type.Monsters,
                                    ParamStatusEffects = new StatusEffectStackData[]
                                    {
                                        new StatusEffectStackData
                                        {
                                            statusId = VanillaStatusEffectIDs.Quick,
                                            count = 1
                                        }
                                    }
                                }
                            }
                        }
                    }
            }.BuildAndRegister();

            CustomUnitSynthesisPatcher.Log($"Built CharacterData: {fastfoodcharacter.GetName()}."); // EDIT


            CardData FastFood = new CardDataBuilder
            {
                CardID = "Sweetkin_Card_FastFood",
                Name = "Fast Food",
                NameKey = "Card_XXXIV",
                CardLoreTooltipKeys = new List<string> { "Lore_Card_XXXIV" },
                Cost = 1,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Rare,
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "cakeCard.png", // change later EDIT
                ClanID = VanillaClanIDs.Hellhorned, // EDIT
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.UnitsAllBanner, VanillaCardPoolIDs.MegaPool },
                EffectBuilders = new List<CardEffectDataBuilder>
                    {
                        new CardEffectDataBuilder
                        {
                            EffectStateType = typeof(CardEffectSpawnMonster),
                            TargetMode = TargetMode.DropTargetCharacter,
                            ParamCharacterData = fastfoodcharacter,
                            EffectStateName = "CardEffectSpawnMonster"
                        }
                    }
            }.BuildAndRegister();

            //FastFood.BuildAndRegister();

            CustomUnitSynthesisPatcher.Log($"Created card: {FastFood.GetName()}."); // EDIT


            new CardUpgradeDataBuilder()
            {
                UpgradeTitleKey = "Sweetkin_Essence_XXXIV_UpgradeTitleKey",
                UpgradeTitle = "Sweetkin_Essence_XXXIV", // NOT CAPITALIZED, needed to set UpgradeTitleKey, UpgradeDescriptionKey, and UpgradgeNotificationKey
                SourceSynthesisUnit = CustomCharacterManager.GetCharacterDataByID("Sweetkin_Unit_FastFood"),
                UpgradeDescription = "Sweetkin synthesis description words.",
                UpgradeDescriptionKey = "Mon_XIV",

                StatusEffectUpgrades = new List<StatusEffectStackData>
                {
                     new StatusEffectStackData
                     {
                        statusId = VanillaStatusEffectIDs.Quick,
                        count = 1
                     },
                },

            }.Build();

            CustomUnitSynthesisPatcher.Log($"Created upgrade synthesis for {fastfoodcharacter.GetName()}."); // EDIT
        }
    }
}
