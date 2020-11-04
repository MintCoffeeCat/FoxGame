using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Singleton<Player>
{
    public static bool isInit = false;
    public Rigidbody2D rb;
    public Animator anim;
    [DisplayOnly]
    public int faceDirection;
    public Sprite header;

    [Header("存档关键信息")]
    public string sceneName;
    public float X;
    public float Y;
    public int fruitPoint;
    public MyDictionary eventTable = new MyDictionary();
    public int lastSaveSlot;
    public int hp;
    public int maxHp;
    public int mp;
    public int maxMp;

    [Space]
    [Header("碰撞检测相关")]
    //碰撞检测相关
    private float footDistance = 0.2f;
    private float checkDistance = 0.2f;
    private float footYoffset = -0.82f;
    private float wallCheckoffset = 0.25f;

    [Space]
    [Header("速度和移动相关")]
    //速度和移动相关
    public int inputX;
    public int inputY;
    public float speed;
    public float speedX;
    public float speedY;
    public MoveController moveController;
    public int uncontrolledFrameLeft;
    public Vector2 moveDirection;
    [DisplayOnly]
    public bool couldControll;
    [DisplayOnly]
    public bool onStair;
    [Space]
    [Header("跳跃相关")]
    //跳跃相关
    public float jumpPower;
    public bool jumpPressed;
    public int jumpCount;
    public int jumpProtectFrame;
    public int jumpProtectFrameLeft;
    [DisplayOnly]
    public bool isJump;
    [DisplayOnly]
    public bool onGround;
    [Space]
    [Header("反墙跳相关")]
    //反墙跳能力相关
    [DisplayOnly]
    public bool wallDirectionPressed;
    [DisplayOnly]
    public bool onWall;
    public bool wallJumpAcquired;
    public int wallJumpProtectFrame;
    public int lastWallDirection;
    [Space]
    [Header("受伤相关")]
    //受伤相关
    public bool isHurt;
    public int invincibleFrame;
    public int invincibleFrameLeft;
    [Space]
    [Header("声音相关")]
    //声音相关
    public AudioSource footstep;
    public AudioSource jumpAudio;

    protected void Awake()
    {
        this.rb.transform.position = new Vector2(this.X, this.Y);
        this.gameObject.SetActive(false);
        //Time.timeScale = 0.5f;
        base.Awake();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            UIController uc = (UIController)UIController.instance;
            uc.ShowPauseMenu();
        }

        if (Input.GetButtonDown("Jump") && (this.jumpCount > 0 || (this.jumpCount <= 0 && this.onWall && this.wallDirectionPressed)))
        {
            this.jumpPressed = true;
        }

        if ((this.onWall && Input.GetButton("Left") && this.faceDirection == -1) ||
            (this.onWall && Input.GetButton("Right") && this.faceDirection == 1))
        {
            this.wallDirectionPressed = true;
        }
        else
        {
            this.wallDirectionPressed = false;
        }
    }

    void FixedUpdate()
    {
        if (!Player.isInit) return;

        this.CheckControll();
        this.CheckOnGround();
        this.CheckHead();
        this.CheckWall();
        float f = Input.GetAxisRaw("Horizontal");
        float ff = Input.GetAxisRaw("Vertical");
        this.inputX = (int)f;
        this.inputY = (int)ff;

        this.JudgeMoveState();
        this.PlayerMove();
        this.CheckHurt();
        this.CheckJump();
        this.ChangeState();
    }



    private void PlayerMove()
    {
        //返回-1,0,1三个之一
        if (this.couldControll)
        {
            this.updateFaceDir(this.inputX);
        }


        this.moveController.Move();

        this.updateSpeed();
        this.X = this.rb.transform.position.x;
        this.Y = this.rb.transform.position.y;
    }
    private void CheckOnGround()
    {
        if (this.jumpProtectFrameLeft > 0)
        {
            this.jumpProtectFrameLeft--;
        }

        bool ground = false, halfground = false;

        RaycastHit2D ptBehind = RayCastHelper.RayCast(this.getBehindFoot(), Vector2.down, this.checkDistance, "Ground");
        RaycastHit2D ptFore = RayCastHelper.RayCast(this.getForeFoot(), Vector2.down, this.checkDistance, "Ground");

        if( (ptBehind || ptFore) && (this.speedY <=0 || !this.isJump && this.speedY >0)  )
        {
            ground = true;
        }

        ptBehind = RayCastHelper.RayCast(this.getBehindFoot(), Vector2.down, this.checkDistance, "halfGround");
        ptFore = RayCastHelper.RayCast(this.getForeFoot(), Vector2.down, this.checkDistance, "halfGround");

        if ( (ptBehind || ptFore) && this.speedY <= 0)
        {
            halfground = true;
        }

        //bool ground = groundCheck.IsTouchingLayers(groundLayer);
        //bool halfground = (groundCheck.IsTouchingLayers(LayerMask.NameToLayer("halfground")) && this.speedY <= 0);

        if (ground || halfground)
        {
            if (!this.onGround) this.updateSpeedY(0);
            this.onGround = true;
            this.jumpProtectFrameLeft = 0;
        }
        else
        {
            if (this.onGround)
            {
                this.jumpProtectFrameLeft = this.jumpProtectFrame;
            }
            this.onGround = false;
        }
    }
    private void CheckWall()
    {
        Vector2 top = new Vector2(this.X + (this.wallCheckoffset * this.faceDirection), this.Y -0.05f);
        Vector2 bottom = new Vector2(this.X + (this.wallCheckoffset * this.faceDirection), this.Y - 0.5f);
        RaycastHit2D hitPointTop = RayCastHelper.RayCast(top, Vector2.right * this.faceDirection, 0.22f, "Ground");
        RaycastHit2D hitPointBottom = RayCastHelper.RayCast(bottom, Vector2.right * this.faceDirection, 0.22f, "Ground");

        if (hitPointTop && hitPointBottom)
        {
            this.onWall = true;
            this.lastWallDirection = this.faceDirection;
            this.updateSpeedX(0);
        }
        else
        {
            this.onWall = false;
        }
    }
    private void CheckHead()
    {
        Vector2 behind = new Vector2(this.X - 0.2f * this.faceDirection, this.Y + 0.2f);
        Vector2 fore = new Vector2(this.X + 0.2f * this.faceDirection, this.Y + 0.2f);

        RaycastHit2D behindPoint = RayCastHelper.RayCast(behind, Vector2.up, 0.15f, "Ground");
        RaycastHit2D forePoint = RayCastHelper.RayCast(fore, Vector2.up, 0.15f, "Ground");

        if(behindPoint || forePoint)
        {
            if(this.speedY >= -1)
                this.speedY = -1;
        }

    }
    private void JudgeMoveState()
    {
        ObjectPool op = (ObjectPool)ObjectPool.instance;
        if (this.wallJumpAcquired && this.onWall && this.wallDirectionPressed && this.speedY < 0 && !this.onGround)
        {
            this.moveController = op.wall;

        }
        else if (this.onStair && this.inputY != 0)
        {
            this.moveController = op.stair;
            this.jumpCount = 1;
            this.isJump = false;
        }
        else if (!this.onStair || (this.onStair && this.inputY <= 0 && this.onGround) || (this.onStair && this.isJump))
        {
            this.moveController = op.normal;
        }
    }
    private void CheckHurt()
    {
        if (this.isHurt)
        {
            this.invincibleFrameLeft -= 1;
            if (this.invincibleFrameLeft < 0)
            {
                this.isHurt = false;
                this.anim.SetBool("hurt", this.isHurt);
            }
        }
    }
    private void CheckControll()
    {
        if (this.uncontrolledFrameLeft > 0)
        {
            this.uncontrolledFrameLeft--;
        }
        else
        {
            this.couldControll = true;
        }
    }
    private void CheckJump()
    {
        if (!this.couldControll) return;

        if (this.onGround)
        {
            this.isJump = false;
            this.jumpCount = 1;
        }

        this.moveController.Jump();
    }
    public void VerticalRebound()
    {
        this.updateSpeedY(this.jumpPower);
    }
    public void Rebound(Vector2 vc)
    {

        this.updateDirectionAndSpeed(vc, Mathf.Sqrt(vc.x * vc.x + vc.y * vc.y));

    }
    public void SetUncontrolledFrame(int f)
    {
        this.uncontrolledFrameLeft = f;
        this.couldControll = false;
    }
    public void Hurt(int v)
    {
        if (!this.isHurt)
        {
            this.hp -= v;
            if (this.hp <= 0)
            {
                this.hp = 0;
                ((UIController)UIController.instance).getStatusBar().setHp();
                this.Death();
            }
            else
            {
                ((UIController)UIController.instance).getStatusBar().setHp();
                this.SetHurt();
            }

        }

    }
    public void Death()
    {
        ((UIController)UIController.instance).showDeadPanel();
        this.gameObject.SetActive(false);
    }
    public void resetUI()
    {
        ((UIController)UIController.instance).getStatusBar().setHp();
        ((UIController)UIController.instance).getStatusBar().setMp();
    }
    public void updateFaceDir(int f)
    {
        if (f == 0) return;
        this.faceDirection = f;
        this.transform.localScale = new Vector3(faceDirection, 1, 1);
    }

    public void updateDirectionAndSpeed(Vector2 dir, float spd)
    {
        this.moveDirection = dir.normalized;
        this.speed = spd;
        Vector2 v = this.moveDirection * this.speed;
        this.speedX = v.x;
        this.speedY = v.y;
        this.updateSpeed();
    }
    private void updateSpeed()
    {
        this.rb.velocity = new Vector2(this.speedX, this.speedY);
    }

    public void updateSpeedX(float x)
    {
        this.speedX = x;
        this.speed = Mathf.Sqrt(this.speedX * this.speedX + this.speedY * this.speedY);
        this.updateSpeed();
    }
    public void updateSpeedY(float y)
    {
        this.speedY = y;
        this.speed = Mathf.Sqrt(this.speedX * this.speedX + this.speedY * this.speedY);
        this.updateSpeed();
    }

    private void ChangeState()
    {
        anim.SetFloat("running", Mathf.Abs(this.speedX));
        ObjectPool op = (ObjectPool)ObjectPool.instance;
        if (this.moveController == op.normal)
        {
            if (this.onGround)
            {
                this.SetIdle();
            }
            else if (this.isJump)
            {
                if (this.speedY > 0)
                {
                    this.SetJump();
                }
                else
                {
                    this.SetFall();
                }
            }
            else
            {
                if (this.speedY < 0)
                {
                    this.SetFall();
                }

            }
        }
        else if (this.moveController == op.wall)
        {
            this.SetOnWall();
        }
        else if (this.moveController == op.stair)
        {
            this.SetOnStair();
            if (this.inputX != 0 || this.inputY != 0)
            {
                this.SetStairMove();
            }
            else
            {
                this.SetNotStairMove();
            }
        }
    }
    public void GetFruit(int pt)
    {
        this.fruitPoint += pt;
    }

    private void SetFall()
    {
        anim.SetBool("falling", true);
        anim.SetBool("jumping", false);
        anim.SetBool("onStair", false);
        anim.SetBool("onWall", false);
        anim.SetBool("stairMove", false);
    }
    private void SetJump()
    {
        anim.SetBool("falling", false);
        anim.SetBool("jumping", true);
        anim.SetBool("onWall", false);
        anim.SetBool("onStair", false);
        anim.SetBool("stairMove", false);
    }

    private void SetIdle()
    {
        anim.SetBool("falling", false);
        anim.SetBool("jumping", false);
        anim.SetBool("onWall", false);
        anim.SetBool("onStair", false);
        anim.SetBool("stairMove", false);
    }
    private void SetOnWall()
    {
        anim.SetBool("onWall", true);
    }
    private void SetNotOnWall()
    {
        anim.SetBool("onWall", false);
    }

    private void SetOnStair()
    {
        anim.SetBool("onStair", true);
        anim.SetBool("falling", false);
        anim.SetBool("jumping", false);
    }

    private void SetNotOnStair()
    {
        anim.SetBool("onStair", false);
    }
    private void SetStairMove()
    {
        anim.SetBool("stairMove", true);
    }

    private void SetNotStairMove()
    {
        anim.SetBool("stairMove", false);
    }

    private void SetHurt()
    {
        this.isHurt = true;
        this.invincibleFrameLeft = this.invincibleFrame;
        this.SetUncontrolledFrame(this.invincibleFrame);

        this.anim.SetBool("hurt", this.isHurt);
        this.SetNotOnStair();
        this.SetNotStairMove();
        ObjectPool op = (ObjectPool)ObjectPool.instance;
        this.moveController = op.normal;
    }

    public void Spawn(float newx, float newy, string scene)
    {
        ((Player)Player.instance).rb.transform.position = new Vector2(newx, newy);
        this.sceneName = scene;
    }


    public Vector2 getForeFoot()
    {
        float foreX = this.X + (this.footDistance * this.faceDirection);
        Vector2 foreFoot = new Vector2(foreX, this.Y + this.footYoffset);
        return foreFoot;
    }
    public Vector2 getBehindFoot()
    {
        float behindX = this.X - ((this.footDistance + 0.15f) * this.faceDirection);
        Vector2 behindFoot = new Vector2(behindX, this.Y + this.footYoffset);
        return behindFoot;
    }
}
