using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {


    public GameObject buttonContinue, soundOnButton, soundMuteButton, musicOnButton, musicMuteButton;
    public Animator animPanelSetting;
    public AudioSource audioMusic;
    public AudioSource audioSound;

    public AudioClip pressButton;
    // toast and double click exit game
    bool doubleBackToExitPressedOnce = false;
    string toastString;
    string input;
    AndroidJavaObject currentActivity;
    AndroidJavaClass UnityPlayer;
    AndroidJavaObject context;

    // Use this for initialization
    void Start () {
        UnityEngine.Time.timeScale = 1;
        CGameManager.instance.DisableItems();

        SoundControl();
        MusicControl();
        SetButtonMusic();
        SetButtonSound();

        if (PlayerPrefs.GetInt("MaxLevel") != 0)
        {
            buttonContinue.SetActive(true);
        }
        else
        {
            buttonContinue.SetActive(false);
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
        }
    }
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKey(KeyCode.Escape))
        //{
        //    Application.Quit();
        //}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (doubleBackToExitPressedOnce)
            {
                Application.Quit();
            }
            doubleBackToExitPressedOnce = true;

            showToastOnUiThread("Please click BACK again to exit");

            StartCoroutine(DoubleClickExit());

        }
    }
    IEnumerator DoubleClickExit()
    {
        yield return new WaitForSeconds(1.5f);
        doubleBackToExitPressedOnce = false;
    }


    public void showToastOnUiThread(string toastString)
    {
        this.toastString = toastString;
        currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(showToast));
    }

    void showToast()
    {
        Debug.Log(this + ": Running on UI thread");

        AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
        AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", toastString);
        AndroidJavaObject toast = Toast.CallStatic<AndroidJavaObject>("makeText", context, javaString, Toast.GetStatic<int>("LENGTH_SHORT"));
        toast.Call("show");
    }
    public void Play()
    {
        audioSound.PlayOneShot(pressButton);
        CGameManager.instance.levelCurrent = 1;
        PlayerPrefs.SetInt("Bomb", 0);
        PlayerPrefs.SetInt("MaxLevel", 0);
        PlayerPrefs.SetInt("MaxDollar", 0);
        Application.LoadLevel("NextLevel");
        
    }
    public void PlayContinue()
    {
        audioSound.PlayOneShot(pressButton);
        int maxLevel = PlayerPrefs.GetInt("MaxLevel");
        if(maxLevel == 0)
        {
            CGameManager.instance.levelCurrent = 1;
            Play();
        }
        else
        {
            CGameManager.instance.levelCurrent = maxLevel + 1;
            Application.LoadLevel("Shop");
        }
    }
    
    public void Setting()
    {
        audioSound.PlayOneShot(pressButton);
        animPanelSetting.SetBool("In", true);
        animPanelSetting.SetBool("Out", false);
    }
    public void ExitSetting()
    {
        audioSound.PlayOneShot(pressButton);
        animPanelSetting.SetBool("Out", true);
    }
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
