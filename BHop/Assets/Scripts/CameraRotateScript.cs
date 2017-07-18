using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public
    class CameraRotateScript : MonoBehaviour
{

    public
        float speed = 1f;

    public
        float minimumY = -60F;

    public
        float maximumY = 60F;

    float rotationY = 0F;

    Quaternion originalRotation;

    void Start()
    {
        originalRotation = transform.localRotation;
    }

    void Update()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        rotationY += Input.GetAxis("Mouse Y") * speed;
#endif

#if UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            rotationY += touch.deltaPosition.y * speed;
        }
#endif

        rotationY = ClampAngle(rotationY, minimumY, maximumY);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
        transform.localRotation = originalRotation * yQuaternion;
    }

    public
        static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
