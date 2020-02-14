/*
 * author : Kirakosyan Nikita
 * e-mail : noomank.games@gmail.com
 */
using UnityEngine;

public class ExtraButton : MonoBehaviour
{
    private ChangeMemoryManager _changeMemoryManager = null;

    private void Start()
    {
        _changeMemoryManager = FindObjectOfType<ChangeMemoryManager>();
    }

    public void UndoChanges()
    {
        _changeMemoryManager.UndoLastChange();
    }

    public void ClearCellValue()
    {
        Cell[] cells = FindObjectsOfType<Cell>();
        foreach (Cell targetCell in cells)
        {
            if (targetCell.selected == true && targetCell.storedNumber != targetCell.trueStoredNumber)
            {
                _changeMemoryManager.AddChange(targetCell, targetCell.storedNumber);
                targetCell.Clear();
            }
        }
    }

    public void ShowTrueStoredNumber()
    {
        Cell[] cells = FindObjectsOfType<Cell>();
        foreach (Cell targetCell in cells)
        {
            if (targetCell.selected == true && targetCell.storedNumber != targetCell.trueStoredNumber)
            {
                _changeMemoryManager.AddChange(targetCell, targetCell.storedNumber);
                targetCell.SetStorageNumber(targetCell.trueStoredNumber);
                targetCell.ChangeStoredNumberTextColor();
            }
        }
    }
}