using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameCore.Loot
{
    public class CoinsKeeper
    {
        public int Coins { get; private set; }


        public void AddCoin() => Coins++;
    }
}