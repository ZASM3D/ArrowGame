using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Handles character movement via a character controller
 */

public class CC_Con : MonoBehaviour
{

    private CharacterController charControl;
    private Vector3 moveDirection = Vector3.zero;
    private string horizontal = "HorizontalP1";
    private string jump = "JumpP1";
    private float startZ;
    private bool updated = false;

    public bool p1; // Are you the first player?
    public float speed;
    public float jumpSpeed;
    public float gravity;
    public readonly object deadLock = new object();

    void Start() {
        charControl = GetComponent<CharacterController>();
        if (!p1) {
            horizontal = "HorizontalP2";
            jump = "JumpP2";
        }
        startZ = this.transform.position.z;
    }

    void Update() {
        moveDirection = new Vector3(Input.GetAxis(horizontal) * speed, moveDirection.y, 0.0f);

        // Jumping only when on the ground
        if (charControl.isGrounded) {
            if (Input.GetButton(jump)) {
                moveDirection.y = jumpSpeed;
            } else {
                moveDirection.y = 0.0f;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        charControl.Move(moveDirection * Time.deltaTime);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, startZ);
    }

    // Let's the game know which player has died and awards points
    public void die() {
        lock (deadLock) {
            // Locked to help with points being scored repeatedly for 1 kill
            if (updated) return;
            updated = true;
            if (p1)
                GameData.P2Points++;
            else
                GameData.P1Points++;

            GameObject controller = GameObject.FindGameObjectWithTag("GameController");
            controller.GetComponent<GameController>().reloadScene();
            Destroy (this.gameObject);
        }
    }
}
