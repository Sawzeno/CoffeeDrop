using UnityEngine;
using UnityEngine.UI;

using Eflatun.SceneReference;

using App.SceneManagement;
using static Utils.Helpers;

namespace Game.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] SceneReference StartingLevel;
        [SerializeField] Button PlayButton;
        [SerializeField] Button QuitButton;

        void Awake()
        {
            PlayButton.onClick.AddListener(() => Loader.Load(StartingLevel));
            QuitButton.onClick.AddListener(() => QuitGame()); // TODO this does not work in editor
            Time.timeScale = 1f;
        }
    }
}
