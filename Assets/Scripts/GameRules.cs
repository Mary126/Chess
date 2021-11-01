using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    private GameManager gameManager;
    private int moveDirection = -1;
    private int PawnPosition = 6;

    bool CheckMoveDirection(int row, int i)
    {
        if (gameManager.instances.playlablePosition == "top")
        {
            moveDirection = -1;
            PawnPosition = 1;
            if (row + i <= 7) {
                return true;
            }
            return false;
        }
        else
        {
            moveDirection = 1;
            PawnPosition = 6;
            if (row - i >= 0)
            {
                return true;
            }
            return false;
        }
    }
    public void PawnMovement(FigureInfo figureInfo)
    {
        int row = figureInfo.fieldRow;
        int column = figureInfo.fieldColumn;
        //open forward fields
        if (CheckMoveDirection(row, 1) && gameManager.ReturnFigureOnSquare(figureInfo.instances.field[row - (1 * moveDirection), column]) == null)
        {
            gameManager.OpenFreeField(figureInfo.gameObject, figureInfo.instances.field[row - (1 * moveDirection), column]);
            if (CheckMoveDirection(row, 2) && row == PawnPosition)
            {
                gameManager.OpenFreeField(figureInfo.gameObject, figureInfo.instances.field[row - (2 * moveDirection), column]);
            }
        }
        //move diagonal fields
        if (CheckMoveDirection(row, 1) && (column - 1 >= 0 || column + 1 <= 7))
        {
            gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[row - (1 * moveDirection), column - 1]);
            gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[row - (1 * moveDirection), column + 1]);
        }
    }
    public void KnightMovement(FigureInfo figureInfo)
    {
        int row = figureInfo.fieldRow;
        int column = figureInfo.fieldColumn;
    }

    void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }

    
}
