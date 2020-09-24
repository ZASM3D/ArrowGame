using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Handles arrow behavior
 */

public class Arrow_Behav : MonoBehaviour
{

    private bool onGround;
    private bool hitPlayer = false;
    private float killVelocity = .125f;
    private Rigidbody rb;

    public List<Collider> hitPoints;
    public bool temp = false;
    public bool explode = false;
    public Object explosion;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity != Vector3.zero) {
            // Arrow looks more natural if it faces where it's going
            this.transform.rotation = Quaternion.LookRotation(rb.velocity);
        } else if (rb.velocity.sqrMagnitude <= killVelocity && hitPlayer) {
            hitPlayer = false;
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "bouncy" || collision.gameObject.tag == "Arrow") return;

        ContactPoint [] hits = new ContactPoint[16];
        int num = collision.GetContacts(hits);

        for (int i = 0; i < num && i < 16; i++) {
            if (hitPoints.Contains(hits[i].thisCollider)) {
                if (collision.gameObject.tag == "Player" && rb.velocity.sqrMagnitude >= killVelocity) {
                    collision.gameObject.GetComponent<CC_Con>().die();
                    return;
                }
                
                // Spawns the explosion at the tip of the arrow
                if (explode) Instantiate(explosion, this.transform.GetChild(0).GetChild(0).position, transform.rotation);
                if (temp) Destroy(this.gameObject);

                // Arrow sticks where it lands
                rb.velocity = Vector3.zero;
                rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY |
                    RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
                return;
            }
        }
    }

    void OnTriggerStay(Collider other) {
        OnTriggerEnter(other);
    }

    // Arrows tell the player to collect them
    void OnTriggerEnter(Collider other) {
        if (!hitPlayer && other.gameObject.tag == "Player" && rb.velocity.sqrMagnitude <= killVelocity) {
            hitPlayer = true;
            if (other.gameObject.GetComponent<Arrow_Fire>().Gather()) {
                Destroy(this.gameObject);
            }
        }
    }
}
