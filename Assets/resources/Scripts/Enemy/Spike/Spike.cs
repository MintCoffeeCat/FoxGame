using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player p = (Player)Player.instance;
            Vector2 v = collision.gameObject.transform.position - this.gameObject.transform.position;
            v = v.normalized;
            p.Rebound(v * 5);
            p.Hurt(p.maxHp/2);
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
