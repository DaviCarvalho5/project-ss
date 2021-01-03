using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour
{
  public static bool isInventoryOpen = false;
  public static bool isHoldingAGun = false;
  public static bool isBuilding = false;
  public static bool isHoldingAnItemRep = false;
  public static ItemObject catchedItem;
  public static GameObject catchedItemGameObject;
  public static int catchedQuant;
  public static BuildableItem buildingItem;

  public static void SetInventoryOpen(bool state)
  {
    isInventoryOpen = state;
  }

  public static void SetHoldingAnItemRep(bool state)
  {
    isHoldingAnItemRep = state;
  }

  public static void SetBuilding(bool state)
  {
    isBuilding = state;
  }

  public static void SetCatchedItem(ItemObject item)
  {
    catchedItem = item;
  }

  public static void SetCatchedQuant(int quant)
  {
    catchedQuant = quant;
  }

  public static void SetCatchedItemGameObject(GameObject go)
  {
    catchedItemGameObject = go;
  }

  public static void SetHoldingAGun(bool state)
  {
    isHoldingAGun = state;
  }

  public static void ClearCatch()
  {
    Destroy(catchedItemGameObject);
    SetCatchedItemGameObject(null);
    SetCatchedItem(null);
    SetHoldingAnItemRep(false);
    SetCatchedQuant(0);
  }

  public static void SetBuildingItem(BuildableItem item)
  {
    buildingItem = item;
  }
}
