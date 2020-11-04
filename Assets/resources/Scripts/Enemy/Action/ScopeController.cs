using UnityEngine;
using System.Collections;

public class ScopeController : MonoBehaviour
{
    [Tooltip("该视野所属的敌人")]
    public Enemy target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)     //10 是 body
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                this.target.getTarget = true;
                float val = collision.gameObject.transform.position.x - this.gameObject.transform.position.x;
                this.target.faceDirection = (int)(Mathf.Abs(val) / val);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)     //10 是 body
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                this.target.getTarget = false;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)     //10 是 body
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                float val = collision.gameObject.transform.position.x - this.gameObject.transform.position.x;
                this.target.faceDirection = (int)(Mathf.Abs(val) / val);
            }
        }
    }
}
