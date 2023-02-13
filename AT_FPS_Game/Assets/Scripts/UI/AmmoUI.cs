using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AmmoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ammo;
    public static int ammo;

    private void Start()
    {
        ammo = 0;
    }

    private void Update()
    {
        if (ammo <= 0)
        {
            ammo = 0;
        }
        _ammo.text = (ammo + " :AMMO");
    }
}
