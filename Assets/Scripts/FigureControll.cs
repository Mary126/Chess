using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureControll : MonoBehaviour
{
    private FigureInfo figureInfo;
    public void MoveToPoint(int x, int y)
    {
        transform.position = new Vector3(x, y, -1);
    }
    private void OnMouseDown()
    {
        if (figureInfo.isPlaylable)
        {
            Debug.Log("MousePressed");
            figureInfo.gameManager.DisableControlledFigure();
            figureInfo.gameManager.SetControlledFigure(gameObject);
            figureInfo.gameManager.OpenAvailableFields(figureInfo);
            figureInfo.isControlled = true;
        }
    }
    public void MoveFigure(Transform newTransform, int fieldPositionRow, int fieldPositionColumn)
    {
        transform.position = new Vector3(newTransform.position.x, newTransform.position.y, -1);
        figureInfo.fieldRow = fieldPositionRow;
        figureInfo.fieldColumn = fieldPositionColumn;
    }
    void Awake()
    {
        figureInfo = GetComponent<FigureInfo>();
    }
}
