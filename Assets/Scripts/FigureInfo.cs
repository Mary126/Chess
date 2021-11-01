using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureInfo : MonoBehaviour
{
    public Instances instances;
    public bool isPlaylable;
    public bool isControlled = false;
    public int fieldColumn = 0;
    public int fieldRow = 0;
    public string type = "null";
    public string color = "null";
    public GameManager gameManager;
}
