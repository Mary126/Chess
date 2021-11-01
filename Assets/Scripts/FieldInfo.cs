using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldInfo : MonoBehaviour
{
    public int positionRow;
    public int positionColumn;
    public GameObject figureOnSquare = null;
    public bool isactive = false;
    public string color;
    public GameManager gameManager;
}
