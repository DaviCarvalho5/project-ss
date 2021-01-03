using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
  Vector2 movimentTarget;
  [SerializeField] private float playerSpeed, standardPlayerSpeed, runSpeed;
  Camera mainCamera;
  [SerializeField] Rigidbody2D rb2d;

  void Start()
  {
    transform.position = SpawnsController.getRandom();
    mainCamera = Camera.main;
    standardPlayerSpeed = 4f;
    runSpeed = 6f;
    // rb2d = this.GetComponent<Rigidbody2D>();
  }

  void Update()
  {

    SetMovimentTarget();

    playerSpeed = Input.GetKey(KeyCode.LeftShift)
      ? playerSpeed = runSpeed
      : playerSpeed = standardPlayerSpeed;

    if (!PlayerAttributes.isLive || GameStates.isInventoryOpen)
    {
      playerSpeed = 0;
    }

    if (movimentTarget != Vector2.zero && playerSpeed == runSpeed && !GameStates.isInventoryOpen)
    {
      FindObjectOfType<AudioManager>().PlayLoop("Walk1", runSpeed / standardPlayerSpeed);
    }
    else if (movimentTarget != Vector2.zero && !GameStates.isInventoryOpen)
    {
      FindObjectOfType<AudioManager>().PlayLoop("Walk1");
    }
    else
    {
      FindObjectOfType<AudioManager>().Stop("Walk1");
    }

    // Vector2 gridPostion = Grid.WorldPositionToGridPosition(new Vector2(this.transform.position.x - WorldSettings.cellRadius, this.transform.position.y - WorldSettings.cellRadius));
    // PathfinderController.worldGrid.SetTargetPosition(gridPostion * WorldSettings.cellDiameter);
    // PathfinderController.worldGrid.UpdateGridRender();
  }

  void FixedUpdate()
  {
    rb2d.MovePosition(rb2d.position + movimentTarget * Time.deltaTime * playerSpeed);
  }

  void SetMovimentTarget()
  {
    movimentTarget.x = (Vector2.right * Input.GetAxisRaw("Horizontal")).x;
    movimentTarget.y = (Vector2.up * Input.GetAxisRaw("Vertical")).y;
  }
}
