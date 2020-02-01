using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveParticalEffect : MonoBehaviour
{
    Ray rayFromCam;
    RaycastHit rayHit;
    [SerializeField] LayerMask mask;
    [SerializeField] GameObject particles;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        rayFromCam = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(rayFromCam, out rayHit, 4.7f, mask))
        {
            Debug.Log(rayHit.transform.gameObject.name);
            particles.transform.position = rayHit.collider.transform.position + (Vector3.forward * 2);
            Debug.DrawRay(Camera.main.transform.position, rayFromCam.direction * 4.8f, Color.green);
        }
    }
}
