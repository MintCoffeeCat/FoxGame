using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField]
    private Text hp;
    [SerializeField]
    private Text maxHp;
    [SerializeField]
    private Text mp;
    [SerializeField]
    private Text maxMp;
    [SerializeField]
    private Image hpBar;
    [SerializeField]
    private Image mpBar;
    [SerializeField]
    private float hpBarOriLength;
    [SerializeField]
    private float mpBarOriLength;

    private float hpChangeLength;
    private float lastTimeHp;
    private float lastTimeMaxHp;

    private bool hpChangeDone = true;
    private bool mpDecreaseDone;

    protected void Awake()
    {
        this.lastTimeHp = ((Player)Player.instance).hp;
        this.lastTimeMaxHp = ((Player)Player.instance).maxHp;
        this.hpBarOriLength = this.hpBar.rectTransform.rect.xMax;
        this.mpBarOriLength = this.mpBar.rectTransform.rect.xMax;
        this.setHp();
        this.setMp();

    }

    public void setHp()
    {

        Player p = (Player)Player.instance;
        float nowHpPersent = (float)p.hp / p.maxHp;
        float lastHpPersent = this.lastTimeHp / this.lastTimeMaxHp;
        this.hpChangeLength += this.hpBarOriLength * (nowHpPersent - lastHpPersent);

        this.lastTimeHp = p.hp;
        this.lastTimeMaxHp = p.maxHp;

        if (this.hpChangeLength != 0)
        {
            StartCoroutine(this.hpChange());
        }


        this.hp.text = p.hp.ToString();
        this.maxHp.text = p.maxHp.ToString();
        //this.hpBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.hpBarOriLength * (p.hp / (float)p.maxHp));
    }

    public void setMp()
    {
        Player p = (Player)Player.instance;
        this.mp.text = p.mp.ToString();
        this.maxMp.text = p.maxMp.ToString();
        if (p.maxMp == 0)
        {
            this.mpBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
        }
        else
        {
            this.mpBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.mpBarOriLength * (p.mp / (float)p.maxMp));
        }
    }

    private IEnumerator hpChange()
    {
        if(!this.hpChangeDone)
        {
            yield break;
        }

        this.hpChangeDone = false;

        while (this.hpChangeLength != 0)
        {
            //正的表示增加，负的表示减少
            int positive = (int)(this.hpChangeLength / Mathf.Abs(this.hpChangeLength));
            float changeVal = (0.5f + Mathf.Abs(this.hpChangeLength)/24) * positive;

            this.hpChangeLength -= changeVal;
            Debug.Log(this.hpChangeLength);
            if(positive > 0)
            {
                if(hpChangeLength <= 0)
                {
                    this.hpChangeDone = true;
                    yield break;
                }
            }else if(positive < 0)
            {
                if (hpChangeLength >= 0)
                {
                    this.hpChangeDone = true;
                    this.hpChangeLength = 0;
                    yield break;
                }
            }

            RectTransform rect = this.hpBar.rectTransform;

            rect.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Horizontal,
                rect.rect.xMax + changeVal
            );
            yield return null;
        }

    }
}
