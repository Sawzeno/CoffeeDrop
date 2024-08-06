using System;
using UnityEngine;
using Utils;
using Game.Events.Channel;
namespace Game.CollectionSystem
{
    public interface ICoinController
    {
        void Collect(int coins);
        void UpdateView(int coins);
        void Save();
        ICoinModel Load();
    }

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
        public Observable<int> Coins {get;}

        public void Deserialize(CoinData savedData)
        {
            throw new NotImplementedException();
        }

        public CoinData Serialize()
        {
            throw new NotImplementedException();
        }
    }
    public interface ICoinView
    {
        // View is the interface , HOW ALL THIS DATA IS PRESENTED TO THE USER [FACE OF THE GAME]
        // responsible for the layout and the formatting of the data that it fets from the CONTROLLER or sometimes MODEL
        void UpdateCoinsDisplay(int coins);
    }
    public class CoinView : MonoBehaviour , ICoinView
    {
        [SerializeField] IntEventChannel IntChannel;
        public void UpdateCoinsDisplay(int coins)
        {
            Debug.Log("Voin view signaled for update");
            IntChannel.Invoke(coins);
        }
    }
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
    public class CoinController : ICoinController
    {
        readonly ICoinModel Model;
        readonly ICoinView View;
        readonly ICoinService Service;

        CoinController(ICoinView view, ICoinService service){
            Preconditions.CheckNotNull(view, "Coinview cannot be null");
            Preconditions.CheckNotNull(service, "CoinService cannot be null");
            View = view;
            Service = service;
            Model = Load();
            Model.Coins.AddListener(UpdateView);
            Model.Coins.Invoke();
        }
        
        // nullcheck

        public void Collect(int coins) => Model.Coins.Set(Model.Coins.Value + coins);
        public void UpdateView(int coins) => View.UpdateCoinsDisplay(coins);
        public void Save() => Service.Save(Model);
        public ICoinModel Load() => Service.Load();

        public class Builder{
            ICoinModel model;
            ICoinService service;

            public Builder WithModel(ICoinModel model){
                this.model  =   model;
                return this;
            }
            public Builder WithService(ICoinService service){
                this.service = service;
                return this;
            }
            public CoinController Build(ICoinView view){
                return new CoinController(view, service);
            }
        }
    }
}
