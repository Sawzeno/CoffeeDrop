using UnityEngine;
using Game.Events.Channel;
namespace ToBeChanged
{
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
}