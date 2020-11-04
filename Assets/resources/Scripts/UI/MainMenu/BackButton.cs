using UnityEngine;
using System.Collections;

public class BackButton : MyButton
{
    public override void ClickEvent(int mode = 0)
    {
        this.previousLevel();
    }
}
