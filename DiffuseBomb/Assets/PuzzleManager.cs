using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject[] puzzlePieces; //Array of puzzle pieces
    public Transform[] puzzleSlots; //Array of puzzle slots where the pieces can be placed

    //public PuzzleScript puzzleScript;

    private bool[] puzzleComplete; //Array of booleans to check if each puzzle piece is in its correct slot


    public GameObject gameToReveal;

    void Start()
    {
        puzzleComplete = new bool[puzzlePieces.Length];
    }

    void Update()
    {
        CheckPuzzleComplete();
    }

    void CheckPuzzleComplete()
    {
        bool complete = true;

        for(int i = 0; i < puzzlePieces.Length; i ++)
        {
            if(!puzzleComplete[i])
            {
                complete = false;
                break;
            }
        }

        if(complete)
        {
            Debug.Log("Puzzle Complete!");
        }
    }

    public void CheckPieceSlot(GameObject piece)
    {
        int index = System.Array.IndexOf(puzzlePieces, piece);

        if(index != -1)
        {
            if (Vector3.Distance(piece.transform.position, puzzleSlots[index].position) < 0.5f)
            {
                puzzleComplete[index] = true;
            }
            else
            {
                puzzleComplete[index] = false;
            }
        }
    }
    public void OnPuzzleSolved()
    {
        gameToReveal.SetActive(true);
    }

    /* public void OnPuzzleSolved()
     {
         puzzleScript.OnPuzzleSolved();
    }*/
}
