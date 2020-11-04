using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections;

public class TalkUnit
{
    public Sprite header;
    public string name;
    public string sentense;

    public TalkUnit(Sprite header, string name, string sentense)
    {
        this.header = header;
        this.name = name;
        this.sentense = sentense;
    }
}
public class TextSpeed
{
    public static float SLOW = 0.1f;
    public static float NORMAL = 0.06f;
    public static float FAST = 0.02f;
}

public class MyDialog : MonoBehaviour
{
    [SerializeField]
    private Text txt;
    [SerializeField]
    private Text name;
    [SerializeField]
    private Text keyTip;
    [SerializeField]
    private Image header;
    private bool displayDone;
    private bool wantSkip;
    [SerializeField]
    public static float textSpeed;
    private  Queue<TalkUnit> content = new Queue<TalkUnit>();
    private void Update()
    {
        if (Input.GetButtonDown("Continue"))
        {
            if (!this.displayDone)
            {
                this.wantSkip = true;
                return;
            }

            this.displayDone = false;
            if(this.content.Count <= 0)
            {
                this.gameObject.SetActive(false);
                ((Player)Player.instance).gameObject.SetActive(true);
                return;
            }

            StartCoroutine(this.next());
        }    
    }

    public void StartDialog(Talkable tk)
    {
        ((Player)Player.instance).gameObject.SetActive(false);
        this.content.Clear();
        this.gameObject.SetActive(true);
        string[] texts = Regex.Split(tk.scripts.text, "\r\n", RegexOptions.IgnoreCase); 

        TalkUnit unit = new TalkUnit(null, null, null);

        foreach(string s in texts)
        {
            s.Replace("\r", string.Empty);
            if (s.Equals("Player"))
            {
                unit.header = ((Player)Player.instance).header;
                unit.name = "Player";

            }else if (s.Equals("Target"))
            {
                unit.header = tk.header;
                unit.name = tk.name;
            }
            else
            {
                unit.sentense = s;
                this.content.Enqueue(unit);
                unit = new TalkUnit(null, null, null);
            }
        }

        StartCoroutine(this.next());
    }

    IEnumerator next()
    {
        TalkUnit tk = content.Dequeue();
        this.name.text = tk.name;
        this.header.sprite = tk.header;
        this.txt.text = "";
        if(content.Count <= 0)
        {
            this.keyTip.text = "finish";
        }
        else
        {
            this.keyTip.text = "continue";
        }
        yield return null;
        for(int i = 0; i < tk.sentense.Length; i++)
        {
            this.txt.text += tk.sentense[i];
            if (this.wantSkip)
            {
                this.txt.text = tk.sentense;
                this.wantSkip = false;
                break;
            }
            yield return new WaitForSeconds(MyDialog.textSpeed);
        }
        this.displayDone = true;
    }
}