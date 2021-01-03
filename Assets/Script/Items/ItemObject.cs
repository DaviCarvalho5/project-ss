using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemObject : MonoBehaviour
{
  public Item item;
  public int quant;
  public bool isCaught;
  public Vector2Int inventoryPosition;

  public ItemObject(Item item, int quant, bool isCaught = false)
  {
    this.item = item;
    this.quant = quant;
    this.isCaught = isCaught;
  }

  public void Clicked()
  {
    if (!isCaught && !GameStates.isHoldingAnItemRep)
    {
      isCaught = true;
      GameStates.SetHoldingAnItemRep(true);
      GameStates.SetCatchedItem(this);
      GameStates.SetCatchedQuant(this.quant);
      GameStates.SetCatchedItemGameObject(gameObject);

      PlayerInventoryController.RemoveItemAt(inventoryPosition.x, inventoryPosition.y);
    }
    else if (!isCaught && GameStates.isHoldingAnItemRep)
    {
      if (item.itemName == GameStates.catchedItem.item.itemName)
      {
        if (quant + GameStates.catchedQuant <= item.maxStack)
        {
          quant += GameStates.catchedQuant;
          GameStates.ClearCatch();
        }
        else
        {
          int difference = item.maxStack - quant;
          quant = item.maxStack;
          PlayerInventory.slots[inventoryPosition.x, inventoryPosition.y].quant = quant;

          GameStates.catchedQuant -= difference;
          GameStates.catchedItemGameObject.GetComponent<ItemObject>().quant -= difference;
          GameStates.catchedItemGameObject.GetComponent<ItemObject>().UpDateText();
          UpDateText();
        }
      }
    }
  }

  void Update()
  {
    if (isCaught)
    {
      gameObject.transform.position = Input.mousePosition + new Vector3(40, -80, 0);
    }
  }

  public void UpDateText()
  {
    GetComponentInChildren<Text>().text = quant.ToString();
  }
}
