using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (PlayerAttributes.isLive)
    {
      if (other.tag == "Drop")
      {
        ItemDrop item = other.gameObject.GetComponent<ItemDrop>();
        PlayerInventoryController.AddItem(item.item, item.quant);
        FindObjectOfType<AudioManager>().Play("TakeDrop" + Mathf.RoundToInt(Random.Range(1f, 3f)));
        Hotbar.Render();
        Destroy(other.gameObject);
      }
    }
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
  }
}
