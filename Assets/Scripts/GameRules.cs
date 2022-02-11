using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    private GameManager gameManager;
    private BoardGenerator boardGenerator;
    private int moveDirection = -1;
    private int PawnPosition = 6;
    public bool kingDanger = false;

    bool CheckMoveDirection(int row, int i)
    {
        if (gameManager.instances.playablePosition == "top")
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
    public void ReplacePawnWithQueen(FigureInfo figureInfo)
    {
        int row = figureInfo.fieldRow;
        int column = figureInfo.fieldColumn;
        if (figureInfo.color == "black")
        {
            boardGenerator.PlaceFigure(boardGenerator.BlackQueen, row, column, "queen", "black");
        }
        else if (figureInfo.color == "white")
        {
            boardGenerator.PlaceFigure(boardGenerator.WhiteQueen, row, column, "queen", "white");
        }
        else Debug.LogError("Error - figure color does not exist");
        Destroy(figureInfo.gameObject);
    }
    void MoveTop(FigureInfo figureInfo)
    {
        int column = figureInfo.fieldColumn;
        int movingRow = figureInfo.fieldRow;
        while (movingRow > 0)
        {
            if (!gameManager.OpenFreeField(figureInfo.gameObject, figureInfo.instances.field[--movingRow, column]))
            {
                break;
            };
        }
        gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[movingRow, column]);
    }
    void MoveBottom(FigureInfo figureInfo)
    {
        int column = figureInfo.fieldColumn;
        int movingRow = figureInfo.fieldRow;
        while (movingRow < 7)
        {
            if (!gameManager.OpenFreeField(figureInfo.gameObject, figureInfo.instances.field[++movingRow, column]))
            {
                break;
            };
        }
        gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[movingRow, column]);
    }
    void MoveLeft(FigureInfo figureInfo)
    {
        int movingColumn = figureInfo.fieldColumn;
        int row = figureInfo.fieldRow;
        while (movingColumn > 0)
        {
            if (!gameManager.OpenFreeField(figureInfo.gameObject, figureInfo.instances.field[row, --movingColumn]))
            {
                break;
            };
        }
        gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[row, movingColumn]);
    }
    void MoveRight(FigureInfo figureInfo)
    {
        int movingColumn = figureInfo.fieldColumn;
        int row = figureInfo.fieldRow;
        while (movingColumn < 7)
        {
            if (!gameManager.OpenFreeField(figureInfo.gameObject, figureInfo.instances.field[row, ++movingColumn]))
            {
                break;
            };
        }
        gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[row, movingColumn]);
    }
    void MoveTopRight(FigureInfo figureInfo)
    {
        int movingRow = figureInfo.fieldRow;
        int movingColumn = figureInfo.fieldColumn;
        while (movingRow > 0 && movingColumn < 7)
        {
            if (!gameManager.OpenFreeField(figureInfo.gameObject, figureInfo.instances.field[--movingRow, ++movingColumn]))
            {
                break;
            };
        }
        gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[movingRow, movingColumn]);
    }
    void MoveTopLeft(FigureInfo figureInfo)
    {
        int movingRow = figureInfo.fieldRow;
        int movingColumn = figureInfo.fieldColumn;
        while (movingRow > 0 && movingColumn > 0)
        {
            if (!gameManager.OpenFreeField(figureInfo.gameObject, figureInfo.instances.field[--movingRow, --movingColumn]))
            {
                break;
            };
        }
        gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[movingRow, movingColumn]);
    }
    void MoveBottomRight(FigureInfo figureInfo)
    {
        int movingRow = figureInfo.fieldRow;
        int movingColumn = figureInfo.fieldColumn;
        while (movingRow < 7 && movingColumn < 7)
        {
            if (!gameManager.OpenFreeField(figureInfo.gameObject, figureInfo.instances.field[++movingRow, ++movingColumn]))
            {
                break;
            };
        }
        gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[movingRow, movingColumn]);
    }
    void MoveBottomLeft(FigureInfo figureInfo)
    {
        int movingRow = figureInfo.fieldRow;
        int movingColumn = figureInfo.fieldColumn;
        while (movingRow < 7 && movingColumn > 0)
        {
            if (!gameManager.OpenFreeField(figureInfo.gameObject, figureInfo.instances.field[++movingRow, --movingColumn]))
            {
                break;
            };
        }
        gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[movingRow, movingColumn]);
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
        if (CheckMoveDirection(row, 1) && column - 1 >= 0)
        {
            gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[row - (1 * moveDirection), column - 1]);
        }
        if (CheckMoveDirection(row, 1) && column + 1 <= 7)
        {
            gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[row - (1 * moveDirection), column + 1]);
        }
    }
    public void RookMovement(FigureInfo figureInfo)
    {
        MoveBottom(figureInfo);
        MoveTop(figureInfo);
        MoveRight(figureInfo);
        MoveLeft(figureInfo);
    }
    public void KnightMovement(FigureInfo figureInfo)
    {
        int row = figureInfo.fieldRow;
        int column = figureInfo.fieldColumn;
        if (row - 2 >= 0 && column + 1 <= 7)
        {
            gameManager.OpenFreeField(figureInfo.gameObject, figureInfo.instances.field[row - 2, column + 1]);
            gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[row - 2, column + 1]);
        }
        if (row - 2 >= 0 && column - 1 >= 0)
        {
            gameManager.OpenFreeField(figureInfo.gameObject, figureInfo.instances.field[row - 2, column - 1]);
            gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[row - 2, column - 1]);
        }
        if (row + 2 <= 7 && column + 1 <= 7)
        {
            gameManager.OpenFreeField(figureInfo.gameObject, figureInfo.instances.field[row + 2, column + 1]);
            gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[row + 2, column + 1]);
        }
        if (row + 2 <= 7 && column - 1 >= 0)
        {
            gameManager.OpenFreeField(figureInfo.gameObject, figureInfo.instances.field[row + 2, column - 1]);
            gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[row + 2, column - 1]);
        }
        if (row - 1 >= 0 && column + 2 <= 7)
        {
            gameManager.OpenFreeField(figureInfo.gameObject, figureInfo.instances.field[row - 1, column + 2]);
            gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[row - 1, column + 2]);
        }
        if (row - 1 >= 0 && column - 2 >= 0)
        {
            gameManager.OpenFreeField(figureInfo.gameObject, figureInfo.instances.field[row - 1, column - 2]);
            gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[row - 1, column - 2]);
        }
        if (row + 1 <= 7 && column + 2 <= 7)
        {
            gameManager.OpenFreeField(figureInfo.gameObject, figureInfo.instances.field[row + 1, column + 2]);
            gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[row + 1, column + 2]);
        }
        if (row + 1 <= 7 && column - 2 >= 0)
        {
            gameManager.OpenFreeField(figureInfo.gameObject, figureInfo.instances.field[row + 1, column - 2]);
            gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[row + 1, column - 2]);
        }
    }
    public void BishopMovement(FigureInfo figureInfo)
    {
        MoveTopLeft(figureInfo);
        MoveTopRight(figureInfo);
        MoveBottomLeft(figureInfo);
        MoveBottomRight(figureInfo);
    }
    public void QueenMovement(FigureInfo figureInfo)
    {
        MoveBottom(figureInfo);
        MoveTop(figureInfo);
        MoveRight(figureInfo);
        MoveLeft(figureInfo);
        MoveTopLeft(figureInfo);
        MoveTopRight(figureInfo);
        MoveBottomLeft(figureInfo);
        MoveBottomRight(figureInfo);
    }
    public void KingMovement(FigureInfo figureInfo)
    {
        int row = figureInfo.fieldRow;
        int column = figureInfo.fieldColumn;
        //diagonal movement
        if (row - 1 >= 0 && column - 1 >= 0)
        {
            gameManager.OpenFreeField(figureInfo.gameObject, figureInfo.instances.field[row - 1, column - 1]);
            gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[row - 1, column - 1]);
            
        }
        if (row - 1 >= 0 && column + 1 <= 7)
        {
            gameManager.OpenFreeField(figureInfo.gameObject, figureInfo.instances.field[row - 1, column + 1]);
            gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[row - 1, column + 1]);
        }
        if (row + 1 <= 7 && column - 1 >= 0)
        {
            gameManager.OpenFreeField(figureInfo.gameObject, figureInfo.instances.field[row + 1, column - 1]);
            gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[row + 1, column - 1]);
        }
        if (row + 1 <= 7 && column + 1 <= 7)
        {
            gameManager.OpenFreeField(figureInfo.gameObject, figureInfo.instances.field[row + 1, column + 1]);
            gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[row + 1, column + 1]);
        }
        //straight movement
        if (row + 1 <= 7)
        {
            gameManager.OpenFreeField(figureInfo.gameObject, figureInfo.instances.field[row + 1, column]);
            gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[row + 1, column]);
        }
        if (row - 1 >= 0)
        {
            gameManager.OpenFreeField(figureInfo.gameObject, figureInfo.instances.field[row - 1, column]);
            gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[row - 1, column]);
        }
        if (column + 1 <= 7)
        {
            gameManager.OpenFreeField(figureInfo.gameObject, figureInfo.instances.field[row, column + 1]);
            gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[row, column + 1]);
        }
        if (column - 1 >= 0)
        {
            gameManager.OpenFreeField(figureInfo.gameObject, figureInfo.instances.field[row, column - 1]);
            gameManager.OpenOccupiedField(figureInfo.gameObject, figureInfo.instances.field[row, column - 1]);
        }
    }
    public bool CheckIfKingIsInDanger(int kingPositionRow, int kingPositionColumn, FigureInfo king)
    {
        for (int row = 0; row < 8; row++)
        {
            for (int column = 0; column < 8; column++)
            {
                if (king.instances.field[row, column].GetComponent<FieldInfo>().figureOnSquare != null &&
                    gameManager.ReturnFigureOnSquare(king.instances.field[row, column]).GetComponent<FigureInfo>().color != king.color)
                {
                    gameManager.OpenAvailableFields(gameManager.ReturnFigureOnSquare(king.instances.field[row, column]).GetComponent<FigureInfo>());
                    if (king.instances.field[kingPositionRow, kingPositionColumn].GetComponent<FieldInfo>().isactive == true)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    // also check if a player can eat the figure that puts king in danger!!! and if king is still in danger after that
    public bool CanKingMove(FigureInfo king)
    {
        if (king != null)
        {
            int numberOfAvailableFields = 0;
            int numberOfDangerousFields = 0;
            int row = king.fieldRow;
            int column = king.fieldColumn;
            //diagonal movement
            if (row - 1 >= 0 && column - 1 >= 0)
            {
                if (gameManager.OpenFreeField(king.gameObject, king.instances.field[row - 1, column - 1]) == true ||
                    gameManager.OpenOccupiedField(king.gameObject, king.instances.field[row - 1, column - 1]))
                {
                    gameManager.DisableControlledFigure();
                    int newPositionRow = row - 1;
                    int newPositionColumn = column - 1;
                    numberOfAvailableFields++;
                    if (CheckIfKingIsInDanger(newPositionRow, newPositionColumn, king))
                    {
                        numberOfDangerousFields++;
                    }
                }
            }
            if (row - 1 >= 0 && column + 1 <= 7)
            {
                if (gameManager.OpenFreeField(king.gameObject, king.instances.field[row - 1, column + 1]) == true ||
                    gameManager.OpenOccupiedField(king.gameObject, king.instances.field[row - 1, column + 1]))
                {
                    gameManager.DisableControlledFigure();
                    int newPositionRow = row - 1;
                    int newPositionColumn = column - 1;
                    numberOfAvailableFields++;
                    if (CheckIfKingIsInDanger(newPositionRow, newPositionColumn, king))
                    {
                        numberOfDangerousFields++;
                    }
                }
            }
            if (row + 1 <= 7 && column - 1 >= 0)
            {
                if (gameManager.OpenFreeField(king.gameObject, king.instances.field[row + 1, column - 1]) == true ||
                     gameManager.OpenOccupiedField(king.gameObject, king.instances.field[row + 1, column - 1]))
                {
                    gameManager.DisableControlledFigure();
                    int newPositionRow = row - 1;
                    int newPositionColumn = column - 1;
                    numberOfAvailableFields++;
                    if (CheckIfKingIsInDanger(newPositionRow, newPositionColumn, king))
                    {
                        numberOfDangerousFields++;
                    }
                }
            }
            if (row + 1 <= 7 && column + 1 <= 7)
            {
                if (gameManager.OpenFreeField(king.gameObject, king.instances.field[row + 1, column + 1]) == true ||
                    gameManager.OpenOccupiedField(king.gameObject, king.instances.field[row + 1, column + 1]))
                {
                    gameManager.DisableControlledFigure();
                    int newPositionRow = row - 1;
                    int newPositionColumn = column - 1;
                    numberOfAvailableFields++;
                    if (CheckIfKingIsInDanger(newPositionRow, newPositionColumn, king))
                    {
                        numberOfDangerousFields++;
                    }
                }
            }
            //straight movement
            if (row + 1 <= 7)
            {
                if (gameManager.OpenFreeField(king.gameObject, king.instances.field[row + 1, column]) == true ||
                    gameManager.OpenOccupiedField(king.gameObject, king.instances.field[row + 1, column]))
                {
                    gameManager.DisableControlledFigure();
                    int newPositionRow = row - 1;
                    int newPositionColumn = column - 1;
                    numberOfAvailableFields++;
                    if (CheckIfKingIsInDanger(newPositionRow, newPositionColumn, king))
                    {
                        numberOfDangerousFields++;
                    }
                }
            }
            if (row - 1 >= 0)
            {
                if (gameManager.OpenFreeField(king.gameObject, king.instances.field[row - 1, column]) == true ||
                    gameManager.OpenOccupiedField(king.gameObject, king.instances.field[row - 1, column]))
                {
                    gameManager.DisableControlledFigure();
                    int newPositionRow = row - 1;
                    int newPositionColumn = column - 1;
                    numberOfAvailableFields++;
                    if (CheckIfKingIsInDanger(newPositionRow, newPositionColumn, king))
                    {
                        numberOfDangerousFields++;
                    }
                }
            }
            if (column + 1 <= 7)
            {
                if (gameManager.OpenFreeField(king.gameObject, king.instances.field[row, column + 1]) == true ||
                    gameManager.OpenOccupiedField(king.gameObject, king.instances.field[row, column + 1]))
                {
                    gameManager.DisableControlledFigure();
                    int newPositionRow = row - 1;
                    int newPositionColumn = column - 1;
                    numberOfAvailableFields++;
                    if (CheckIfKingIsInDanger(newPositionRow, newPositionColumn, king))
                    {
                        numberOfDangerousFields++;
                    }
                }
            }
            if (column - 1 >= 0)
            {
                if (gameManager.OpenFreeField(king.gameObject, king.instances.field[row, column - 1]) == true ||
                    gameManager.OpenOccupiedField(king.gameObject, king.instances.field[row, column - 1]))
                {
                    gameManager.DisableControlledFigure();
                    int newPositionRow = row - 1;
                    int newPositionColumn = column - 1;
                    numberOfAvailableFields++;
                    if (CheckIfKingIsInDanger(newPositionRow, newPositionColumn, king))
                    {
                        numberOfDangerousFields++;
                    }
                }
            }
            Debug.Log(numberOfAvailableFields);
            Debug.Log(numberOfDangerousFields);
            if (numberOfAvailableFields == numberOfDangerousFields)
            {
                return false;
            }
            else return true;
        }
        Debug.Log("No King Given");
        return true;
    }
    public void CheckForCheckmate(FigureInfo king)
    {
        if (kingDanger == true)
        {
            kingDanger = false;
            if (CanKingMove(king) == false)
            {
                gameManager.userUI.ShowWinScreen();
            }
        }
    }
    void Awake()
    {
        boardGenerator = GetComponent<BoardGenerator>();
        gameManager = GetComponent<GameManager>();
    }

}
