using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
  public bool isOpen;
  public static Slot[,] slots;
  public Vector2Int size = new Vector2Int(4, 4);

  public static int hotbarSlotSelected = 0;

  private void Awake()
  {
    // The first line is the hotbar
    slots = new Slot[size.x, size.y];
    //

    for (int i = 0; i < size.x; i++)
    {
      for (int j = 0; j < size.y; j++)
      {
        slots[i, j] = new Slot(ListOfItems.voiditem, 0);
      }
    }
  }
}
