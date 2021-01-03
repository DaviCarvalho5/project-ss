using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
  public Item item;
  public int quant;
  public GameObject itemObject;
  public bool isItemObjectRendered;

  public Slot(Item item, int quant, GameObject itemObject = null, bool isItemObjectRendered = false)
  {
    this.item = item;
    this.quant = quant;
    this.itemObject = itemObject;
    this.isItemObjectRendered = isItemObjectRendered;
  }
}
