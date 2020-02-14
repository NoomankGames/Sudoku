/*
 * author : Kirakosyan Nikita
 * e-mail : noomank.games@gmail.com
 */
using UnityEngine;

public class DifficultManager : MonoBehaviour
{
    public string currentDifficult = "Лёгкая";
    public string[] difficulties = new string[0];

    private void Start()
    {
        DifficultManager[] managers = FindObjectsOfType<DifficultManager>();
        foreach (DifficultManager targetManager in managers)
        {
            if (targetManager != this) Destroy(targetManager.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void SetDifficult(string value)
    {
        for (int i = 0; i < difficulties.Length; i++)
        {
            if (value == difficulties[i])
            {
                currentDifficult = value;
                break;
            }
        }
    }
}