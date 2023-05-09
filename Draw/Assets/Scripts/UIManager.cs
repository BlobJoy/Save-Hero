using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{

    public GameObject clock, fail, complete, winPanel,failPanel;

    private float timer;

    public float timerMax;

    public TextMeshProUGUI clockText, levelText;

    private bool startClock;

    private void Awake()
    {
        startClock = false;
        timer = timerMax;
    }

    void Start()
    {
        levelText.text = "LEVEL " + (GameController.instance.levelIndex + 1).ToString();
    }

    void Update()
    {
        if (startClock)
        {
            if(timer > 0.0f)
            {
                timer -= Time.deltaTime;
                clockText.text = Mathf.CeilToInt(timer).ToString();
            }

            else
            {
                startClock = false;
                clock.SetActive(false);
                ShowResult();
            }

            if (GameController.instance.currentState == GameController.STATE.GAMEOVER)
            {
                startClock = false;
                AudioManager.instance.failAudio.Play();
                clock.SetActive(false);
                StartCoroutine(ShowGameOverIE());
            }
        }
    }

    private void ShowResult()
    {
        if(GameController.instance.currentState == GameController.STATE.GAMEOVER)
        {
            AudioManager.instance.failAudio.Play();
            StartCoroutine(ShowGameOverIE());
        }
        else
        {
            AudioManager.instance.winAudio.Play();
            GameController.instance.currentState = GameController.STATE.FINISH;
            StartCoroutine(ShowGameWinIE());
        }
    }

    public void ActiveClock()
    {
        clock.SetActive(true);
        startClock = true;
    }

    public void Replay()
    {
        PlayerPrefs.SetInt("CurrentLevel", GameController.instance.levelIndex);
        SceneManager.LoadScene("Level");
    }

    public void NextLevel()
    {
        GameController.instance.levelIndex++;
        PlayerPrefs.SetInt("CurrentLevel", GameController.instance.levelIndex);
        SceneManager.LoadScene("Level");
    }

    public void Home()
    {
        SceneManager.LoadScene("Home");
    }

    IEnumerator ShowGameOverIE()
    {
        fail.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        failPanel.SetActive(true);
        int roundsCount = PlayerPrefs.GetInt("TimeForAd", 0);
        roundsCount++;
        PlayerPrefs.SetInt("TimeForAd", roundsCount);
        this.gameObject.GetComponent<GameController>().CheckForAd();
        //AdsControl.Instance.ShowInterstitalRandom();
    }

    IEnumerator ShowGameWinIE()
    {
        int levelUnlock = PlayerPrefs.GetInt("UnlockLevel");
        levelUnlock++;
        PlayerPrefs.SetInt("UnlockLevel", levelUnlock);
        complete.SetActive(true);

        yield return new WaitForSeconds(1.0f);
        winPanel.SetActive(true);
        int roundsCount = PlayerPrefs.GetInt("TimeForAd", 0);
        roundsCount++;
        PlayerPrefs.SetInt("TimeForAd", roundsCount);
        this.gameObject.GetComponent<GameController>().CheckForAd();
        //AdsControl.Instance.ShowInterstitalRandom();
    }
}
