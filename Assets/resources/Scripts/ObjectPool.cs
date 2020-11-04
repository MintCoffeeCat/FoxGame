using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : Singleton<ObjectPool>
{
    public bool isInit = false;
    public MoveController normal;
    public MoveController wall;
    public MoveController stair;

    public int dashShadowNum;
    public Queue<GameObject> dashPool;
    public List<Spawn> SpawnList;

    private void Awake()
    {
        base.Awake();
        if (!this.isInit)
        {
            this.initPool();
        }
    }
    private void initPool()
    {
        this.dashPool = new Queue<GameObject>(this.dashShadowNum);
        this.normal = new NormalMove((Player)Player.instance);
        this.wall = new WallMove((Player)Player.instance);
        this.stair = new StairMove((Player)Player.instance);
    }
}
