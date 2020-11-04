
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    public CoverController cover;
    public SpawnPosition to;

    public GameObject background;
    public bool isNewGame;
    public bool isLoadGame;
    public void ChangeScene(SpawnPosition to)
    {
        cover.SetActive();
        cover.SetShow();
        this.to = to;
        StartCoroutine(changeSceneAsyc());
    }
    public void ContinueGame()
    {
        cover.SetActive();
        cover.SetShow();
        Player p = (Player)Player.instance;
        this.to = new SpawnPosition(p.sceneName, p.X, p.Y);
        p.resetUI();
        StartCoroutine(changeSceneAsyc());
    }

    public void createGameObject()
    {
        Object player = Resources.Load("prefabs/player");
        Object camera = Resources.Load("prefabs/Camera");
        Object pool = Resources.Load("prefabs/ObjectPool");
        Object ui = Resources.Load("prefabs/UI");
        Instantiate(player);
        GameObject cmr = (GameObject)Instantiate(camera);
        Instantiate(pool);
        Instantiate(ui);
    }

    IEnumerator changeSceneAsyc()
    {
        Player.instance.gameObject.SetActive(false);
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("Loading");

        loadOperation.allowSceneActivation = false;

        FileStream f = File.Create("errorLog.txt");
        byte[] content = System.Text.Encoding.UTF8.GetBytes("正在读取场景...\n");
        f.Write(content, 0, content.Length);
        f.Close();
        while (!loadOperation.isDone)
        {
            if (loadOperation.progress >= 0.9f)
            {
                while (!cover.isCoverComplete)
                {
                    yield return null;
                }
                cover.SetHide();
                loadOperation.allowSceneActivation = true;
                AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(to.sceneName);
                loadSceneOperation.allowSceneActivation = false;

                while (!loadSceneOperation.isDone)
                {
                    if (loadSceneOperation.progress >= 0.9f)
                    {
                        while (this.cover.isCoverComplete)
                        {
                            yield return null;
                        }
                        this.cover.SetActive();
                        this.cover.SetShow();
                        while (!this.cover.isCoverComplete)
                        {
                            yield return null;
                        }
                        loadSceneOperation.allowSceneActivation = true;
                        Player p = (Player)Player.instance;
                        p.Spawn(to.x, to.y, to.sceneName);
                        this.cover.SetHide();
                        Player.instance.gameObject.SetActive(true);
                        Player.isInit = true;
                    }
                    yield return null;
                }
            }
            yield return null;
        }
    }


}
