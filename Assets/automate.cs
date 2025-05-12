using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static rotateCube;

public class automate : MonoBehaviour
{
    public static List<string> moveList = new List<string>() { };
    public static List<string> pll = new List<string>() { };
    public static List<string> oll = new List<string>() { };
    public static List<string> pattern = new List<string>() { };
    //public static List<string> pllUa = new List<string>() { };
    //public static List<string> pllUb = new List<string>() { };
    private bool lastAlgoWasPLLorOLL = false;
    public GameObject algoRawImage;
    public GameObject algoCameraObject;
    private rotateCube rotation;

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
        rotation = FindObjectOfType<rotateCube>();
    }

    // Update is called once per frame
    void Update()
    {
        if(moveList.Count > 0 && !state.autoRotating && state.started)
        {
            lastAlgoWasPLLorOLL = false;
            doMove(moveList[0]);
            moveList.Remove(moveList[0]);
        }
        if(pll.Count > 0 && !state.autoRotating && state.started)
        {
            lastAlgoWasPLLorOLL = true;
            doMove(pll[0]);
            pll.Remove(pll[0]);
            //algoCameraObject.SetActive(true);
        }
        if (oll.Count > 0 && !state.autoRotating && state.started)
        {
            lastAlgoWasPLLorOLL = true;
            doMove(oll[0]);
            oll.Remove(oll[0]);
            //algoCameraObject.SetActive(true);
        }
        if (pattern.Count > 0 && !state.autoRotating && state.started)
        {
            doMove(pattern[0]);
            pattern.Remove(pattern[0]);
        }
        updateAlgoCameraState();
    }

    void updateAlgoCameraState()
    {
        bool isAlgo = pll.Count > 0 || oll.Count > 0;

        bool active = isAlgo || (state.autoRotating && lastAlgoWasPLLorOLL);

        algoCameraObject.SetActive(active);
        if (algoRawImage != null)
            algoRawImage.SetActive(active);
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
    public void doPermT()
    {
        if (pll.Count == 0)
        {
            pll = new List<string>()
            {
                "R", "U", "R'", "U'", "R'", "F",
                "R2", "U'", "R'", "U'", "R", "U",
                "R'", "F'",
            };
            Debug.Log("Doing: " + string.Join(" ", pll));
        }
    }
    public void doPermY()
    {
        if (pll.Count == 0)
        {   //F R U' R' U' R U R' F' R U R' U' R' F R F'
            pll = new List<string>()
            {
                "F", "R", "U'", "R'", "U'", "R",
                "U", "R'", "F'", "R", "U", "R'",
                "U'", "R'", "F", "R", "F'"
            };
            Debug.Log("Doing: " + string.Join(" ", pll));
        }
    }
    public void doPermUa()
    {
        if (pll.Count == 0)
        {           //R2 U R U R' U' R' U' R' U R'
            pll = new List<string>()
            {
                "R2", "U", "R", "U", "R'", "U'",
                "R'", "U'", "R'", "U", "R'",
            };
            Debug.Log("Doing: " + string.Join(" ", pll));
        }
    }
    public void doPermUb()
    {
        if (pll.Count == 0)
        {           //L2 U' L' U' L U L U L U' L
            pll = new List<string>()
            {
                "L2", "U'", "L'", "U'", "L", "U",
                "L", "U", "L", "U'", "L",
            };
            Debug.Log("Doing: " + string.Join(" ", pll));
        }
    }
    //21
    public void doOrient21()
    {
        if(oll.Count == 0)
        {
            oll = new List<string>()
            {       //F, 3 sexy moves, F'
                "F", "R", "U", "R'", "U'",
                "R", "U", "R'", "U'",
                "R", "U", "R'", "U'", "F'",
            };
        }
    }
    //22
    public void doOrient22()
    {
        if (oll.Count == 0)
        {           //R' U2 R2 U R2 U R2 U2 R'
            oll = new List<string>()
            {
                "R'", "U2", "R2", "U", "R2",
                "U", "R2", "U2", "R'",  
            };
        }
    }
    //23
    public void doOrient23()
    {
        if (oll.Count == 0)
        {           //R2 D' R U2 R' D R U2 R
            oll = new List<string>()
            {
                "R2", "D'", "R", "U2", "R'",
                "D", "R", "U2", "R",
            };
        }
    }
    public void doOrient26()
    {
        if(oll.Count == 0)
        {       //R U2 R' U' R U' R'
            oll = new List<string>()
            {
                "R", "U2", "R'", "U'",
                "R", "U'", "R'",
            };
        }
    }
    public void doOrient27()
    {
        if (oll.Count == 0)
        {       //R U R' U R U2 R'
            oll = new List<string>()
            {
                "R", "U", "R'", "U",
                "R", "U2", "R'",
            };
        }
    }
    public void doOrient33()
    {
        if (oll.Count == 0)
        {       //R U R' U' R' F R F'
            oll = new List<string>()
            {
                "R", "U", "R'", "U'",
                "R'", "F", "R", "F'",
            };
        }
    }
    public void doPattern1()
    {
        if (pattern.Count == 0)
        {       //U B D' F2 D B' U' R2 D F2 D' R2 D F2 D' R2
            pattern = new List<string>()
            {
                "U", "B", "D'", "F2", "D", "B'",
                "U'", "R2", "D", "F2", "D'", "R2",
                "D", "F2", "D'", "R2",
            };
        }
    }
    public void doPattern2()
    {
        if (pattern.Count == 0)
        {       //U2 D2 R2 L2 F2 B2
            pattern = new List<string>()
            {
                "U2", "D2", "R2", "L2", "F2",
                "B2",
            };
        }
    }
    public void doPattern3()
    {
        if (pattern.Count == 0)
        {       //F U F R L2 B D' R D2 L D' B R2 L F U F
            pattern = new List<string>()
            {
                "F", "U", "F", "R", "L2", "B",
                "D'", "R", "D2", "L", "D'", "B",
                "R2", "L", "F", "U", "F",
            };
        }
    }public void doPattern4()
    {
        if (pattern.Count == 0)
        {       //F L F U' R U F2 L2 U' L' B D' B' L2 U
            pattern = new List<string>()
            {
                "F", "L", "F", "U'", "R", "U",
                "F2", "L2", "U'", "L'", "B", "D'",
                "B'", "L2", "U",
            };
        }
    }
}


