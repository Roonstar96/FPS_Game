using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AmmoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ammo;
    public static int ammo;
    public static int ammoMax;

    public static bool ammoLeft;
    private void Start()
    {
        ammo = 0;
        ammoLeft = false;
    }

    private void Update()
    {
        if (ammo <= 0)
        {
            ammo = 0;

            if (MagUI.mag <= 0)
            {
                ammoLeft = false;
            }
        }
        else
        {
            ammoLeft = true;
        }
        _ammo.text = (ammo + " :AMMO");
    }
}
