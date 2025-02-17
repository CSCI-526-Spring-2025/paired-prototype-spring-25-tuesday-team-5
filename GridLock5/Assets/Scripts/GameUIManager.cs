using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance {  get; private set; }
    public GameUIOption[] options;
    public Button startButton;
    private void Awake()
    {
        Instance = this; 
    }
    private void Start()
    {
        foreach(GameUIOption option in options)
            option.Init();
        startButton.onClick.AddListener(() =>
        {
            GameController.Instance.Init();
            gameObject.SetActive(false);
        });
    }
}
