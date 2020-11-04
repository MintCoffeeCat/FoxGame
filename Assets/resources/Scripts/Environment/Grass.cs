using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    private bool isPlaying;
    private bool playingEnter;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.setTouch();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
    }


    public void setTouch()
    {
        if (!this.isPlaying)
        {
            anim.SetBool("touch", true);
            isPlaying = true;
        }
        else
        {
            this.setRepeat();
        }
    }

    public void setNormal()
    {
        anim.SetBool("touch", false);
        this.isPlaying = false;
    }

    public void setRepeat()
    {
        anim.SetBool("repeat", true);
    }

    public void RepatDone()
    {
        anim.SetBool("repeat", false);
    }
}
