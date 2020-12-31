using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
  [SerializeField] GameObject flower;
  [SerializeField] GameObject tree;
  void Awake()
  {
    for (int i = 0; i < WorldSettings.worldWidthinCells; i++)
    {
      for (int j = 0; j < WorldSettings.worldWidthinCells; j++)
      {
        int generate = (int)Random.Range(0f, 50f);
        if (generate == 1)
        {
          Instantiate(flower, new Vector2(WorldSettings.cellDiameter * i + 0.36f, WorldSettings.cellDiameter * j + 0.36f), Quaternion.identity);
        }
      }
    }

    for (int i = 0; i < WorldSettings.worldWidthinCells / 2; i++)
    {
      for (int j = 0; j < WorldSettings.worldWidthinCells / 2; j++)
      {
        int generate = (int)Random.Range(0f, 50f);

        if (generate == 2)
        {
          Instantiate(tree, new Vector2(WorldSettings.cellDiameter * i * 2, WorldSettings.cellDiameter * j * 2 + 0.36f), Quaternion.Euler(1, 1, Random.Range(0f, 360f)));
        }
      }
    }
  }
}
