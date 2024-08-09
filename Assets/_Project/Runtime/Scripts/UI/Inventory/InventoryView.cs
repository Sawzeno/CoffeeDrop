using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
namespace Game.UI.Inventory
{
    public class InventoryView : StorageView{
        [SerializeField] string PanelName = "Inventory";
        public override IEnumerator InitializeView(int size = 20)
        {
            Slots = new Slot[size];
            Root  = new document.rootVisualElement;
        }
    }
}