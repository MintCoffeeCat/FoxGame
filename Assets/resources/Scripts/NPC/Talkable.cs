using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Talkable : Event
{
    public string name;
    public Sprite header;
    public TextAsset scripts;

    protected override void ActivateEvent()
    {
        UIController ui = (UIController)UIController.instance;
        if (ui == null)
        {
            Debug.LogError("UI控制器获取失败，可能是由于未加载完毕导致的");
            return;
        }

        ui.startDialog(this);
    }
}
