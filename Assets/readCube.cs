using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class readCube : MonoBehaviour
{
    public Transform tUP;
    public Transform tDOWN;
    public Transform tLEFT;
    public Transform tRIGHT;
    public Transform tFRONT;
    public Transform tBACK;

    private List<GameObject> frontRay = new List<GameObject>();
    private List<GameObject> backRay = new List<GameObject>();
    private List<GameObject> leftRay = new List<GameObject>();
    private List<GameObject> rightRay = new List<GameObject>();
    private List<GameObject> upRay = new List<GameObject>();
    private List<GameObject> downRay = new List<GameObject>();

    private int layerMask = 1 << 8;
    cubeState state;
    cubeMap map;
    public GameObject emptyGO;
    void Start()
    {
        setRayTransform();
        state = FindObjectOfType<cubeState>();
        map = FindObjectOfType<cubeMap>();
        readState();
        state.started = true;
    }

    // Update is called once per frame
    void Update()
    {
        //readState();
    }
    public void readState()
    {
        state = FindObjectOfType<cubeState>();
        map = FindObjectOfType<cubeMap>();

        state.Up = readFace(upRay, tUP);
        state.Down = readFace(downRay, tDOWN);
        state.Left = readFace(leftRay, tLEFT);
        state.Right = readFace(rightRay, tRIGHT);
        state.Front = readFace(frontRay, tFRONT);
        state.Back = readFace(backRay, tBACK);

        map.Set();
    }
    void setRayTransform()
    {
        upRay = buildRays(tUP, new Vector3(90, 90, 0));
        downRay = buildRays(tDOWN, new Vector3(270, 90, 0));
        leftRay = buildRays(tLEFT, new Vector3(0, 180, 0));
        rightRay = buildRays(tRIGHT, new Vector3(0, 0, 0));
        frontRay = buildRays(tFRONT, new Vector3(0, 90, 0));
        backRay = buildRays(tBACK, new Vector3(0, 270, 0));
    }
    List <GameObject> buildRays(Transform rayTransform, Vector3 dir)
    {
        int rayCount = 0;
        List<GameObject> rays = new List<GameObject>();
        /* Ray 0 at top left, Ray 8 at bottom right
         0 1 2
         3 4 5
         6 7 8
         */
        for(int y = 1; y > -2; y--)
        {
            for (int x = -1; x < 2; x++)
            {
                Vector3 startPos = new Vector3(rayTransform.localPosition.x + x,
                                               rayTransform.localPosition.y + y,
                                               rayTransform.localPosition.z);
                GameObject rayStart = Instantiate(emptyGO, startPos, Quaternion.identity, rayTransform);
                rayStart.name = rayCount.ToString();
                rays.Add(rayStart);
                rayCount++;
            }
        }
        rayTransform.localRotation = Quaternion.Euler(dir);
        return rays;
    }
    public List<GameObject> readFace(List<GameObject> rayStarts, Transform rayTransform)
    {
        List<GameObject> facesHit = new List<GameObject>();
        
        foreach(GameObject rayStart in rayStarts)
        {
            Vector3 Ray = rayStart.transform.position;
            RaycastHit hit;
            //does the ray intersect any object in the layer mask?
            if (Physics.Raycast(Ray, rayTransform.forward, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(Ray, rayTransform.forward * hit.distance, Color.yellow);
                facesHit.Add(hit.collider.gameObject);
                //print(hit.collider.gameObject);
            }
            else
            {
                Debug.DrawRay(Ray, rayTransform.forward * 1000, Color.green);
            }  
        }
        return facesHit;
    }
}
