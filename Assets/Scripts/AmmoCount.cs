using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCount : MonoBehaviour
{
    private Text display;
    private char arrow = '\u27b6';

    void Awake() {
        display = this.gameObject.GetComponent<Text>();
    }


    public void updateCount(int newCount) {
        if (!display) return;

        if (newCount == -1) {
            // In case of death
            display.text = "";
        } else {
            display.text = newCount.ToString() + arrow;
        }
    }
}
