using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AmmoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ammo;
    [SerializeField] private WeaponManager _weapon;

    private void Awake()
    {
        _weapon = GameObject.Find("Player").GetComponent<WeaponManager>();
    }
    private void Update()
    {
        _ammo.text = (_weapon.CurrentAmmo + " :AMMO");
    }
}
