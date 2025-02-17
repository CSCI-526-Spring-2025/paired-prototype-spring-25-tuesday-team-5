using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIOptionPlayer : GameUIOption
{
    public override void Init()
    {
        current = GameController.Instance.PLAYER_SIZE;
        lowerBound = 2;
        upperBound = 4;
        base.Init();
    }
    protected override void GoDown()
    {
        base.GoDown();
        GameController.Instance.PLAYER_SIZE = current;
    }
    protected override void GoUp()
    {
        base.GoUp();
        GameController.Instance.PLAYER_SIZE = current;
    }
}
