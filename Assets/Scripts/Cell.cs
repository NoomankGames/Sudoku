/*
 * author : Kirakosyan Nikita
 * e-mail : noomank.games@gmail.com
 */
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerClickHandler
{
    [Header("Cell Information")]
    public int storedNumber = 0;
    public int trueStoredNumber = 0;
    public bool selected = false;

    [Header("UI")]
    [SerializeField] private Text _storedNumberText = null;
    [HideInInspector] public Image image = null;

    [Header("Visualization")]
    public Color normalColor = Color.white;
    public Color highlightedColor = Color.grey;
    public Color sameValueColor = Color.cyan;
    public Color selectedColor = Color.black;
    public Color changedTextColor = Color.blue;
    public Color mistakeTextColor = Color.red;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        if (selected == true)
        {
            HighlightCellsWithSameValues();
            HighlightCellsInSameRowAndColumn();
            image.color = selectedColor;
        }

        CheckStoredValue();
    }

    public void SetStorageNumber(int number)
    {
        storedNumber = number;
        HighlightCellsWithSameValues();
        HighlightCellsInSameRowAndColumn();
        CheckStoredValue();
    }

    public void Clear()
    {
        storedNumber = 0;
        HighlightCellsWithSameValues();
        HighlightCellsInSameRowAndColumn();
        CheckStoredValue();
    }

    public void ChangeStoredNumberTextColor()
    {
        if (storedNumber == trueStoredNumber) _storedNumberText.color = changedTextColor;
        else
        {
            _storedNumberText.color = mistakeTextColor;
            GameManager.Mistakes++;
        }
    }

    public void SelectCell()
    {
        Cell[] cells = FindObjectsOfType<Cell>();
        foreach (Cell targetCell in cells)
        {
            targetCell.image.color = targetCell.normalColor;
            targetCell.selected = false;
        }

        HighlightCellsWithSameValues();
        HighlightCellsInSameRowAndColumn();

        image.color = selectedColor;
        selected = true;
    }

    private void CheckStoredValue()
    {
        if (storedNumber < 1 || storedNumber > 9)
        {
            _storedNumberText.text = string.Empty;
            storedNumber = 0;
        }
        else
        {
            _storedNumberText.text = storedNumber.ToString();
        }
    }

    private void HighlightCellsWithSameValues()
    {
        Cell[] cells = FindObjectsOfType<Cell>();
        foreach (Cell targetCell in cells)
        {
            targetCell.image.color = targetCell.normalColor;
            if (targetCell.storedNumber == storedNumber && targetCell.storedNumber != 0)
            {
                targetCell.image.color = targetCell.sameValueColor;
            }
        }
    }

    private void HighlightCellsInSameRowAndColumn()
    {
        NumberGenerator numGenerator = FindObjectOfType<NumberGenerator>();
        int index = 0;
        int delta = 9;
        for (int x = 0; x < 9; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                if (numGenerator.cells[index] == this)
                {
                    x *= 9;
                    for (int step = 0; step < 9; step++)
                    {
                        numGenerator.cells[x].image.color = numGenerator.cells[x].highlightedColor;
                        numGenerator.cells[y].image.color = numGenerator.cells[y].highlightedColor;
                        y += delta;
                        x++;
                    }
                    x = 9;
                    break;
                }
                index++;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SelectCell();
    }
}