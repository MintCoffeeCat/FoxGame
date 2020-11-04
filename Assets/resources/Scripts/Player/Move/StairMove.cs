using UnityEngine;
using System.Collections;

public class StairMove : MoveController
{
    public StairMove(Player ply) : base(ply)
    {
        this.maxSpeedX = 4;
        this.maxSpeedY = 4;
        this.accelerationX = 0;
        this.accelerationY = 0;
        this.dragX = 0;
        this.dragY = 0;
    }
    public override void Jump()
    {
        if (p.jumpPressed)
        {
            p.jumpPressed = false;
            p.isJump = true;
            p.updateSpeedY(p.jumpPower);

            p.jumpAudio.Play();
            p.jumpCount--;
            p.onStair = false;
        }
    }

    public override void Move()
    {
        if (!p.onStair) p.onStair = true;
        this.MoveX();
        this.MoveY();
    }

    public override void MoveX()
    {
        if(this.p.inputX > 0)
        {
            p.speedX = this.maxSpeedX;
        }else if(this.p.inputX < 0)
        {
            p.speedX = -this.maxSpeedX;
        }
        else
        {
            p.speedX = 0;
        }
    }

    public override void MoveY()
    {
        if (this.p.inputY > 0)
        {
            p.speedY = this.maxSpeedY;
        }
        else if (this.p.inputY < 0)
        {
            p.speedY = -this.maxSpeedY;

        }
        else
        {
            p.speedY = 0;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
