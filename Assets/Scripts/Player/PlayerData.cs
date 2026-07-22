using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerData
    {
        public int Coins { get; private set; }
        public int MaxHealthUpgradeIndex { get; private set; }
        public int SpeedUpgradeIndex { get; private set; }
        public int RegenerationUpgradeIndex { get; private set; }
        public int ExpRangeUpgradeIndex { get; private set; }
        public void TrySpendCoins(int value)
        {
            if (value <= 0 || value > Coins)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
            Coins -= value;
        }
        public void AddCoin() => Coins++;

        public void AddRewardCoins(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
            Coins += value;
        }
         public void SetUpgradeIndex(int value, int id)
        {
            if (value < 0 || value > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
            if (id == 1)
            {
                MaxHealthUpgradeIndex = value;
            }
            else if (id == 2)
            {
                SpeedUpgradeIndex = value;  
            }
            else if (id == 2)
            {
                RegenerationUpgradeIndex = value;
            }
            else if (id == 2)
            {
                ExpRangeUpgradeIndex = value;
            }
        }



    }
}