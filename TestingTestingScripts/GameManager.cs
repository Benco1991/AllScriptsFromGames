using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource music;
    [SerializeField]
    private AudioSource ambienceMusic;
    [SerializeField]
    private AudioClip[] musicClips;
    [SerializeField]
    private AudioClip[] ambienceClips;
    [SerializeField]
    private GameObject guidePanel;
    [SerializeField]
    private GameObject escPanel;

    public Image canvasBCK;
    private float startTime;
    private float endTime;
    private bool isTiming = false;
    private bool guideOn;
    private bool escPanelBool;
    public SpriteRenderer bckDefault;
    public MouseMovement player;
    private bool waitForClick = true;
    public LVL[] lvl;
    private int _actualLVL;
    public int ActualLVL { get { return _actualLVL; } set { _actualLVL = value; } }
    public BckLitMovement BckLitMovement;

    public GameObject canvasBestTime;
    public Text textBestTime;
    public Text latestTime;
    public Laser laser;
    private List<float> recordedTimes = new List<float>();

    public delegate void AudioClipDelegate(AudioClip clip);
    public static AudioClipDelegate PlayAudioClip;
    public static void TriggerAudioEvent(AudioClip clip)
    {
        if (PlayAudioClip != null)
        {
            PlayAudioClip.Invoke(clip);
        }
        Debug.Log("Trigger");
    }

    private void Start()
    {

        music = GetComponent<AudioSource>();
        music.clip = musicClips[0];
        music.Play();
        //RWStart(actualLVL);
        //BckLitMovement.StartMovement();
        ActualLVL = 0;
        GenerateLVL(lvl[ActualLVL], laser, player, false);

        StartCoroutine(ChangingAlpha());
        //Invoke("FindAllRW", 0.5f);
        //FindAllRW();
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (guideOn)
            {
                guideOn = false;
                guidePanel.SetActive(false);
            }
            else if (!escPanelBool)
            {
                escPanel.SetActive(true);
                escPanelBool = true;
            }
            else if (escPanelBool)
            {
                escPanel.SetActive(false);
                escPanelBool = false;
            }

        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (escPanelBool && !guideOn)
            {
                Application.Quit();
            }
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (escPanelBool)
            {
                guideOn = true;
                guidePanel.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ChangeMusic(1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ChangeMusic(2);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            ChangeMusic(3);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            ChangeMusic(4);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            ChangeMusic(5);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            ChangeMusic(6);
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            ChangeMusic(7);
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            ChangeMusic(8);
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            ChangeMusic(9);
        }
    }
    private void ChangeMusic(int numberOfClip)
    {
        music.Stop();
        music.clip = musicClips[numberOfClip -1];
        music.Play();
    }
    public void StartTiming()
    {
        startTime = Time.time;
        isTiming = true;
    }
    public void StopTiming()
    {
        if (isTiming)
        {
            if (ActualLVL < 2)
            {
                isTiming = false;
                startTime = 0;
                endTime = 0;
            }
            else
            {
                endTime = Time.time;
                isTiming = false;
                float finalTime = endTime - startTime;
                finalTime = Mathf.Round(finalTime * 100f) / 100f;
                recordedTimes.Add(finalTime);
                if (recordedTimes.Count > 0)
                {
                    recordedTimes.Sort();
                    float smallestTime = recordedTimes[0];
                    textBestTime.text = smallestTime.ToString();
                    latestTime.text = finalTime.ToString();
                }
            }
        }
    }
    void FindAllRW()
    {
        RedWall[] finding = FindObjectsOfType<RedWall>();
            foreach (RedWall found in finding)
            {
            found.StartAgain();
            }
    }
    private IEnumerator ChangingAlpha()
    {
        while (true)
        {
            Color tmp = bckDefault.GetComponent<SpriteRenderer>().color;
            tmp.a = 1f;
            bckDefault.GetComponent<SpriteRenderer>().color = tmp;
            yield return new WaitForSeconds(0.15f);
            tmp = bckDefault.GetComponent<SpriteRenderer>().color;
            tmp.a = 0f;
            bckDefault.GetComponent<SpriteRenderer>().color = tmp;
            yield return new WaitForSeconds(0.25f);//0.15f
        }
    }
    private IEnumerator ChangingCanvasBCKAlpha()
    {
        while (true)
        {
            Color tmp = canvasBCK.GetComponent<Image>().color;
            tmp.a = 1f;
            canvasBCK.GetComponent<Image>().color = tmp;
            yield return new WaitForSeconds(0.25f);
            tmp = canvasBCK.GetComponent<Image>().color;
            tmp.a = 0f;
            canvasBCK.GetComponent<Image>().color = tmp;
            yield return new WaitForSeconds(0.25f);//0.15f
        }
    }
    public void CheckScoreToLVL()
    { lvl[ActualLVL].CheckScore(1); }

    public void GenerateLVL(LVL lvl, Laser laser, MouseMovement player, bool restart)
    {
        if (laser != null && player != null)
        {
            ambienceMusic.Stop();
            ambienceMusic.clip = ambienceClips[0];
            ambienceMusic.Play();
            bckDefault.gameObject.SetActive(true);
            BckLitMovement.gameObject.SetActive(true);
            lvl.score = 0;
            if (canvasBestTime.gameObject.activeInHierarchy)
            { canvasBestTime.SetActive(false); }

            player.gameObject.layer = LayerMask.NameToLayer("Player");
            this.lvl[ActualLVL].gameObject.SetActive(true);//toto neviem ci mi nerobi bordel
            if (restart) FindAllRW();
            player.transform.gameObject.SetActive(true);
            this.player.moveSpeed = 40.0f;
            player.Col.radius = 1.5f;
            this.lvl[ActualLVL].numberOfLVL = NumberOfLVL.One;
            player.transform.position = Vector3.zero;
            player.mouseDetect = false;

            lvl.Initialize(4, BckLitMovement, this, 0, restart);
            //player.transform.position = Vector3.zero;
            //actualLVL++;
        }
        else if (lvl.maxScore == 4 && player != null)
        {
            ambienceMusic.Stop();
            ambienceMusic.clip = ambienceClips[1];
            ambienceMusic.Play();
            lvl.score = 0;
            player.transform.position = Vector3.zero;
            player.mouseDetect = false;
            player.Col.radius = 0.5f;


            //BckLitMovement.StartMovement();
            this.lvl[ActualLVL - 1].gameObject.SetActive(false);
            this.lvl[ActualLVL].gameObject.SetActive(true);
            DetectionMouse[] detection = FindObjectsOfType<DetectionMouse>();
            for (int i = 0; i < detection.Length; i++)
                detection[i].Col.enabled = false;
            this.laser.StartLaserMovement();
            //this.lvl[actualLVL].numberOfLVL = NumberOfLVL.Two;
            this.lvl[ActualLVL].Initialize(12, BckLitMovement, this, 0, restart);


            //actualLVL++;
        }
        else if (lvl.numberOfLVL == NumberOfLVL.Two)//inicializacia lvl3
        {
            ambienceMusic.Stop();
            lvl.score = 0;
            this.player.transform.position = Vector3.zero;
            this.player.mouseDetect = false;

            this.player.moveSpeed *= 1.5f;
            //BckLitMovement.StartMovement();
            this.lvl[_actualLVL -1].gameObject.SetActive(false);
            this.lvl[_actualLVL].gameObject.SetActive(true);
            //this.lvl[actualLVL].numberOfLVL = NumberOfLVL.Three;
            this.lvl[_actualLVL].Initialize(8, BckLitMovement, this, 0, restart);

            //actualLVL++;
        }
        else if (lvl.maxScore == 8)//koniec+skore
        {
            SpawnEffect(2, 3);
            bckDefault.gameObject.SetActive(false);
            BckLitMovement.gameObject.SetActive(false);
            lvl.score = 0;
            //BckLitMovement.StartMovement();
            //this.lvl[actualLVL].Initialize(8, BckLitMovement, this, 0);
            this.lvl[ActualLVL - 1].gameObject.SetActive(false);
            //BckLitMovement.StartMovement();
            canvasBestTime.SetActive(true);
            StartCoroutine(ChangingCanvasBCKAlpha());
            StartWaitingOnClick();

        }
    }
    void SpawnEffect(int number, float duration)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.clip = ambienceClips[number];
            audioSource.Play();
            Destroy(audioSource, duration);
        }
    }
    private void StartWaitingOnClick()
    {
        StartCoroutine(WaitForMouseClick());
    }
    private IEnumerator WaitForMouseClick()
    {
        while (waitForClick)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Click");
                waitForClick = false;
                //BckLitMovement.StartMovement();
            }
            yield return null;

        }
        canvasBestTime.SetActive(false);
        WaitToRestart();
        waitForClick = true;
    }
    public void WaitToRestart()
    {
        SpawnEffect(3, 0.5f);
        StopTiming();
        StartCoroutine(Restarting());
    }
    private IEnumerator Restarting()
    {
        for(int i = 0; i < lvl.Length; i++)
        {
            lvl[i].gameObject.SetActive(false);
            lvl[i].score = 0;
        }
        player.transform.gameObject.SetActive(false);

        yield return new WaitForSeconds(1);

        Restart();
    }
    private void Restart()
    {
        ActualLVL = 0;
        GenerateLVL(lvl[ActualLVL], laser, player, true);
    }

}
