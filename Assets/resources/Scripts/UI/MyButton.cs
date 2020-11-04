using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class MyButton : MonoBehaviour
{
    [SerializeField]
    protected List<MyButton> childButtons;
    [SerializeField]
    protected List<MyButton> neighborButtons;
    [SerializeField]
    protected List<MyButton> fatherButtons;

    protected bool clicked;

    public abstract void ClickEvent(int mode = 0);

    public void nextLevel()
    {
        foreach(MyButton child in childButtons)
        {
            child.gameObject.SetActive(true);
        }
        foreach (MyButton neighbor in neighborButtons)
        {
            neighbor.gameObject.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }

    public void previousLevel()
    {
        foreach (MyButton child in childButtons)
        {
            child.gameObject.SetActive(false);
        }
        foreach(MyButton neighbor in neighborButtons)
        {
            neighbor.gameObject.SetActive(false);
        }
        foreach(MyButton father in fatherButtons)
        {
            father.gameObject.SetActive(true);
        }
        this.gameObject.SetActive(false);
    }
}
