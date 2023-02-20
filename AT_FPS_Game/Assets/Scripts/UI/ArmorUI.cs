using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ArmorUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _armor;
    [SerializeField] private PlayerStatus _playerStat;

    private void Awake()
    {
        _playerStat = GameObject.Find("Player").GetComponent<PlayerStatus>();
    }
    private void Update()
    {
        _armor.text = ("ARMOR: " + _playerStat.Armour);
    }
}
