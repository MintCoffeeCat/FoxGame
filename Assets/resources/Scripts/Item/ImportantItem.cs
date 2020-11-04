using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImportantItem : Event
{
    public string name;
    public string description;
    private float oriY;
    private float sinX = 0;
    protected void Awake()
    {
        this.onlyOnce = true;
        this.auto = true;
        this.oriY = this.gameObject.transform.position.y;
        base.Awake();
    }
    protected void Update()
    {
        Vector3 oriV = this.gameObject.transform.position;
        float delta = 0.3f * Mathf.Sin(this.sinX);
        Vector2 v = new Vector2(oriV.x, this.oriY + delta);
        if (this.sinX <= Mathf.PI * 2)
        {
            this.sinX += 0.02f;
        }
        else
        {
            this.sinX = 0;
        }
        this.gameObject.transform.position = v;

        base.Update();
    }
    protected override void ActivateEvent()
    {
        ((UIController)UIController.instance).showItemGetPanel(this);
    }
}
