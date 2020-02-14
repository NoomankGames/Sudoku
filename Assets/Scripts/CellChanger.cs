/*
 * author : Kirakosyan Nikita
 * e-mail : noomank.games@gmail.com
 */
using UnityEngine;
using UnityEngine.UI;

public class CellChanger : MonoBehaviour
{
    [SerializeField] private int _number = 1;
    private int _count = 0;

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

                
                if(targetCell.storedNumber == targetCell.trueStoredNumber)
                {
                    GameManager.Score += GetPoints();
                }
            }
        }

        CheckNumberInCells();
    }

    private void CheckNumberInCells()
    {
        Cell[] cells = FindObjectsOfType<Cell>();
        foreach (Cell targetCell in cells)
        {
            if (targetCell.storedNumber == _number && targetCell.trueStoredNumber == _number)
            {
                if (_count < 9) _count++;
            }
        }

        if (_count == 9)
        {
            _text.SetActive(false);
            _button.interactable = false;
            _count = 9;
            GameManager.CompleteCells += 9;
        }
        else
        {
            _text.SetActive(true);
            _button.interactable = true;
            _count = 0;
        }
    }

    private int GetPoints()
    {
        int points = 50;

        DifficultManager difficultManager = FindObjectOfType<DifficultManager>();
        for(int i = 0; i < difficultManager.difficulties.Length; i++)
        {
            if(difficultManager.currentDifficult == difficultManager.difficulties[i])
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