using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;

    public int ghostMulitplier = 1;

    public int score {get; private set;}
    public int lives {get; private set;}

    public Text scoreText;
    public Text livesText;

    private SoundEffects sound;

    Freezer delay;

    private void Awake()
    {
        sound = GetComponent<SoundEffects>();
        delay = GetComponent<Freezer>();
    }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (this.lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        sound.introSoundEffect();
        delay.Freeze(4.5f);
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        InvokeRepeating(nameof(PlayBackGroundMusic), 0.1f, 2.0f);
        foreach(Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        
       ResetState();

    }

    private void ResetState()
    {
        ResetGhostMultiplier();

        for (int i = 0 ; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }

        this.pacman.ResetState();
    }

    private void GameOver()
    {
        for (int i = 0 ; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false);
    }
    

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = lives.ToString();
    }

    


    public void GhostEaten(Ghost ghost)
    {
        sound.GhostEatenSound();
        delay.Freeze(0.5f);

        int points = ghost.points * this.ghostMulitplier;
        SetScore(this.score + points);
        this.ghostMulitplier++;
    }

    public void PacmanEaten()
    {
        
        sound.pacmanEatenSound();
        delay.Freeze(3.0f);

        this.pacman.gameObject.SetActive(false);

        SetLives(this.lives - 1);

        if (this.lives > 0) {
            Invoke(nameof(ResetState), 0.1f);  
        }
        else {
            GameOver();
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        sound.PelletEatenSound();

        pellet.gameObject.SetActive(false);
        SetScore(this.score + pellet.points);

        if (!HasRemainingPellets())
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(pellet.duration);
        }
        
        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
        InvokeRepeating(nameof(PlayBackGroundMusic), 0.1f, 2.0f);
        
    }

    private bool HasRemainingPellets()
    {
        foreach(Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    private void ResetGhostMultiplier()
    {
        this.ghostMulitplier = 1;
    }

    private void PlayBackGroundMusic()
    {
        sound.PlayBGMusic();
    }

}
