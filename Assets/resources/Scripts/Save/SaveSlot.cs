using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SaveSlot : MyButton
{   
    public enum SlotType { SAVE, LOAD }

    public int slotNum;
    [SerializeField]
    public SlotType type;
    public Text txt;
    public override void ClickEvent(int mode)
    {
        if(this.type == SlotType.SAVE)
        {
            this.SaveGame(mode);

        }else if(this.type == SlotType.LOAD)
        {
            this.LoadGame();
        }
    }

    public void LoadGame()
    {
        SaveController sv = new SaveController(this.slotNum);
        bool success = sv.Load();
        if (!success) return;

        SceneController sc = (SceneController)SceneController.instance;
        sc.ContinueGame();
    }

    public void SaveGame(int spawn)
    {
        SaveController sv = new SaveController(this.slotNum);
        sv.Save();
        if (spawn != 0)
        {
            SceneController sc = (SceneController)SceneController.instance;
            sc.ContinueGame();
        }
    }
}
