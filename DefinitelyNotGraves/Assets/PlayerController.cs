using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int MoveSpeed = 50;

    Rigidbody thisRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidBody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MouseLook();
        Movement();
    }

    void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X");

        Vector3 lookDir = gameObject.transform.eulerAngles;
        lookDir.y += mouseX;
        gameObject.transform.eulerAngles = lookDir;
    }

    void Movement()
    {
        Vector3 oldVel = thisRigidBody.velocity;
        float oldY = oldVel.y;

        bool forward = Input.GetKey(KeyCode.W);
        bool backward = Input.GetKey(KeyCode.S);
        bool strafeLeft = Input.GetKey(KeyCode.A);
        bool strafeRight = Input.GetKey(KeyCode.D);

        Vector3 inputVector = new Vector3();

        if (forward && !backward) inputVector.z = 1.0f;
        if (backward && !forward) inputVector.z = -1.0f;
        if (strafeLeft && !strafeRight) inputVector.x = -1.0f;
        if (strafeRight && !strafeLeft) inputVector.x = 1.0f;

        inputVector.Normalize();

        // Convert input & looking direction into desired movement direction
        Vector3 newVector = gameObject.transform.rotation * new Vector3(inputVector.x, 0f, inputVector.z);
        
        // Replace velocity
        newVector.y = oldY;

        thisRigidBody.velocity = newVector * MoveSpeed * thisRigidBody.mass * Time.deltaTime;
    }
}
