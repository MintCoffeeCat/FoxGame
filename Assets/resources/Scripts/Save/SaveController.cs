using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

public class SaveController
{
    private static string saveDir = "saveData";
    private int saveNum;
    private BinaryFormatter binaryFormatter;

    public SaveController(int num)
    {
        this.saveNum = num;
        this.binaryFormatter = new BinaryFormatter();
    }
    public static List<string> readSaveInfos()
    {
        List<string> infos = new List<string>();
        string saveDir = Application.persistentDataPath + "/" + SaveController.saveDir;
        if (Directory.Exists(saveDir))
        {
            string[] saveslots = Directory.GetDirectories(saveDir);
            foreach(string s in saveslots){
                string[] sv = Directory.GetFiles(s);
                if(sv.Length > 0)
                {
                    DateTime tm = Directory.GetCreationTime(sv[0]);
                    infos.Add(tm.ToString());
                }
            }
        }
        return infos;
    }

    public void Save()
    {
        Debug.Log("正在查找存档目录...");
        string saveDir = Application.persistentDataPath + "/" + SaveController.saveDir;

        if (Directory.Exists(saveDir))
        {
            Debug.Log("查询到存档目录");
        }
        else
        {
            Debug.Log("未查询到游戏目录，正在创建目录...");
            Directory.CreateDirectory(saveDir);
            Debug.Log("创建完成，存档目录为: " + saveDir);
        }
        string saveNumDir = saveDir + "/save" + this.saveNum.ToString();
        if (Directory.Exists(saveNumDir))
        {
            Debug.Log("正在进行存档...");
        }
        else
        {
            Debug.Log("正在创建存档位...");
            Directory.CreateDirectory(saveNumDir);
            Debug.Log("正在进行存档...");
        }
        string playerSave = saveNumDir + "/" + "player.save";
        FileStream f = File.Create(playerSave);
        Player p = (Player)Player.instance;
        if (p == null)
        {
            SceneController sc = (SceneController)SceneController.instance;
            sc.createGameObject();
            p = (Player)Player.instance;
        }
        p.lastSaveSlot = this.saveNum;
        string js = JsonUtility.ToJson(p);

        this.binaryFormatter.Serialize(f, js);
        Debug.Log("存档完成！存档位于: " + playerSave);
        f.Close();
    }

    public bool Load()
    {
        string saveDir = Application.persistentDataPath + "/" + SaveController.saveDir;
        string saveNumDir = saveDir + "/save" + this.saveNum.ToString();
        string playerSave = saveNumDir + "/" + "player.save";
        if(!Directory.Exists(saveDir) || !Directory.Exists(saveNumDir))
        {
            Debug.Log("存档未建立,无法读取");
            FileStream f = File.Create("errorLog");
            this.binaryFormatter.Serialize(f, "存档未建立, 无法读取");
            return false;
        }

        if (File.Exists(playerSave))
        {
            FileStream file = File.Open(playerSave, FileMode.Open);
            string playerData = (string)this.binaryFormatter.Deserialize(file);
            Player p = (Player)Player.instance;

            if(p == null)
            {
                SceneController sc = (SceneController)SceneController.instance;
                sc.createGameObject();
                p = (Player)Player.instance;
            }

            Rigidbody2D r = p.rb;
            Animator anim = p.anim;
            AudioSource a1 = p.footstep;
            AudioSource a2 = p.jumpAudio;
            Sprite hd = p.header;

            JsonUtility.FromJsonOverwrite(playerData, p);

            p.rb = r;
            p.anim = anim;
            p.footstep = a1;
            p.jumpAudio = a2;
            p.header = hd;
            return true;
        }
        else
        {
            Debug.Log("文件不存在,无法读取.");
            return false;
        }
    }
}
