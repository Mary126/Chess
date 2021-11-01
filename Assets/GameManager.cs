using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Instances instances;
    public GameObject controlledFigure;
    GameObject ReturnFigureOnSquare(GameObject field)
    {
        if (field.GetComponent<FieldInfo>().figureOnSquare != null)
        {
            return field.GetComponent<FieldInfo>().figureOnSquare;
        }
        else return null;
    }
    GameObject ReturnFieldUnderFigure(GameObject figure)
    {
        RaycastHit hit;
        if (Physics.Raycast(figure.transform.position, transform.TransformDirection(Vector3.forward), out hit, 10f))
        {
            return hit.collider.gameObject;
        }
        else return null;
    }
    void EatFigure(GameObject figure)
    {
        Destroy(figure);
    }
    public bool CheckColor(GameObject figure1, GameObject figure2)
    {
        if (figure2 != null && (figure1.GetComponent<FigureInfo>().color != figure2.GetComponent<FigureInfo>().color))
        {
            figure2.GetComponent<BoxCollider>().enabled = false;
            return true;
        }
        else return false;
    }
    void OpenField(GameObject figure1, GameObject field)
    {
        GameObject figure2 = ReturnFigureOnSquare(field);
        if (figure2 == null)
        {
            field.GetComponent<SpriteRenderer>().color = new Color32(0, 128, 0, 0xFF);
            field.GetComponent<FieldInfo>().isactive = true;
        }
        else if (CheckColor(figure1, figure2))
        {
            field.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 0xFF);
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
        }
    }
    public void PawnMovement(FigureInfo figureInfo)
    {
        int row = figureInfo.fieldRow;
        int column = figureInfo.fieldColumn;
        if (row - 1 >= 0)
        {
            OpenField(figureInfo.gameObject, figureInfo.instances.field[row - 1, column]);
        }
        if (row - 2 >= 0)
        {
            OpenField(figureInfo.gameObject, figureInfo.instances.field[row - 2, column]);
        }
    }
    public void OpenAvailableFields(FigureInfo figureInfo)
    {
        PawnMovement(figureInfo);
    }
    public void SetControlledFigure(GameObject figure)
    {
        controlledFigure = figure;
    }
    public void MoveControlledFigure(Transform newTransform, int fieldPositionRow, int fieldPositionColumn)
    {
        if (controlledFigure != null)
        {
            GameObject figure = ReturnFigureOnSquare(instances.field[fieldPositionRow, fieldPositionColumn]);
            instances.field[fieldPositionRow, fieldPositionColumn].GetComponent<FieldInfo>().figureOnSquare = newTransform.gameObject;
            if (figure != null)
            {
                EatFigure(figure);
            }
            controlledFigure.GetComponent<FigureControll>().MoveFigure(newTransform, fieldPositionRow, fieldPositionColumn);
        }
    }
    void Awake()
    {
        instances = GetComponent<Instances>();
    }
}
