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
            Debug.Log("ClickFigure");
            figureInfo.gameManager.DisableControlledFigure();
            figureInfo.gameManager.controlledFigure = gameObject;
            figureInfo.gameManager.OpenAvailableFields(figureInfo);
            figureInfo.isControlled = true;
        }
    }
    public void MoveFigure(Transform newTransform, int fieldPositionRow, int fieldPositionColumn)
    {
        transform.position = new Vector3(newTransform.position.x, newTransform.position.y, -1);
        figureInfo.fieldRow = fieldPositionRow;
        figureInfo.fieldColumn = fieldPositionColumn;
        // if pawn reached the end, replace with queen
        if (figureInfo.type == "pawn")
        {
            if (figureInfo.gameManager.instances.playablePosition == "top" && figureInfo.fieldRow == 7)
            {
                figureInfo.gameManager.gameRules.ReplacePawnWithQueen(figureInfo);
            }
            else if (figureInfo.gameManager.instances.playablePosition == "bottom" && figureInfo.fieldRow == 0)
            {
                figureInfo.gameManager.gameRules.ReplacePawnWithQueen(figureInfo);
            }
        }
    }
    void Awake()
    {
        figureInfo = GetComponent<FigureInfo>();
    }
}
