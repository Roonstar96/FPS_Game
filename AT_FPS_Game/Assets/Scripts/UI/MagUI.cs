using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MagUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _mag;
    public static int mag;

    private void Awake()
    {
        mag = 0;
    }

    private void Update()
    {
        MagFunciton();        
    }

    private void MagFunciton()
    {
        if (!AmmoUI.ammoLeft)
        {
            mag = 0;
        }
        else
        {
            if (mag < 0)
            {
                mag = 10;
                AmmoUI.ammo -= mag;
            }
        }
        _mag.text = (mag + " :MAG");
    }
}
