using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : Singleton<UIController>
{
    public GameObject helpPanel;
    public GameObject pointPanel;
    public GameObject keyPressPanel;
    public PauseMenu pauseMenu;
    public MyDialog dialog;
    public DeadPanel deadPanel;
    public StatusBar statusBar;
    public ItemGetPanel itemGetPanel;
    public int addPoint(int pt)
    {
        PointPanel pp = this.pointPanel.gameObject.GetComponent<PointPanel>();
        return pp.addPoint(pt);
    }

    public int minusPoint(int pt)
    {
        PointPanel pp = this.pointPanel.gameObject.GetComponent<PointPanel>();
        return pp.minusPoint(pt);
    }

    public void showHelpPanel(string s)
    {
        HelpPanel hp = this.helpPanel.gameObject.GetComponent<HelpPanel>();
        if(hp == null)
        {
            Debug.LogError("UI控制器所控制的helpPanel没有 HelpPanel 类型的脚本");
            return;
        }
        hp.setText(s);
        hp.Show();
    }
    public void hideHelpPanel()
    {
        HelpPanel hp = this.helpPanel.gameObject.GetComponent<HelpPanel>();
        hp.Hide();
    }

    public void showKeyPressPanel()
    {
        KeyPressPanel kp = this.keyPressPanel.gameObject.GetComponent<KeyPressPanel>();
        kp.Show();
    }
    public void showKeyPressPanel(string tp, string k, string act)
    {
        KeyPressPanel kp = this.keyPressPanel.gameObject.GetComponent<KeyPressPanel>();
        kp.setText(tp, k, act);
        kp.Show();
    }
    public void hideKeyPressPanel()
    {
        KeyPressPanel kp = this.keyPressPanel.gameObject.GetComponent<KeyPressPanel>();
        kp.Hide();
    }

    public void ShowPauseMenu()
    {
        PointPanel pp = this.pointPanel.gameObject.GetComponent<PointPanel>();
        pp.setPoint(((Player)Player.instance).fruitPoint);
        this.pauseMenu.Show();


    }

    public void showDeadPanel()
    {
        this.deadPanel.show();
    }

    public void startDialog(Talkable tk)
    {
        this.dialog.StartDialog(tk);
    }

    public StatusBar getStatusBar()
    {
        return this.statusBar;
    }

    public void showItemGetPanel(ImportantItem it)
    {
        this.itemGetPanel.show(it);
    }

    public ItemGetPanel getItemPanel()
    {
        return this.itemGetPanel;
    }
}
