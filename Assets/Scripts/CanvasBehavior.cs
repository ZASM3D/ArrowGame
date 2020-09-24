using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Basic scoreboard setup
 */

public class CanvasBehavior : MonoBehaviour
{
    public GameObject pointsDisplay;

    // Start is called before the first frame update
    void Awake() {
        GameData.Canvas = this.gameObject;
    }

    // Update is called once per frame
    void Start() {
        Text display = pointsDisplay.GetComponent<Text>();
        display.text = "SCORE\n" + GameData.P1Points + " - " + GameData.P2Points;
    }
}
