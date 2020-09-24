using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* A powerup box that hovers in the play area and gives
 * one of three abilities.
 */

public class PowerUp : MonoBehaviour
{
    public int power = 0;
    public Material instant;
    public Material multi;
    public Material explode;


    public void setPowerup(int pwr) {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        power = pwr;
        switch (power) {
            case 1:
                mr.material = instant;
                break;
            case 2:
                mr.material = explode;
                break;
            default:
                mr.material = multi;
                break;
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            switch (power) {
                case 1:
                    other.gameObject.GetComponent<Arrow_Fire>().instantShot = true;
                    break;
                case 2:
                    other.gameObject.GetComponent<Arrow_Fire>().explosiveShot = true;
                    break;
                default:
                    other.gameObject.GetComponent<Arrow_Fire>().multiShot = true;
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
