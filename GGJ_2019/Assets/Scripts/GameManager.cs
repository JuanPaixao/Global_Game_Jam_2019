using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UiManager uiManager;
    public int grassCounter, rockCounter;
    public GameObject[] lights;
    public GameObject rockWal, fadeGameObject, littleBear, hugeBear, water, cam1, cam2, whaleTrigger;
    public SpriteRenderer fade;
    public Tilemap tile;
    public bool helpedBear, helpedWhale, helpedCow;
    private Scene sceneName;
    public GameObject[] animalsText;
    public Player player;

    public void LoadNextScene(string Scene)
    {
        SceneManager.LoadSceneAsync(Scene);
    }
    void Start()
    {
        sceneName = SceneManager.GetActiveScene();
    }
    void Update()
    {
        if (sceneName.name == "Menu")
        {
            if (Input.anyKeyDown)
            {
                LoadNextScene("Intro");
            }
        }
          if (sceneName.name == "Intro")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                LoadNextScene("Stage_01");
            }
        }
        if (sceneName.name == "Ending")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StartCoroutine(GoToMenu());
            }
        }

        if (helpedBear)
        {
            animalsText[0].SetActive(false);
            player.helpedBear = true;
        }
        else if (helpedCow)
        {
            animalsText[1].SetActive(false);
        }
        else if (helpedWhale)
        {
            animalsText[2].SetActive(false);
        }
    }
    public void Active(int i)
    {
        uiManager.Active(i);
    }
    public void Deactive(int i)
    {
        uiManager.Deactive(i);
    }
    public void IncreaseRockCounter(int i)
    {
        uiManager.counter.text = i.ToString();
    }
    public void IncreaseGrassCounter()
    {
        grassCounter++;
        if (grassCounter >= 4)
        {
            lights[0].SetActive(true);
            helpedCow = true;
        }
    }
    public void BuildRockWall()
    {
        fadeGameObject.SetActive(true);
        StartCoroutine(FadeRoutine());
    }

    public IEnumerator FadeRoutine()
    {
        yield return new WaitForSecondsRealtime(0.75f);
        rockWal.SetActive(true);
    }
    public void BearMissonComplete()
    {
        lights[1].SetActive(true);
        littleBear.SetActive(true);
        helpedBear = true;
        uiManager.Deactive(3);
    }
    public void WhaleMissionComplete()
    {
        StartCoroutine(WhaleMission());
        lights[2].SetActive(true);

    }
    public void DeactiveBear()
    {
        water.SetActive(true);
        cam2.SetActive(false);
        cam1.SetActive(true);
        hugeBear.SetActive(false);
        water.SetActive(true);
        helpedWhale = true;
        water.SetActive(true);
    }
    public IEnumerator WhaleMission()
    {
        if (whaleTrigger != null)
        {
            whaleTrigger.SetActive(false);
            yield return new WaitForSecondsRealtime(1f);
            StartCoroutine(WhaleMissionPart2());
        }
    }
    public IEnumerator WhaleMissionPart2()
    {
        fadeGameObject.SetActive(true);
        hugeBear.SetActive(true);
        cam1.SetActive(false);
        cam2.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        fadeGameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        DeactiveBear();
    }
    void FixedUpdate()
    {
        if (helpedWhale && helpedCow && helpedBear)
        {
            Debug.Log("Game Over");
            StartCoroutine(GoToEnding());
        }
    }
    public void DestroyWall()
    {
        tile.SetTile(new Vector3Int(40, -2, 0), null);
        tile.SetTile(new Vector3Int(40, -3, 0), null);
    }
    private IEnumerator GoToMenu()
    {
        yield return new WaitForSecondsRealtime(20f);
        LoadNextScene("Menu");
    }
    private IEnumerator GoToEnding()
    {
        yield return new WaitForSecondsRealtime(5f);
        LoadNextScene("Ending");
    }
}
