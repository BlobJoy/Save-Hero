using UnityEngine;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public Level currentLevel;

    [HideInInspector]
    public int levelIndex;

    public int maxLevel;

    public static GameController instance;

    public GameObject drawManager;

    public UIManager uiManager;

    public Sprite[] phoneList;

    public GameObject BackGround;

    public AdManager _adManager;

    public enum STATE
    {
        DRAWING,
        PLAYING,
        FINISH,
        GAMEOVER
    }

    public STATE currentState;

    private void Awake()
    {
        print(gameObject.name);
        instance = this;
        currentState = STATE.DRAWING;
        levelIndex = PlayerPrefs.GetInt("CurrentLevel");
        CreateLevel();
        Application.targetFrameRate = 60;
        CheckForAd();

    }

    public void CheckForAd()
    {
        int rounds = PlayerPrefs.GetInt("TimeForAd", 0);
        if(rounds>=3)
        {
            _adManager.ShowInterstitialAd();
            PlayerPrefs.SetInt("TimeForAd", 0);
        }
    }

    private void Start()
    {
        BackGround.GetComponent<SpriteRenderer>().sprite = phoneList[Random.Range(0, phoneList.Length)];
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && currentState == STATE.DRAWING)
        {
            currentState = STATE.PLAYING;
            ActiveDog();
            StartWater();
            uiManager.ActiveClock();
        }
           
    }

    private void ActiveDog()
    {
        for(int i = 0; i < currentLevel.dogList.Count; i++)
        {
            currentLevel.dogList[i].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void StartWater()
    {
        if(currentLevel.WaterDrop.Count>0)
        {
            for(int i=0;i<currentLevel.WaterDrop.Count;i++)
            {
                currentLevel.WaterDrop[i].GetComponent<DropSpawner>().StartSpawning(0);
            }
        }
    }

    private void CreateLevel()
    {
        if(levelIndex >= maxLevel)
        {
            levelIndex = levelIndex - maxLevel;
        }

        GameObject levelObj = Instantiate(Resources.Load("Levels/Level" + (levelIndex + 1).ToString())) as GameObject;
        currentLevel = levelObj.GetComponent<Level>();
    }
}
