using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
  private static Vector2Int inventorySize;
  private static Transform renderTransform;
  private static bool[] isRendered;
  private static GameObject[] itemsObjects;
  [SerializeField] private Transform renderTransformNS;

  void Start()
  {
    isRendered = new bool[PlayerInventoryController.playerInventory.size.x];
    itemsObjects = new GameObject[PlayerInventoryController.playerInventory.size.x];
    inventorySize = PlayerInventoryController.playerInventory.size;
    renderTransform = renderTransformNS;
  }
  public static void Render()
  {
    for (int i = 0; i < inventorySize.x; i++)
    {
      for (int j = 0; j < 1; j++)
      {
        Destroy(itemsObjects[i]);

        GameObject slotObject = PlayerInventoryController.slotObjects[i, j];
        Slot slot = PlayerInventory.slots[i, j];

        if (slot.item.itemName != "void")
        {
          isRendered[i] = true;
          SlotObject slotObjectCode = slotObject.GetComponent<SlotObject>();

          GameObject rep = Instantiate(PlayerInventoryController.itemObject);

          itemsObjects[i] = rep;

          rep.GetComponentInChildren<Image>().sprite = slot.item.sprite;
          rep.GetComponentInChildren<Text>().text = slot.quant.ToString();

          rep.transform.SetParent(renderTransform);
          rep.transform.localScale = Vector2.one;
          int x, y;

          x = i * 105 + 75;
          y = 72;

          rep.transform.position = new Vector2(x, y);
        }
      }
    }
  }

}
