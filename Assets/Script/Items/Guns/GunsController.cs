using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsController : MonoBehaviour
{
  public static float damage;
  public static float timeForNextShot;
  public static float realoadTime;
  public static string ammunitionType;
  public static bool canShot = true;
  public static GameObject bullet;
  public GameObject bulletNS;
  public static Transform shotPoint;
  public Transform shotPointNS;

  private void Start()
  {
    bullet = bulletNS;
    shotPoint = shotPointNS;
    shotPoint = GameObject.Find("Player hand").transform;
  }

  public void Shot()
  {
    GameObject spawnedBullet = Instantiate(bullet);
    spawnedBullet.transform.rotation = shotPoint.rotation;
    spawnedBullet.transform.position = shotPoint.position;
    spawnedBullet.GetComponent<Rigidbody2D>().AddForce(shotPoint.transform.right * 15, ForceMode2D.Impulse);
    FindObjectOfType<AudioManager>().Play("Shot" + Mathf.RoundToInt(Random.Range(1f, 3f)));
    StartCoroutine("WaitTime", timeForNextShot);
  }

  IEnumerator WaitTime(float time)
  {
    canShot = false;
    yield return new WaitForSeconds(time);
    canShot = true;
  }
}
