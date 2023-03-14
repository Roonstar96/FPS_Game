using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuScript : MonoBehaviour
{
    [Header("Canvas's")]
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _levelSelect;
    [SerializeField] private GameObject _options;
    //[SerializeField] private GameObject _playerButton, _returnToMainButton;

    [Header("Level buttons")]
    [SerializeField] private GameObject[] _levelButtons = new GameObject[4];

    [SerializeField] private GameManagerScript _gameMan;
    private void Awake()
    {
        _mainMenu.SetActive(true);
        _levelSelect.SetActive(false);
        _options.SetActive(false);
    }

    public void PlayButton()
    {
        LevelSelect();
    }

    public void LevelSelect()
    {
        _levelSelect.SetActive(true);
        _mainMenu.SetActive(false);

        if (_gameMan.Level2)
        {
            _levelButtons[0].SetActive(true);
        }
        if (_gameMan.Level3)
        {
            _levelButtons[1].SetActive(true);
        }
        if (_gameMan.Level4)
        {
            _levelButtons[2].SetActive(true);
        }
        if (_gameMan.Level5)
        {
            _levelButtons[3].SetActive(true);
        }
    }

    public void Options()
    {
        _options.SetActive(true);
        _mainMenu.SetActive(false);
    }

    public void RetroFilter()
    {
        
    }

    public void ReturnToMenu()
    {
        _mainMenu.SetActive(true);
        _levelSelect.SetActive(false);
        _options.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayLevel1()
    {
        WeaponManager.HasPistol = true;
        WeaponManager.PistolAmmo = 40;
        SceneManager.LoadScene(1);
    }
    public void PlayLevel2()
    {
        WeaponManager.HasPistol = true;
        WeaponManager.PistolAmmo = 40;
        SceneManager.LoadScene(2);
    }
    public void PlayLevel3()
    {
        WeaponManager.HasPistol = true;
        WeaponManager.PistolAmmo = 40;
        SceneManager.LoadScene(3);
    }
    public void PlayLevel4()
    {
        WeaponManager.HasPistol = true;
        WeaponManager.PistolAmmo = 40;
        SceneManager.LoadScene(4);
    }
    public void PlayLevel5()
    {
        WeaponManager.HasPistol = true;
        WeaponManager.PistolAmmo = 40;
        SceneManager.LoadScene(5);
    }
}
