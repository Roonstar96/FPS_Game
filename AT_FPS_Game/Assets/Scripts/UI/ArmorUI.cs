using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ArmorUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _armor;

    private void Update()
    {
        _armor.text = ("ARMOR: " + PlayerStatus.armor);
    }
}
