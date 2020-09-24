using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Handles aiming and firing arrows as well as the actual
 * application of powerup effects
 */

public class Arrow_Fire : MonoBehaviour
{
    public float minPower = 2.0f;
    public GameObject reticle; // Used to indicate aiming direction
    public GameObject ammoDisplayPrefab;

    private AmmoCount myAmmo;
    private float power = 1.0f;
    private float multiOffset = 5; // Offset for multishot powerup
    private Vector3 aimDirection = new Vector3(1.0f, 0.0f, 0.0f);
    private float lastFire = -1.0f;

    // Various input definitions
    private string horizontal = "AimHP1";
    private string vertical = "AimVP1";
    private string fire = "FireP1";
    private CharacterController controller;

    public int numArrows = 1;
    public bool p1; // Is it the first player?
    public float maxPWR;
    public float rate; // Rate at which power builds
    public Rigidbody arrow;

    // The powerups
    public bool instantShot = false; 
    public bool multiShot = false;
    public bool explosiveShot = false;

    void Start() {
        controller = GetComponent<CharacterController>();
        power = minPower;
        if (!p1) {
            horizontal = "AimHP2";
            vertical = "AimVP2";
            fire = "FireP2";
        }

        myAmmo = Instantiate(ammoDisplayPrefab, GameData.Canvas.transform).GetComponent<AmmoCount>();
        myAmmo.updateCount(numArrows);
    }

    void Update() {
        TrackAim();

        myAmmo.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }

    void OnDestroy() {
        // For the UI to update propperly
        myAmmo.updateCount(-1);
    }

    /* Handles firing arrows and updating the reticule
     */
    void TrackAim() {
        aimDirection = new Vector3(Input.GetAxis(horizontal), Input.GetAxis(vertical), 0.0f);

        if (aimDirection.sqrMagnitude < .1) {
            if (controller.velocity.sqrMagnitude > .1) {
                // Shoot the direction you're moving if not specified
                aimDirection = controller.velocity.normalized;
            }
            // Hide the reticle
            reticle.transform.position = this.transform.position;
        } else {
            if (!instantShot) {
                // Reticle moves further away the more power you are using
                reticle.transform.position = this.transform.position + aimDirection * (.2f + power / (2 * maxPWR));
            } else {
                // Instantshot just maxes out the distance
                reticle.transform.position = this.transform.position + aimDirection * .7f;
            }
        }

        aimDirection = aimDirection.normalized;

        // Arrow firing sections
        if (numArrows > 0) {
            if (Input.GetButton(fire) && power < maxPWR && Time.time - lastFire > 1.0f) {
                power += rate * Time.deltaTime;
            } else if (power > minPower) {
                lastFire = Time.time;
                numArrows--;
                myAmmo.updateCount(numArrows);

                if (instantShot) power = maxPWR;
                Fire(false);

                // Spawns extra arrows at slight offset for multishot
                if (multiShot) {
                    aimDirection = Quaternion.Euler(0, 0, -multiOffset) * aimDirection;
                    Fire(false);
                    aimDirection = Quaternion.Euler(0, 0, multiOffset * 2) * aimDirection;
                    Fire(false);
                }
                power = minPower;
            }
        }
    }

    void Fire(bool temp) {
        Rigidbody clone;
        clone = Instantiate(arrow, this.transform.position + aimDirection * .55f, Quaternion.LookRotation(aimDirection));
        if (explosiveShot) clone.gameObject.GetComponent<Arrow_Behav>().explode = true;

        clone.velocity = aimDirection * power;
        clone.gameObject.GetComponent<Arrow_Behav>().temp = temp;
    }

    public bool Gather() {
        numArrows++;
        myAmmo.updateCount(numArrows);
        return true;
    }
}
