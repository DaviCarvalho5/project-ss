using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  float force = 15f;
  public int damage = 1;
  void Start()
  {
    GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right * force, ForceMode2D.Impulse);
  }
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Enemy")
    {
      Instantiate(GameplayController.particleBloodHit, transform.position, transform.rotation);
      FindObjectOfType<AudioManager>().Play("ZombieHit" + Mathf.RoundToInt(Random.Range(1f, 2f)));
    }
    else if (other.name == "Box(Clone)")
    {
      Instantiate(GameplayController.particleWoodHit, transform.position, transform.rotation);
      FindObjectOfType<AudioManager>().Play("WoodHit" + Mathf.RoundToInt(Random.Range(1f, 3f)));
    }
    Destroy(gameObject);
  }
}
