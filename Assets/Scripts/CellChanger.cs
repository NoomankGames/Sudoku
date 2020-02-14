/*
 * author : Kirakosyan Nikita
 * e-mail : noomank.games@gmail.com
 */
using UnityEngine;
using UnityEngine.UI;

public class CellChanger : MonoBehaviour
{
    [SerializeField] private int _number = 1;
    [SerializeField] private AudioClip _soundFX = null;

    [SerializeField] private GameObject _text = null;

    private Button _button = null;

    private void Start()
    {
        _button = GetComponent<Button>();
        CheckNumberInCells();
    }

    public void SetNumberInCell()
    {
        Cell[] cells = FindObjectsOfType<Cell>();
        foreach (Cell targetCell in cells)
        {
            if (targetCell.selected == true && targetCell.storedNumber != _number && targetCell.storedNumber != targetCell.trueStoredNumber)
            {
                ChangeMemoryManager changeMemoryManager = FindObjectOfType<ChangeMemoryManager>();
                changeMemoryManager.AddChange(targetCell, targetCell.storedNumber);

                targetCell.SetStorageNumber(_number);
                targetCell.ChangeStoredNumberTextColor();


                if (targetCell.storedNumber == targetCell.trueStoredNumber)
                {
                    GameManager.Score += GetPoints();
                }
            }
        }

        CheckNumberInCells();
        AudioSource audioSource = GameObject.Find("Audio Point").GetComponent<AudioSource>();
        audioSource.clip = _soundFX;
        audioSource.Play();
    }

    public void CheckNumberInCells()
    {
        int count = 0;

        Cell[] cells = FindObjectsOfType<Cell>();
        foreach (Cell targetCell in cells)
        {
            if (targetCell.storedNumber == _number && targetCell.trueStoredNumber == _number)
            {
                if (count < 9) count++;
            }
        }

        if (count == 9)
        {
            _text.SetActive(false);
            _button.interactable = false;
        }
        else
        {
            _text.SetActive(true);
            _button.interactable = true;
        }
    }

    private int GetPoints()
    {
        int points = 50;

        DifficultManager difficultManager = FindObjectOfType<DifficultManager>();
        for (int i = 0; i < difficultManager.difficulties.Length; i++)
        {
            if (difficultManager.currentDifficult == difficultManager.difficulties[i])
            {
                points *= i + 1;
                break;
            }
        }

        if (GameManager.Mistakes == 1) points -= 10;
        else if (GameManager.Mistakes == 2) points -= 15;

        return points;
    }
}