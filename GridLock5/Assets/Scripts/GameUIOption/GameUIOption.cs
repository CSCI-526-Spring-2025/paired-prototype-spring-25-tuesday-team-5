
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameUIOption : MonoBehaviour
{
    public Button leftButton;
    public TMP_Text numberText;
    public Button rightButton;
    protected int lowerBound = 3;
    protected int upperBound = 5;
    protected int current;
    private void Start()
    {
        leftButton.onClick.AddListener(GoDown);
        rightButton.onClick.AddListener(GoUp);
    }
    public virtual void Init()
    {
        numberText.text = current.ToString();
    }
    protected virtual void GoDown()
    {
        current--;
        current = Mathf.Max(lowerBound, current);
        numberText.text = current.ToString();
    }
    protected virtual void GoUp()
    {
        current++;
        current = Mathf.Min(upperBound, current);
        numberText.text = current.ToString();
    }
}
