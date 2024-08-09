using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UI.Inventory
{

    public class Slot{};
    public abstract class StorageView : MonoBehaviour
    {
        public Slot[] Slots;

        [SerializeField] protected UIDocument document;
        [SerializeField] protected StyleSheet styleSheet;

        protected VisualElement Root;
        protected VisualElement Container;

        IEnumerator Start(){
            yield return StartCoroutine(InitializeView());
        }
        public abstract IEnumerator InitializeView(int size = 20);
    }
}