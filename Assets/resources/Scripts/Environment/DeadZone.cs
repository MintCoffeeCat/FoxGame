using UnityEngine;
using System.Collections;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ((Player)Player.instance).Death();
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
