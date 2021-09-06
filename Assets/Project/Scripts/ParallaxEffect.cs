using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour {

    public Vector2 effectMultiplier;

    private Transform cameraTransform;
    private Vector3 lastCameraPostition;
    private float textureUnitWidth;
    private float textureUnitHeight;

    void Start() {
        cameraTransform = Camera.main.transform;
        lastCameraPostition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitWidth = (texture.width / sprite.pixelsPerUnit) * transform.localScale.x;
        textureUnitHeight = (texture.height / sprite.pixelsPerUnit) * transform.localScale.y;
    }

    void FixedUpdate() {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPostition;
        Vector3 newPosition = transform.position;

        //newPosition += new Vector3(
        //    deltaMovement.x * effectMultiplier.x, 
        //    deltaMovement.y * effectMultiplier.y);

        float cameraDeltaX = Mathf.Floor(cameraTransform.position.x - transform.position.x);
        float cameraDeltaY = Mathf.Floor(cameraTransform.position.y - transform.position.y);

        if (Mathf.Abs(cameraDeltaX) >= textureUnitWidth)
            newPosition = new Vector3(cameraTransform.position.x - (cameraDeltaX < 0 ? 1 : 0), newPosition.y);

        if (Mathf.Abs(cameraDeltaY) >= textureUnitHeight)
            newPosition = new Vector3(newPosition.x, cameraTransform.position.y - (cameraDeltaY < 0 ? 1 : 0));
        
        transform.position = newPosition;
        lastCameraPostition = cameraTransform.position;
    }
}
