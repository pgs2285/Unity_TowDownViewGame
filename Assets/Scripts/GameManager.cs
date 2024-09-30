using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public TextMeshProUGUI talkText;
    public GameObject scanObject;
    public bool isAction;
    public void Action(GameObject scanObj)
    {
        if (isAction)
        { //Exit
            isAction = false;
        }
        else
        { // Enter
            isAction = true;
            scanObject = scanObj;
            talkText.text = "테스트 1. " + scanObject.name + "와 대화중입니다.";
        }
        talkPanel.SetActive(isAction);
    }
}
