using System;
using Utils;

namespace CoffeeDrop
{
    public interface ICollectibleController {
        void Collect(int Collectible);
        void UpdateView(int Collectible);
        void Save();
        ICollectibleModel Load();
    };
    internal interface ICollectibleService
    {
    }
    internal interface IColelctibleView
    {
    }
    public interface ICollectibleModel
    {
    }
    public class CollectibleController : ICollectibleController{
        readonly ICollectibleModel model;
        readonly IColelctibleView view;
        readonly ICollectibleService service;

        CollectibleController(IColelctibleView view, ICollectibleService service){
            Preconditions.CheckNotNull(view, "CoinView cannot be null");
            Preconditions.CheckNotNull(service, "Service cannot be bull");

            this.view = view;
            this.service = service;

            // model = Load();

        }

        public void Collect(int Collectible)
        {
            throw new NotImplementedException();
        }

        public ICollectibleModel Load()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateView(int Collectible)
        {
            throw new NotImplementedException();
        }
    }

}
