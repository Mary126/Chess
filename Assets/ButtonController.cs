using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private Button buttn;
    void TaskOnClick()
    {
        switch (buttn.tag)
        {
            case "ExitButton": Application.Quit(); break;
            default: break;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        buttn = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        buttn.onClick.AddListener(TaskOnClick);
    }
}
