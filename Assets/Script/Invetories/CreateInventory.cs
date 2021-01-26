using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateInventory : MonoBehaviour
{
  public PlayerInventory playerInventory;
  public GameObject slotObject, inventoryHotbar, inventoryNormal;

  public void StartInventoryCreation()
  {
    Debug.Log("Inventory size => x: " + playerInventory.size.x + " y: " + playerInventory.size.y);
    for (int i = 0; i < playerInventory.size.x; i++)
    {
      for (int j = 0; j < playerInventory.size.y; j++)
      {
        Debug.Log("i: " + i + " j: " + j);
        GameObject so = Instantiate(slotObject);

        so.GetComponent<SlotObject>().coordinatesOnArray = new Vector2Int(i, j);

        if (j == 0)
        {
          so.transform.SetParent(inventoryHotbar.transform);
          Debug.Log("HOT");
        }
        else
        {
          so.transform.SetParent(inventoryNormal.transform);
        }

        so.transform.localScale = Vector3.one;
        PlayerInventoryController.slotObjects[i, j] = so;
      }
    }
  }
}
