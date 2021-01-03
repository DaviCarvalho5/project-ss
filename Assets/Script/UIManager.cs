using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  GameObject worldSelector;
  GameObject playerHand;
  public static CanvasScaler canvasScaler;
  public static float scaleFactor;
  public static GameObject uiInventory, uiHotbar;

  public GameObject playerHandNS, uiInventoryNS, uiHotbarNS;
  public CanvasScaler canvasScalerNS;
  public CursorController cursorControllerNS;

  public static bool holdingAnItem;
  public static GameObject holdObject;


  private void Awake()
  {
    canvasScaler = canvasScalerNS;
    playerHand = playerHandNS;
    uiInventory = uiInventoryNS;
    uiHotbar = uiHotbarNS;
  }

  void Start()
  {
    GameStates.SetInventoryOpen(false);
    GameStates.SetBuilding(false);
    CursorController.UpdateCursor();
    holdingAnItem = false;
    holdObject = null;
    UpdateUIScale();
    worldSelector = GameObject.Find("Selector");
    Hotbar.Render();
  }

  void UpdateUIScale()
  {
    scaleFactor = Display.displays[0].renderingWidth / canvasScaler.referenceResolution.x;
    Debug.Log("[UI Manager] Scale Factor updated: " + scaleFactor);
  }

  public static void ChangeInventoryActive()
  {
    bool state = !uiInventory.activeSelf;
    uiInventory.SetActive(state);
    uiHotbar.SetActive(!state);

    GameStates.isInventoryOpen = state;

    CursorController.UpdateCursor();
  }

  public static void SetInventoryActive(bool state)
  {
    uiInventory.SetActive(state);
    uiHotbar.SetActive(!state);

    GameStates.isInventoryOpen = state;

    CursorController.UpdateCursor();
  }
}
