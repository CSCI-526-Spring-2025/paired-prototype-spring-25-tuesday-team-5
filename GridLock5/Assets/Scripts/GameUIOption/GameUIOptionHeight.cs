using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIOptionHeight : GameUIOption
{
    public override void Init()
    {
        current = GameController.Instance.BOARD_HEIGHT;
        upperBound = 6;
        lowerBound = 4;
        base.Init();
    }
    protected override void GoDown()
    {
        base.GoDown();
        GameController.Instance.BOARD_HEIGHT = current;
    }
    protected override void GoUp()
    {
        base.GoUp();
        GameController.Instance.BOARD_HEIGHT = current;
    }
}
