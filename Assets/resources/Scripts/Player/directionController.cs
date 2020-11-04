using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class directionController : MonoBehaviour
{

    public Vector2 p1;
    public Vector2 p2;
    RaycastHit2D left;
    RaycastHit2D right;
    public int colliderCount;

    public Vector2 getDirection(float face)
    {
        Vector2 result;
        if (colliderCount < 2)
        {
            return new Vector2(1, 0) * face;
        }

        if (face < 0)
        {
            result = this.left.point - this.right.point;
        }
        else
        {
            result = this.right.point - this.left.point;
        }

        if (Mathf.Abs(result.y) < 1E-4)
        {
            result.y = 0;
        }
        return result.normalized;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.setDir();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        this.setDir();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        this.colliderCount = 0;
    }
    private void setDir()
    {
        this.colliderCount = 0;
        Vector2 v = this.gameObject.transform.position;

        p1 = new Vector2(v.x - 0.1f, v.y);
        p2 = new Vector2(v.x + 0.1f, v.y);

        Vector2 dire = new Vector2(0, -1f);

        left = Physics2D.Raycast(p1, dire, 0.5f, 1 << LayerMask.NameToLayer("Ground"));
        right = Physics2D.Raycast(p2, dire, 0.5f, 1 << LayerMask.NameToLayer("Ground"));
        if (left.collider == null)
        {
            Debug.Log("Left: null");
        }
        else
        {
            this.colliderCount += 1;
        }
        if (right.collider == null)
        {
            Debug.Log("Right: null");
        }
        else
        {
            this.colliderCount += 1;
        }
    }
}
