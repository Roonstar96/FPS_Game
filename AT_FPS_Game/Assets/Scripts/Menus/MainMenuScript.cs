using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private Canvas _mainMenu;
    [SerializeField] private Canvas _levelSelect;
    [SerializeField] private Canvas _options;

    public static bool _level1;
    public static bool _level2;
    public static bool _level3;
    public static bool _level4;
    public static bool _level5;

    private void Awake()
    {
        _mainMenu.enabled = true;
        _levelSelect.enabled = false;
        _options.enabled = false;

        _level1 = false;
        _level2 = false;
        _level3 = false;
        _level4 = false;
        _level5 = false;
    }

    private void InMainMenu()
    {

    }

    private void InLevelSelect()
    {

    }

    private void InOptions()
    {

    }

    private void ReturnToMenu()
    {

    }
}
