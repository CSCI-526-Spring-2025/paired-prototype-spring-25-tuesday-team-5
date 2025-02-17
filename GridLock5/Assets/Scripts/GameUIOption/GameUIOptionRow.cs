using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIOptionRow : GameUIOption
{
    public override void Init()
    {
        current = GameController.Instance.IN_A_ROW;
        lowerBound = 3;
        upperBound = 5;
        base.Init();
    }
    protected override void GoDown()
    {
        base.GoDown();
        GameController.Instance.IN_A_ROW = current;
    }
    protected override void GoUp()
    {
        base.GoUp();
        GameController.Instance.IN_A_ROW = current;
    }
}
