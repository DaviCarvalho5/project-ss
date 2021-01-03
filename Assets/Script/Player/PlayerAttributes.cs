using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
  public static bool isLive;
  public static float life;
  public static float maxLife;
  public GameObject deathFace;
  bool isDeathTheme;
  bool deathFaceOn;
  [SerializeField] Transform lifeBar;
  void Start()
  {
    deathFaceOn = false;
    isLive = true;
    maxLife = 10f;
    life = maxLife;
    isDeathTheme = true;
  }

  void Update()
  {
    if (life >= 0)
    {
      Vector3 newScale = Vector3.forward + Vector3.up + (Vector3.right * (life / maxLife));
      lifeBar.localScale = Mathf.Abs(lifeBar.localScale.x - newScale.x) > 0.01f
      ? Vector3.Lerp(lifeBar.localScale, newScale, 0.1f)
      : lifeBar.localScale;
    }

    if (life <= 0)
    {
      isLive = false;
      Instantiate(GameplayController.blood, transform.position, transform.rotation);

      if (isDeathTheme)
      {
        FindObjectOfType<AudioManager>().Play("DeathTheme");
        isDeathTheme = false;
      }
    }

    if (!deathFaceOn && !isLive)
    {
      UIManager.SetInventoryActive(false);
      PlayerInventoryController.DropAllItems();
      OpenDeathScreen();
    }

    if (Input.GetKeyUp(KeyCode.Space) && !isLive && deathFaceOn)
    {
      respawn();
    }
  }

  void OnCollisionStay2D(Collision2D other)
  {
    takeAHit(other);
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    takeAHit(other);
  }

  void takeAHit(Collision2D other)
  {
    if (other.gameObject.tag == "Enemy" && other.gameObject.GetComponent<Enemy>().isAttacking && life > 0)
    {
      life -= 1f;
      FindObjectOfType<AudioManager>().Play("PlayerHit" + Mathf.RoundToInt(Random.Range(1f, 4f)));
      Instantiate(GameplayController.particleBloodHit, other.GetContact(0).point, other.gameObject.transform.rotation);
    }
  }

  void OpenDeathScreen()
  {
    deathFace.SetActive(true);
    deathFaceOn = true;
  }

  void CloseDeathScreen()
  {
    deathFace.SetActive(false);
    deathFaceOn = false;
  }

  void respawn()
  {
    life = maxLife;
    isLive = true;
    CloseDeathScreen();
    transform.position = SpawnsController.getRandom();
    FindObjectOfType<AudioManager>().Play("Respaw1");
    isDeathTheme = true;
  }
}
