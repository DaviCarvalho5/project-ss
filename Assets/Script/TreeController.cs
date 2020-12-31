using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
  public GameObject itemDrop;
  public int life;
  void Start()
  {
    life = Mathf.RoundToInt(Random.Range(2f, 5f));
  }

  void Update()
  {
    if (life <= 0)
    {
      Debug.Log(life);
      GameObject drop = Instantiate(itemDrop, transform.position, transform.rotation);
      drop.GetComponent<ItemDrop>().item = ItemsList.woodBox;
      drop.GetComponent<ItemDrop>().quant = Mathf.RoundToInt(Random.Range(2f, 4f));
      drop.GetComponent<ItemDrop>().sprite = ItemsList.woodBox.sprite;
      Destroy(transform.parent.gameObject);
    }
  }
}
