using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsableBridge : Event
{
    public List<BridgeUnit> bridges;

    private void Awake()
    {
        this.animationDone = false;
        this.destroyTarget = this.gameObject.transform.parent.gameObject;
        base.destroyOnce();
    }
    protected override void ActivateEvent()
    {
        StartCoroutine(this.collapse());
    }

    IEnumerator collapse()
    {
        foreach(BridgeUnit bridge  in this.bridges)
        {
            bridge.collapse();
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.8f);
        this.animationDone = true;
    }
}
