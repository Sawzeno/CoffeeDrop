using UnityEngine;

namespace ToBeChanged
{
    public interface ICoinService
    {
        void Save(ICoinModel model);
        ICoinModel Load();
    }
        public class CoinService : ICoinService
    {
        int testAmount = 100;
        public ICoinModel Load()
        {
            Debug.Log("loading coins");
            CoinModel model = new();
            model.Coins.Set(testAmount);
            return model;
        }
        public void Save(ICoinModel model)
        {
            Debug.Log("Save coins");
        }
    }
}

