using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance => instance;

    [SerializeField] private Character character;

    [SerializeField] private Image healthBar;

    [SerializeField] private CanvasGroup hudCanvasGroup;
    [SerializeField] private CanvasGroup gameOverCanvasGroup;

    [SerializeField] private TextMeshProUGUI coinCounterText;

    //[SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private Image[] lifeHearts;

    [SerializeField] private float fadeTime = 2f;

    [SerializeField] private CanvasGroup victoryCanvasGroup;

    private int coinCounter = 0;
    private bool isFading = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        this.hudCanvasGroup.alpha = 1f;
        this.gameOverCanvasGroup.alpha = 0f;
        this.victoryCanvasGroup.alpha = 0f;
    }

    void Update()
    {
        
        float percent =
            this.character.GetCurrentHealth() /
            this.character.GetMaxHealth();

        this.healthBar.fillAmount = percent;
        //this.livesText.text =
        //    "Lives: " + this.character.GetCurrentLives();
        for (int i = 0; i < this.lifeHearts.Length; i++)
        {
            this.lifeHearts[i].enabled =
                i < this.character.GetCurrentLives();
        }

        if (this.character.IsDead() && !this.isFading)
        {
            if (this.character.GetCurrentLives() > 1)
            {
                this.character.LoseLife();
                this.character.RespawnAtCheckpoint();
                this.character.RestoreHealth();
            }
            else
            {
                this.character.LoseLife();
                StartCoroutine(FadeToGameOver());
            }
        }
    }

    IEnumerator FadeToGameOver()
    {
        this.isFading = true;

        float timer = 0f;

        while (timer < this.fadeTime)
        {
            float percent = timer / this.fadeTime;

            this.hudCanvasGroup.alpha = 1f - percent;
            this.gameOverCanvasGroup.alpha = percent;

            timer += Time.deltaTime;

            yield return null;
        }

        this.hudCanvasGroup.alpha = 0f;
        this.gameOverCanvasGroup.alpha = 1f;
    }

    IEnumerator FadeBackToHUD()
    {
        float timer = 0f;

        while (timer < this.fadeTime)
        {
            float percent = timer / this.fadeTime;

            this.hudCanvasGroup.alpha = percent;
            this.gameOverCanvasGroup.alpha = 1f - percent;

            timer += Time.deltaTime;

            yield return null;
        }

        this.hudCanvasGroup.alpha = 1f;
        this.gameOverCanvasGroup.alpha = 0f;

        this.isFading = false;
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void CollectCoin()
    {
        this.coinCounter++;

        this.coinCounterText.text =
            this.coinCounter.ToString();
    }

    public void ShowVictoryScreen()
    {
        StartCoroutine(FadeToVictory());
    }

    IEnumerator FadeToVictory()
    {
        this.isFading = true;

        float timer = 0f;

        while (timer < this.fadeTime)
        {
            float percent = timer / this.fadeTime;

            this.hudCanvasGroup.alpha = 1f - percent;
            this.victoryCanvasGroup.alpha = percent;

            timer += Time.deltaTime;
            yield return null;
        }

        this.hudCanvasGroup.alpha = 0f;
        this.victoryCanvasGroup.alpha = 1f;
    }
    
}