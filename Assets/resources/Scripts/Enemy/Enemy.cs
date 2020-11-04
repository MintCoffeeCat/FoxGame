using UnityEngine;
using System.Collections;
using UnityEditor;

public abstract class Enemy : MonoBehaviour
{

    [Tooltip("动画控制器")]
    public Animator anim;
    [Tooltip("用来检测视野的物体")]
    public Collider2D ScopeCheck;
    [Tooltip("该精灵的刚体对象")]
    public Rigidbody2D rb;

    [Header("音效")]
    [Tooltip("死亡音效所在路径")]
    public string deathAudio;
    [Tooltip("受伤音效")]
    public AudioSource hurtAudio;
    [Tooltip("移动速度")]
    public float speed;
    [Tooltip("攻击力")]
    public int attack = 1;

    [Space]
    [DisplayOnly]
    [Tooltip("是否撞墙")]
    public bool collideWall;
    [DisplayOnly]
    [Tooltip("是否发现玩家")]
    public bool getTarget;
    [DisplayOnly]
    [Tooltip("目前面朝的方向")]
    public int faceDirection;


    public abstract void setHurt(int ht);

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 v = collision.gameObject.transform.position - this.gameObject.transform.position;
            v = v.normalized;
            Player pcl = (Player)Player.instance;
            pcl.Hurt(this.attack);
            pcl.Rebound(v * 5);
        }
    }
}


