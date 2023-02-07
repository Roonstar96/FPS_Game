using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ArmorUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _armor;
    public static int armor;
    public static int armorMax;

    private void Start()
    {
        armor = 0;
        armorMax = 100;
    }

    private void Update()
    {
        _armor.text = ("ARMOR: " + armor);
    }
}
