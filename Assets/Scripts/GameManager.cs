using System;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }
    [SerializeField] private Transform parent;

    [SerializeField] private List<GameObject> levels;

    public event Action <int> MatchesScoreUpdate;
    public event Action <int> TurnsScoreUpdate;
    public event Action OnLevelComplete;
    public event Action OnLevelStarted;

    private int matches;
    public int Matches() => matches;

    private int turns;
    public int Turns() => turns;

    private GameObject currentLevel;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 1);
        }
        SetLevel();
    }

    public void SetLevel()
    {
        if (currentLevel != null)
            Destroy(currentLevel);

        if (PlayerPrefs.GetInt("Level") > levels.Count)
        {
            currentLevel = Instantiate(levels[UnityEngine.Random.Range(0, levels.Count)],parent);

        }
        else
        {
            currentLevel = Instantiate(levels[PlayerPrefs.GetInt("Level")-1],parent);
        }
        currentLevel.SetActive(true);
    }

    public void AddMatches()
    {
        matches++;
        MatchesScoreUpdate?.Invoke(matches);
    }

    public void AddTurns()
    {
        turns++;
        TurnsScoreUpdate?.Invoke(turns);
    }

    public void LevelCompleted()
    {
        Debug.Log("Level Completed!");
        OnLevelComplete?.Invoke();
    }

    public void LevelStarted()
    {
        Debug.Log("Level Started!");
        SetLevel();
        matches = 0;
        turns = 0;

        MatchesScoreUpdate?.Invoke(matches);
        TurnsScoreUpdate?.Invoke(turns);
        OnLevelStarted?.Invoke();
    }

}
