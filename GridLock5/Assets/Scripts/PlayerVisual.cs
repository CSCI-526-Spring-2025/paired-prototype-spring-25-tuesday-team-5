using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    public TMP_Text outlineText;
    public TMP_Text nameText;
    public TMP_Text scoreText;
    [HideInInspector] public int ID = 0;
    [HideInInspector] public int score = 0;
    [HideInInspector] public char character;
    private Color color;
    private RectTransform rectTransform;
    private void Start()
    {
       
    }
    public void Init(int ID, char character)
    {
        this.character = character;
        this.ID = ID; 
        this.rectTransform = GetComponent<RectTransform>();
        //Init anchor position
        if (GameController.Instance.PLAYER_SIZE <= 2)
            rectTransform.anchoredPosition = new Vector2(ID % 2 * 1200 - 600, 0);
        else
            rectTransform.anchoredPosition = new Vector2(ID % 2 * 1200 - 600, ID / 2 * -600 + 300);

        //Init outline with certain character.
        string outline = "";
        for(int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 13; j++)
                if (i == 0 || i == 4) 
                    outline += character;
                else if (j == 0 || j == 12)
                    outline += character;
                else
                    outline += ' ';
            if (i != 4)
                outline += '\n';
        }
        outlineText.text = outline;
        //Init score.
        scoreText.text = "Score: " + score;
        nameText.text = character.ToString();
    }

    public void UpdateScore(int score)
    {
        this.score += score;
        scoreText.text = "Score: " + this.score;
    }
}
