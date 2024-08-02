using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.UI;
using static Utils.Helpers;

namespace CoffeeDrop
{
    public class MainMenuUI : MonoBehaviour{
        [SerializeField] SceneReference StartingLevel;
        [SerializeField]Button PlayButton;
        [SerializeField]Button QuitButton;

        void Awake(){
            PlayButton.onClick.AddListener(() => Loader.Load(StartingLevel));
            QuitButton.onClick.AddListener(() => QuitGame()); // TODO this does not work in editor
            Time.timeScale = 1f;
        }
    }
}
