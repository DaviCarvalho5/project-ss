using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryNormal : MonoBehaviour
{
  public static bool open = false;
  public static GameObject inventory;

  void Start()
  {
    inventory = GameObject.Find("Normal Inventory");
    SetState(false);
  }

  public void SetState(bool state)
  {
    open = state;
    inventory.SetActive(state);
  }

  public bool IsOpen()
  {
    return open;
  }
}
