using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 7;
    public float jumpForce = 17;
    private float direction = 0;
    private Rigidbody2D player;
    private bool freezeInput = false;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isGrounded;

    public List<GameObject> lifeCountPrefabs;
    public GameObject fallDetector;
    public GameObject deadPlayerStation;
    public GameObject playerDead;

    public GameObject bulletPref;
    public List<GameObject> bulletCountPrefabs;
    public float bulletForce;
    private bool playerDirection = true;
    private bool bullet = false;

    public GameObject youWon;
    public GameObject gameOver;
    public GameObject buttonNextLevel;
    public GameObject buttonsEndGame;
    public GameObject pointsTable;
    public GameObject scoreDisplay;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scorePointsText;
    public TextMeshProUGUI lifeScoreText;
    public TextMeshProUGUI levelMultiplierText;
    public TextMeshProUGUI finalScoreText;

    private Animator playerAnimation;

    void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        UpdateLifeCountPrefab();
        UpdateBulletCountPrefab();
        StaticValues.lifeScore = StaticValues.playerLives * 1000;
        LevelMultiplier();
        scoreDisplay.SetActive(true);
        scoreText.text = "Score: " + StaticValues.scorePoints;
    }

    private void Update()
    {
        if (!freezeInput)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            direction = Input.GetAxis("Horizontal");

            if (direction > 0)
            {
                player.velocity = new Vector2(direction * speed, player.velocity.y);
                transform.localScale = new Vector2(1, 1);
                playerDirection = true;
            }
            else if (direction < 0)
            {
                player.velocity = new Vector2(direction * speed, player.velocity.y);
                transform.localScale = new Vector2(-1, 1);
                playerDirection = false;
            }
            else
            {
                player.velocity = new Vector2(0, player.velocity.y);
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                player.velocity = new Vector2(player.velocity.x, jumpForce);
                SFXController.PlaySound("JumpSound");
            }

            if (Input.GetButtonDown("Fire1") && bullet && StaticValues.totalBullets > 0)
            {
                StaticValues.totalBullets--;
                if (playerDirection == true)
                {
                    Vector2 poopSpawnPos = player.position + new Vector2(0.5f, 0);
                    GameObject newPoop = Instantiate(bulletPref, poopSpawnPos, Quaternion.identity);
                    Rigidbody2D poopRb = newPoop.GetComponent<Rigidbody2D>();
                    poopRb.AddForce(new Vector2(bulletForce, 0));
                }
                else
                {
                    Vector2 poopSpawnPos = player.position + new Vector2(-0.5f, 0);
                    GameObject newPoop = Instantiate(bulletPref, poopSpawnPos, Quaternion.identity);
                    Rigidbody2D poopRb = newPoop.GetComponent<Rigidbody2D>();
                    poopRb.AddForce(new Vector2(bulletForce * -1, 0));
                }
                UpdateBulletCountPrefab();
            }

            playerAnimation.SetFloat("Speed", Mathf.Abs(player.velocity.x));

            fallDetector.transform.position = new Vector2(player.transform.position.x, fallDetector.transform.position.y);
            deadPlayerStation.transform.position = new Vector2(player.transform.position.x, deadPlayerStation.transform.position.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherGO = collision.gameObject;
        if (otherGO.CompareTag("Enemy") || otherGO.CompareTag("Bomb"))
        {
            PlayerDeath();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject otherGO = collision.gameObject;
        if (otherGO.CompareTag("Cake"))
        {
            SFXController.PlaySound("CrunchSound");
            otherGO.SetActive(false);
            StaticValues.scorePoints += 100;
            scoreText.text = "Score: " + StaticValues.scorePoints;
            bullet = true;
            if (StaticValues.totalBullets >= 0 && StaticValues.totalBullets < 4)
                StaticValues.totalBullets += 3;
            else if (StaticValues.totalBullets == 4)
                StaticValues.totalBullets += 2;
            else if (StaticValues.totalBullets == 5)
                StaticValues.totalBullets += 1;
            else
                StaticValues.totalBullets += 0;

            UpdateBulletCountPrefab();
        }
        else if (otherGO.CompareTag("FallDetector") || otherGO.CompareTag("Spike"))
        {
            PlayerDeath();
        }
        else if (otherGO.CompareTag("Door"))
        {
            if (SceneManager.GetActiveScene().name == "Level 1")
            {
                StaticValues.scorePoints += 500;
                SFXController.StopSound("BgMusic");
                freezeInput = true;
                player.velocity = new Vector2(0, 0);
                SFXController.PlaySound("VictorySound");
                CalculateScore();
                scoreDisplay.SetActive(false);
                youWon.SetActive(true);
                pointsTable.SetActive(true);
                buttonNextLevel.SetActive(true);
            }
            else
            {
                StaticValues.scorePoints += 500;
                SFXController.StopSound("BgMusic");
                freezeInput = true;
                player.velocity = new Vector2(0, 0);
                SFXController.PlaySound("VictorySound");
                CalculateScore();
                scoreDisplay.SetActive(false);
                youWon.SetActive(true);
                pointsTable.SetActive(true);
                buttonsEndGame.SetActive(true);
                ResetScore();
            }
        }
    }

    private void UpdateLifeCountPrefab()
    {
        for (int i = 0; i < lifeCountPrefabs.Count; i++)
        {
            lifeCountPrefabs[i].SetActive(i == StaticValues.playerLives);
        }
    }

    private void UpdateBulletCountPrefab()
    {
        for (int i = 0; i < bulletCountPrefabs.Count; i++)
        {
            bulletCountPrefabs[i].SetActive(i == StaticValues.totalBullets);
        }

        if (StaticValues.totalBullets > 0)
        {
            bullet = true;
        }
        else 
        {
            bullet = false; 
        }
    }

    private void PlayerDeath()
    {
        StartCoroutine(PlayerDeathCoroutine());
    }

    private IEnumerator PlayerDeathCoroutine()
    {
        GameObject deadPlayerInstance = Instantiate(playerDead, player.position, Quaternion.identity);
        Rigidbody2D deadRb = deadPlayerInstance.GetComponent<Rigidbody2D>();
        deadRb.AddForce(Vector3.up * 5, ForceMode2D.Impulse);
        SFXController.StopSound("BgMusic");
        SFXController.PlaySound("FailSound");

        if (StaticValues.playerLives > 1)
        {
            StaticValues.playerLives--;
            UpdateLifeCountPrefab();
            StaticValues.totalBullets = 0;
            freezeInput = true;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, -7, gameObject.transform.position.y);
            player.velocity = new Vector2(0, 0);
            float timer = 0;
            float deathInterval = 3;

            while (timer < deathInterval)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            RestartLevel();
        }
        else
        {
            StaticValues.playerLives--;
            UpdateLifeCountPrefab();
            StaticValues.lifeScore = 0;
            Destroy(gameObject);
            CalculateScore();
            scoreDisplay.SetActive(false);
            gameOver.SetActive(true);
            pointsTable.SetActive(true);
            buttonsEndGame.SetActive(true);
            ResetScore();
        }
    }

    private void LevelMultiplier()
    {
        if (SceneManager.GetActiveScene().name == "Level 1")
            StaticValues.levelMultiplier = 1;
        else if (SceneManager.GetActiveScene().name == "Level 2")
            StaticValues.levelMultiplier = 2;
        else if (SceneManager.GetActiveScene().name == "Level 3")
            StaticValues.levelMultiplier = 3;
        else
            StaticValues.levelMultiplier = 0;
    }

    private void CalculateScore()
    {
        StaticValues.finalScore = (StaticValues.scorePoints + StaticValues.lifeScore) * StaticValues.levelMultiplier;
        scorePointsText.text = "Score: " + StaticValues.scorePoints;
        lifeScoreText.text = "Life Bonus: +" + StaticValues.lifeScore;
        levelMultiplierText.text = "Level Bonus: * " + StaticValues.levelMultiplier;
        if (StaticValues.playerLives > 0)
        {
            if (SceneManager.GetActiveScene().name == "Level 1")
            {
                finalScoreText.text = "Score: " + StaticValues.finalScore;
            }
            else
            {
                finalScoreText.text = "Final Score: " + StaticValues.finalScore;
            }
        }
        else
        {
            finalScoreText.text = "Final Score: " + StaticValues.finalScore;
        }

        StaticValues.scorePoints = StaticValues.finalScore;
    }

    private void ResetScore()
    {
        StaticValues.scorePoints = 0;
        StaticValues.lifeScore = 0;
        StaticValues.finalScore = 0;
        StaticValues.totalBullets = 0;
        StaticValues.playerLives = 3;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
