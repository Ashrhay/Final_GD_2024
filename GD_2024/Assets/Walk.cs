using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{

    public float sensitivityX;
    public float sensitivityY;
    public Transform orientation;
    public float rotateX;
    public float rotateY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;

        rotateY += mouseX;
        rotateX += mouseY;
        //avoid looking up at more than 90 degrees
        rotateX = Mathf.Clamp(rotateX, -90f, 90f);
        
        transform.rotation=Quaternion.Euler(rotateX,rotateY,0);
        orientation.rotation = Quaternion.Euler(0, rotateY, 0);
        
    }
}