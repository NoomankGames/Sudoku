/*
 * author : Kirakosyan Nikita
 * e-mail : noomank.games@gmail.com
 */
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Menu
{
    public class Menu : MonoBehaviour
    {
        public void ShowDifficultWindow(GameObject target)
        {
            target.GetComponent<Animation>().Play();
        }

        public void LoadLevel(string levelName)
        {
            SceneManager.LoadScene(levelName);
        }

        public void SelectDifficult(string difficult)
        {
            DifficultManager difficultManager = FindObjectOfType<DifficultManager>();
            difficultManager.SetDifficult(difficult);
        }
    }
}