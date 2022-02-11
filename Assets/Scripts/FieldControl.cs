using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldControl : MonoBehaviour
{
    private FieldInfo fieldInfo;
    void Awake()
    {
        fieldInfo = GetComponent<FieldInfo>();
    }
    private void OnMouseDown()
    {
        if (fieldInfo.isactive)
        {
            fieldInfo.gameManager.MoveControlledFigure(transform, fieldInfo.positionRow, fieldInfo.positionColumn);
            fieldInfo.gameManager.DisableActiveField();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
