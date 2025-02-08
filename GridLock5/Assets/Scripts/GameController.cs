using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public TMP_Text[] buttonList; // Text[] buttonList;
    private string playerSide;

    public GameObject gameOverPanel;
    public TMP_Text gameOverText;
    private int moveCount;

    void Awake()
    {
        gameOverPanel.SetActive(false);
        SetGameControllerReferenceOnButtons();
        playerSide = "X";
        moveCount = 0;
    }

    void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridButtons>().SetGameControllerReference(this);
        }
    }

    public string GetPlayerSide()
    {
        return playerSide;
    }

    public void EndTurn()
    {
        moveCount++;

        // Horizontal Lines
        if (buttonList[0].text == playerSide &&  buttonList[1].text == playerSide && buttonList[2].text == playerSide)
        {
            GameOver();
        }
        if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide)
        {
            GameOver();
        }
        if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver();
        }

        // Vertical Lines
        if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver();
        }
        if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide)
        {
            GameOver();
        }
        if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver();
        }

        // Diagonal Lines
        if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver();
        }
        if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver();
        }

        if (moveCount >= 9) {
            gameOverPanel.SetActive(true);
            gameOverText.text = "It's a draw!";
        }

        ChangeSides();
        // Debug.Log("EndTurn is not implemented yet!");
    }

    void GameOver()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = false;
        }

        gameOverPanel.SetActive(true);
        gameOverText.text = playerSide + " Wins!";
    }

    void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
    }
}
