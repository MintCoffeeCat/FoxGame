using UnityEngine;
using System.Collections;

public class QuitButton : MyButton
{

    public override void ClickEvent(int mode = 0)
    {
        Application.Quit();
    }
}
