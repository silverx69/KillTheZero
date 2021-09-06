using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAutoHide : MonoBehaviour {

    public float delay = 3f;
    private float enableTime = 0f;

    private void OnEnable() {
        enableTime = Time.realtimeSinceStartup;
    }

    private void Reset() {
        enableTime = Time.realtimeSinceStartup;
    }

    void LateUpdate() {
        if (enableTime > 0f && Time.realtimeSinceStartup - enableTime >= delay) {
            enableTime = 0f;
            gameObject.SetActive(false);
        }
    }
}
