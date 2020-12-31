using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Construction : MonoBehaviour
{
  GridGraph gridGraph;
  float constructableRadius;
  bool isConstructAllow, isDestructAllow;
  public Transform player;
  float selectorDistance;
  [SerializeField] private GameObject selector;
  [SerializeField] private GameObject box;
  public GameObject itemDrop;

  void Start()
  {
    isConstructAllow = true;
    isDestructAllow = true;
    gridGraph = AstarPath.active.data.gridGraph;
    constructableRadius = 3f;
  }

  void Update()
  {
    selectorDistance = Vector2.Distance(selector.transform.position, player.position);

    if (selectorDistance > constructableRadius)
    {
      isConstructAllow = false;
      isDestructAllow = false;
      selector.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
    }
    else
    {
      isConstructAllow = true;
      isDestructAllow = true;
      selector.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
    }

    if (InventoryHotBar.hotBarItems[InventoryHotBar.selectorPosition].item.buildable == true)
    {
      // selector.GetComponent<SpriteRenderer>.color = new Color(1, 1, 1, 1);
    }
    else if (InventoryHotBar.hotBarItems[InventoryHotBar.selectorPosition].item.gun == true)
    {
      isConstructAllow = false;
      isDestructAllow = false;
      selector.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }
    else
    {
      isConstructAllow = false;
      isDestructAllow = true;
      selector.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0f);
    }

    if (Input.GetMouseButtonUp(0) && isDestructAllow)
    {
      destroyBlock();
    }
    if (Input.GetMouseButtonUp(1) && isConstructAllow)
    {
      createBlock();
    }
  }

  void createBlock()
  {
    Vector2 gridPostion = Grid.WorldPositionToGridPosition(selector.transform.position);
    if (!Physics2D.OverlapCircle(gridPostion * WorldSettings.cellDiameter + Vector2.one * WorldSettings.cellRadius, WorldSettings.cellRadius - 0.1f))
    {
      Instantiate(box, selector.transform.position, Quaternion.identity);
      box.GetComponent<SpriteRenderer>().sprite = InventoryHotBar.hotBarItems[UIManager.selectorPosition].item.sprite;
      InventoryHotBar.hotBarItems[UIManager.selectorPosition].quant -= 1;
      if (InventoryHotBar.hotBarItems[UIManager.selectorPosition].quant <= 0)
      {
        InventoryHotBar.clearPosition(UIManager.selectorPosition);
      }
      else { InventoryHotBar.render(); }

      // REFATORAR
      Vector2 gridPosition = AStartController.WorldPositionToGridPosition(selector.transform.position);
      gridGraph.GetNode((int)gridPosition.x, (int)gridPosition.y).Walkable = false;

      FindObjectOfType<AudioManager>().Play("WoodHit" + Mathf.RoundToInt(Random.Range(1f, 3f)));
    }
  }

  void destroyBlock()
  {
    Collider2D collider = Physics2D.OverlapCircle((Vector2)this.transform.position + Vector2.one * 0.10f, 0.05f);

    if (collider && collider.tag == "block")
    {
      GameObject drop = Instantiate(itemDrop, selector.transform.position + (new Vector3(0.36f, 0.36f, 0)), Quaternion.identity);
      drop.GetComponent<ItemDrop>().item = ItemsList.woodBox;
      drop.GetComponent<ItemDrop>().quant = 1;
      Destroy(collider.gameObject);
      Vector2 gridPostion = Grid.WorldPositionToGridPosition(collider.transform.position);
      // PathfinderController.worldGrid.UpdateGridPosition((int)gridPostion.x, (int)gridPostion.y, new Node(true, collider.transform.position, (int)gridPostion.x, (int)gridPostion.y));

      // REFATORAR
      Vector2 gridPosition = AStartController.WorldPositionToGridPosition(selector.transform.position);
      gridGraph.GetNode((int)gridPosition.x, (int)gridPosition.y).Walkable = true;

      FindObjectOfType<AudioManager>().Play("WoodHit" + Mathf.RoundToInt(Random.Range(1f, 3f)));
    }

    if (collider && collider.tag == "Tree")
    {
      TreeController tree = collider.gameObject.GetComponent<TreeController>();
      Debug.Log("l:" + tree.life);
      tree.life -= 1;
    }
  }
}
