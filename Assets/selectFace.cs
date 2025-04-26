using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectFace : MonoBehaviour
{
    cubeState state;
    readCube read;
    int layerMask = 1 << 8;
    // Start is called before the first frame update
    void Start()
    {
        read = FindObjectOfType<readCube>();
        state = FindObjectOfType<cubeState>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !state.autoRotating)
        {
            //read curr state of the cube
            read.readState();
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f, layerMask))
            {
                GameObject face = hit.collider.gameObject;
                List<List<GameObject>> cubeSides = new List<List<GameObject>>()
                {
                    state.Up,
                    state.Down,
                    state.Left,
                    state.Right,
                    state.Front,
                    state.Back
                };
                //if the face hit exist within a side
                foreach(List<GameObject> cubeSide in cubeSides)
                {
                    if(cubeSide.Contains(face))
                    {
                        //pick it up
                        state.pickUp(cubeSide);
                        cubeSide[4].transform.parent.GetComponent<pivotRotation>().rotate(cubeSide);
                    }
                }
            }
        }
    }
}
