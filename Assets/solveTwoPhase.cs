using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kociemba;
using System.Security.Cryptography;

public class solveTwoPhase : MonoBehaviour
{
    private cubeState state;
    private readCube read;
    private bool doOnce = true;
    void Start()
    {
        state = FindObjectOfType<cubeState>();
        read = FindObjectOfType<readCube>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state.started && doOnce)
        {
            doOnce = false;
            Solver();
        }
    }

    public void Solver()
    {
        read.readState();
        //get state of the cube as string
        string moveString = state.getStateString();
        print(moveString);
        //solve the cube
        string info = "";
        //first time use build the tables, this line of code is used only one time during production
        //users don't need to build the tables
        //string solution = SearchRunTime.solution(moveString, out info, buildTables: true);
        //every other time
        string solution = Search.solution(moveString, out info);
        //convert the solved moves from a string to a list
        List<string> solutionList = stringToList(solution);

        //automate the list
        automate.moveList = solutionList;
        print(info);
    }
    List<string> stringToList(string solution)
    {
        List<string> solutionList = new List<string>(solution.Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries));
        return solutionList;
    }    
}
