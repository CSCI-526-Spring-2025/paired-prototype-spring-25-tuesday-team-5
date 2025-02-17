using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIOptionWidth : GameUIOption
{
    public override void Init()
    {
        current = GameController.Instance.BOARD_WIDTH;
        upperBound = 6;
        lowerBound = 4;
        base.Init();
    }
    protected override void GoDown()
    {
        base.GoDown();
        GameController.Instance.BOARD_WIDTH = current;
    }
    protected override void GoUp()
    {
        base.GoUp();
        GameController.Instance.BOARD_WIDTH = current;
    }
}
