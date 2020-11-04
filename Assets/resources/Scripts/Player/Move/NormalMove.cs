using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class NormalMove : MoveController
{
    private float checkDistance = 0.8f;
    public NormalMove(Player ply) : base(ply)
    {
        this.maxSpeedX = 8;
        this.maxSpeedY = 15;
        this.accelerationX = 1.5f;
        this.accelerationY = 0.6f;
        this.dragX = 0.8f;
        this.dragY = 0;
    }
    public NormalMove() : base()
    {
        this.maxSpeedX = 10;
        this.maxSpeedY = 10;
        this.accelerationX = 2;
        this.accelerationY = 0.55f;
        this.dragX = 2;
        this.dragY = 0;
    }
    public override void Move()
    {
        //this.getGroundDirection();
        if (p.onGround)
        {
            Vector2 newDirection = this.getGroundDirection(); //dcl.getDirection(p.faceDirection);
            if (p.inputX == 0)
            {
                if (p.speedX == 0) return;

                p.speed -= this.dragX;
                if (p.speed < 0) p.speed = 0;
            }
            else
            {
                directionController dcl = this.p.gameObject.GetComponentInChildren<directionController>();
                int newXDir = p.faceDirection;
                int moveXDir = (int)(Mathf.Abs(p.speedX) / p.speedX);
                if (newXDir == -moveXDir)
                {
                    p.speed -= this.accelerationX;
                    if (p.speed <= 0)
                    {
                        p.speed = 0;
                        p.moveDirection = newDirection;
                    }
                }
                else
                {
                    p.moveDirection = newDirection;
                    p.speed += this.accelerationX;
                    if (p.speed > this.maxSpeedX)
                    {
                        p.speed = this.maxSpeedX;
                    }
                }
            }
            Vector2 v = p.speed * p.moveDirection;
            if (p.couldControll)
            {
                p.speedX = v.x;
            }
            p.speedY = v.y;

        }
        else
        {
            if (p.couldControll)
            {
                this.MoveX();
            }
            this.MoveY();
        }
        if (p.speedX != 0 && p.onGround)
        {
            if (!p.footstep.isPlaying)
            {
                p.footstep.Play();
            }
        }
        else
        {
            p.footstep.Pause();
        }
        this.reCalculateSpeed();
    }

    public override void MoveX()
    {

        int nowDirection = 0;

        if (p.speedX != 0) nowDirection = (int)(Mathf.Abs(p.speedX) / p.speedX);
        //无移动,阻力减速
        if (p.inputX == 0)
        {
            if (p.speedX == 0) return;

            p.speedX -= this.dragX * p.faceDirection;
            if (nowDirection == -p.faceDirection) p.speedX = 0;
        }
        //有移动
        else
        {
            int wantDirection = p.faceDirection;
            p.speedX += wantDirection * this.accelerationX;
            if (p.speedX >= 0)
            {
                p.moveDirection = new Vector2(1, 0);
            }
            else
            {
                p.moveDirection = new Vector2(-1, 0);
            }
            if (Mathf.Abs(p.speedX) > this.maxSpeedX)
            {
                p.speedX = this.maxSpeedX * nowDirection;
            }
        }
    }

    public override void MoveY()
    {
        p.speedY -= this.accelerationY;
        if (p.speedY < -this.maxSpeedY)
        {
            p.speedY = -this.maxSpeedY;
        }
    }

    public override void Jump()
    {
        if ((p.onGround || (!p.onGround && p.jumpProtectFrameLeft > 0)) && p.jumpPressed)
        {
            p.jumpPressed = false;
            p.isJump = true;
            p.updateSpeedY(p.jumpPower);

            p.jumpAudio.Play();
            p.jumpCount--;
        }
    }

    private Vector2 getGroundDirection()
    {
        Vector2 groundDirection = new Vector2(1 * this.p.faceDirection, 0);
        RaycastHit2D ptBehind = RayCastHelper.RayCast(this.p.getBehindFoot(), Vector2.down, this.checkDistance, "Ground");
        RaycastHit2D ptFore = RayCastHelper.RayCast(this.p.getForeFoot(), Vector2.down, this.checkDistance, "Ground");

        if (ptBehind && ptFore)
        {
            groundDirection = ptFore.point - ptBehind.point;
            groundDirection = groundDirection.normalized;
            if (Mathf.Abs(groundDirection.y) < 1E-3)
                groundDirection.y = 0;
        }

        return groundDirection;
    }
}
