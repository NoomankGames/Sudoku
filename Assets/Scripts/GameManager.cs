/*
 * author : Kirakosyan Nikita
 * e-mail : noomank.games@gmail.com
 */
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool IsPaused = false;

    public static int Score = 0;

    public static int Mistakes = 0;
    [SerializeField] private int _maxMistakes = 3;

    public float seconds = 0.0f;
    public float minutes = 0.0f;

    public static int CompleteCells = 0;

    [Header("UI")]
    [SerializeField] private Text _difficultText = null;
    [SerializeField] private Text _mistakesText = null;
    [SerializeField] private Text _scoreText = null;
    [SerializeField] private Text _timeText = null;

    [Header("Windows")]
    [SerializeField] private GameObject _winWindow = null;
    [SerializeField] private GameObject _defeatWindow = null;

    private void Awake()
    {
        IsPaused = false;
        Score = 0;
        Mistakes = 0;
        CompleteCells = 0;
    }

    private void Start()
    {
        DifficultManager difficultManager = FindObjectOfType<DifficultManager>();
        _difficultText.text = difficultManager.currentDifficult;
    }

    private void LateUpdate()
    {
        CountCompletedCells();

        DisplayMistakes();
        DisplayScore();

        if (IsPaused == true) return;

        if (CompleteCells == 81)
        {
            FindObjectOfType<VideoAdsManager>().shown = false;
            DisplayWindow(_winWindow);
            IsPaused = true;
            return;
        }
        else if (Mistakes >= 3)
        {
            FindObjectOfType<VideoAdsManager>().shown = false;
            DisplayWindow(_defeatWindow);
            IsPaused = true;
            return;
        }

        DisplayTime();
    }

    private void DisplayTime()
    {
        _timeText.text = "Время: " + minutes.ToString("00") + ":" + seconds.ToString("00");

        seconds += Time.deltaTime;
        if (seconds >= 59.0f)
        {
            minutes++;
            seconds = 0.0f;
        }
    }

    private void DisplayMistakes()
    {
        _mistakesText.text = "Ошибки: " + Mistakes.ToString() + "/" + _maxMistakes.ToString();
    }

    private void DisplayScore()
    {
        _scoreText.text = "Очки: " + Score.ToString();
    }

    private void DisplayWindow(GameObject target)
    {
        target.SetActive(true);
    }

    private void CountCompletedCells()
    {
        int completed = 0;

        Cell[] cells = FindObjectsOfType<Cell>();
        for (int i = 0; i < 81; i++)
        {
            if (cells[i].storedNumber == cells[i].trueStoredNumber)
            {
                completed++;
            }
        }

        CompleteCells = completed;
    }
}