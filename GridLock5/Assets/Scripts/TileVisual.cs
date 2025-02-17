using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileVisual : MonoBehaviour
{
    public Button button;
    public TMP_Text text;
    public Image backgroundImage;
    public GameObject[] outlineImage;
    public GameObject[] exitImage;
    public int player = -1;
    public int x;
    public int y;
    public void Start()
    {
        button.onClick.AddListener(() =>
        {
            GameController.Instance.Play(this);
            button.interactable = false;
        });
    }
    public void Init(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public void SetExit(params int[] exits)
    {
        for (int i = 0; i < outlineImage.Length; i++)
        {
            outlineImage[i].SetActive(true);
        }
        for (int i=0;i<exitImage.Length; i++)
        {
            exitImage[i].SetActive(false);
        }
        for (int i=0;i<exits.Length; i++)
        {
            outlineImage[exits[i]].SetActive(false);
            exitImage[exits[i]].SetActive(true);
        }

    }
    public void Use()
    {
        player = -1;
        backgroundImage.color = new Color(255 / 255f, 170 / 255f, 175 / 255f);
    }
}
