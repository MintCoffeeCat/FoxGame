using UnityEngine;
using System.Collections;

public class checkPoint : Event
{
    private void Awake()
    {
        this.press = "按下";
        this.key = "Event";
        this.act = "保存游戏";
    }
    protected override void ActivateEvent()
    {
        SaveController sv = new SaveController(0);
        sv.Save();
    }
}
