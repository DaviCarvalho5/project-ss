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
      Instantiate(GameplayController.particleSimpleBlackCircle, transform.position, transform.rotation);
      GameObject drop = Instantiate(itemDrop, transform.position, transform.rotation);
      drop.GetComponent<ItemDrop>().item = ListOfItems.woodenBox;
      drop.GetComponent<ItemDrop>().quant = Mathf.RoundToInt(Random.Range(1, 3));

      Destroy(gameObject);
    }
  }
}
