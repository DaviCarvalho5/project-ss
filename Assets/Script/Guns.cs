using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour
{
  GameObject playerHand;
  [SerializeField] GameObject bulletGO;
  public static bool gunOnHand;
  bool isWaiting;
  float waitTime;
  void Start()
  {
    playerHand = GameObject.Find("Player hand");
    gunOnHand = false;
    isWaiting = false;
    waitTime = 0.25f;
  }

  void Update()
  {
    if (Input.GetMouseButton(0) && gunOnHand && !isWaiting && PlayerAttributes.isLive)
    {
      GameObject bullet = Instantiate(bulletGO, playerHand.transform.position, playerHand.transform.rotation);
      FindObjectOfType<AudioManager>().Play("Shot" + Mathf.RoundToInt(Random.Range(1f, 3f)));
      Instantiate(GameplayController.particleShot, playerHand.transform.position, playerHand.transform.rotation);
      bullet.GetComponent<Bullet>().damage = 1;
      StartCoroutine(waitNextShot());
    }
  }

  IEnumerator waitNextShot()
  {
    isWaiting = true;
    yield return new WaitForSeconds(waitTime);
    isWaiting = false;
  }
}
