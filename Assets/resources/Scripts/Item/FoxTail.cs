using UnityEngine;
using System.Collections;

public class FoxTail : ImportantItem
{

    protected void Awake()
    {
        base.Awake();
        this.name = "狐尾";
        this.description = "生命值上限 + 1, 并且完全恢复生命值。";
    }

    protected override void ActivateEvent()
    {
        base.ActivateEvent();
        Player p = ((Player)Player.instance);
        p.maxHp += 1;
        p.hp = p.maxHp;
        p.resetUI();
    }
}
