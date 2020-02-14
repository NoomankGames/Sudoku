/*
 * author : Kirakosyan Nikita
 * e-mail : noomank.games@gmail.com
 */
using UnityEngine;

public class ExtraButton : MonoBehaviour
{
    [SerializeField] private AudioClip _soundFX = null;

    private ChangeMemoryManager _changeMemoryManager = null;

    private void Start()
    {
        _changeMemoryManager = FindObjectOfType<ChangeMemoryManager>();
    }

    public void UndoChanges()
    {
        _changeMemoryManager.UndoLastChange();
        AudioSource audioSource = GameObject.Find("Audio Point").GetComponent<AudioSource>();
        audioSource.clip = _soundFX;
        audioSource.Play();

        CellChanger[] cellChangers = FindObjectsOfType<CellChanger>();
        foreach (CellChanger targetChanger in cellChangers)
        {
            targetChanger.CheckNumberInCells();
        }
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

                AudioSource audioSource = GameObject.Find("Audio Point").GetComponent<AudioSource>();
                audioSource.clip = _soundFX;
                audioSource.Play();
                break;
            }
        }

        CellChanger[] cellChangers = FindObjectsOfType<CellChanger>();
        foreach (CellChanger targetChanger in cellChangers)
        {
            targetChanger.CheckNumberInCells();
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

                AudioSource audioSource = GameObject.Find("Audio Point").GetComponent<AudioSource>();
                audioSource.clip = _soundFX;
                audioSource.Play();
                break;
            }
        }
    }
}