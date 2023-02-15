using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _health;

    private void Update()
    {
        _health.text = ("HEALTH: " + PlayerStatus.health);
    }
}
