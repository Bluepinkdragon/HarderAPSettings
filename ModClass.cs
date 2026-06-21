using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Dynamic;
using HutongGames.PlayMaker.Actions;
using JetBrains.Annotations;
using Modding;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using HKMirror;
using HKMirror.Reflection.StaticClasses;
using Unity.IO.LowLevel.Unsafe;


namespace HarderAPSettings
{
    public partial class HarderAPSettings : Mod, IMenuMod, ILocalSettings<LocalSettings>

    {
        public static LocalSettings Settings { get; set; } = new LocalSettings();

        public override int LoadPriority() => 100;
        public bool ToggleButtonInsideMenu => false;

        public HarderAPSettings() : base("HarderAPSettings") { }

        public override string GetVersion() => "1.0.0.2";

        public override void Initialize()
        {
            ModHooks.BeforeSceneLoadHook += BeforeSceneLoad;
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += SceneChanged;
            On.GameManager.BeginSceneTransition += GameManager_BeginSceneTransition;
            ModHooks.AfterTakeDamageHook += AfterTakeDamage;
            ModHooks.AttackHook += OnAttack;
            //ModHooks.HeroUpdateHook += OnHeroUpdate;
            //ModHooks.CharmUpdateHook += OnCharmUpdate;
            Log("Initialized");
        }
        



        public BossSequenceController.ChallengeBindings bindings;
       
        public int pdMana;
        public int pdManaReserve;
        public static BossSequenceController.BossSequenceData currentData;

        private static BossSequence currentSequence;

        private static int bossIndex;

        public static bool BoundNail;

        public static bool WasCompleted;

        public static BossSequenceDoor.Completion currentCompletion;

        public int[] previousEquippedCharms;


        public static void SetupNewSequence(BossSequenceController.ChallengeBindings bindings)
        {
            bossIndex = 0;
            currentCompletion.canUnlock = false;
            currentCompletion.unlocked = false;
            currentCompletion.completed = false;
            currentCompletion.allBindings = false;
            currentCompletion.noHits = false;
            currentCompletion.boundNail = false;
            currentCompletion.boundShell = false;
            currentCompletion.boundCharms = false;
            currentCompletion.boundSoul = false;
            currentCompletion.viewedBossSceneCompletions = new List<string>();
            currentData = new BossSequenceController.BossSequenceData
            {
                bindings = bindings,
                timer = 0f,
                knightDamaged = false,
                bossSequenceName = "Boss Sequence Tier 5",
                playerData = "bossDoorStateTier5",
                wasOvercharmed = false,
                previousEquippedCharms = [0],
                previousCompletion = currentCompletion
            };
            
            WasCompleted = false;
            PlayerData.instance.currentBossSequence = currentData;
        }



        //AbsRad Ending Change
        public string BeforeSceneLoad(string sceneName)
        {
            Log("beforeSceneloaded");
            pdMana = PlayerDataAccess.MPCharge;
            pdManaReserve = PlayerDataAccess.MPReserve;
            Log($" Current scene: {UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}");
            if (sceneName == "Cinematic_Ending_D" && Settings.HardMode == true)
            {
                sceneName = "Cinematic_Ending_C";
                return sceneName;
            }
            if (sceneName == "Dream_Final_Boss" && Settings.HardMode == true)
            {
                sceneName = "GG_Radiance";
                return sceneName;
            }
            return sceneName;
        }

        public void SceneChanged(Scene oldScene, Scene newScene)
        {
             if (newScene.name == "Menu_Title" || newScene.name == "Quit_To_Menu")
            {
                return;
            }
            if (PlayerData.instance == null)
            {
                Log("playerdata was null");
                return;
            }
            Log("afterSceneloaded");
            Log($"old {oldScene.name}, new {newScene.name}");
            Log($" Current scene: {UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}");
            if(GameManager.instance.sceneName == "GG_Radiance" && Settings.HardMode == true)
            {  
                Log("giving back mana");
                if ((PlayerDataAccess.MPCharge + PlayerDataAccess.MPReserve) != (pdMana + pdManaReserve))
                {   
                    Log("giving back mana,inside");
                    HeroController.instance.AddMPCharge(pdMana);
                    HeroController.instance.AddMPCharge(pdManaReserve);
                }
                //PlayMakerFSM.BroadcastEvent("MP SET");
                Log("Finished giving mana, before caller");
                BossSequenceControllerR.LoadCurrentSequence(caller);
                Log("Finished giving mana, after caller");
            }
        }
        public MonoBehaviour caller;






        // public int PlayerIntSet(string target, int value)
        // {
        //     value = Settings.ReduceMasks;
        //     target = PlayerDataAccess.maxHealth;
        //     return PlayerDataAccess.maxHealth;
        // }


