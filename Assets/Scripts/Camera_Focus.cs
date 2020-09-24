using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Lets the camera keep track of two people and tries to keep 
 * them both in frame without zooming out too far.
 */

public class Camera_Focus : MonoBehaviour
{

    public Transform focus1;
    public Transform focus2;
    public float minZoom;
    public float yOffset;
    public float maxZoom = 20;
    public float panThreshold = 10.0f;

    private Vector3 newPos;
    private float panSpeed = 5.0f;

    void Update() {

        // In case a player dies
        if (focus1 == null) {
            focus1 = focus2;
            minZoom = 5.0f;
        } else if (focus2 == null) {
            focus2 = focus1;
            minZoom = 5.0f;
        }

        // Finds 'ideal' position
        newPos = focus1.position - (focus1.position - focus2.position) / 2.0f;
        float diff = Mathf.Abs(focus1.position.x - focus2.position.x) +
                        Mathf.Abs(focus1.position.y - focus2.position.y) + minZoom;

        // Makes the camera pan smoothly not exceeding a certain speed
        if (diff > panThreshold) {
            panSpeed = diff / (panThreshold / 2);
        } else {
            panSpeed = 1.0f;
        }

        if (diff > maxZoom) {
            diff = maxZoom;
        }

        newPos = newPos + new Vector3(0.0f, yOffset * diff / 10, -diff);
        this.transform.position = Vector3.Lerp(this.transform.position, newPos, Time.deltaTime * panSpeed);
    }
}
