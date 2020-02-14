/*
 * author : Kirakosyan Nikita
 * e-mail : noomank.games@gmail.com
 */
using System.Collections.Generic;
using UnityEngine;

public class ChangeMemoryManager : MonoBehaviour
{
    public List<Cell> changedCells = new List<Cell>(0);
    public List<int> changedValues = new List<int>(0);

    public void AddChange(Cell targetCell, int value)
    {
        changedCells.Add(targetCell);
        changedValues.Add(value);
    }

    public void UndoLastChange()
    {
        if (changedCells.Count > 0)
        {
            Cell lastCell = changedCells[changedCells.Count - 1];
            int lastValue = changedValues[changedValues.Count - 1];

            lastCell.SetStorageNumber(lastValue);
            lastCell.SelectCell();

            changedCells.RemoveAt(changedCells.Count - 1);
            changedValues.RemoveAt(changedValues.Count - 1);
        }
    }
}