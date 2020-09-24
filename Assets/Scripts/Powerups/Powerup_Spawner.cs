using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Spawns powerups at a random point in the bounding box.
 * Waits until it gets called by the game director to spawn powerups.
 */

public class Powerup_Spawner : MonoBehaviour
{

    public GameObject power;

    public void Spawn(int pwr) {
        GameObject spawn = Instantiate(power, RandomPoint(GetComponent<Collider>().bounds),
                                Quaternion.identity);

        spawn.GetComponent<PowerUp>().setPowerup(pwr);
    }

    public static Vector3 RandomPoint(Bounds bounds) {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
