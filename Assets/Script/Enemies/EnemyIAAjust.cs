using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyIAAjust : MonoBehaviour
{
  AIPath aiPath;
  GridGraph gridGraph;
  AIDestinationSetter aiDestinationSetter;
  [SerializeField] Transform playerTransform;

  float playerDistance;
  float distanceOfView;
  void Start()
  {
    aiPath = GetComponent<AIPath>();
    aiDestinationSetter = GetComponent<AIDestinationSetter>();
    playerTransform = GameObject.FindWithTag("Player").transform;

    aiPath.maxSpeed = Random.Range(2f, 10f);
    aiDestinationSetter.target = playerTransform;
    distanceOfView = Random.Range(5f, 8f);
  }

  void Update()
  {
    playerDistance = Vector2.Distance(transform.position, playerTransform.position);

    if (playerDistance <= distanceOfView)
    {
      aiPath.canMove = true;
      aiPath.canSearch = true;
    }
    else
    {
      aiPath.canMove = false;
      aiPath.canSearch = false;
    }
  }
}
