using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class rotateCube : MonoBehaviour
{
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    Vector3 previousMousePos;
    Vector3 mouseDelta;

    public GameObject Target;
    float speed = 200f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Swipe();
        Drag();
    }

    void Drag()
    {
        if(Input.GetMouseButton(1)) 
        {
            mouseDelta = Input.mousePosition - previousMousePos;
            mouseDelta *= 0.1f;
            transform.rotation = Quaternion.Euler(mouseDelta.y, -mouseDelta.x, 0) * transform.rotation;
        }
        else
        {
            if (transform.rotation != Target.transform.rotation)
            {
                var step = speed * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Target.transform.rotation, step);
            }
        }
        previousMousePos = Input.mousePosition;
    }
    void Swipe()
    {
        if (Input.GetMouseButtonDown(1))
        {
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if(Input.GetMouseButtonUp(1))
        {
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
            currentSwipe.Normalize();
            if (leftSwipe(currentSwipe))
            {
                Target.transform.Rotate(0, 90, 0, Space.World);
            }
            else if (rightSwipe(currentSwipe))
            {
                Target.transform.Rotate(0, -90, 0, Space.World);
            }
            else if (upLeftSwipe(currentSwipe))
            {
                Target.transform.Rotate(90, 0, 0, Space.World);
            }
            else if (downRightSwipe(currentSwipe))
            {
                Target.transform.Rotate(-90, 0, 0, Space.World);
            }
            else if (upRightSwipe(currentSwipe))
            {
                Target.transform.Rotate(0, 0, -90, Space.World);
            }
            else if (downLeftSwipe(currentSwipe))
            {
                Target.transform.Rotate(0, 0, 90, Space.World);
            }
        }
    }
    bool leftSwipe(Vector2 swipe)
    {
        return swipe.x < 0 && swipe.y > -0.5f && swipe.y < 0.5f;
    }    
    bool rightSwipe(Vector2 swipe)
    {
        return swipe.x > 0 && swipe.y > -0.5f && swipe.y < 0.5f;
    }
    bool upLeftSwipe(Vector2 swipe)
    {
        return swipe.y > 0 && swipe.x < 0f;
    }
    bool upRightSwipe(Vector2 swipe)
    {
        return swipe.y > 0 && swipe.x > 0f;
    }
    bool downLeftSwipe(Vector2 swipe)
    {
        return swipe.y < 0 && swipe.x < 0f;
    }
    bool downRightSwipe(Vector2 swipe)
    {
        return swipe.y < 0 && swipe.x > 0f;
    }

}