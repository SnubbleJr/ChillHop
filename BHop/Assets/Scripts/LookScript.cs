using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public
    class LookScript : MonoBehaviour
{

    public
        float speed = 1f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

#if UNITY_STANDALONE || UNITY_EDITOR
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseX * speed, 0);

        if (Input.GetButtonDown("Cancel"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
#endif

#if UNITY_IOS || UNITY_ANDROID

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector2 delta = touch.deltaPosition;

            transform.Rotate(0, delta.x * speed, 0);
        }
#endif
    }
}
