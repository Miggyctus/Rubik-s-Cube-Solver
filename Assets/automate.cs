using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class automate : MonoBehaviour
{
    public static List<string> moveList = new List<string>() { };
    private readonly List<string> allMoves = new List<string>() 
    {
        "U", "D", "R", "L", "F", "B",
        "U2", "D2", "R2", "L2", "F2", "B2",
        "U'", "D'", "R'", "L'", "F'", "B'",
    };
    private cubeState state;
    private readCube read;
    void Start()
    {
        state = FindObjectOfType<cubeState>();
        read = FindObjectOfType<readCube>();
    }

    // Update is called once per frame
    void Update()
    {
        if(moveList.Count > 0 && !state.autoRotating && state.started)
        {
            doMove(moveList[0]);
            moveList.Remove(moveList[0]);
        }
    }
    public void shuffle()
    {
        List<string> moves = new List<string>();
        int shuffleL = Random.Range(15, 30);
        for(int i = 0; i < shuffleL; i++)
        {
            int randomMove = Random.Range(0, allMoves.Count);
            moves.Add(allMoves[randomMove]);
        }
        moveList = moves;
        Debug.Log("Shuffle Moves: " + string.Join(" ", moveList));
    }
    void doMove(string move)
    {
        read.readState();
        state.autoRotating = true;
        if (move == "U")
        {
            rotateSide(state.Up, -90);
        }
        if (move == "U'")
        {
            rotateSide(state.Up, 90);
        }
        if (move == "U2")
        {
            rotateSide(state.Up, 180);
        }
        if (move == "D")
        {
            rotateSide(state.Down, -90);
        }
        if (move == "D'")
        {
            rotateSide(state.Down, 90);
        }
        if (move == "D2")
        {
            rotateSide(state.Down, 180);
        }
        if (move == "R")
        {
            rotateSide(state.Right, -90);
        }
        if (move == "R'")
        {
            rotateSide(state.Right, 90);
        }
        if (move == "R2")
        {
            rotateSide(state.Right, 180);
        }
        if (move == "L")
        {
            rotateSide(state.Left, -90);
        }
        if (move == "L'")
        {
            rotateSide(state.Left, 90);
        }
        if (move == "L2")
        {
            rotateSide(state.Left, 180);
        }
        if (move == "F")
        {
            rotateSide(state.Front, -90);
        }
        if (move == "F'")
        {
            rotateSide(state.Front, 90);
        }
        if (move == "F2")
        {
            rotateSide(state.Front, 180);
        }
        if (move == "B")
        {
            rotateSide(state.Back, -90);
        }
        if (move == "B'")
        {
            rotateSide(state.Back, 90);
        }
        if (move == "B2")
        {
            rotateSide(state.Back, 180);
        }
    }
    private void rotateSide(List<GameObject> side, float angle)
    {
        pivotRotation pR = side[4].transform.parent.GetComponent<pivotRotation>();
        pR.startAutoRotate(side, angle);
    }
}
