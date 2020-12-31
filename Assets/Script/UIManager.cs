using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  public static int selectorPosition;
  GameObject invSelector;
  GameObject worldSelector;
  GameObject playerHand;
  public static CanvasScaler canvasScaler;
  public static float scaleFactor;
  public static InventoryNormal inventoryNormal;
  public CanvasScaler canvasScalerNS;
  public GameObject playerHandNS;

  public static bool holdingAnItem;
  public static GameObject holdObject;

  bool playedTakeSFX = false;

  private void Awake()
  {
    canvasScaler = canvasScalerNS;
    playerHand = playerHandNS;
  }

  void Start()
  {
    holdingAnItem = false;
    holdObject = null;
    UpdateUIScale();
    inventoryNormal = new InventoryNormal();
    invSelector = GameObject.Find("Inventory Selector");
    worldSelector = GameObject.Find("Selector");
    // selectorPosition = 0;
  }

  void Update()
  {
    if (InventoryHotBar.hotBarItems[InventoryHotBar.selectorPosition].item.handUsable)
    {
      playerHand.SetActive(true);
      playerHand.GetComponent<SpriteRenderer>().sprite = InventoryHotBar.hotBarItems[InventoryHotBar.selectorPosition].item.sprite;
      Guns.gunOnHand = true;
    }
    else
    {
      playerHand.SetActive(false);
      Guns.gunOnHand = false;
    }

    if (Guns.gunOnHand && !playedTakeSFX)
    {
      FindObjectOfType<AudioManager>().Play("TakeGun1");
      playedTakeSFX = true;
    }
    else if (!Guns.gunOnHand)
    {
      playedTakeSFX = false;
    }
  }

  void UpdateUIScale()
  {
    scaleFactor = Display.displays[0].renderingWidth / canvasScaler.referenceResolution.x;
    Debug.Log("Scale Updated to " + scaleFactor);
  }
}
