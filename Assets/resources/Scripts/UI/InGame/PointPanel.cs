using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointPanel : MonoBehaviour
{

    public int fruitPoint;
    public Text pointText;

    public int addPoint(int pt)
    {
        this.fruitPoint += pt;
        this.pointText.text = this.fruitPoint.ToString();
        return this.fruitPoint;
    }

    public int minusPoint(int pt)
    {
        if(this.fruitPoint - pt < 0)
        {
            return -1;
        }

        this.fruitPoint -= pt;
        this.pointText.text = this.fruitPoint.ToString();
        return this.fruitPoint;
    }

    public int setPoint(int pt)
    {
        if (pt < 0) return -1;

        this.fruitPoint = pt;
        this.pointText.text = this.fruitPoint.ToString();
        return this.fruitPoint;
    }
}
