using UnityEngine;
using System.Collections;

public class DeadPanel : MonoBehaviour
{
    private bool displayComplete;
    private void Update()
    {
        if (displayComplete && Input.anyKeyDown)
        {
            Player p = (Player)Player.instance;
            SaveController sv = new SaveController(p.lastSaveSlot);
            bool success = sv.Load();
            if (!success) return;

            SceneController sc = (SceneController)SceneController.instance;
            this.gameObject.SetActive(false);
            sc.ContinueGame();
        }
    }

    public void setComplete()
    {
        this.displayComplete = true;
    }

    public void show()
    {
        this.gameObject.SetActive(true);
        this.displayComplete = false;
    }
}
