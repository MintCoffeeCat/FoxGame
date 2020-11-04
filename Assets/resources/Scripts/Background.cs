using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private Transform pictures;
    [SerializeField]
    private Collider2D col;

    private Transform camera;


    private void Awake()
    {
        cameraController cmr = (cameraController)cameraController.instance;
        this.camera = cmr.camera.transform;
        if(col != null)
        {
            cmr.confiner.m_BoundingShape2D = col;
        }
    }
    void Update()
    {
        if(this.pictures == null)
        {
            Debug.Log("背景图片缺失,将取消背景图片的跟随");
        }
        this.pictures.position = new Vector2(this.camera.position.x, this.camera.position.y);
    }
}
