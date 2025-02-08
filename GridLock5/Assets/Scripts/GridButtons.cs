using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridButtons : MonoBehaviour
{
    public Button button;
    public TMP_Text buttonText; // Text buttonText;
    public string playerSide;

    public void SetSpace()
    {
        buttonText.text = playerSide;
        button.interactable = false;
    }

}
