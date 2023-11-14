using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;

    public HealtHeartBar heartBar;
    private Player player;
    private Spawner spawner;

    public TMP_Text scoreText;
    public GameObject playButton;

    public SphereCollider playerCollider;
    public Animator animator;
    public int score { get; private set; }
    private bool isPlaying = false;
    public Scrollbar yourScrollbar;
    public Image handler;
    public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    public Sprite image4;
    public int MaxBarM;
    public int winscore;

    public GameObject Lift;
    public GameObject Right; 
    public GameObject Up;

    public Spawner S;
    public Spawner1 S1;
    public SpawnerRow SR;
    public SpawnerRow1 SR1;

    public AudioSource hitSound;
    public AudioSource notplayBGSound;
    public AudioSource UnderWaterPlayed;

    public GameObject ScrollB;
    public GameObject GameName;
    public GameObject GameOver;
    public GameObject GameWin;
    public GameObject heart;
    public GameObject Mtext;

    public static bool IsImmortal = false;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        Pause();
    }

    private void Start()
    {
        GameName.SetActive(true);

        ScrollB.SetActive(false);
        GameOver.SetActive(false);
        GameWin.SetActive(false);
        heart.SetActive(false);
        Mtext.SetActive(false);
    }

    public void Play()
    {
        Up.SetActive(false);
        Lift.SetActive(false);
        Right.SetActive(false);
        handler.sprite = image1;
        if (!isPlaying)
        {
            isPlaying = true;
            StartCoroutine(AddScoreOverTime());
        }

        Mtext.SetActive(true);
        heart.SetActive(true);
        ScrollB.SetActive(true);
        playButton.SetActive(false);
        GameOver.SetActive(false);
        GameName.SetActive(false);

        Player.currentHp = Player.maxHp;
        heartBar.DrawHearts();
        //UnderWaterPlayed.volume = 0.5f;
        //notplayBGSound.volume = 0f;
        notplayBGSound.Stop();
        UnderWaterPlayed.Play();

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();
        RightMove[] rM = FindObjectsOfType<RightMove>();
        LiftMove[] lM = FindObjectsOfType<LiftMove>();
        UpMove[] uM = FindObjectsOfType<UpMove>();

        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }

        for (int i = 0; i < rM.Length; i++)
        {
            Destroy(rM[i].gameObject);
        }

        for (int i = 0; i < lM.Length; i++)
        {
            Destroy(lM[i].gameObject);
        }

        for (int i = 0; i < uM.Length; i++)
        {
            Destroy(uM[i].gameObject);
        }
    }

    public void HitObstruction()
    {
        if(!IsImmortal)
        {
            hitSound.Play();
            Player.currentHp -= 2;
            OnPlayerDamaged?.Invoke();
            animator.SetTrigger("IsHit");
            StartCoroutine(StartImune());
        }

        if (Player.currentHp <= 0)
        {
            OnPlayerDeath?.Invoke();
            playButton.SetActive(true);
            GameOver.SetActive(true);
            ScrollB.SetActive(false);
            heart.SetActive(false);
            Mtext.SetActive(false);
            Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        score = 0;
        //notplayBGSound.volume = 0.1f;
        //UnderWaterPlayed.volume = 0f;
        notplayBGSound.Play();
        UnderWaterPlayed.Stop();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        
    }

    IEnumerator StartImune()
    {
        IsImmortal = true;
        yield return new WaitForSeconds(3);
        IsImmortal = false;
    }

    private IEnumerator AddScoreOverTime()
    {
        while (isPlaying)
        {
            score += 1;
            scoreText.text = "M: " + score.ToString();
            yield return new WaitForSeconds(0.03f); // เพิ่มคะแนนทุก 1 วินาที, คุณสามารถปรับเวลาได้ตามต้องการ
        }
    }

    public void Stop()
    {
        isPlaying = false;
    }
    void Update()
    {
        float normalizedValue = Mathf.Clamp((float)score / MaxBarM, 0f, 1f);
        yourScrollbar.value = normalizedValue;
        if(score >= winscore) 
        {
            playButton.SetActive(true);
            GameWin.SetActive(true);
            ScrollB.SetActive(false);
            heart.SetActive(false);
            Mtext.SetActive(false);
            Pause();
        }

        if (score == 500)
        {
            Up.SetActive(true);
        }
        if (score == 1000)
        {
            Right.SetActive(true);
        }
        if (score == 2000)
        {
            Lift.SetActive(true);
            
        }

        if(score == 750)
        {
            handler.sprite = image2;
        }
        else if(score == 1500)
        {
            handler.sprite = image2;
        }
        else if (score == 2250)
        {
            handler.sprite = image3;
        }
        else if (score == 3000)
        {
            handler.sprite = image4;
        }
    }
}

