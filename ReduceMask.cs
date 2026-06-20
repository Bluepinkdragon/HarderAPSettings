// using System;
// using System.Collections;
// using System.Configuration;
// using System.Threading.Tasks;
// using HKMirror;
// using HutongGames.PlayMaker.Actions;
// using JetBrains.Annotations;
// using Modding;

// namespace HarderAPSettings
// {
//     public partial class HarderAPSettings : Mod
//     {
        


//         public void OnCharmUpdate(PlayerData data, HeroController controller)
//         {
//             // int charm = 0;
//             // if (PlayerDataAccess.equippedCharm_23)
//             // {
//             //     charm = 2;
//             // }
//             // if (PlayerDataAccess.equippedCharm_27)
//             // {
//             //     truehealthBase = PlayerDataAccess.maxHealthBase;
//             //     Log("Equipped Shit Blessing");
//             //     //Log($"{PlayerDataAccess.joniHealthBlue = (int)Math.Ceiling(((float)PlayerDataAccess.maxHealthBase + charm - Settings.ReduceMasks) * 1.4f)}");
//             //     //PlayerDataAccess.joniHealthBlue = (int)Math.Ceiling(((float)PlayerDataAccess.maxHealthBase + charm - Settings.ReduceMasks) * 1.4f);
//             //     PlayerDataAccess.maxHealthBase = PlayerDataAccess.maxHealthBase - Settings.ReduceMasks;
//             //     PlayerDataAccess.MaxHealth();
//             //     return;
//             // }
//             // else
//             // {
//             //     PlayerDataAccess.maxHealthBase = truehealthBase;
//             // }
//         }

//         public int truehealthBase = 5;

    
//         public void OnHeroUpdate()
//         {
//             UpdateHealth();
//         }

//         public void UpdateHealth()
//         {
//             int charm = 0;
//             if (PlayerDataAccess.equippedCharm_23)
//             {
//                 charm = 2;
//             }
//             if(!PlayerData.instance.equippedCharm_27)
//             {

//                 //PlayerDataAccess.maxHealthBase = 5 - Settings.ReduceMasks;

//                 int oldMaxHealth = PlayerData.instance.maxHealth;
//                 int newBase = PlayerData.instance.maxHealthBase + charm;

//                 PlayerDataAccess.maxHealth = newBase-Settings.ReduceMasks;

//                 if(PlayerDataAccess.maxHealth != oldMaxHealth && PlayerDataAccess.health > PlayerDataAccess.maxHealth)
//                 {
//                     PlayerDataAccess.health = PlayerDataAccess.maxHealth;
//                 }
//                 if (PlayerDataAccess.maxHealth != oldMaxHealth)
//                 {
//                     if (!GameCameras.instance.hudCanvas.gameObject.activeInHierarchy)
//                         GameCameras.instance.hudCanvas.gameObject.SetActive(true);
//                     else
//                     {
//                         GameCameras.instance.hudCanvas.gameObject.SetActive(false);
//                         GameCameras.instance.hudCanvas.gameObject.SetActive(true);
//                     }
//                     // for (int i = 2; i < 12; i++)
//                     // {
//                     //     var health = GameCameras.instance.hudCanvas.transform.Find("Health/Health "+i).GetComponent<PlayMakerFSM>();
//                     //     if(i <= PlayerDataAccess.maxHealth)
//                     //     {
//                     //         Log("Send max hp up");
//                     //         health.SendEvent("MAX HP UP");
//                     //     }
//                     //     else
//                     //     {   
//                     //         Log("Send over max");
//                     //         health.SendEvent("OVER MAX");
//                     //     }
//                     // }
//                 }




//             }
//         }



// //         if (PlayerDataAccess.equippedCharm_28 && !shapeOfUnnVessel)
// // {
// //     shapeOfUnnVessel = true;
// //     HeroController.instance.AddToMaxMPReserve(33);
// //     PlayMakerFSM.BroadcastEvent("NEW SOUL ORB");
// //     //Log("Shape of Unn adding soul vessel.");
// // }
// // else if (shapeOfUnnVessel && !PlayerDataAccess.equippedCharm_28)
// // {
// //     shapeOfUnnVessel = false;
// //     HeroController.instance.AddToMaxMPReserve(-33);
// //     var vessel = GameCameras.instance.hudCanvas.transform.FindChild("Soul Orb").FindChild("Vessels").FindChild("Vessel 1").GetComponent<PlayMakerFSM>();
// //     if (PlayerDataAccess.MPReserveMax <= 0)
// //     {
// //         vessel.SendEvent("UNBIND VESSEL ORB");
// //         //Log("Hide vessel 1.");
// //     }
// //     if (PlayerDataAccess.MPReserveMax <= 33)
// //     {
// //         vessel = GameCameras.instance.hudCanvas.transform.FindChild("Soul Orb").FindChild("Vessels").FindChild("Vessel 2").GetComponent<PlayMakerFSM>();
// //         vessel.SendEvent("UNBIND VESSEL ORB");
// //         //Log("Hide vessel 2.");
// //     }
// //     if (PlayerDataAccess.MPReserveMax <= 66)
// //     {
// //         vessel = GameCameras.instance.hudCanvas.transform.FindChild("Soul Orb").FindChild("Vessels").FindChild("Vessel 3").GetComponent<PlayMakerFSM>();
// //         vessel.SendEvent("UNBIND VESSEL ORB");
// //         //Log("Hide vessel 3.");
// //     }
// //     if (PlayerDataAccess.MPReserveMax <= 99)
// //     {
// //         vessel = GameCameras.instance.hudCanvas.transform.FindChild("Soul Orb").FindChild("Vessels").FindChild("Vessel 4").GetComponent<PlayMakerFSM>();
// //         vessel.SendEvent("UNBIND VESSEL ORB");
// //         //Log("Hide vessel 4.");
// //     }
// //     if (PlayerDataAccess.MPReserve > PlayerDataAccess.MPReserveMax)
// //     {
// //         PlayerDataAccess.MPReserve = PlayerDataAccess.MPReserveMax;
// //     }
// //     //Log("Shape of Unn soul vessel removed.");







//     }
// }