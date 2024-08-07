using System;
using UnityEngine;
using Utils;
using Game.Events.Channel;

namespace ToBeChanged
{
    public interface ICoinController
    {
        void Collect(int coins);
        void UpdateView(int coins);
        void Save();
        ICoinModel Load();
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

