using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Handles powerup spawners and resetting the game after a
 * victory. 
 */

public class GameController : MonoBehaviour
{
    public List<GameObject> spawnLocations;
    private float lastFire = -8.0f;
    private int numPowerups = 3;
    public float delay = 5.0f;

    void Start() {
        // string []cons = Input.GetJoystickNames();
        // foreach (string s in cons) {
        //     Debug.Log(s);
        // }

        GameData.NextScene = SceneManager.GetActiveScene().name;
        Debug.Log ("P1: " + GameData.P1Points + " P2: " + GameData.P2Points);
    }

    void Update() {
        // Time to spawn some random powerups
        if (Time.time - lastFire > delay) {
            lastFire = Time.time;
            GameObject spawner = spawnLocations[Random.Range(0, spawnLocations.Count)];
            spawner.GetComponent<Powerup_Spawner>().Spawn(Random.Range(0, numPowerups));
        }
    }

    public void reloadScene() {
        // Pause time for a bit first
        Time.timeScale = .3f;
        StartCoroutine(waitToEnd());
    }

    private IEnumerator waitToEnd() {
        yield return new WaitForSecondsRealtime(1.0f);
        Time.timeScale = 1f;
        SceneManager.LoadScene (GameData.NextScene);
    }
}
