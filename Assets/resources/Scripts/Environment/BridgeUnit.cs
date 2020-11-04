using UnityEngine;
using UnityEditor;

public class BridgeUnit : MonoBehaviour
{
    public Animator anim;
    public Collider2D col;
    public void collapse()
    {
        anim.SetBool("collapse", true);
        Destroy(this.col);
    }
}