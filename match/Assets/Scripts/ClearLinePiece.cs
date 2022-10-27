using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearLinePiece : ClearablePiece
{
    public bool isRow;

    public override void Clear()
    {
        base.Clear();

        if (isRow)
        {
            // clear row
            piece.GridRef.ClearRow(piece.Y);
        }
        else
        {
            // clear column
            piece.GridRef.ClearColumn(piece.X);
        }
    }
}
