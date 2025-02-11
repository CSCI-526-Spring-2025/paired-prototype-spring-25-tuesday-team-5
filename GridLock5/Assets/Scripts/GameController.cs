using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public TMP_Text[] buttonList; // Text array for the 5x5 grid buttons
    private string playerSide;
    private bool[,] blockedGrid; // Grid to track blocked positions
    private int playerXScore, playerOScore;
    private int moveCount;

    public GameObject gameOverPanel;
    public TMP_Text gameOverText;
    public TMP_Text playerXScoreText, playerOScoreText;

    private const int gridSize = 5; // 5x5 Grid

    void Awake()
    {
        gameOverPanel.SetActive(false);
        blockedGrid = new bool[gridSize, gridSize];
        SetGameControllerReferenceOnButtons();
        playerSide = "X";
        moveCount = 0;
        playerXScore = 0;
        playerOScore = 0;
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

        int score = CalculateScore();
        if (playerSide == "X")
            playerXScore += score;
        else
            playerOScore += score;

        UpdateScoreUI();

        // Check if all cells are blocked or filled
        if (moveCount >= gridSize * gridSize || AreAllCellsBlocked())
        {
            DeclareWinner();
            return;
        }

        ChangeSides();
    }

    private int CalculateScore()
    {
        int totalScore = 0;
        int[,] directions = { { 0, 1 }, { 1, 0 }, { 1, 1 }, { 1, -1 } }; // Right, Down, Diagonal Right, Diagonal Left

        for (int r = 0; r < gridSize; r++)
        {
            for (int c = 0; c < gridSize; c++)
            {
                if (buttonList[r * gridSize + c].text == playerSide && !blockedGrid[r, c])
                {
                    for(int i=0;i<4;i++)
                    {
                        int count = CountSequence(r, c, directions[i,0], directions[i,1]);
                        if (count >= 3)
                        {
                            totalScore += GetScoreForSequence(count);
                            BlockCells(r, c, directions[i,0], directions[i,1], count);
                        }
                    }
                }
            }
        }
        return totalScore;
    }

    private int CountSequence(int r, int c, int dr, int dc)
    {
        int count = 0;
        while (r >= 0 && r < gridSize && c >= 0 && c < gridSize &&
               buttonList[r * gridSize + c].text == playerSide && !blockedGrid[r, c])
        {
            count++;
            r += dr;
            c += dc;
        }
        return count;
    }

    private int GetScoreForSequence(int count)
    {
        if (count < 3) return 0;
        int score = 3; // Base score for 3 blocks
        for (int i = 4; i <= count; i++)
        {
            score += 2; // Each extra block adds +2
        }
        return score;
    }

    private void BlockCells(int r, int c, int dr, int dc, int count)
    {
        for (int i = 0; i < count; i++)
        {

            buttonList[r*5+c].transform.parent.GetComponent<Image>().color=Color.black;
            blockedGrid[r, c] = true;
            r += dr;
            c += dc;
        }
    }

    private bool AreAllCellsBlocked()
    {
        for (int r = 0; r < gridSize; r++)
        {
            for (int c = 0; c < gridSize; c++)
            {
                if (!blockedGrid[r, c] && string.IsNullOrEmpty(buttonList[r * gridSize + c].text))
                {
                    return false; // At least one cell is playable
                }
            }
        }
        return true;
    }

    private void DeclareWinner()
    {
        gameOverPanel.SetActive(true);
        if (playerXScore > playerOScore)
            gameOverText.text = "Player X Wins! Score: " + playerXScore;
        else if (playerOScore > playerXScore)
            gameOverText.text = "Player O Wins! Score: " + playerOScore;
        else
            gameOverText.text = "It's a Draw!";
    }

    private void UpdateScoreUI()
    {
        playerXScoreText.text = "Player X: " + playerXScore;
        playerOScoreText.text = "Player O: " + playerOScore;
    }

    void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
    }
}
