/*
 * author : Kirakosyan Nikita
 * e-mail : noomank.games@gmail.com
 */
using UnityEngine;

public class NumberGenerator : MonoBehaviour
{
    public Cell[] cells = new Cell[0];
    private int[,] _valueMatrix = new int[9, 9];

    private void Start()
    {
        Generate();
        MixGrid();
        HideCellValues();
    }

    public void Generate()
    {
        int firstNumberInRowArea = 1;//The first number in row area 9x3 (total 3 rows).
        int number = firstNumberInRowArea;
        int previousNumber = number;//The previous number throughout the entire first column.
        int delta = 3;

        int index = 0;
        for (int i = 0; i < cells.Length;)
        {
            cells[index].SetStorageNumber(number);
            cells[index].trueStoredNumber = number;
            number++;
            if (number > 9)
            {
                number = 1;
            }

            i++;
            index++;
            if (i % 27 == 0)//When start new row area 9x3 (total 3 rows).
            {
                firstNumberInRowArea++;
                number = firstNumberInRowArea;
                previousNumber = number;
            }
            else if (i % 9 == 0)//When start new row in grid (total 9 rows).
            {
                number = previousNumber + delta;
                previousNumber = number;
            }
        }

        UpdateValueMatrix();
    }

    public void UpdateValueMatrix()
    {
        int index = 0;
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                _valueMatrix[x, y] = cells[index].trueStoredNumber;
                index++;
            }
        }
    }

    public void TransposingGrid()
    {
        int index = 0;
        for (int x = 0; x < 9; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                cells[index].SetStorageNumber(_valueMatrix[x, y]);
                cells[index].trueStoredNumber = _valueMatrix[x, y];
                index++;
            }
        }

        UpdateValueMatrix();
    }

    public void SwapRowsSmall()
    {
        int cellsInRow = 9;
        int firstRow = Random.Range(1, 10);
        int secondRow = 0;

        if (firstRow == 1)
        {
            secondRow = Random.Range(2, 4);
        }
        else if (firstRow == 3 || firstRow == 6 || firstRow == 9)
        {
            secondRow = Random.Range(1, 3);
            secondRow = firstRow - secondRow;
        }
        else if (firstRow == 2 || firstRow == 5 || firstRow == 8)
        {
            secondRow = Random.Range(0, 2);
            if (secondRow == 0) secondRow = -1;
            secondRow += firstRow;
        }
        else if (firstRow == 4 || firstRow == 7)
        {
            secondRow = Random.Range(1, 3);
            secondRow += firstRow;
        }

        int y = secondRow * cellsInRow - cellsInRow;
        for (int x = firstRow * cellsInRow - cellsInRow; x < firstRow * cellsInRow; x++)
        {
            int firstRowStoredNumber = cells[x].storedNumber;

            cells[x].SetStorageNumber(cells[y].storedNumber);
            cells[x].trueStoredNumber = cells[y].storedNumber;
            cells[y].SetStorageNumber(firstRowStoredNumber);
            cells[y].trueStoredNumber = firstRowStoredNumber;

            y++;
        }

        UpdateValueMatrix();
    }

    public void SwapColumnsSmall()
    {
        TransposingGrid();
        SwapRowsSmall();
        TransposingGrid();

        UpdateValueMatrix();
    }

    public void SwapRowsArea()
    {
        int firstRowsArea = Random.Range(1, 3);
        int delta = 27;

        for (int i = firstRowsArea * delta - delta; i < firstRowsArea * delta; i++)
        {
            int saveNumber = cells[i].storedNumber;
            cells[i].SetStorageNumber(cells[i + delta].storedNumber);
            cells[i].trueStoredNumber = cells[i + delta].storedNumber;
            cells[i + delta].SetStorageNumber(saveNumber);
            cells[i + delta].trueStoredNumber = saveNumber;
        }
    }

    public void SwapColumnsArea()
    {
        SwapRowsArea();
        TransposingGrid();

        UpdateValueMatrix();
    }

    public void MixGrid()
    {
        int mixSteps = Random.Range(5, 31);
        for (int i = 0; i < mixSteps; i++)
        {
            int methodIndex = Random.Range(1, 6);

            if (methodIndex == 1) TransposingGrid();
            else if (methodIndex == 2) SwapRowsSmall();
            else if (methodIndex == 3) SwapColumnsSmall();
            else if (methodIndex == 4) SwapRowsArea();
            else if (methodIndex == 5) SwapColumnsArea();
        }

        UpdateValueMatrix();
    }

    public void HideCellValues()
    {
        int hideCellsCount = 1;
        DifficultManager difficultManager = FindObjectOfType<DifficultManager>();

        if (difficultManager != null)
        {
            if (difficultManager.currentDifficult == "Лёгкая") hideCellsCount = 32;
            else if (difficultManager.currentDifficult == "Средняя") hideCellsCount = 42;
            else if (difficultManager.currentDifficult == "Сложная") hideCellsCount = 47;
            else if (difficultManager.currentDifficult == "Экспертная") hideCellsCount = 52;
        }
        else Debug.LogError("Can't find: " + typeof(DifficultManager) + " !");

        for (int i = 0; i < hideCellsCount;)
        {
            int index = Random.Range(0, cells.Length);
            if (cells[index].storedNumber != 0)
            {
                cells[index].SetStorageNumber(0);
                i++;
            }
        }
    }
}