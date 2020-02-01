using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{
    public Rubiks rubiks;
    Vector3 frontRot;

    float mouseX;
    float mouseY;

    public float slowDownRate = 1;
    public float reverseVelocityRate = 3;

    Rigidbody rb;
    Vector3 currentAV;
    float time;

    Vector3 direction;

    Vector2[] mousePos;
    float delta;
    public float timeGapAcceleration = 0.05f;
    float timeGap;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mousePos = new Vector2[2];
        mousePos[0] = Vector2.zero;
        mousePos[1] = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        direction = new Vector3(-mouseY, -mouseX, 0).normalized;

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKey(KeyCode.Mouse1) && !rubiks.Working())
        {
            rb.AddTorque(direction, ForceMode.VelocityChange);
            time = 0;
        }
        else
        {
            if (time == 0)
                currentAV = rb.angularVelocity;
            time += Time.deltaTime;
            rb.angularVelocity = Vector3.Lerp(currentAV, Vector3.zero, time * slowDownRate);
        }

        rb.angularVelocity -= rb.angularVelocity / reverseVelocityRate;
    }
}
