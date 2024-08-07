using System;
using UnityEngine;
using Utils;

namespace ToBeChanged
{
    [Serializable]
    public struct CoinData { }
    public interface ICoinModel
    {
        // holds the essential data and state of the game at runtime [BACKBONE OF THE LOGIC]
        // primary concern is just to represent data accurately ad meaningfully not so much about storage and retrival
        // data that you are storing and persisitng throughout the game is not the same as the MODEL
        // the data can be serializable in model, but not the same as having an active concern of stroing any data
        Observable<int> Coins { get; }
        CoinData Serialize();
        void Deserialize(CoinData savedData);
    }
    public class CoinModel : ICoinModel
    {
        public Observable<int> Coins { get; }

        public void Deserialize(CoinData savedData)
        {
            throw new NotImplementedException();
        }

        public CoinData Serialize()
        {
            throw new NotImplementedException();
        }
    }
}