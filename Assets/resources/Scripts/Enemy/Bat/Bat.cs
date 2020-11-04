using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : FlyingEnemy
{
    private bool isAwaking;
    private bool isAwake;

    public Collider2D body;
    [SerializeField]
    protected float attackRushSpeed;
    [SerializeField]
    [DisplayOnly]
    protected bool isKeepDistance;
    [SerializeField]
    [DisplayOnly]
    protected bool isAttack;
    [SerializeField]
    [Range(1, 10)]
    private int beforeAttackCount;
    [SerializeField]
    [DisplayOnly]
    private int countLeft;
    [SerializeField]
    [DisplayOnly]
    private quadratic rushLine;

    [SerializeField]
    [DisplayOnly]
    protected Vector2 attackPos;
    protected Vector2 endPos;
    [SerializeField]
    [DisplayOnly]
    protected int attackDirection;

    [SerializeField]
    private float keepDistance;

    private void Awake()
    {
        this.countLeft = this.beforeAttackCount;
    }
    private void FixedUpdate()
    {
        this.AI();
    }




    public void AI()
    {

        if (!this.isAwaking && this.getTarget) this.setAwake();

        if (!this.isAwake) return;

        if(this.isAttack && this.body.IsTouchingLayers(1 << LayerMask.NameToLayer("Ground")))
        {
            this.setAttack(false);
            this.flyOn();
        }

        Vector2 direction = new Vector2(0, 0);
        float additionY = 0;


        if (this.isAttack && this.getTarget)
        {
            //检测是否达到攻击终点
            if (Mathf.Abs(this.rushLine.getEndPoint().x - this.gameObject.transform.position.x) <= 0.1f)
            {
                this.setAttack(false);
            }
            else
            {
                Vector2 next = new Vector2(
                    this.gameObject.transform.position.x + 0.1f * this.attackDirection,
                    this.rushLine.getY(this.gameObject.transform.position.x + 0.1f * this.attackDirection));

                direction = next - (Vector2)this.gameObject.transform.position;
                direction = direction.normalized;
                this.rb.velocity = new Vector2(direction.x * this.attackRushSpeed, direction.y * this.attackRushSpeed);
                return;
            }

        }

        if (this.getTarget)
        {
            Player p = (Player)Player.instance;
            this.gameObject.transform.localScale = new Vector3(-this.faceDirection, 1, 1);
            Vector2 slf = this.gameObject.transform.position;
            Vector2 ply = p.gameObject.transform.position;
            direction = ply - slf;
            direction.y = direction.y + 0.5f;
            float dis = Mathf.Abs(direction.x);
            if (dis >= this.keepDistance && dis <= this.keepDistance + 1.5f)
            {
                direction.x = 0;
                this.isKeepDistance = true;
            }
            else
            {
                this.isKeepDistance = false;
                if (dis <= this.keepDistance)
                {
                    direction.x = -direction.x;
                }
                additionY = direction.normalized.y / 15;
            }
            direction = direction.normalized;
        }

        this.rb.velocity = new Vector2(direction.x * this.speed, this.rb.velocity.y - this.gravity + additionY);
    }
    public void flyOn()
    {
        this.rb.velocity = new Vector2(0, this.flyPower);
    }

    public void prepareAttack()
    {
        if (this.isKeepDistance && this.gameObject.transform.position.y > ((Player)Player.instance).Y)
        {
            this.countLeft--;
            if (this.countLeft > 0)
            {
                this.countLeft--;
                return;
            }
            else if (this.countLeft == 0)
            {
                this.setAttack(true);
                this.attackPos = ((Player)Player.instance).gameObject.transform.position;
                this.rushLine = new quadratic(this.attackPos, this.gameObject.transform.position);
                this.attackDirection = this.faceDirection;
            }
            else
            {
                this.countLeft = this.beforeAttackCount;
            }
        }
    }

    public override void setHurt(int ht)
    {
        bool b = ht == 0 ? false : true;
        this.anim.SetBool("hurt", b);
    }
    public void setAwake()
    {
        this.isAwaking = true;
        BoxCollider2D col = (BoxCollider2D)this.ScopeCheck;
        col.size = new Vector2(19, col.size.y);
        col.offset = new Vector2(col.offset.x - 2f, col.offset.y + 0.5f);
        this.anim.SetBool("awake", true);
        this.anim.SetBool("flying", false);
    }
    public void setAttack(bool b)
    {
        this.anim.SetBool("attack", b);
        this.isAttack = b;
    }

    public void setFlying()
    {
        this.isAwake = true;
        this.anim.SetBool("flying", true);
        this.anim.SetBool("awake", false);
    }
}
