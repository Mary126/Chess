using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Instances instances;
    public GameObject controlledFigure;
    private GameRules gameRules;
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
        Destroy(figure);
    }
    public void OpenOccupiedField(GameObject figure1, GameObject field)
    {
        GameObject figure2 = ReturnFigureOnSquare(field);
        if (figure2 != null && figure1.GetComponent<FigureInfo>().color != figure2.GetComponent<FigureInfo>().color)
        {
            field.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 0xFF);
            figure2.GetComponent<BoxCollider>().enabled = false;
            field.GetComponent<FieldInfo>().isactive = true;
        }
    }
    public void OpenFreeField(GameObject figure1, GameObject field)
    {
        GameObject figure2 = ReturnFigureOnSquare(field);
        if (figure2 == null)
        {
            field.GetComponent<SpriteRenderer>().color = new Color32(0, 128, 0, 0xFF);
            field.GetComponent<FieldInfo>().isactive = true;
        }
    }
    public void DisableControlledFigure()
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
        gameRules.PawnMovement(figureInfo);
    }
    public void ChangeTurns()
    {
        if (instances.playlablePosition == "top")
        {
            instances.playlablePosition = "bottom";
        }
        else if (instances.playlablePosition == "bottom")
        {
            instances.playlablePosition = "top";
        }
        else Debug.LogError("Error - playablePosition name is wrong");
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
    }
    public void MoveControlledFigure(Transform newTransform, int fieldPositionRow, int fieldPositionColumn)
    {
        if (controlledFigure != null)
        {
            GameObject figure = ReturnFigureOnSquare(instances.field[fieldPositionRow, fieldPositionColumn]);
            instances.field[fieldPositionRow, fieldPositionColumn].GetComponent<FieldInfo>().figureOnSquare = 
                controlledFigure;
            if (figure != null)
            {
                EatFigure(figure);
            }
            instances.field[controlledFigure.GetComponent<FigureInfo>().fieldRow,
                controlledFigure.GetComponent<FigureInfo>().fieldColumn].GetComponent<FieldInfo>().figureOnSquare = null;
            controlledFigure.GetComponent<FigureControll>().MoveFigure(newTransform, fieldPositionRow, fieldPositionColumn);
            DisableControlledFigure();
            ChangeTurns();
        }
    }
    void Awake()
    {
        instances = GetComponent<Instances>();
        gameRules = GetComponent<GameRules>();
    }
}
