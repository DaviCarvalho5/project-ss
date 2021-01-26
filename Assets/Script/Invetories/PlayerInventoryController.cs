using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryController : MonoBehaviour
{
  public static GameObject itemObject, inventoryUI, hotbarSelector, playerHand, itemDrop;
  public static Transform dropPoint;
  public static PlayerInventory playerInventory;
  public static CreateInventory createInventory;
  public static GameObject[,] slotObjects;
  public static bool debugMode;

  public GameObject itemObjectNS, inventoryUINS, hotbarSelectorNS, playerHandNS, itemDropNS;
  public Transform dropPointNS;
  public PlayerInventory playerInventoryNS;
  public CreateInventory createInventoryNS;
  public bool debugModeNS;

  private void Awake()
  {
    itemObject = itemObjectNS;
    inventoryUI = inventoryUINS;
    playerInventory = playerInventoryNS;
    createInventory = createInventoryNS;
    debugMode = debugModeNS;
    hotbarSelector = hotbarSelectorNS;
    playerHand = playerHandNS;
    dropPoint = dropPointNS;
    itemDrop = itemDropNS;
    slotObjects = new GameObject[playerInventory.size.x, playerInventory.size.y];
  }

  private void Start()
  {
    debugMode = false;
    createInventory.StartInventoryCreation();
    AddItem(ListOfItems.pistol, 1);
    AddItem(ListOfItems.woodenBox, 2);
    // debugMessage(DebugInventory());
    VerifyItemSelected();
  }

  public static int AddItem(Item catchedItem, int catchedQuant, GameObject itemRep = null)
  {
    for (int j = 0; j < playerInventory.size.y; j++)
    {
      for (int i = 0; i < playerInventory.size.x; i++)
      {
        Slot slot = PlayerInventory.slots[i, j];

        if (slot.item.itemName != "void" && catchedQuant > 0)
        {
          if (slot.item.itemName == catchedItem.itemName)
          {
            if (slot.quant + catchedQuant <= catchedItem.maxStack)
            {
              slot.quant += catchedQuant;
              debugMessage("[Player Inventory Controller] More " + catchedQuant + " " + catchedItem.itemName + " at (" + i + ", " + j + ")");
              catchedQuant = 0;
              Render();
              return catchedQuant;
            }
            else
            {
              int difference = catchedItem.maxStack - slot.quant;
              debugMessage("[Player Inventory Controller] More " + difference + " " + catchedItem.itemName + " at (" + i + ", " + j + ")");
              slot.quant += difference;
              catchedQuant -= difference;
            }
          }
        }
      }
    }

    for (int j = 0; j < playerInventory.size.y; j++)
    {
      for (int i = 0; i < playerInventory.size.x; i++)
      {
        Slot slot = PlayerInventory.slots[i, j];

        if (slot.item.itemName == "void" && catchedQuant > 0)
        {
          if (slot.quant + catchedQuant <= catchedItem.maxStack)
          {
            debugMessage("[Player Inventory Controller] It added " + catchedQuant + " " + catchedItem.itemName + " at (" + i + ", " + j + ")");

            PlayerInventory.slots[i, j].item = catchedItem;
            PlayerInventory.slots[i, j].quant = catchedQuant;

            catchedQuant = 0;
            Render();
            return catchedQuant;
          }
          else
          {
            PlayerInventory.slots[i, j].item = catchedItem;
            PlayerInventory.slots[i, j].quant = catchedItem.maxStack;

            debugMessage("[Player Inventory Controller] It added " + catchedItem.maxStack + " " + catchedItem.itemName + " at (" + i + ", " + j + ")");
            catchedQuant -= catchedItem.maxStack;
          }
        }
      }
    }
    debugMessage("[Player Inventory Controller] No more slots. " + catchedQuant + " items are remaining");
    Render();
    return catchedQuant;
  }

  public void RemoveAll()
  {
    for (int i = 0; i < playerInventory.size.x; i++)
    {
      for (int j = 0; j < playerInventory.size.y; j++)
      {
        PlayerInventory.slots[i, j] = new Slot(ListOfItems.voiditem, 0);
      }
    }
    debugMessage("[Player Inventory Controller] Player Invetory was cleaned");
  }

  public static void RemoveItemAt(int x, int y, bool death = false)
  {
    Slot slot = PlayerInventory.slots[x, y];

    if (slot.item.itemName != "void" && !slot.itemObject.GetComponent<ItemObject>().isCaught)
    {
      Destroy(slot.itemObject.gameObject);
    }

    PlayerInventory.slots[x, y] = new Slot(ListOfItems.voiditem, 0);
    debugMessage("[Player Inventory Controller] The position (" + x + ", " + y + ") was cleaned");

    Render();
  }

  public static void DropAllItems()
  {
    for (int j = 0; j < playerInventory.size.y; j++)
    {
      for (int i = 0; i < playerInventory.size.x; i++)
      {
        Slot slot = PlayerInventory.slots[i, j];
        DropItem(slot.item, slot.quant);
        RemoveItemAt(i, j, true);
      }
    }
    GameStates.ClearCatch();
    Hotbar.Render();
    Render();
  }

  public string DebugInventory()
  {
    string drawedInventory = "";

    for (int i = 0; i < playerInventory.size.x; i++)
    {
      for (int j = 0; j < playerInventory.size.y; j++)
      {
        Slot slot = PlayerInventory.slots[i, j];
        string text;

        if (slot.item.itemName != "void")
        {
          text = slot.item.itemName + " x " + slot.quant;
        }
        else
        {
          text = "empty ";
        }

        drawedInventory = drawedInventory + "[ " + "(" + i + ", " + j + ") " + text + " ] ";

        if (j == playerInventory.size.y - 1)
        {
          drawedInventory = drawedInventory + "\r\n";
        }
      }
    }

    return "[Player Inventory Controller] Player Invetory:\r\n" + drawedInventory;
  }

  public static int AddItemAt(Item catchedItem, int catchedQuant, int x, int y, GameObject itemRep = null)
  {
    Slot slot = PlayerInventory.slots[x, y];

    if (slot.item.itemName != "void")
    {
      if (slot.item.itemName == catchedItem.itemName)
      {
        if (slot.quant + catchedQuant <= catchedItem.maxStack)
        {
          PlayerInventory.slots[x, y].quant += catchedQuant;
          PlayerInventory.slots[x, y].itemObject.GetComponent<ItemObject>().quant += catchedQuant;
          Render();
          return 0;
        }
        else
        {
          PlayerInventory.slots[x, y].quant = catchedItem.maxStack;
          int difference = catchedQuant - (catchedItem.maxStack - PlayerInventory.slots[x, y].quant);
          Render();
          return difference;
        }
      }

      if (slot.item.itemName != catchedItem.itemName)
      {
        Render();
        return catchedQuant;
      }
    }
    else
    {
      if (catchedQuant <= catchedItem.maxStack)
      {
        PlayerInventory.slots[x, y].item = catchedItem;
        PlayerInventory.slots[x, y].quant = catchedQuant;
        Render();
        return 0;
      }
      else
      {
        PlayerInventory.slots[x, y].item = catchedItem;
        PlayerInventory.slots[x, y].quant = catchedItem.maxStack;
        Render();
        return catchedQuant - catchedItem.maxStack;
      }
    }
    Render();
    return 0;
  }

  public static void Render()
  {
    for (int i = 0; i < playerInventory.size.x; i++)
    {
      for (int j = 0; j < playerInventory.size.y; j++)
      {
        GameObject slotObject = slotObjects[i, j];
        Slot slot = PlayerInventory.slots[i, j];

        if (slot.item.itemName != "void" && slot.quant > 0)
        {
          if (!slot.isItemObjectRendered)
          {
            slot.isItemObjectRendered = true;
            SlotObject slotObjectCode = slotObject.GetComponent<SlotObject>();

            GameObject rep = Instantiate(itemObject);
            rep.GetComponentInChildren<ItemObject>().item = slot.item;
            rep.GetComponentInChildren<ItemObject>().quant = slot.quant;
            rep.GetComponentInChildren<ItemObject>().inventoryPosition = new Vector2Int(i, j);
            rep.GetComponentInChildren<Image>().sprite = slot.item.sprite;
            rep.GetComponentInChildren<Text>().text = slot.quant.ToString();
            rep.transform.SetParent(inventoryUI.transform);
            rep.transform.localScale = Vector2.one;
            slot.itemObject = rep;
            slot.itemObject.GetComponent<ItemObject>().quant = slot.quant;
            int x, y;

            Debug.Log("pos: " + i + "  " + j);

            x = i * 105 + 475;
            y = j == 0
              ? j * 100 + 135
              : j * 100 + 135 + 40;

            rep.transform.position = new Vector2(x, y);


          }
          else if (slot.isItemObjectRendered)
          {
            slot.itemObject.GetComponentInChildren<Text>().text = slot.quant.ToString();
            slot.itemObject.GetComponent<ItemObject>().quant = slot.quant;
          }
        }
        else if (slot.item.itemName != "void" && slot.quant == 0)
        {
          Destroy(slot.itemObject);
        }
      }
    }

    VerifyItemSelected();
  }

  public void takeAndItem(SlotObject slotObject)
  {
    for (int i = 0; i < playerInventory.size.x; i++)
    {
      for (int j = 0; j < playerInventory.size.y; j++)
      {
      }
    }
  }
  private static void debugMessage(string message)
  {
    if (debugMode)
    {
      Debug.Log(message);
    }
  }

  public static void SetSelection(int position)
  {
    PlayerInventory.hotbarSlotSelected = position;
    float x = 75 + position * (105);
    float y = hotbarSelector.transform.position.y;
    hotbarSelector.transform.position = new Vector2(x, y);
    VerifyItemSelected();
  }

  public static void VerifyItemSelected()
  {
    Item item = PlayerInventory.slots[PlayerInventory.hotbarSlotSelected, 0].item;

    if (item.itemName != "void")
    {
      if (item is HandableItem)
      {
        playerHand.GetComponent<SpriteRenderer>().sprite = item.getSprite();
      }

      if (item is BuildableItem)
      {
        GameStates.SetBuilding(true);
        GameStates.SetBuildingItem(item as BuildableItem);
      }
      else
      {
        GameStates.SetBuilding(false);
        Construction.SetConstructorSelectorOpacity(0f);
      }

      if (item is GunItem)
      {
        GunItem itemAsGun = item as GunItem;
        GameStates.SetHoldingAGun(true);
        GunsController.damage = itemAsGun.damage;
        GunsController.timeForNextShot = itemAsGun.timeForNextShot;
        FindObjectOfType<AudioManager>().Play("TakeGun1");
      }
      else
      {
        GameStates.SetHoldingAGun(false);
      }

      CursorController.UpdateCursor();
    }
    else
    {
      playerHand.GetComponent<SpriteRenderer>().sprite = null;
      GameStates.SetHoldingAGun(false);
      GameStates.SetBuilding(false);
      Construction.SetConstructorSelectorOpacity(0f);
      CursorController.UpdateCursor();
    }
  }

  public static void DropItem(Item item, int quant)
  {
    GameObject itemDropGO = Instantiate(itemDrop);
    itemDropGO.GetComponent<ItemDrop>().item = item;
    itemDropGO.GetComponent<ItemDrop>().quant = quant;
    itemDropGO.transform.position = dropPoint.position + Vector3.one * Random.Range(-2f, 2f);
  }
}
