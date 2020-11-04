using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
    [Tooltip("获得该道具后的得分")]
    public int pt;
    [Tooltip("获得音效所在路径")]
    public string getAudio;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            Player pcl = (Player)Player.instance;
            pcl.GetFruit(this.pt);
            Object o = Resources.Load("prefabs/itemGet");
            GameObject go = (GameObject)Instantiate(o, this.gameObject.transform.position, this.gameObject.transform.rotation);
            
            DeathAni d = go.GetComponent<DeathAni>();
            d.setAudio(this.getAudio);

            Destroy(this.gameObject);
        }
    }
}
