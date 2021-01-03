using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
  public Item item;
  public int quant = 1;
  private float rotationSpeed;
  int deleteSeconds = 60 * 10;
  private void Start()
  {
    rotationSpeed = Random.Range(0.5f, 1f);
    gameObject.GetComponentInChildren<SpriteRenderer>().sprite = item.getSprite();
    transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
    StartCoroutine(DestroyAt(deleteSeconds));
  }

  private void Update()
  {
    if (quant <= 0)
    {
      this.SelfDestroy();
    }

    transform.Rotate(0, 0, rotationSpeed);
  }

  private IEnumerator DestroyAt(float seconds)
  {
    yield return new WaitForSeconds(seconds);
    this.SelfDestroy();
  }

  private void SelfDestroy()
  {
    Destroy(gameObject);
  }
}
