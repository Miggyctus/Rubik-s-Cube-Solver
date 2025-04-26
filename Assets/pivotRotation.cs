using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pivotRotation : MonoBehaviour
{
    private List<GameObject> activeSide;
    private Vector3 localForward;
    private Vector3 mouseRef;

    private bool dragging = false;

    private bool autoRotate = false;
    private Quaternion targetQuaternion;
    private float speed = 300.0f;

    private float sens = 0.4f;
    private Vector3 rotation;

    private cubeState state;
    private readCube read;
    void Start()
    {
        state = FindObjectOfType<cubeState>();
        read = FindObjectOfType<readCube>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (dragging && !autoRotate)
        {
            spinSide(activeSide);
            if(Input.GetMouseButtonUp(0))
            {
                dragging = false;
                rotateToRightAngle();
            }
        }
        if(autoRotate)
        {
            autoRotation();
        }
    }

    private void spinSide(List<GameObject> side)
    {
        rotation = Vector3.zero;

        Vector3 offSet = (Input.mousePosition - mouseRef);

        if (side == state.Up)
        {
            rotation.y = (offSet.x + offSet.y) * sens * 1;
        }
        if (side == state.Down)
        {
            rotation.y = (offSet.x + offSet.y) * sens * -1;
        }
        if (side == state.Left)
        {
            rotation.z = (offSet.x + offSet.y) * sens * 1;
        }
        if (side == state.Right)
        {
            rotation.z = (offSet.x + offSet.y) * sens * -1;
        }
        if (side == state.Front)
        {
            rotation.x = (offSet.x + offSet.y) * sens * -1;
        }
        if (side == state.Back)
        {
            rotation.x = (offSet.x + offSet.y) * sens * 1;
        }
        transform.Rotate(rotation, Space.Self);
        mouseRef = Input.mousePosition;
    }
    
    public void rotate(List<GameObject> side)
    {
        activeSide = side;
        mouseRef = Input.mousePosition;
        dragging = true;
        localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
    }
    public void startAutoRotate(List<GameObject> side, float angle)
    {
        state.pickUp(side);
        Vector3 localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
        targetQuaternion = Quaternion.AngleAxis(angle, localForward) * transform.localRotation;
        activeSide = side;
        autoRotate = true;
    }
    private void rotateToRightAngle()
    {
        Vector3 vec = transform.localEulerAngles;
        vec.x = Mathf.Round(vec.x / 90) * 90;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = Mathf.Round(vec.z / 90) * 90;
        targetQuaternion.eulerAngles = vec;
        autoRotate = true;
    }

    private void autoRotation()
    {
        dragging = false;
        var step = speed * Time.deltaTime;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);

        if(Quaternion.Angle(transform.localRotation, targetQuaternion) <= 1)
        {
            transform.localRotation = targetQuaternion;
            state.putDown(activeSide, transform.parent);
            read.readState();
            state.autoRotating = false;
            autoRotate = false;
            dragging = false;
        }
    }
}
