using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KeyPressPanel : MonoBehaviour
{
    public string pressType;
    public string key;
    public string act;
    public Text txt;
    public Animator ani;

    private void Awake()
    {
        this.Hide();
    }

    public void Show()
    {
        this.ani.SetBool("show", true);
    }

    public void Hide()
    {
        this.ani.SetBool("show", false);
    }
    public void setKey(string s)
    {
        this.setText(this.pressType, s, this.act);
    }
    public void setPressType(string s)
    {
        this.setText(s, this.key, this.act);
    }
    public void setAct(string s)
    {
        this.setText(this.pressType, this.key, s);
    }
    public void setText(string press, string key, string act)
    {
        this.pressType = press;
        this.key = key;
        this.act = act;
        this.txt.text = this.pressType + " " + this.key + " 按键 " + this.act;
    }
}
