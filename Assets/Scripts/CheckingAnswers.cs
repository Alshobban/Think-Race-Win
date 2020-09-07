using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckingAnswers : MonoBehaviour
{
    //used for detecting the eventclick by the quiz controller!
    public static bool clicked;

    public void ClickEvent()
    {
        clicked = true;
    }

}
