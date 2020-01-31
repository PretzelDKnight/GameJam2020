using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float forward;
    float side;
    float mouseX;
    float mouseY;

    Rigidbody rb;

    public float limit;

    Quaternion currentRot;

    public float rate;

    float timeF = 0;
    float lastF;
    float timeS = 0;
    float lastS;

    public float slowDownRate = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        forward = Input.GetAxis("Vertical");
        side = Input.GetAxis("Horizontal");
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        //transform.position -= new Vector3(side, 0, forward) * rate;
        rb.AddForce(transform.right * rate * side, ForceMode.Impulse);
        rb.AddForce(transform.forward * rate * forward, ForceMode.Impulse);

        if (Mathf.Abs(rb.velocity.x) >= limit)
        {
            float temp = rb.velocity.x;
            float tempAbs = Mathf.Abs(rb.velocity.x);
            rb.velocity = new Vector3(limit * temp/tempAbs, 0, rb.velocity.z);
        }

        if (Mathf.Abs(rb.velocity.z) >= limit)
        {
            float temp = rb.velocity.z;
            float tempAbs = Mathf.Abs(rb.velocity.z);
            rb.velocity = new Vector3(rb.velocity.x, 0, limit * temp / tempAbs);
        }

        // Slows down and sets starting velocities back to 0
        // To Solve : Direction of force not normalized hence time taken to 
        // slow down on either axis is not the same, leading to curves 
        // and twists depending on direction of force
        if (forward == 0)
        {
            if (timeF == 0)
                lastF = rb.velocity.z;
            timeF += Time.deltaTime * slowDownRate;
            rb.velocity = new Vector3(rb.velocity.x, 0, Mathf.Lerp(lastF, 0, timeF));
        }
        else
            timeF = 0;

        if (side == 0)
        {
            if (timeS == 0)
                lastS = rb.velocity.x;
            timeS += Time.deltaTime * slowDownRate;
            rb.velocity = new Vector3(Mathf.Lerp(lastS, 0, timeS), 0, rb.velocity.z);
        }
        else
            timeS = 0;

        currentRot = transform.rotation;
        currentRot.eulerAngles += new Vector3(0, mouseX, 0);
        transform.rotation = currentRot;
    }
}
