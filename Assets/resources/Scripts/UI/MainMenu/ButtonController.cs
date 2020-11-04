using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    public MyButton newGameButton;
    public MyButton loadGameButton;
    public MyButton quitGameButton;

    public void NewGame()
    {
        SceneController sc = (SceneController)SceneController.instance;
        sc.ChangeScene(new SpawnPosition("FirstPlain", -30, -2));
    }
}
