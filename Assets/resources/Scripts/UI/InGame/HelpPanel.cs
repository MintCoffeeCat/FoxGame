using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpPanel : MonoBehaviour
{

    public bool show;
    public Animator ani;
    public Text text;

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

    public void setText(string txt)
    {
        this.text.text = txt;
    }
}
