using UnityEngine;
using System.Collections;

public class CoverController : MonoBehaviour
{
    public Animator cover;
    public SceneController sc;
    public bool isCoverComplete;

    public void SetShow()
    {
        this.cover.SetBool("show", true);
    }

    public void SetHide()
    {
        this.cover.SetBool("show", false);
    }

    public void SetActive()
    {
        this.gameObject.SetActive(true);
    }

    public void SetUnActive()
    {
        this.gameObject.SetActive(false);
    }
    public void SetComplete()
    {
        this.isCoverComplete = true;
    }
    public void SetUnComplete()
    {
        this.isCoverComplete = false;
    }
}
