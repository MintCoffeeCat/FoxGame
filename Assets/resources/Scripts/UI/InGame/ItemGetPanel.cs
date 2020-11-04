using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ItemGetPanel : MonoBehaviour
{
    public Text name;
    public Text description;
    public bool displayDone = false;

    private void Update()
    {
        if (Input.anyKeyDown && displayDone)
        {
            this.gameObject.SetActive(false);
            this.displayDone = false;
            ((Player)Player.instance).gameObject.SetActive(true);
        }    
    }

    public void setValue(ImportantItem it)
    {
        this.name.text = "获得 " + it.name;
        this.description.text = it.description;
    }

    public void show(ImportantItem it)
    {
        this.setValue(it);
        this.gameObject.SetActive(true);
        ((Player)Player.instance).gameObject.SetActive(false);
    }

    public void DisplayDone()
    {
        this.displayDone = true;
    }
}
