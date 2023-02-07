using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _health;
    public static int health;
    public static int healthMax;

    private void Start()
    {
        health = 100;
        healthMax = 100;
    }

    private void Update()
    {
        _health.text = ("HEALTH: " + health);
    }
}
