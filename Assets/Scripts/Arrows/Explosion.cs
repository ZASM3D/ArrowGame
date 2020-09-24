using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Covers the behavior of explosions
 */
public class Explosion : MonoBehaviour
{
    public List<Collider> hitPoints;
    public float duration;

    private float time;
    private Vector3 origScale;

    void Start() {
        time = Time.time;
        origScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update() {
        if (Time.time - time > duration) {
            Destroy (this.gameObject);
        }

        this.transform.localScale = origScale * (Time.time - time) / duration;
    }

    void OnTriggerStay (Collider other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<CC_Con>().die();
            return;
        }
    }
}
