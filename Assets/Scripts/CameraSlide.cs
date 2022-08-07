using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSlide : MonoBehaviour
{
    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x + 0.01f, transform.position.y - 0.01f, -10f);
    }
}
