using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  [SerializeField] public int life;
  public bool isAttacking;
  void Start()
  {
    life = (int)Random.Range(5, 8);
    StartCoroutine(idleSound());
    isAttacking = false;
    StartCoroutine(changeAttack());
    Instantiate(GameplayController.particleSimpleBlackCircle, transform.position, transform.rotation);
  }

  void Update()
  {
    if (life <= 0)
    {
      Instantiate(GameplayController.particleSimpleCircle, transform.position, transform.rotation);
      Instantiate(GameplayController.blood, transform.position, transform.rotation);
      FindObjectOfType<AudioManager>().Play("ZombieDeath1");
      Destroy(gameObject);
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Bullet")
    {
      life -= 1;
    }
  }

  IEnumerator idleSound()
  {
    float waitTime = Random.Range(3f, 10f);
    yield return new WaitForSeconds(waitTime);
    AudioSource source = FindObjectOfType<AudioManager>().GetSound("ZombieIdle" + Mathf.RoundToInt(Random.Range(1f, 3f)));
    GetComponent<AudioSource>().clip = source.clip;
    GetComponent<AudioSource>().Play();
    StartCoroutine(idleSound());
  }

  IEnumerator changeAttack()
  {
    float waitTime = 0.1f;
    isAttacking = false;
    yield return new WaitForSeconds(waitTime);
    isAttacking = true;
    yield return new WaitForSeconds(Time.deltaTime);
    StartCoroutine(changeAttack());
  }
}
