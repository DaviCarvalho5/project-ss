using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
  public static GameObject particleSimpleCircle, particleShot, particleBloodHit, particleWoodHit, blood, particleSimpleBlackCircle;
  public GameObject particleSimpleCircleNS, particleShotNS, particleBloodHitNS, particleWoodHitNS, bloodNS, particleSimpleBlackCircleNS;
  public Text textDebug;
  [SerializeField] GameObject[] enemies;
  void Start()
  {
    particleSimpleCircle = particleSimpleCircleNS;
    particleShot = particleShotNS;
    particleBloodHit = particleBloodHitNS;
    particleWoodHit = particleWoodHitNS;
    particleWoodHit = particleWoodHitNS;
    blood = bloodNS;
    particleSimpleBlackCircle = particleSimpleBlackCircleNS;
    // spawnZombies(10);
    InventoryHotBar.addItem(ItemsList.woodBox, 5);
    InventoryHotBar.addItem(ItemsList.gun, 1);
    StartCoroutine(fpsUpdate());
  }

  IEnumerator fpsUpdate()
  {
    textDebug.text = "FPS: " + Mathf.RoundToInt((1 / Time.deltaTime)).ToString();
    yield return new WaitForSeconds(0.5f);
    StartCoroutine(fpsUpdate());
  }

  void spawnZombies(int numberOfZombies)
  {
    for (int i = 0; i < numberOfZombies; i++)
    {
      float x = Random.Range(0, WorldSettings.worldWidthinCells * WorldSettings.cellDiameter);
      float y = Random.Range(0, WorldSettings.worldWidthinCells * WorldSettings.cellDiameter);
      Vector2 position = new Vector2(x, y);

      Instantiate(enemies[0], position, Quaternion.identity);
    }
  }
}
