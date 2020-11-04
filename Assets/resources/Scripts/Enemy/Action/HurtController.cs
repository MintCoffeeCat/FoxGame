using UnityEngine;
using System.Collections;

public class HurtController : MonoBehaviour
{
    [Tooltip("该碰撞体所属的敌人")]
    public Enemy target;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void SetDeathAni()
    {
        Object o = Resources.Load("prefabs/enemyDeath");
        GameObject go = (GameObject)Instantiate(o, this.gameObject.transform.position, this.gameObject.transform.rotation);
        DeathAni d = go.GetComponent<DeathAni>();
        d.setAudio(target.deathAudio);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.CompareTag("playerAttack"))
        {
            this.target.setHurt(1);
            this.SetDeathAni();
            Destroy(this.target.gameObject);
        }
        else if (collision.gameObject.CompareTag("reboundPlayerAttack"))
        {
            this.target.setHurt(1);
            Player pcl = (Player)Player.instance;
            pcl.VerticalRebound();

            this.SetDeathAni();
            Destroy(this.target.gameObject);
        }
    }
}
