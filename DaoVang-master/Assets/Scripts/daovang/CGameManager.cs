using UnityEngine;
using System.Collections;
public enum TypeAction {Nghi, ThaCau, KeoCau};
public enum EnumFishAction {Boi, CanCau, DopMoi, NhayVaoGio};
public enum EnumStateGame {Play, Pause, Win, Lose, Menu};

public delegate void OnStateChangeHandler();

public class CGameManager : MonoBehaviour {
    public static CGameManager instance;

    public int music = 1, sound = 1;
    private const string MUSIC = "Music", SOUND = "Sound";

    //public int[] scoreLevels;
	public float maxX;
	public float minX;
	public float maxY;
	public float minY;
	public string keyLevelNow = "levelNow";
	public string keyLevelMax = "levelMax";
	public string keyNumberLevel = "numberLevel";

    //public int[] scoreTarget = new int[60];

//	private static CGameManager _instance = null;    
	public event OnStateChangeHandler OnStateChange;
	public EnumStateGame gameState { get; private set; }
	public int score { get; private set; }
	public int level { get; private set; }
	public int maxScore { get; private set; }
	public int timePlay { get; private set; }
	public int typeLuoiCau { get; private set; }

    public int levelCurrent;

    public bool power, bookStone, clover, diamond, clock, powerCurrent;
	//public static CGameManager Instance { get; private set; }
	

	private void Awake() {
        DisableItems();
        //scoreTarget = new int[60];
        //PlayerPrefs.SetInt("MaxLevel", 20);
        //PlayerPrefs.SetInt("MaxDollar", 36500);
        IsGameStartedForTheFirstTime();
        //PlayerPrefs.DeleteAll();
        //Debug.Log("Khoi tao Manager " + gameObject.name);
        MakeSingleInstance();
        music = GetMusic();
        sound = GetSound();

        //SetScoreTarget();
        int maxLevel = GetMaxLevel();
        if(maxLevel == 0)
        {
            levelCurrent = 1;
        }
        else
        {
            levelCurrent = maxLevel;
        }
        //if (Instance != null) {
        //	DestroyImmediate(gameObject);
        //	return;
        //}
        //		if(PlayerPrefs.GetInt(keyLevelNow) == 0) {
        //			PlayerPrefs.SetInt(keyLevelNow, 1);
        //		}
        //		if(PlayerPrefs.GetInt(keyLevelMax) == 0) {
        //			PlayerPrefs.SetInt(keyLevelMax, 1);
        //		}
        //		if(PlayerPrefs.GetInt(keyNumberLevel) == 0) {
        //			PlayerPrefs.SetInt(keyNumberLevel, 10);
        //		}
        //Instance = this;
        //DontDestroyOnLoad(gameObject);
    }
    void MakeSingleInstance()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    //	protected CGameManager() {}
    //
    //	// Singleton pattern implementation
    //	public static CGameManager Instance { 
    //		get {
    //			if (_instance == null) {
    //				_instance = new CGameManager(); 
    //			}  
    //			return _instance;
    //		} 
    //	}
    public int GetScoreTarget(int level)
    {
        if (level == 1)
        {
            return 800;
        }
        else if (level == 2)
        {
            return 2050;
        }
        else
        {
            return 800 + (level - 1) * 1250 + (level - 2) * 500;
        }
    }
    //void SetScoreTarget()
    //{
    //    scoreTarget[0] = 0;
    //    scoreTarget[1] = 800;
    //    scoreTarget[2] = 2050;
    //    scoreTarget[3] = 3795;
    //    scoreTarget[4] = 5500;
    //    scoreTarget[5] = 7050;
    //    scoreTarget[6] = 8575;
    //    scoreTarget[7] = 10195;
    //    scoreTarget[8] = 12270;
    //    scoreTarget[9] = 14095;
    //    scoreTarget[10] = 16995;
    //    scoreTarget[11] = 18695;
    //    scoreTarget[12] = 20404;
    //    scoreTarget[13] = 22107;
    //    scoreTarget[14] = 25657;
    //    scoreTarget[15] = 27447;
    //    scoreTarget[16] = 29447;
    //    scoreTarget[17] = 31647;
    //    scoreTarget[18] = 33750;
    //    scoreTarget[19] = 31600;
    //    scoreTarget[20] = 35800;
    //    scoreTarget[21] = 38100;
    //    scoreTarget[22] = 40000;
    //    scoreTarget[23] = 42300;
    //    scoreTarget[24] = 44700;
    //    scoreTarget[25] = 47200;
    //    scoreTarget[26] = 49600;
    //    scoreTarget[27] = 52200;
    //    scoreTarget[28] = 54900;
    //    scoreTarget[29] = 57700;
    //    scoreTarget[30] = 60200;
    //    scoreTarget[31] = 62800;
    //    scoreTarget[32] = 65700;
    //    scoreTarget[33] = 68500;
    //    scoreTarget[34] = 71500;
    //    scoreTarget[35] = 74510;
    //    scoreTarget[36] = 77460;
    //    scoreTarget[37] = 80430;
    //    scoreTarget[38] = 83430;
    //    scoreTarget[39] = 86630;
    //    scoreTarget[40] = 90330;
    //}
	void Update() {

	}
    public void DisableItems()
    {
        //power, bookStone, clover, diamond, clock;
        power = false;
        bookStone = false;
        clover = false;
        diamond = false;
        clock = false;
        powerCurrent = false;
    }
    public void SetGameState(EnumStateGame gameState) {
		this.gameState = gameState;
		if(OnStateChange!=null) {
			OnStateChange();
		}
	}

	public void SetScore(int newScore) {
		this.score = newScore;
	}


	public void SetTimePlay(int newTime) {
		this.timePlay = newTime;
	}

	public void SetTypeLuoiCau(int type) {
		this.typeLuoiCau = type;
	} 

    void IsGameStartedForTheFirstTime()
    {
        if (!PlayerPrefs.HasKey("IsGameStartedForTheFirstTime"))
        {
            PlayerPrefs.SetInt("Bomb", 0);
            PlayerPrefs.SetInt("MaxLevel", 0);
            PlayerPrefs.SetInt("MaxDollar", 0);
            PlayerPrefs.SetInt(MUSIC, 1);
            PlayerPrefs.SetInt(SOUND, 1);

            PlayerPrefs.SetInt("IsGameStartedForTheFirstTime", 0);

        }
    }
    // get set max level
    public void SetMaxLevel(int maxLevel)
    {
        PlayerPrefs.SetInt("MaxLevel", maxLevel);
    }
    public int GetMaxLevel()
    {
        return PlayerPrefs.GetInt("MaxLevel");
    }
    
    // get set max Score
    public void SetMaxScore(int maxScore)
    {
        PlayerPrefs.SetInt("MaxDollar", maxScore);
    }
    public int GetMaxScore()
    {
        return PlayerPrefs.GetInt("MaxDollar");
    }

    // get set music background
    public void SetMusic(int music)
    {
        PlayerPrefs.SetInt(MUSIC, music);
    }
    public int GetMusic()
    {
        return PlayerPrefs.GetInt(MUSIC);
    }
    // get set sound effect
    public void SetSound(int sound)
    {
        PlayerPrefs.SetInt(SOUND, sound);
    }
    public int GetSound()
    {
        return PlayerPrefs.GetInt(SOUND);
    }
}
