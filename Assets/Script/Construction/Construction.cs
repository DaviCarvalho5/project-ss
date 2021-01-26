using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Construction : MonoBehaviour
{
  public static GridGraph gridGraph;
  float constructableRadius;
  static bool isConstructAllow, isDestructAllow, isDistanceOK;
  public Transform player;
  float selectorDistance;
  private static GameObject selector;
  [SerializeField] public GameObject selectorNS;
  [SerializeField] private GameObject blockNS;
  [SerializeField] private static GameObject block;

  private void Awake()
  {
    selector = selectorNS;
    block = blockNS;
  }

  void Start()
  {
    isConstructAllow = false;
    isDestructAllow = false;
    gridGraph = AstarPath.active.data.gridGraph;
    constructableRadius = 3f;
    selector.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
  }

  void Update()
  {
    if (GameStates.isBuilding)
    {
      selectorDistance = Vector2.Distance(selector.transform.position, player.position);

      if (selectorDistance > constructableRadius)
      {
        isDistanceOK = false;
        SetConstructorSelectorOpacity(0.5f);
      }
      else
      {
        isDistanceOK = true;
        SetConstructorSelectorOpacity(1f);
      }
    }
  }

  public static void SetConstructorSelectorOpacity(float Alpha)
  {
    selector.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alpha);
  }

  public static void CreateBlock()
  {
    Vector2 gridPostion = Grid.WorldPositionToGridPosition(selector.transform.position);

    if (isDistanceOK && isConstructAllow && !Physics2D.OverlapCircle(gridPostion * WorldSettings.cellDiameter + Vector2.one * WorldSettings.cellRadius, WorldSettings.cellRadius - 0.1f))
    {
      Item item = PlayerInventory.slots[PlayerInventory.hotbarSlotSelected, 0].item;
      GameObject blockGO = Instantiate(block, selector.transform.position, Quaternion.identity);
      blockGO.GetComponent<SpriteRenderer>().sprite = item.sprite;
      blockGO.GetComponent<Block>().item = item;

      // REFATORAR
      PlayerInventory.slots[PlayerInventory.hotbarSlotSelected, 0].quant -= 1;
      PlayerInventoryController.Render();
      Hotbar.Render();

      // REFATORAR
      Vector2 gridPosition = AStartController.WorldPositionToGridPosition(selector.transform.position);
      gridGraph.GetNode((int)gridPosition.x, (int)gridPosition.y).Walkable = false;

      FindObjectOfType<AudioManager>().Play("WoodHit" + Mathf.RoundToInt(Random.Range(1f, 3f)));
    }

    if (PlayerInventory.slots[PlayerInventory.hotbarSlotSelected, 0].quant <= 0)
    {
      PlayerInventoryController.RemoveItemAt(PlayerInventory.hotbarSlotSelected, 0);

      PlayerInventoryController.Render();
      Hotbar.Render();
    }

  }

  public static void setConstruction(bool state)
  {
    isConstructAllow = state;
  }

  public static void setDesconstruction(bool state)
  {
    isDestructAllow = state;
  }

  public static void DestroyBlock()
  {
    Vector2 gridPostion = Grid.WorldPositionToGridPosition(selector.transform.position);
    Collider2D collider = Physics2D.OverlapCircle(gridPostion * WorldSettings.cellDiameter + Vector2.one * WorldSettings.cellRadius, WorldSettings.cellRadius - 0.1f, 9);

    if (collider && isDestructAllow)
    {
      if (collider.tag == "Tree")
      {
        TreeController tree = collider.gameObject.GetComponentInParent<TreeController>();
        FindObjectOfType<AudioManager>().Play("WoodHit" + Mathf.RoundToInt(Random.Range(1f, 3f)));
        tree.life -= 1;
      }

      if (collider.tag == "block")
      {
        GameObject itemDropGO = Instantiate(PlayerInventoryController.itemDrop);
        itemDropGO.GetComponent<ItemDrop>().item = collider.GetComponent<Block>().item;
        itemDropGO.GetComponent<ItemDrop>().quant = 1;
        itemDropGO.transform.position = collider.transform.position + Vector3.one * Random.Range(0.1f, 0.62f);
        FindObjectOfType<AudioManager>().Play("WoodHit" + Mathf.RoundToInt(Random.Range(1f, 3f)));
        Destroy(collider.gameObject);
      }
    }
  }

  public static void Interact()
  {
    Vector2 gridPostion = Grid.WorldPositionToGridPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0.4f, 0.4f, 0));
    Collider2D collider = Physics2D.OverlapCircle(gridPostion * WorldSettings.cellDiameter - Vector2.one * WorldSettings.cellDiameter, WorldSettings.cellRadius - 0.01f);

    if (collider)
    {
      if (collider.tag == "Door")
      {
        Debug.Log("a");
        collider.GetComponent<Door>().ChangeState();
      }
    }
  }
}
