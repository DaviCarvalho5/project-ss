using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnsController : MonoBehaviour
{
  public static Transform[] spawnPoits;

  private void Awake()
  {
    GameObject[] go = GameObject.FindGameObjectsWithTag("Spawn");
    spawnPoits = new Transform[go.Length];

    for (int i = 0; i < go.Length; i++)
    {
      spawnPoits[i] = go[i].transform;
    }
  }

  public static Vector2 getRandom()
  {

    Vector2 p;
    int ID = Mathf.RoundToInt(Random.Range(0, spawnPoits.Length));

    p = spawnPoits[ID].position;

    return p;
  }
}
