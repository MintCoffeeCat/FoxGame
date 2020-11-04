using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour
{
    public static Singleton<T> instance;

    protected void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
