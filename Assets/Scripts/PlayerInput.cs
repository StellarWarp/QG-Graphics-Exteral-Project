using System;
using UnityEngine;
public class PlayerInput : MonoBehaviour
{
    public static Vector2 rotate;
    public static Vector3 movement;
    public static float zoom;

    public static bool EditPlace()
    {
        return !Input.GetKey(KeyCode.X) && Input.GetMouseButtonDown(0);
    }

    public static bool EditRemove()
    {
        return Input.GetKey(KeyCode.X) && Input.GetMouseButtonDown(0);
    }

    void Update()
    {
        Vector2 mouseChange = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        
        rotate = Vector2.zero;
        if(Input.GetMouseButton(1))
        {
            rotate = mouseChange;
        }
        
        movement = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            movement += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += Vector3.right;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            movement += Vector3.down;
        }
        if (Input.GetKey(KeyCode.E))
        {
            movement += Vector3.up;
        }
        movement = Vector3.Normalize(movement);


        zoom = Input.mouseScrollDelta.y;
    }
}

