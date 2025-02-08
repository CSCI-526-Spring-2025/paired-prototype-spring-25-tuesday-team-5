using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridButtons : MonoBehaviour
{
    public Button button;
    public TMP_Text buttonText; // Text buttonText;
    // public string playerSide;

    private GameController gameController;

    public void SetSpace()
    {
        buttonText.text = gameController.GetPlayerSide(); // playerSide;
        button.interactable = false;
        gameController.EndTurn();
    }

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }

}
