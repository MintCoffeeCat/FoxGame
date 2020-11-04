using UnityEngine;
using System.Collections;

public abstract class WalkingEnemy : Enemy
{
    [Tooltip("用来检测地面的物体")]
    public Collider2D groundCheck;
    [Tooltip("跳跃力")]
    public float jumpPower;

    [DisplayOnly]
    [Tooltip("是否落地")]
    public bool onGround;


    protected virtual void CheckOnGround()
    {
        this.onGround = this.groundCheck.IsTouchingLayers(LayerMask.NameToLayer("Ground")) || this.groundCheck.IsTouchingLayers(LayerMask.NameToLayer("halfground"));
    }

    protected virtual void ChangeState()
    {
        if (this.onGround)
        {
            this.SetIdle();
        }
        else
        {
            if (this.rb.velocity.y > 1)
            {
                this.SetJump();
            }
            else if (this.rb.velocity.y < -1)
            {
                this.SetFall();
            }
        }
    }

    protected virtual void SetIdle()
    {
        this.anim.SetBool("jumping", false);
        this.anim.SetBool("falling", false);
    }
    protected virtual void SetJump()
    {
        this.anim.SetBool("jumping", true);
        this.anim.SetBool("falling", false);
    }
    protected virtual void SetFall()
    {
        this.anim.SetBool("jumping", false);
        this.anim.SetBool("falling", true);
    }
}
