/*
 * author : Kirakosyan Nikita
 * e-mail : noomank.games@gmail.com
 */
using UnityEngine;

public class FrameManager : MonoBehaviour
{
    public int targetFrameRate = 60;

    private void Start()
    {
        FrameManager[] managers = FindObjectsOfType<FrameManager>();
        foreach (FrameManager targetManager in managers)
        {
            if (targetManager != this) Destroy(targetManager.gameObject);
        }
        DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = targetFrameRate;
    }
}