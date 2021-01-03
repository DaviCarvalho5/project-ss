using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Enemy")
    {
      Instantiate(GameplayController.particleBloodHit, transform.position, transform.rotation);
      FindObjectOfType<AudioManager>().Play("ZombieHit" + Mathf.RoundToInt(Random.Range(1f, 2f)));
      Destroy(gameObject);
    }
  }
}
