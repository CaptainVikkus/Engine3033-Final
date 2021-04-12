using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer
{
    public Timer() { }
    public Timer(float hours, float minutes, float seconds) 
    {
        _hours = hours; _minutes = minutes; _seconds = seconds;
    }
    private float _seconds = 0;
    public float Seconds { get { return _seconds; } }
    private float _minutes = 0;
    public float Minutes { get { return _minutes; } }
    private float _hours = 0;
    public float Hours { get { return _hours; } }

    public void AddTime(float seconds, float minutes = 0, float hours = 0)
    {
        _seconds += seconds;
        _minutes += minutes;
        _hours += hours;
        if (_seconds >= 60)
        { _seconds -= 60; _minutes += 1; }
        if (_minutes >= 60)
        { _minutes -= 60; _hours += 1; }
    }

    public void RemoveTime(float seconds, float minutes = 0, float hours = 0)
    {
        _seconds -= seconds;
        _minutes -= minutes;
        _hours -= hours;
        if (_minutes <= 0 && _hours <= 0 && _seconds < 0)
        { _seconds = 0; return; }

        if (_seconds <= 0)
        { _seconds += 60; _minutes -= 1; }
        if (_minutes <= 0)
        { _minutes = 60; _hours -= 1; }
        if (_hours < 0 )
        { _minutes = 0; _hours = 0; }
    }

    public string PrintTime()
    {
        string seconds = _seconds < 10 ? "0" + ((int)_seconds).ToString() : ((int)_seconds).ToString();
        string minutes = _minutes < 10 ? "0" + ((int)_minutes).ToString() : ((int)_minutes).ToString();

        return _hours.ToString() + ":" + minutes + ":" + seconds;
    }
}

public class LevelManager : Singleton<LevelManager>
{
    [Range(1, 10)]
    public static int Difficulty = 1;
    public Vector3 Timer = new Vector3(0, 5, 0);
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI difficultyText;
    private Timer clock;
    private bool won;

    private void Start()
    {
        won = false;
        clock = new Timer(Timer.x, Timer.y, Timer.z);
        clock.AddTime(0, (Difficulty - 1));
        SpawnerManager.spawnTime /= Difficulty;
        SpawnerManager.maxZombies = Difficulty * SpawnerManager.maxZombies;
        difficultyText.text = $"Difficulty: {Difficulty}";
    }

    // Update is called once per frame
    void Update()
    {
        if (clock.Seconds <= 0 && clock.Minutes <= 0 && clock.Hours <= 0)
        {
            if (!won)
            {
                StartCoroutine(GameWin());
                if (Difficulty < 10)
                {
                    Difficulty++;
                    SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
                }
            }

            return;
        }

        clock.RemoveTime(Time.deltaTime);
        timerText.text = clock.PrintTime();
    }

    IEnumerator GameWin()
    {
        timerText.text = "YOU WIN!";
        won = true;
        while (isActiveAndEnabled)
        {
            yield return new WaitForSeconds(.75f);
            timerText.enabled = !timerText.enabled;
        }
    }
}
