using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class SpawnPosition
{
    public string sceneName;
    public float x;
    public float y;

    public SpawnPosition(string sceneName, float x, float y)
    {
        this.sceneName = sceneName;
        this.x = x;
        this.y = y;
    }
}

public class Spawn : Event
{
    public string nowSceneName;
    public SpawnPosition toSpawn;
    public bool inEnterArea;
    private void Awake()
    {
        this.press = "按下";
        this.act = "进入";
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void ActivateEvent()
    {
        SceneController scl = (SceneController)SceneController.instance;
        scl.ChangeScene(this.toSpawn);
    }
}
