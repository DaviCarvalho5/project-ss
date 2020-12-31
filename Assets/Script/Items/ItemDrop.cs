using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
  public Item item;
  public int quant;
  public Sprite sprite;

  void Start()
  {
    GetComponent<SpriteRenderer>().sprite = sprite;
    transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
  }
}
