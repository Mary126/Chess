using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserUI : MonoBehaviour
{
    private Instances instances;
    public Text turn;
    public GameObject WinScreen;
    public Text color;
    public void ChangeTurns()
    {
        turn.text = instances.turn;
    }
    public void ShowWinScreen()
    {
        WinScreen.SetActive(true);
        color.text = instances.turn + " won!";
    }
    // Start is called before the first frame update
    void Start()
    {
        instances = GetComponent<Instances>();
    }
}
