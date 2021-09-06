using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFarAway : MonoBehaviour {

    public float destroyDistance = 1000f;
    // don't set from editor
    public bool wasDestroyed = false;

    void FixedUpdate() {
        if (Vector3.Distance(Camera.main.transform.position, transform.position) > destroyDistance) {
            wasDestroyed = true;
            Destroy(gameObject);
        }
    }
}
