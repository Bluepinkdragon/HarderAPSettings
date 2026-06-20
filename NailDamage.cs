using System;
using GlobalEnums;
using HKMirror;
using HKMirror.Reflection.InstanceClasses;
using HKMirror.Reflection.SingletonClasses;
using Modding;

namespace HarderAPSettings
{
    public partial class HarderAPSettings : Mod
    {
        public bool strength = false;
        public void OnAttack( AttackDirection dir)
        {
            int beforenailchange = PlayerDataAccess.nailDamage;
            int nailLevel = PlayerDataAccess.nailSmithUpgrades;
            if (beforenailchange <= 32)
            {
                switch (nailLevel)
                {
                case 0:
                    switch (Settings.ReduceNail)
                    {
                        case 0:
                            PlayerDataAccess.nailDamage = 5;
                            break;
                        case 1: 
                            PlayerDataAccess.nailDamage = 1;
                            break;
                        case 2: 
                            PlayerDataAccess.nailDamage = 5;
                            break;
                        default:
                            break;
                    }
                    break;
                case 1:
                    switch (Settings.ReduceNail)
                    {
                        case 0:
                            PlayerDataAccess.nailDamage = 9;
                            break;
                        case 1: 
                            PlayerDataAccess.nailDamage = 1;
                            break;
                        case 2: 
                            PlayerDataAccess.nailDamage = 5;
                            break;
                        default:
                            break;
                    }
                    break;
                case 2:
                    switch (Settings.ReduceNail)
                    {
                        case 0:
                            PlayerDataAccess.nailDamage = 13;
                            break;
                        case 1: 
                            PlayerDataAccess.nailDamage = 1;
                            break;
                        case 2: 
                            PlayerDataAccess.nailDamage = 5;
                            break;
                        case 3:
                            PlayerDataAccess.nailDamage = 9;
                            break;
                        default:
                            break;
                    }
                    break;
                case 3:
                    switch (Settings.ReduceNail)
                    {
                        case 0:
                            PlayerDataAccess.nailDamage = 17;
                            break;
                        case 1: 
                            PlayerDataAccess.nailDamage = 1;
                            break;
                        case 2: 
                            PlayerDataAccess.nailDamage = 5;
                            break;
                        case 3:
                            PlayerDataAccess.nailDamage = 9;
                            break;
                        case 4:
                            PlayerDataAccess.nailDamage = 13;
                            break;
                        default:
                            break;
                    }
                    break;
                case 4:
                    switch (Settings.ReduceNail)
                    {
                        case 0:
                            PlayerDataAccess.nailDamage = 21;
                            break;
                        case 1: 
                            PlayerDataAccess.nailDamage = 1;
                            break;
                        case 2: 
                            PlayerData.instance.nailDamage = 5;
                            break;
                        case 3:
                            PlayerDataAccess.nailDamage = 9;
                            break;
                        case 4:
                            PlayerDataAccess.nailDamage = 13;
                            break;
                        case 5:
                            PlayerDataAccess.nailDamage = 17;
                            break;
                        default:
                            break;
                    }
                    break;
                    //this is still the fat ass switch case checking nailUpgrades
                }
                int newNail = PlayerDataAccess.nailDamage;
                if (newNail != beforenailchange)
                {
                    PlayMakerFSM.BroadcastEvent("UPDATE NAIL DAMAGE");
                }
            }
        }

        //more methods under











    }


}