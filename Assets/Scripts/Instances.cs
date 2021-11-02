using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instances : MonoBehaviour
{
    [System.NonSerialized] public GameObject[,] field;
    public string turn = "White";
    public string playablePosition = "null";
    // Start is called before the first frame update
    void Awake()
    {
        field = new GameObject[8, 8];
    }
}
