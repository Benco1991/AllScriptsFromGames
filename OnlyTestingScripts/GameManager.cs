using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject badKarmaEffect;

    public AudioSource bckMusic;
    public AudioSource badKarmaSound;
    public AudioClip badKarmaSoundClip;
    public AudioSource shiftSound;
    public AudioClip shiftSoundClip;
    public Text looseScoreText;
    public AudioSource escSound;
    public AudioClip escSoundClip;
    public AudioSource explosion;
    public AudioSource click;

    public GameObject escPanel;
    public GameObject guidePanel;
    public GameObject godMode;
    public GameObject pressShift;

    private bool gameOver;
    public int bkScore;
    public int gkScore;
    public SpriteRenderer[] BKscoreSprites;
    public SpriteRenderer[] GKscoreSprites;
    private bool escPanelBool;

    public PeopleBehaviour peoplePrefab;
    public Hero hero;
    public PeopleBehaviour people;
    public float enemyTimer;
    private float enemyCountDown;
    public int timeShift = 5;

    public GameObject shiftEffect;
    private int numberOfLoose;
    private bool guideOn;


    void Start()
    {
        StartCoroutine("Flashing");
        ResetState();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(guideOn)
            {
                guideOn = false;
                guidePanel.SetActive(false);
                Time.timeScale = 1f;
                bckMusic.Play();
                escSound.Play();
            }
            else if(!escPanelBool)
            {
                Time.timeScale = 0f;
                bckMusic.Pause();
                escPanel.SetActive(true);
                looseScoreText.text = numberOfLoose.ToString();
                escPanelBool = true;
                escSound.Play();
            }
            else if (escPanelBool)
            {
                Time.timeScale = 1f;
                bckMusic.Play();
                escSound.Play();
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (escPanelBool)
            {
                ESCReset();
                escPanel.SetActive(true);
                Time.timeScale = 1f;
                bckMusic.Play();
                escSound.Play();
                escPanel.SetActive(false);
                escPanelBool = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (escPanelBool)
            {
                guideOn = true;
                guidePanel.SetActive(true);
                Time.timeScale = 0f;
                bckMusic.Pause();
                escSound.Play();
                escPanel.SetActive(false);
                escPanelBool = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && timeShift == 5)
        {
            shiftSound.Play();
            GameObject shift = GameObject.Instantiate(shiftEffect);
            pressShift.SetActive(false);
            godMode.SetActive(true);
            Camera.main.orthographicSize = 12;
            Camera.main.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-25, 25));
            Invoke("CameraZoom", 0.25f);
            Destroy(shift, 0.5f);
            Debug.Log("Shift");
            StartCoroutine(CountDown(5));
            StartShiftTime(0.5f);
            hero.movement.ChangeSpeed(10, true);
        }


        enemyTimer -= Time.deltaTime;
        if(enemyTimer <= 0 )
        {
            Spawning();
            enemyTimer = enemyCountDown;
            enemyCountDown *= 0.95f;
            if (enemyCountDown < 1)
            { 
                enemyCountDown = 1;
            }
        }
    }

     //--------------------------------------------------------//
    //--------------------------------------------------------//
   //--------------------------------------------------------//


    private void CameraZoom()
    {
        Camera.main.orthographicSize = 16;
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void KarmaMeter(int karma)
    {
        if (!gameOver && karma > 0)
        {
            gkScore += karma;
        }
        else if (!gameOver && karma < 0)
        {
            if (!badKarmaEffect.activeInHierarchy) { 
                badKarmaEffect.SetActive(true);
                StartCoroutine(DeaktivateEffects(1, badKarmaEffect));
            }

            if (gkScore > 0)
            {
                gkScore--;
            }
            else bkScore += Mathf.Abs(karma);

            if (gkScore < 0)
                gkScore = 0;

            if (gkScore > 5)
                gkScore = 5;
        }

        SetKarmaSprites();

        if (bkScore >= 5)
        {
            bkScore = 0;
            gkScore = 0;
            gameOver = true;
            numberOfLoose++;
            ResetState();
            SetKarmaSprites();
        }
    }
    private void ESCReset()
    {
        bkScore = 0;
        gkScore = 0;
        gameOver = true;
        numberOfLoose++;
        ResetState();
        SetKarmaSprites();
    }
    IEnumerator Flashing()
    {
        foreach (SpriteRenderer badKarma in BKscoreSprites)
        {
            SpriteRenderer spriteRendererBK = badKarma.GetComponent<SpriteRenderer>();
            if (spriteRendererBK != null && spriteRendererBK.color.a > 0.95f)
            {
                spriteRendererBK.color = new Color(1, 0, 0, 0.5f);
            }
            else if (spriteRendererBK != null && spriteRendererBK.color.a > 0.15f && spriteRendererBK.color.a < 0.55f )
            {
                spriteRendererBK.color = new Color(1, 0, 0, 1f);
            }
        }
        foreach (SpriteRenderer goodKarma in GKscoreSprites)
        {
            SpriteRenderer spriteRendererGK = goodKarma.GetComponent<SpriteRenderer>();
            if (spriteRendererGK != null && spriteRendererGK.color.a > 0.95f)
            {
                spriteRendererGK.color = new Color(1, 1, 1, 0.5f);
            }
            else if (spriteRendererGK != null && spriteRendererGK.color.a > 0.15f && spriteRendererGK.color.a < 0.55f)
            {
                spriteRendererGK.color = new Color(1, 1, 1, 1f);
            }
        }
        yield return new WaitForSeconds(0.25f);
        StartCoroutine("Flashing");
    }
    private void SetKarmaSprites()
    {
        for (int i = 0; i < bkScore; i++)
        {
            BKscoreSprites[i].color = new Color(1, 0, 0, 1f);
        }
        for (int i = BKscoreSprites.Length - 1; i > bkScore; i--)
        {
            BKscoreSprites[i].color = new Color(1, 0, 0, 0.1f);
        }

        for (int i = 0; i < gkScore; i++)
        {
            if(i == 4)
            {
                break;
            }
            GKscoreSprites[i].color = new Color(1, 1, 1, 1f);
        }

        for (int i = GKscoreSprites.Length - 1; i > gkScore; i--)
        {
            GKscoreSprites[i].color = new Color(1, 1, 1, 0.1f);
        }
    }

    private void StartShiftTime(float speed)
    {
        foreach (Transform children in transform)
        {
            PeopleBehaviour childMovement = children.GetComponentInChildren<PeopleBehaviour>();
            if (childMovement != null)
            {
                childMovement.movement.ChangeSpeed(speed, false);
            }
        }
        
    }
    public void RotCourutine()
    {
        StartCoroutine("CameraRotCountDown", 15);
        explosion.Play();
    }
    IEnumerator CameraRotCountDown(float frequency)
    {
        float count = frequency;

        while (count > 0)
        {

            count -= 10 * Time.deltaTime;
            float rot = count;
            Camera.main.transform.rotation = Quaternion.Euler(0, Random.Range(-rot, rot), Random.Range(-rot, rot));
            yield return null;

        }
    }
    IEnumerator CountDown(int seconds)
    {
        timeShift = 0;
        int count = seconds;

        while (count > 0)
        {
            yield return new WaitForSeconds(1);
            count--;
        }

        timeShift = 5;
        godMode.SetActive(false);
        pressShift.SetActive(true);

    }
    IEnumerator DeaktivateEffects(int seconds, GameObject go)
    {
        yield return new WaitForSeconds(seconds);

        go.SetActive(false);
    }

    private void ResetState ()
    {

        SetKarmaSprites();
        gameOver = false;

        foreach(Transform children in transform)
        {
            Destroy(children.gameObject);
        }
        enemyCountDown = 3;
        enemyTimer = enemyCountDown;
        bkScore = 0;
        gkScore = 0;
    }

    private void Spawning()
    {
        List<Transform> listOfAllChild = new List<Transform>();
        foreach (Transform children in transform)
        {
            listOfAllChild.Add(children);
        }
        if(listOfAllChild.Count < 11)
        {
            PeopleBehaviour peopleSpawned = Instantiate(peoplePrefab, transform.position, Quaternion.identity);
            peopleSpawned.transform.parent = transform;
        }
    }
}
