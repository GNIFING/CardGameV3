using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMove : MonoBehaviour
{
    private GameObject highlightLeft;
    private GameObject highlightRight;
    private GameObject highlightBack;

    private Tile activeLeft;
    private Tile activeRight;
    private Tile activeBack;
    public void UnitMovePosition(int x, int y)
    {
        Debug.Log("Move To another position");
        //Move Diag
        if(x < 6 && y < 5 && x > 0)
        {
            highlightLeft = GameObject.Find($"Tile {x-1} {y+1}");
            highlightRight = GameObject.Find($"Tile {x+1} {y+1}");

            activeLeft = highlightLeft.GetComponent<Tile>();
            activeRight = highlightRight.GetComponent<Tile>();
            activeLeft.NextMoveHighlight(true);
            activeRight.NextMoveHighlight(true);
        }
        else if(x == 6 && y < 5)
        {
            highlightLeft = GameObject.Find($"Tile {x - 1} {y + 1}");
            activeLeft = highlightLeft.GetComponent<Tile>();
            activeLeft.NextMoveHighlight(true);
        }
        else if(x == 0 && y < 5)
        {
            highlightRight = GameObject.Find($"Tile {x + 1} {y + 1}");
            activeRight = highlightRight.GetComponent<Tile>();
            activeRight.NextMoveHighlight(true);
        }

        if( y > 0)
        {
            highlightBack = GameObject.Find($"Tile {x} {y - 1}");
            activeBack = highlightBack.GetComponent<Tile>();
            activeBack.NextMoveHighlight(true);
            Debug.Log("y>0");
        }
    }
    public void MoveFinish()
    {
        if(activeLeft != null)
        {
            activeLeft.NextMoveHighlight(false);
        }
        if(activeRight != null)
        {
            activeRight.NextMoveHighlight(false);
        }
        if (activeBack != null)
        {
            activeBack.NextMoveHighlight(false);
        }
    }
}
