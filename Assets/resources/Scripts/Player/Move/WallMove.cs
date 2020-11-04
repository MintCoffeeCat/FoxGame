using UnityEngine;
using System.Collections;

public class WallMove : MoveController
{
    public WallMove(Player ply) : base(ply)
    {
        this.maxSpeedX = 12;
        this.maxSpeedY = 2f;
        this.accelerationX = 0;
        this.accelerationY = 0.6f;
        this.dragX = 0;
        this.dragY = 0;
    }

    public override void Jump()
    {
        if (p.onWall && p.jumpPressed && p.wallDirectionPressed)
        {
            p.jumpPressed = false;
            p.isJump = true;
            p.updateFaceDir(-p.lastWallDirection);
            p.updateSpeedY(p.jumpPower * 7 / 10);
            p.updateSpeedX(-p.lastWallDirection * p.moveController.maxSpeedX);
            p.moveDirection = new Vector2(-p.lastWallDirection, 0);
            p.jumpAudio.Play();
            p.jumpCount--;
            p.wallDirectionPressed = false;
            p.SetUncontrolledFrame(p.wallJumpProtectFrame);
        }
    }

    public override void Move()
    {
        this.MoveY();
    }

    public override void MoveX()
    {

    }

    public override void MoveY()
    {
        p.speedY -= this.accelerationY;
        if (p.speedY < -this.maxSpeedY)
        {
            p.speedY = -this.maxSpeedY;
        }
    }
}
