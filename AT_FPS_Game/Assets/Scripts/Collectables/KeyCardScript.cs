using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum CardType
{
    green,
    red,
    blue,
    black
}

public class KeyCardScript : MonoBehaviour
{
	[SerializeField] private CardType _keyCard;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
			switch (_keyCard)
			{
				case (CardType.green):
					{
						PlayerMovement._greenCard = true;
						Destroy(gameObject);
						break;
					}
				case (CardType.red):
					{
						PlayerMovement._redCard = true;
						Destroy(gameObject);
						break;
					}
				case (CardType.blue):
					{
						PlayerMovement._blueCard = true;
						Destroy(gameObject);
						break;
					}
				case (CardType.black):
					{
						PlayerMovement._blackCard = true;
						Destroy(gameObject);
						break;
					}
			}
		}
    }
}
