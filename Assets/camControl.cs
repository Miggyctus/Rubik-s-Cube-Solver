using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camControl : MonoBehaviour
{
    public Transform cube;         
    public Transform frontFace;    
    private Vector3 localOffset = new Vector3(-9.6f, 6.53f, -8.2f); // -9.6 6.53 -8.2
    private float smoothSpeed = 30f;

    void LateUpdate()
    {
        if (cube == null || frontFace == null) return;

        // Calculate position of the camera relative to the cube 
        Vector3 worldOffset = cube.TransformDirection(localOffset);
        Vector3 targetPos = frontFace.position + worldOffset;

        // smooth movement
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smoothSpeed);

        // Make it look to F face, with U up
        Vector3 upDir = cube.transform.up;
        transform.rotation = Quaternion.LookRotation(frontFace.position - transform.position, upDir);
    }
}

