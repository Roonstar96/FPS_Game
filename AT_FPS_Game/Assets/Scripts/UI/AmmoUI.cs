using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AmmoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ammo;

    private void Update()
    {
        _ammo.text = (WeaponManager.CurrentAmmo + " :AMMO");
    }
}
