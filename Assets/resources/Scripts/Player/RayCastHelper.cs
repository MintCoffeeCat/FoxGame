using UnityEngine;
using System.Collections;

public class RayCastHelper : MonoBehaviour
{

    public static RaycastHit2D RayCast(Vector2 start, Vector2 direction, float distance, string layerName)
    {
        LayerMask layer = LayerMask.NameToLayer(layerName);
        return RayCastHelper.RayCast(start, direction, distance, layer);
    }
    public static RaycastHit2D RayCast(Vector2 start, Vector2 direction, float distance, LayerMask layer)
    {
        RaycastHit2D hitPoint  = Physics2D.Raycast(start, direction, distance, 1 << layer);
        if (hitPoint)
        {
            Vector2 hit = hitPoint.point;
            float xDistance = hit.x - start.x;
            float yDistance = hit.y - start.y;
            float hitDistance = Mathf.Sqrt(xDistance * xDistance + yDistance * yDistance);
            Debug.DrawRay(start, direction * hitDistance, Color.red);
        }
        else
        {
            Debug.DrawRay(start, direction * distance, Color.white);
        }

        return hitPoint;
    }
}
