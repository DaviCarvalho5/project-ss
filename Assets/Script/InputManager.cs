using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
  public GunsController guns;

  private void Start()
  {
    // guns = gameObject.AddComponent<GunsController>();
  }

  void Update()
  {
    if (Input.GetKeyUp(KeyCode.E))
    {
      UIManager.ChangeInventoryActive();
      PlayerInventoryController.VerifyItemSelected();
      Hotbar.Render();
    }

    if (Input.GetKeyUp(KeyCode.Q))
    {
      if (GameStates.isHoldingAnItemRep)
      {
        PlayerInventoryController.DropItem(GameStates.catchedItem.item, GameStates.catchedItem.quant);
        GameStates.ClearCatch();
      }
    }

    if (PlayerAttributes.isLive)
    {
      if (Input.GetMouseButton(0) && !GameStates.isInventoryOpen)
      {
        if (GameStates.isBuilding)
        {
          Construction.CreateBlock();
          Construction.DestroyBlock();

        }
        if (GameStates.isHoldingAGun && GunsController.canShot)
        {
          guns.Shot();
        }
      }

      if (Input.GetMouseButtonDown(1) && !GameStates.isInventoryOpen)
      {
        Construction.Interact();
      }


      if (Input.GetKeyUp(KeyCode.Alpha1))
      {
        PlayerInventoryController.SetSelection(0);
      }
      else if (Input.GetKeyUp(KeyCode.Alpha2))
      {
        PlayerInventoryController.SetSelection(1);
      }
      else if (Input.GetKeyUp(KeyCode.Alpha3))
      {
        PlayerInventoryController.SetSelection(2);
      }
      else if (Input.GetKeyUp(KeyCode.Alpha4))
      {
        PlayerInventoryController.SetSelection(3);
      }
      else if (Input.GetKeyUp(KeyCode.Alpha5))
      {
        PlayerInventoryController.SetSelection(4);
      }
    }
  }
}
