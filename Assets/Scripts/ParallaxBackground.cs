using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    Transform cameraTransform;
    Sprite sprite;
    Texture2D texture;
    Vector3 cameraLastPosition;
    Vector3 deltaMovement;
    [SerializeField] Vector2 parallaxMultiplier;
    float textureUnitSizeX;
    float textureUnitSizeY;
    float offsetPositionX;
    float offsetPositionY;
    void Start()
    {
        cameraTransform = Camera.main.transform;
        cameraLastPosition = cameraTransform.position;
        sprite = GetComponent<SpriteRenderer>().sprite;
        texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
    }

    void LateUpdate()
    {
        deltaMovement = cameraTransform.position - cameraLastPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxMultiplier.x, deltaMovement.y * parallaxMultiplier.y);
        cameraLastPosition = cameraTransform.position;
        
        if(Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
        {
            offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y);
        }
        if(Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textureUnitSizeY)
        {
            offsetPositionY = (cameraTransform.position.y - transform.position.y) % textureUnitSizeY;
            transform.position = new Vector3(transform.position.x, cameraTransform.position.y + offsetPositionY);
        }
    }
}
