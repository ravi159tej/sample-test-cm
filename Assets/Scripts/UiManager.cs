using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    private static UiManager instance;
    [SerializeField] private TextMeshProUGUI levelNo;
    [SerializeField] private TextMeshProUGUI matches;
    [SerializeField] private TextMeshProUGUI turns;
    [SerializeField] private GameObject HudPanel;
    [SerializeField] private GameObject LevelCompletePanel;
    public static UiManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UiManager>();
            }

            return instance;
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.MatchesScoreUpdate += UpdateMatchText;
        GameManager.Instance.TurnsScoreUpdate += UpdateTurnText;
        GameManager.Instance.OnLevelComplete += ShowLevelCompletePanel;
        GameManager.Instance.OnLevelStarted += UpdateLevelNo;
    }
    private void OnDisable()
    {
        GameManager.Instance.MatchesScoreUpdate -= UpdateMatchText;
        GameManager.Instance.TurnsScoreUpdate -= UpdateTurnText;
        GameManager.Instance.OnLevelComplete -= ShowLevelCompletePanel;
        GameManager.Instance.OnLevelStarted -= UpdateLevelNo;
    }

    private void Start()
    {
        UpdateMatchText(GameManager.Instance.Matches());
        UpdateTurnText(GameManager.Instance.Turns());
        UpdateLevelNo();
    }

    private void UpdateMatchText(int match)
    {
        matches.text = "Matches : " + match;
    }

    private void UpdateTurnText(int turn)
    {
        turns.text = "Turns : " + turn;
    }

    public void UpdateLevelNo()
    {
        levelNo.text = "Level - " + PlayerPrefs.GetInt("Level");
    }

    public void OnClickNext()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        ShowHudPanel();
    }

    public void ShowLevelCompletePanel()
    {
        HudPanel.SetActive(false);
        LevelCompletePanel.SetActive(true);
    }

    public void ShowHudPanel()
    {
        LevelCompletePanel.SetActive(false);
        HudPanel.SetActive(true);
        GameManager.Instance.LevelStarted();
    }
}
