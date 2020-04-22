﻿
using UnityEngine;


public class Movement : Bolt.EntityBehaviour<ICustomCubeState>
{
    private float sens = 100f;
    public Transform playerbody;
    private float xRotate = 0;
    private float yRotate = 0;
    private Rigidbody rb;


    public override void Attached()
    {
        state.SetTransforms(state.CubeTransform, gameObject.transform);
    }

   private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    public override void SimulateOwner()
    { 

        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical") * sens * BoltNetwork.FrameDeltaTime);

        Vector3 newPos = rb.position + rb.transform.TransformDirection(movement);

        rb.MovePosition(newPos);

        float mouseX = Input.GetAxis("Mouse X") * sens * BoltNetwork.FrameDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sens * BoltNetwork.FrameDeltaTime;

        xRotate -= mouseY;
        yRotate += mouseX;

        xRotate = Mathf.Clamp(xRotate, -90, 90);

       
        transform.localRotation = Quaternion.Euler(xRotate, yRotate, 0f);
        playerbody.Rotate(Vector3.up * mouseX);
        playerbody.Rotate(Vector3.left * mouseY);
    }
}