        public int AfterTakeDamage(int hazardType, int damageAmount)
        {
            int damage = damageAmount;
            switch (Settings.ExtraDamage)
            {
                case 0:
                    break;
                case 1:
                    if (hazardType == 1)
                    {
                        damage = damageAmount+1;
                    }
                    break;
                case 2:
                    damage = damageAmount*2;
                    break;
            }
            if (damage >= (PlayerData.instance.GetInt("health") + PlayerData.instance.GetInt("healthBlue")) && GameManager.instance.sceneName == "GG_Radiance" && Settings.HardMode == true) 
            {
                GameManager.instance.sm.mapZone = GlobalEnums.MapZone.FINAL_BOSS;
                BossSequenceController.Reset();
                return damage;
            }
            return damage;
        }

        //AbsRad replacement logic
        public void GameManager_BeginSceneTransition(
            On.GameManager.orig_BeginSceneTransition orig, 
            GameManager self, 
            GameManager.SceneLoadInfo info
        ){
            Log($" ,,, Begining Scene transistion ,, info.SceneName , {info.SceneName} ,,,, gate , {info.EntryGateName},,,,,,");
            if (Settings.HardMode == true)
            {
                if (info.SceneName == "Room_Final_Boss_Core")
                {
                    Log("Restart boss sequence");
                    BossSequenceController.Reset();
                    SetupNewSequence(0);
                }
                if (info.SceneName == "Dream_Final_Boss")
                {
                    Log($",,,goingintoGGRad,,{info.SceneName} ,,,,, gateName , {info.EntryGateName},,,,,before,,,,");
                    PlayerDataAccess.bossReturnEntryGate = "";
                    // info.SceneName = "GG_Radiance";
                    info.EntryGateName = "door_dreamEnter";
                    info.EntryDelay = 0;
                    info.PreventCameraFadeOut = true;
                    info.Visualization = GameManager.SceneLoadVisualizations.GodsAndGlory;
                    PlayerData.instance.currentBossSequence.playerData = "bossDoorStateTier5";
                    PlayerData.instance.currentBossSequence.bossSequenceName = "Boss Sequence Tier 5";
                    Log($",,,,,{info.SceneName} ,,,,, gate ,,, {info.EntryGateName},,,,,after,,,,");
                }
            }
            orig(self, info);
        }  
        


























        public List<IMenuMod.MenuEntry> GetMenuData(IMenuMod.MenuEntry? toggleButtonEntry)
        {
            return new List<IMenuMod.MenuEntry>
            {
                new IMenuMod.MenuEntry
                {
                    Name = "Reduce Nail Damage",
                    Description = "Lower nail damage, useful for skips(Can't give you more damage)",
                    Values = new string[]
                    {
                        "Off",
                        "Nail -1",
                        "Nail 0",
                        "Nail 1",
                        "Nail 2",
                        "Nail 3"
                    },
                    Saver = opt => Settings.ReduceNail = opt,
                    Loader = () => Settings.ReduceNail
                },
                // new IMenuMod.MenuEntry
                // {
                //     Name = "Reduce Total Masks",
                //     Description = "Choose how much to lower your total masks by",
                //     Values = new string[]
                //     {
                //         "0",
                //         "1",
                //         "2",
                //         "3",
                //         "4"
                //     },
                //     Saver = opt => Settings.ReduceMasks = opt,
                //     Loader = () => Settings.ReduceMasks
                // },
                new IMenuMod.MenuEntry
                {
                    Name = "Take Extra Damage",
                    Description = "Skong is an extra mask on enemy hits only, Double is everything",
                    Values = new string[]
                    {
                        "Off",
                        "Skong",
                        "Double"
                    },
                    Saver = opt => Settings.ExtraDamage = opt,
                    Loader = () => Settings.ExtraDamage
                },
                new IMenuMod.MenuEntry
                {
                    Name = "Hard Mode",
                    Description = null,
                    Values = new string[]
                    {
                        "Off",
                        "On"
                    },
                    Saver = opt => Settings.HardMode = opt switch
                    {
                        0 => false,
                        1 => true,
                        // This should never be called
                        _ => throw new InvalidOperationException()
                    },
                    Loader = () => Settings.HardMode switch
                    {
                        false => 0,
                        true => 1,
                    }
                }
            };
        }

        public void OnLoadLocal(LocalSettings localSettings)
        {
            Settings = localSettings;
        }
        public LocalSettings OnSaveLocal()
        {
            return Settings;
        }
        public void Unload()
        {
            ModHooks.BeforeSceneLoadHook -= BeforeSceneLoad;
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= SceneChanged;
            On.GameManager.BeginSceneTransition -= GameManager_BeginSceneTransition;
            ModHooks.AfterTakeDamageHook -= AfterTakeDamage;
            ModHooks.AttackHook -= OnAttack;
            //ModHooks.HeroUpdateHook -= OnHeroUpdate;
            //ModHooks.CharmUpdateHook -= OnCharmUpdate;
        }
    }



}
