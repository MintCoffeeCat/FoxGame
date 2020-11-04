using UnityEngine;
using System;
using System.Collections;

[Serializable]
public abstract class MoveController
{
    protected Player p;
    public float maxSpeedX;
    public float maxSpeedY;
    public float accelerationX;
    public float accelerationY;
    public float dragX;
    public float dragY;

    public MoveController(Player ply)
    {
        this.p = ply;
    }
    public MoveController() { }

    public void setPlayer(Player ply)
    {
        this.p = ply;
    }
    public bool hasPlayer()
    {
        if(this.p == null)
        {
            return false;
        }
        return true;
    }
    public abstract void Move();
    public abstract void MoveX();
    public abstract void MoveY();
    public abstract void Jump();
    public void reCalculateSpeed()
    {
        float x = this.p.speedX * this.p.speedX;
        float y = this.p.speedY * this.p.speedY;

        this.p.speed = Mathf.Sqrt(x + y);
    }
}
