using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
  void Update()
  {
    if (Input.GetKeyUp(KeyCode.Alpha1))
    {
      InventoryHotBar.SetSelectorPosition(0);
    }
    else if (Input.GetKeyUp(KeyCode.Alpha2))
    {
      InventoryHotBar.SetSelectorPosition(1);
    }
    else if (Input.GetKeyUp(KeyCode.Alpha3))
    {
      InventoryHotBar.SetSelectorPosition(2);
    }
    else if (Input.GetKeyUp(KeyCode.Alpha4))
    {
      InventoryHotBar.SetSelectorPosition(3);
    }

    if (Input.GetKeyUp(KeyCode.E))
    {
      bool state = !UIManager.inventoryNormal.IsOpen();
      UIManager.inventoryNormal.SetState(state);
    }
  }
}
