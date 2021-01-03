using UnityEngine;

public class SlotObject : MonoBehaviour
{
  // public Slot slot;
  public Vector2Int coordinatesOnArray;

  public void OnClicked()
  {
    Slot slot = PlayerInventory.slots[coordinatesOnArray.x, coordinatesOnArray.y];

    if (GameStates.isHoldingAnItemRep)
    {
      if (slot.item.itemName == "void")
      {

        PlayerInventoryController.AddItemAt(GameStates.catchedItem.item, GameStates.catchedQuant, coordinatesOnArray.x, coordinatesOnArray.y);
        GameStates.ClearCatch();
        PlayerInventoryController.Render();
      }
    }
  }
}
