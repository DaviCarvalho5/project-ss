using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHotBar : MonoBehaviour
{
  [SerializeField]
  GameObject itemRepObjectNS, inventoryOnUINS, itemDropNS, selectorNS;
  public static InvItem[] hotBarItems;
  public static int inventoryCellSize;
  public static int selectorPosition;

  public static GameObject itemRepObject, inventoryInUI, itemDrop, selector;
  static float selectorStartX;

  void Awake()
  {
    hotBarItems = new InvItem[4];
    inventoryCellSize = 100 + 44;
    itemRepObject = itemRepObjectNS;
    inventoryInUI = inventoryOnUINS;
    itemDrop = itemDropNS;
    selector = selectorNS;
    selectorPosition = 0;
  }

  void Start()
  {
    selectorStartX = selector.transform.position.x;

    for (int i = 0; i < hotBarItems.Length; i++)
    {
      hotBarItems[i] = new InvItem(ItemsList.zero, 1);
    }
  }


  public static void SetSelectorPosition(int position)
  {
    selectorPosition = position;
    float x = selectorStartX + (position * inventoryCellSize * UIManager.scaleFactor);
    float y = selector.transform.position.y;
    selector.transform.position = new Vector2(x, y);
  }

  public static void addItem(Item takedItem, int takedQuant)
  {
    for (int i = 0; i < hotBarItems.Length; i++)
    {
      if (hotBarItems[i].item.itemName == takedItem.itemName && takedQuant > 0)
      {
        if (hotBarItems[i].quant + takedQuant <= hotBarItems[i].item.maxStack)
        {
          hotBarItems[i].quant = hotBarItems[i].quant + takedQuant;
          render();
          return;
        }
        else
        {
          takedQuant = takedQuant - (hotBarItems[i].item.maxStack - hotBarItems[i].quant);
          hotBarItems[i].quant = hotBarItems[i].item.maxStack;
          render();
        }
      }
    }

    for (int i = 0; i < hotBarItems.Length; i++)
    {
      if (hotBarItems[i].item.itemName == "zero" && takedQuant > 0)
      {
        if (hotBarItems[i].quant + takedQuant <= takedItem.maxStack)
        {
          hotBarItems[i].item = takedItem;
          hotBarItems[i].quant = takedQuant;
          render();
          return;
        }
        else
        {
          hotBarItems[i].item = takedItem;
          takedQuant = takedQuant - takedItem.maxStack;
          hotBarItems[i].quant = hotBarItems[i].item.maxStack;
          render();
        }
      }
    }
  }

  public static void addItemInPosition(Item takedItem, int takedQuant, int pos)
  {
    if (hotBarItems[pos].item.itemName == takedItem.itemName)
    {
      if (hotBarItems[pos].quant + takedQuant <= hotBarItems[pos].item.maxStack)
      {
        hotBarItems[pos].quant = hotBarItems[pos].quant + takedQuant;
        render();
        return;
      }
      else
      {
        takedQuant = takedQuant - (hotBarItems[pos].item.maxStack - hotBarItems[pos].quant);
        hotBarItems[pos].quant = hotBarItems[pos].item.maxStack;
        render();
      }
    }

    if (hotBarItems[pos].item.itemName == "zero" && takedQuant > 0)
    {
      if (hotBarItems[pos].quant + takedQuant <= takedItem.maxStack)
      {
        hotBarItems[pos].item = takedItem;
        hotBarItems[pos].quant = takedQuant;
        render();
        return;
      }
      else
      {
        hotBarItems[pos].item = takedItem;
        takedQuant = takedQuant - takedItem.maxStack;
        hotBarItems[pos].quant = hotBarItems[pos].item.maxStack;
        render();
      }
    }
  }

  public static void clearPosition(int pos)
  {
    Destroy(hotBarItems[pos].rep);
    hotBarItems[pos] = new InvItem(ItemsList.zero, 1);
    render();
  }

  public static void render()
  {
    for (int i = 0; i < hotBarItems.Length; i++)
    {
      if (hotBarItems[i].item.itemName != "zero" && hotBarItems[i].rep == null)
      {
        Item item = hotBarItems[i].item;

        float x = 67 + i * inventoryCellSize * UIManager.scaleFactor;
        float y = 147 * UIManager.scaleFactor;
        GameObject itemRep = Instantiate(itemRepObject, new Vector2(x, y), Quaternion.identity);
        itemRep.GetComponent<Image>().sprite = item.sprite;
        itemRep.transform.SetParent(GameObject.Find("ItensRep").transform);
        itemRep.GetComponentInChildren<Text>().text = hotBarItems[i].quant.ToString();
        itemRep.transform.localScale = Vector3.one;
        hotBarItems[i].rep = itemRep;
      }
      else if (hotBarItems[i].item.itemName != "zero" && hotBarItems[i].rep != null)
      {
        hotBarItems[i].rep.GetComponentInChildren<Text>().text = hotBarItems[i].quant.ToString();
      }
    }
  }

  public static void clearAndDropAll(GameObject go)
  {
    for (int i = 0; i < hotBarItems.Length; i++)
    {
      if (hotBarItems[i].item.itemName != "zero")
      {
        GameObject drop = Instantiate(itemDrop,
          new Vector2(go.transform.position.x + Random.Range(-0.72f, +0.72f),
          go.transform.position.y + Random.Range(-0.72f, +0.72f)),
          Quaternion.Euler(0, 0, Random.Range(0, 360))
          );

        drop.GetComponent<ItemDrop>().item = hotBarItems[i].item;
        drop.GetComponent<ItemDrop>().quant = hotBarItems[i].quant;
        drop.GetComponent<ItemDrop>().sprite = hotBarItems[i].item.sprite;
        clearPosition(i);
      }
    }
  }
}
