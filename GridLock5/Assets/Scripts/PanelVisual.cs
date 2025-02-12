using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelVisual : MonoBehaviour
{
    public GameObject content;
    public TMP_Text contentText;
    public void Show(string text)
    {
        contentText.text = text;
        content.SetActive(true);
    }
}
