using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GamePlayScript : MonoBehaviour {
    public static GamePlayScript instance;
	public Text timeText, levelText, targetText, scoreText;
    public Text boomText;
    public Text scoreVictoryText, scoreFailText;
	public int score, scoreTarget;
	private int time;
    private float countDown;
    public GameObject panelMenu, panelVictory, panelFail, soundOnButton, soundMuteButton, musicOnButton, musicMuteButton;
    public Animator animPanelMenu, animPanelDark, animPanelPlay;
    public Button restartGame, restartFailPanel;
	//public GameObject []levelsVang;
	public int level;
	public bool endgame = false;

    public GameObject scoreTextFly, boomFly, boomObject, buttonNextLevel;
    public Transform canvas;


    public int numberBoom;
    public string itemSeclected;
    private GameObject itemDestroy, fireObject;

    private bool nextLevel = false;

    public AudioSource audioMusic;
    public AudioSource audioSound;
    public AudioClip pressButton, explosive, lowValue, normalValue, highValue, last10S, pull, lose, win;

    bool victory, fail, isPause;
    // Use this for initialization
    void Start () {

        score = PlayerPrefs.GetInt("MaxDollar");
        scoreText.text = "$" + score;
        MakeInstance();
		//startGame();
		level = 0;
		this.StartCoroutine("Do");
        
        levelText.text = "LEVEL " + CGameManager.instance.levelCurrent;
        scoreTarget = CGameManager.instance.GetScoreTarget(CGameManager.instance.levelCurrent);
        targetText.text = "$" + scoreTarget.ToString();
        //set clock
        if (CGameManager.instance.clock)
        {
            countDown = 76;
        }
        else
        {
            countDown = 61;
        }
        //sound and music
        SoundControl();
        MusicControl();
        SetButtonMusic();
        SetButtonSound();
        //show menu
        //StartCoroutine(ShowMenuPanel());
        SetNumberBoom();
    }
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            PauseGame();
        }

        
        if (score >= scoreTarget)
        {
            buttonNextLevel.SetActive(true);
        }
        countDown -= UnityEngine.Time.deltaTime;
        time = (int)countDown;
        if (time > 0)
        {
            timeText.text = time.ToString();
            if(time < 10)
            {
                if(victory || fail)
                {

                }
                else
                {
                    PlaySound(6);
                }
                
            }
        }
        else
        {
            if(score >= scoreTarget)
            {
                if (!victory)
                {
                    Victory();
                }
                
            }
            else
            {
                if (!fail)
                {
                    Fail();
                }

            }
        }
        
        
        //Debug.Log("Thoi gian con lai la: " + cd);
    }
    private void OnApplicationPause(bool pause)
    {

        //isPause = pause;
        //Debug.Log("Tam dung game: " + isPause);
        if (pause && !isPause)
        {
            if (victory && fail)
            {

            }
            else
            {
                PauseGame();
            }

        }
    }
    public void PlaySound(int i)
    {
        switch (i)
        {
            case 1:
                audioSound.PlayOneShot(lowValue);
                break;
            case 2:
                audioSound.PlayOneShot(normalValue);
                break;
            case 3:
                audioSound.PlayOneShot(highValue);
                break;
            case 4:
                audioSound.PlayOneShot(explosive);
                break;
            case 5:
                if (!audioSound.isPlaying)
                {
                    if (victory || fail)
                    {

                    }
                    else
                    {
                        audioSound.PlayOneShot(pull);
                    }
                    
                }   
                break;
            case 6:
                if (!audioSound.isPlaying)
                {
                    audioSound.PlayOneShot(last10S);
                }              
                break;
            case 7:
                audioSound.PlayOneShot(lose);
                break;
            case 8:
                audioSound.PlayOneShot(win);
                break;
            case 9:
                audioSound.PlayOneShot(pressButton);
                break;
        }
    }
    public void PauseGame()
    {
        isPause = true;
        audioSound.enabled = false;
        animPanelDark.SetBool("In", true);
        animPanelMenu.SetBool("In", true);
        animPanelMenu.SetBool("Out", false);
        UnityEngine.Time.timeScale = 0;
        restartGame.onClick.RemoveAllListeners();
        restartGame.onClick.AddListener(() => RestartGame());
 
    }

    public void ResumeGame()
    {
        isPause = false;
        audioSound.enabled = true;
        PlaySound(9);

        UnityEngine.Time.timeScale = 1;
        animPanelDark.SetBool("In", false);
        animPanelMenu.SetBool("Out", true);
        //StartCoroutine(MenuPanelOnTop());
    }

    public void Victory()
    {
        audioMusic.enabled = false;
        PlaySound(8);
        victory = true;
        UnityEngine.Time.timeScale = 0;
        panelVictory.SetActive(true);
        scoreVictoryText.text = "$" + score;
        PlayerPrefs.SetInt("MaxLevel", CGameManager.instance.levelCurrent);
        PlayerPrefs.SetInt("MaxDollar", score);
    } 
    public void Fail()
    {
        audioMusic.enabled = false;
        PlaySound(7);
        fail = true;
        UnityEngine.Time.timeScale = 0;
        panelFail.SetActive(true);
        scoreFailText.text = "$" + score;

        restartFailPanel.onClick.RemoveAllListeners();
        restartFailPanel.onClick.AddListener(() => RestartGame());
    }
    public void RestartGame()
    {
        UnityEngine.Time.timeScale = 1;
        CGameManager.instance.powerCurrent = false;
        Application.LoadLevel(Application.loadedLevel);
    }
    public void NextLevel()
    {
        CGameManager.instance.DisableItems();
        Application.LoadLevel("Shop"); 
        CGameManager.instance.levelCurrent++;
    }
    public void NextLevelAndSave()
    {
        PlayerPrefs.SetInt("MaxLevel", CGameManager.instance.levelCurrent);
        PlayerPrefs.SetInt("MaxDollar", score);
        CGameManager.instance.DisableItems();
        Application.LoadLevel("Shop");
        CGameManager.instance.levelCurrent++;
    }
    //IEnumerator ShowMenuPanel()
    //{
    //    yield return new WaitForSeconds(1f);
    //    panelMenu.SetActive(true);
    //}
    public void BackToMenu()
    {
        Application.LoadLevel("MainMenu");
    }
    public void Boom()
    {
        OngGiaScript.instance.DropBomb();
        itemDestroy = GameObject.Find(itemSeclected);
        Vector3 vector3 = itemDestroy.transform.localPosition;
        PlaySound(4);
        //Debug.Log("Cai dang duoc keo la co tag la: " + itemDestroy.tag);
        if(itemDestroy.tag == "Gold")
        {
            fireObject = Instantiate(Resources.Load("GoldFire"), vector3, Quaternion.identity) as GameObject;
            fireObject.transform.localScale = itemDestroy.transform.localScale;
        }
        else if(itemDestroy.tag == "Stone")
        {
            fireObject = Instantiate(Resources.Load("StoneFire"), vector3, Quaternion.identity) as GameObject;
            fireObject.transform.localScale = itemDestroy.transform.localScale;
        }
        else
        {
            fireObject = Instantiate(Resources.Load("OtherFire"), vector3, Quaternion.identity) as GameObject;
        }
        
        Destroy(itemDestroy);
        numberBoom = PlayerPrefs.GetInt("Bomb");
        numberBoom--;
        PlayerPrefs.SetInt("Bomb", numberBoom);
        SetNumberBoom();
        if(GameObject.Find("dayCau").GetComponent<DayCauScript>().typeAction == TypeAction.KeoCau)
        {
            LuoiCauScript.instance.speed = 4;
        }
        //GameObject.Find("dayCau").GetComponent<DayCauScript>().typeAction = TypeAction.Nghi;
    }
    public void Power()
    {
        CGameManager.instance.powerCurrent = true;
        OngGiaScript.instance.Happy();
    }
    public void SetNumberBoom()
    {
        if (PlayerPrefs.GetInt("Bomb") > 0)
        {
            boomObject.SetActive(true);
            boomText.text = PlayerPrefs.GetInt("Bomb").ToString();
        }
        else
        {
            boomObject.SetActive(false);
        }
    }
	public IEnumerator Do ()
	{
		bool add = true;
		while(add){
			yield return new WaitForSeconds (1);
			if(time > 0) {
				time --;
			}
			if(time <= 0 && !endgame) {
				//endGame();
//				StopCoroutine("Do");
			}
		}
	}

    //void endGame() {
    //	endgame = true;
    //	menuEndGame.SetActive(true);
    //	level ++;
    //}

    //void startGame()
    //{
    //    //menuEndGame.SetActive(false);
    //    endgame = false;
    //    time = 60;
    //    score = 0;
    //    for (int i = 0; i < levelsVang.Length; i++)
    //    {
    //        if (level == i)
    //        {
    //            levelsVang[i].SetActive(true);
    //        }
    //        else
    //        {
    //            levelsVang[i].SetActive(false);
    //        }
    //    }
    //}
    public void CreateScoreFly(int score)
    {
        Vector3 vector3 = scoreTextFly.transform.position;
        Instantiate(scoreTextFly, vector3, Quaternion.identity).transform.SetParent(canvas, false);
        TextScoreScript.score = score;
    }

    public void ScoreZoomEffect()
    {
        animPanelPlay.SetBool("Zoom", true);
        StartCoroutine(ScoreZoomOut());
    }
    IEnumerator ScoreZoomOut()
    {
        yield return new WaitForSeconds(1f);
        animPanelPlay.SetBool("Zoom", false);
    }
    public void CreateBoomFly()
    {
        Vector3 vector3 = boomFly.transform.position;
        Instantiate(boomFly, vector3, Quaternion.identity).transform.SetParent(canvas, false);
    }
	// Update is called once per frame
	
    public void SetScoreText()
    {
        scoreText.text = "$" + score.ToString();
    }
    //public void replay() {
    //	startGame();
    //}

    //Music and Sound Control
    void SoundControl()
    {
        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            audioSound.enabled = true;
        }
        else
        {
            audioSound.enabled = false;
        }
    }
    void MusicControl()
    {
        if (PlayerPrefs.GetInt("Music") == 1)
        {
            audioMusic.enabled = true;
        }
        else
        {
            audioMusic.enabled = false;
        }
    }
    public void SetOnMusic()
    {
        PlayerPrefs.SetInt("Music", 1);
        SetButtonMusic();
        MusicControl();
    }
    public void SetOnSound()
    {
        PlayerPrefs.SetInt("Sound", 1);
        SetButtonSound();
        SoundControl();
    }
    public void SetMuteMusic()
    {
        PlayerPrefs.SetInt("Music", 0);
        SetButtonMusic();
        MusicControl();
    }
    public void SetMuteSound()
    {
        PlayerPrefs.SetInt("Sound", 0);
        SetButtonSound();
        SoundControl();
    }
    private void SetButtonSound()
    {
        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            soundOnButton.SetActive(true);
            soundMuteButton.SetActive(false);
        }
        else
        {
            soundOnButton.SetActive(false);
            soundMuteButton.SetActive(true);
        }
    }
    private void SetButtonMusic()
    {
        if (PlayerPrefs.GetInt("Music") == 1)
        {
            musicOnButton.SetActive(true);
            musicMuteButton.SetActive(false);
        }
        else
        {
            musicMuteButton.SetActive(true);
            musicOnButton.SetActive(false);
        }
    }
}
