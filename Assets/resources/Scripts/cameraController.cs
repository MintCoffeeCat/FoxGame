using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : Singleton<cameraController>
{
    public GameObject camera;
    public Cinemachine.CinemachineVirtualCamera machine;
    public Cinemachine.CinemachineConfiner confiner;
    // Update is called once per frame
    private void Awake()
    {
        base.Awake();
        this.machine.Follow = ((Player)Player.instance).gameObject.transform;

    }
}
