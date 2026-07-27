using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameCore.Loot
{
    public class CoinsKeeper
    {
        public int Coins { get; private set; }


        public void AddCoin() => Coins++;
        public void AddCoins(int value)
        {
            if (value > 0)
            {
                Coins += value;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
        }
    }
}