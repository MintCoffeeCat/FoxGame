using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : WalkingEnemy
{
    [Tooltip("用来检测左右碰撞的物体")]
    public Collider2D HoriCheck;

    void Start()
    {
        
    }
    void FixedUpdate()
    {
        this.CheckOnGround();

        this.ChangeState();
    }

    protected override void CheckOnGround()
    {
        base.CheckOnGround();

        if (this.HoriCheck.enabled)
        {
            this.collideWall = this.HoriCheck.IsTouchingLayers(LayerMask.NameToLayer("Ground"));
            if (this.collideWall)
            {
                this.faceDirection = -this.faceDirection;
                this.HoriCheck.enabled = false;
            }
        }
    }

    private void AI()
    {
        if (!this.getTarget) return;

        this.gameObject.transform.localScale = new Vector3(-this.faceDirection, 1, 1);
        if (this.onGround)
        {
            this.rb.velocity = new Vector2(this.rb.velocity.x, this.jumpPower);
        }
        this.rb.velocity = new Vector2(this.speed * this.faceDirection, this.rb.velocity.y);
        this.HoriCheck.GetComponent<Collider2D>().enabled = true;
    }

    public override void setHurt(int ht)
    {
        bool b = ht == 0 ? false : true;
    }
}
