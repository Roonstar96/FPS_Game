using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum LevelEndType 
{ 
    Level1,
    Level2,
    Level3,
    Level4,
    EndLevel
}
public class LevelEndScript : MonoBehaviour
{
    [SerializeField] private GameObject _levelCompleteCanvas;
    [SerializeField] private GameManagerScript _gameMan;
    [SerializeField] private LevelEndType _levelType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;

            switch (_levelType)
            {
                case (LevelEndType.Level1):
                    {
                        GameManagerScript.i += 1;
                        _levelCompleteCanvas.SetActive(true);
                        break;
                    }
                case (LevelEndType.Level2):
                    {
                        GameManagerScript.i += 1;
                        _gameMan.Level2 = true;
                        _levelCompleteCanvas.SetActive(true);
                        break;
                    }
                case (LevelEndType.Level3):
                    {
                        GameManagerScript.i += 1;
                        _gameMan.Level3 = true;
                        _levelCompleteCanvas.SetActive(true);
                        break;
                    }
                case (LevelEndType.Level4):
                    {
                        GameManagerScript.i += 1;
                        _gameMan.Level4 = true;
                        _levelCompleteCanvas.SetActive(true);
                        break;
                    }
                case (LevelEndType.EndLevel):
                    {
                        _gameMan.Level5 = true;
                        SceneManager.LoadScene(6);
                        break;
                    }
            }
        }  
    }
}
