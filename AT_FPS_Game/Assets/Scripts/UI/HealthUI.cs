using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private PlayerStatus _playerStat;

    private void Awake()
    {
        _playerStat = GameObject.Find("Player").GetComponent<PlayerStatus>();
    }
    private void Update()
    {
        _health.text = ("HEALTH: " + _playerStat.Health);
    }
}
