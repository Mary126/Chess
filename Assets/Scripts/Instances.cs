using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instances : MonoBehaviour
{
    [System.NonSerialized] public GameObject[,] field;
    // Start is called before the first frame update
    void Awake()
    {
        field = new GameObject[8, 8];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
