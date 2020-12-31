using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
  public int id;
  public string zone;
  public GameObject canvas;

  void Start()
  {
    canvas = GameObject.Find("Canvas");
  }

  public void SelectSlot()
  {
    if (!UIManager.inventoryNormal.IsOpen())
    {
      InventoryHotBar.selectorPosition = id;
      InventoryHotBar.SetSelectorPosition(id);
    }
    else
    {
      if (zone == "Hotbar" && !UIManager.holdingAnItem && InventoryHotBar.hotBarItems[id].item.itemName != "zero")
      {
        Debug.Log("A");
        UIManager.holdingAnItem = true;

        GameObject itemRep = Instantiate(InventoryHotBar.itemRepObject);
        itemRep.transform.localScale = Vector3.one;
        itemRep.transform.SetParent(canvas.transform);
        itemRep.GetComponent<Image>().sprite = InventoryHotBar.hotBarItems[id].item.sprite;
        itemRep.GetComponentInChildren<Text>().text = InventoryHotBar.hotBarItems[id].quant.ToString();

        UIManager.holdObject = itemRep;

        ItemRep itemRepScript = itemRep.GetComponent<ItemRep>();
        itemRepScript.item = InventoryHotBar.hotBarItems[id].item;
        itemRepScript.quant = InventoryHotBar.hotBarItems[id].quant;
        itemRepScript.taked = true;

        InventoryHotBar.clearPosition(id);
      }

      else if (zone == "Hotbar" && UIManager.holdingAnItem)
      {
        Debug.Log("B");
        ItemRep script = UIManager.holdObject.GetComponent<ItemRep>();
        InventoryHotBar.addItemInPosition(script.item, script.quant, id);
        Destroy(UIManager.holdObject);
        UIManager.holdingAnItem = false;
      }
    }
  }
}
