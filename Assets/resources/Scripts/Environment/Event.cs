using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Event : MonoBehaviour
{
    [SerializeField]
    public string press;
    public string key;
    public string act;
    public bool inArea = false;
    public bool auto;
    public bool onlyOnce;
    protected GameObject destroyTarget;
    public int eventCode;
    protected bool animationDone = true;

    /*
   
     事件代码规定:
     0-99      对话事件
     100-199    道具事件
     200-299    剧情动画事件

    */
    protected void Awake()
    {
        this.destroyTarget = this.gameObject;
        this.destroyOnce();
    }

    protected void destroyOnce()
    {
        if (this.onlyOnce)
        {
            Player p = (Player)Player.instance;
            Dictionary<int, int> dic = p.eventTable.ToDictionary();
            try
            {
                if (dic[this.eventCode] == this.eventCode)
                {
                    Destroy(this.destroyTarget);
                }
            }
            catch (KeyNotFoundException e)
            {
                Debug.Log(this.eventCode + "号事件还未被触发");
            }

        }
    }
    // Update is called once per frame
    protected void Update()
    {
        if (this.auto) return;

        if (Input.GetButtonDown(this.key) && this.inArea)
        {
            this.ActivateEvent();
            StartCoroutine(this.setEventDone());
        }

    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.inArea = true;
            if (this.auto)
            {
                this.ActivateEvent();
                StartCoroutine(this.setEventDone());
                return;
            }
            UIController uc = (UIController)UIController.instance;
            uc.showKeyPressPanel(this.press, this.key, this.act);
        }
    }
    protected void OnTriggerStay2D(Collider2D collision)
    {

    }
    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.inArea = false;
            UIController uc = (UIController)UIController.instance;
            uc.hideKeyPressPanel();
        }
    }

    protected abstract void ActivateEvent();

    private IEnumerator setEventDone()
    {
        if (this.onlyOnce)
        {
            Player p = (Player)Player.instance;
            p.eventTable.Add(this.eventCode, this.eventCode);
            yield return null;
            while (!this.animationDone)
            {
                yield return null;
            }
            Destroy(this.destroyTarget);
        }
    }
}
