using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Instances instances;
    public GameObject controlledFigure;
    public GameRules gameRules;
    public UserUI userUI;
    public GameObject ReturnFigureOnSquare(GameObject field)
    {
        if (field.GetComponent<FieldInfo>().figureOnSquare != null)
        {
            return field.GetComponent<FieldInfo>().figureOnSquare;
        }
        else return null;
    }
    void EatFigure(GameObject figure)
    {
        if (figure.GetComponent<FigureInfo>().type == "king")
        {
            Time.timeScale = 0;
            userUI.ShowWinScreen();
        }
        Destroy(figure);
    }
    public bool OpenOccupiedField(GameObject figure1, GameObject field)
    {
        GameObject figure2 = ReturnFigureOnSquare(field);
        if (figure2 != null && figure1.GetComponent<FigureInfo>().color != figure2.GetComponent<FigureInfo>().color)
        {
            field.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 0xFF);
            figure2.GetComponent<BoxCollider>().enabled = false;
            field.GetComponent<FieldInfo>().isactive = true;
            if (figure2.GetComponent<FigureInfo>().type == "king")
            {
                Debug.Log("king is in danger");
                gameRules.checkKing = field.GetComponent<FieldInfo>().figureOnSquare.GetComponent<FigureInfo>();
            }
            return true;
        }
        return false;
    }
    public bool OpenFreeField(GameObject figure1, GameObject field)
    {
        GameObject figure2 = ReturnFigureOnSquare(field);
        if (figure2 == null)
        {
            field.GetComponent<SpriteRenderer>().color = new Color32(0, 128, 0, 0xFF);
            field.GetComponent<FieldInfo>().isactive = true;
            return true;
        }
        return false;
    }
    public void DisableActiveField()
    {
        //Color all active fields their original color and disable privious controlled figure
        for (int row = 0; row < 8; row++)
        {
            for (int column = 0; column < 8; column++)
            {
                if (instances.field[row, column].GetComponent<FieldInfo>().isactive)
                {
                    GameObject figure = ReturnFigureOnSquare(instances.field[row, column]);
                    //enable all the colliders
                    if (figure != null)
                    {
                        figure.GetComponent<BoxCollider>().enabled = true;
                    }
                    if (instances.field[row, column].GetComponent<FieldInfo>().color == "pink")
                    {
                        instances.field[row, column].GetComponent<SpriteRenderer>().color = new Color32(243, 139, 223, 0xFF);
                    }
                    else
                    {
                        instances.field[row, column].GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0xFF);
                    }
                    instances.field[row, column].GetComponent<FieldInfo>().isactive = false;
                }
            }
        }
        if (controlledFigure != null)
        {
            controlledFigure.GetComponent<FigureInfo>().isControlled = false;
            controlledFigure = null;
        }
    }
    public void OpenAvailableFields(FigureInfo figureInfo)
    {
        switch(figureInfo.type)
        {
            case "pawn": gameRules.PawnMovement(figureInfo); break;
            case "rook": gameRules.RookMovement(figureInfo); break;
            case "knight": gameRules.KnightMovement(figureInfo); break;
            case "bishop": gameRules.BishopMovement(figureInfo); break;
            case "queen": gameRules.QueenMovement(figureInfo); break;
            case "king": gameRules.KingMovement(figureInfo); break;
            default: Debug.LogError("Unknown figure type"); break;
        }
    }
    public void ChangeTurns()
    {
        if (instances.turn == "White")
        {
            instances.turn = "Black";
        }
        else if (instances.turn == "Black")
        {
            instances.turn = "White";
        }
        else Debug.LogError("Error - turn name is wrong");
        if (instances.playablePosition == "bottom")
        {
            instances.playablePosition = "top";
        }
        else if (instances.playablePosition == "top")
        {
            instances.playablePosition = "bottom";
        }
        else Debug.LogError("Error - Playable position name is wrong");
        for (int row = 0; row < 8; row++)
        {
            for (int column = 0; column < 8; column++)
            {
                GameObject figure = ReturnFigureOnSquare(instances.field[row, column]);
                if (figure != null) {
                    figure.GetComponent<FigureInfo>().isPlaylable = !figure.GetComponent<FigureInfo>().isPlaylable;
                }
            }
        }
        userUI.ChangeTurns();
    }
    public void MoveControlledFigure(Transform newTransform, int fieldPositionRow, int fieldPositionColumn)
    {
        if (controlledFigure != null)
        {
            userUI.HideCheckText();
            GameObject figure = ReturnFigureOnSquare(instances.field[fieldPositionRow, fieldPositionColumn]);
            FigureInfo controlledFigureInfo = controlledFigure.GetComponent<FigureInfo>();
            instances.field[fieldPositionRow, fieldPositionColumn].GetComponent<FieldInfo>().figureOnSquare = 
                controlledFigure;
            if (figure != null)
            {
                EatFigure(figure);
            }
            instances.field[controlledFigureInfo.fieldRow, controlledFigureInfo.fieldColumn].GetComponent<FieldInfo>().figureOnSquare = null;
            controlledFigure.GetComponent<FigureControll>().MoveFigure(newTransform, fieldPositionRow, fieldPositionColumn);
            DisableActiveField();
            // if controlled figure is king and it moved to danger
            if (controlledFigureInfo.type == "king" && gameRules.FigureIsInDanger(controlledFigureInfo.fieldRow, controlledFigureInfo.fieldColumn, controlledFigureInfo) == true)
            {
                userUI.ShowCheckText();
            }
            //check if controlled figure can eat a king
            OpenAvailableFields(controlledFigureInfo);
            if (gameRules.checkKing != null)
            {
                userUI.ShowCheckText();
                gameRules.CheckForCheckmate(gameRules.checkKing, controlledFigureInfo);
            }
            DisableActiveField();
            ChangeTurns();
        }
    }
    void Awake()
    {
        instances = GetComponent<Instances>();
        gameRules = GetComponent<GameRules>();
        userUI = GetComponent<UserUI>();
    }
}
