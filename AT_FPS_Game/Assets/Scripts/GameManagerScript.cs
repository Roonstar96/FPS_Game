using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    //add level complete variables
    //add arrays for level & playerspawn objects
    //add canvas
    //public static GameObject[] _levelMenuButtons = new GameObject[4];

    [SerializeField] private GameObject[] _levelObjects;
    [SerializeField] private GameObject[] _playerSpawnObj;

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private MainMenuScript _mainMenu;

    public static int i;

    public static bool _level2;
    public static bool _level3;
    public static bool _level4;
    public static bool _level5;

    public bool Level2 { get => _level2; set => _level2 = value; }
    public bool Level3 { get => _level3; set => _level3 = value; }
    public bool Level4 { get => _level4; set => _level4 = value; }
    public bool Level5 { get => _level5; set => _level5 = value; }

    //public
    //public int I { get => i; set => i = value; }

    private void Awake()
    {
        _level2 = false;
        _level3 = false;
        _level4 = false;
        _level5 = false;
    }

    public void LevelLoadFunction()
    {
        _levelObjects[i].SetActive(true);
        _player.transform.position = _playerSpawnObj[i].transform.position;
        MusicManagerFunction();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void CanvasManagerFunction()
    {
        //add funcitons for rendering Win/lose/level complete canvases
        _gameOverCanvas.SetActive(true);
    }

    private void MusicManagerFunction()
    {
        _levelObjects[i].GetComponent<AudioSource>().Play();
    }

    private void ResetLevelFunction()
    {
        SceneManager.LoadScene(1);

        PlayerStatus.Health = 100;

        if (PlayerStatus._hasArmour)
        {
            PlayerStatus.Armour = 100;
        }
        if (WeaponManager.HasPistol)
        {
            WeaponManager.PistolAmmo = 45;
        }
        if (WeaponManager.HasShotgun)
        {
            WeaponManager.ShotgunAmmo = 30;
        }
        if (WeaponManager.HasBiggun)
        {
            WeaponManager.BigGunAmmo = 20;
        }

        _levelObjects[i].SetActive(true);
        _player.transform.position = _playerSpawnObj[i].transform.position;

    }
}
