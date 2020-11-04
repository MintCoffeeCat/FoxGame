using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class quadratic
{
    // y= a(x-h)^2 + k
    // 定点(h,k)
    public float a;
    public float h;
    public float k;

    public Vector2 start;
    public quadratic(float a, float h, float k)
    {
        this.a = a;
        this.h = h;
        this.k = k;
    }

    public quadratic(Vector2 vertex, Vector2 start)
    {
        this.h = vertex.x;
        this.k = vertex.y;
        this.a = (start.y - this.k) / ((start.x - this.h) * (start.x - this.h));

        this.start = start;
    }
    public float getY(float x)
    {
        return a * (x - h) * (x - h) + k;
    }

    public Vector2 getSymmetryPoint(Vector2 v)
    {
        return new Vector2(2 * h - v.x, v.y);
    }

    public Vector2 getEndPoint()
    {
        if (this.start == null) return new Vector2(0,0);

        return this.getSymmetryPoint(this.start);
    }
}