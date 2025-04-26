using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cubeMap : MonoBehaviour
{
    private cubeState state;
    public Transform up;
    public Transform down;
    public Transform left;
    public Transform right;
    public Transform front;
    public Transform back;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Set()
    {
        state = FindObjectOfType<cubeState>();
        updateMap(state.Front, front);
        updateMap(state.Back, back);
        updateMap(state.Up, up);
        updateMap(state.Down, down);
        updateMap(state.Left, left);
        updateMap(state.Right, right);
    }
    void updateMap(List<GameObject> Face, Transform side)
    {
        int i = 0;
        foreach (Transform map in side)
        {
            if (Face[i].name[0] == 'F')
            {
                map.GetComponent<Image>().color = new Color(1, 0.5f, 0, 1);
            }
            if (Face[i].name[0] == 'B')
            {
                map.GetComponent<Image>().color = Color.red;
            }
            if (Face[i].name[0] == 'U')
            {
                map.GetComponent<Image>().color = Color.yellow;
            }
            if (Face[i].name[0] == 'D')
            {
                map.GetComponent<Image>().color = Color.white;
            }
            if (Face[i].name[0] == 'L')
            {
                map.GetComponent<Image>().color = Color.green;
            }
            if (Face[i].name[0] == 'R')
            {
                map.GetComponent<Image>().color = Color.blue;
            }
            i++;
        }
    }
}
