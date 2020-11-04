using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContinueButton : MyButton
{
    public override void ClickEvent(int mode = 0)
    {
        List<string> savetime = SaveController.readSaveInfos();
        this.nextLevel();
        int i = 0;
        foreach (SaveSlot bt in this.childButtons)
        {
            bt.type = SaveSlot.SlotType.LOAD;
            if (i < savetime.Count)
            {
                bt.txt.text = savetime[i];
                i++;
            }

        }
    }
}
