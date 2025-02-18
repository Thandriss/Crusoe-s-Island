using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class PlayerLook : MonoBehaviour
{
    public Camera camera;
    private float xRotation = 0f;
    public float xSen = 30f;
    public float ySen = 30f;
    
    public void look(Vector2 input)
    {
        float mX = input.x;
        float mY = input.y;
        xRotation -= (mY * Time.deltaTime) * ySen;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * (mX * Time.deltaTime) * xSen);
    }
}
