using Assets.Scripts.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Save3
{
    public class SaveProgress
    {
        private PlayerData _playerData;
        public void SaveData()
        {
            PlayerPrefs.SetInt("Coins",_playerData.Coins);
            PlayerPrefs.SetInt("Health",_playerData.MaxHealthUpgradeIndex);
            PlayerPrefs.SetInt("Speed",_playerData.SpeedUpgradeIndex);
            PlayerPrefs.SetInt("Regen",_playerData.RegenerationUpgradeIndex);
            PlayerPrefs.SetInt("Range",_playerData.ExpRangeUpgradeIndex);
            PlayerPrefs.Save();
        }

        public void LoadData()
        {
            _playerData.AddRewardCoins(PlayerPrefs.GetInt(key: "Coins"));
            _playerData.SetUpgradeIndex(PlayerPrefs.GetInt(key: "Health"), id:1);
            if (PlayerPrefs.GetInt(key: "Health") == 0)
            {
                _playerData.SetUpgradeIndex(1, id: 1);
            }
            _playerData.SetUpgradeIndex(PlayerPrefs.GetInt(key: "Speed"), id:2);
            if (PlayerPrefs.GetInt(key: "Speed") == 0)
            {
                _playerData.SetUpgradeIndex(1, id: 2);
            }
            _playerData.SetUpgradeIndex(PlayerPrefs.GetInt(key: "Regen"), id:3);
            if (PlayerPrefs.GetInt(key: "Regen") == 0)
            {
                _playerData.SetUpgradeIndex(1, id: 3);
            }
            _playerData.SetUpgradeIndex(PlayerPrefs.GetInt(key: "Range"), id:4);
            if (PlayerPrefs.GetInt(key: "Range") == 0)
            {
                _playerData.SetUpgradeIndex(1, id: 4);
            }

        }


        [Inject]
        private void Construct(PlayerData playerData)
        {
            _playerData = playerData;   
        }
    }
}
