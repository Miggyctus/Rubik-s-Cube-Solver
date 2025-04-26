using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeState : MonoBehaviour
{
    public List <GameObject> Front = new List<GameObject>();
    public List <GameObject> Back = new List<GameObject>();
    public List <GameObject> Left = new List<GameObject>();
    public List <GameObject> Right = new List<GameObject>();
    public List <GameObject> Up = new List<GameObject>();
    public List <GameObject> Down = new List<GameObject>();
    public  bool autoRotating = false;
    public bool started = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void pickUp(List<GameObject> cubeSide)
    {
        foreach(GameObject face in cubeSide)
        {
            if (face != cubeSide[4])
            {
                face.transform.parent.transform.parent = cubeSide[4].transform.parent;
            }
        }
    }
    public void putDown(List<GameObject> littleCubes, Transform pivot)
    {
        foreach(GameObject littleCube in littleCubes)
        {
            if(littleCube != littleCubes[4])
            {
                littleCube.transform.parent.transform.parent = pivot;
            }
        }
    }
    string getSideString(List<GameObject> side)
    {
        string sideString = "";
        foreach(GameObject face in side)
        {
            sideString += face.name[0].ToString();
        }
        return sideString;
    }
    public string getStateString()
    {
        string stateString = "";
        stateString += getSideString(Up);
        stateString += getSideString(Right);
        stateString += getSideString(Front);
        stateString += getSideString(Down);
        stateString += getSideString(Left);
        stateString += getSideString(Back);
        return stateString;
    }
}
