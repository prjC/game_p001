using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//リングの数を数える
public class ring_count : MonoBehaviour
{
	private TextMeshProUGUI ring_Count;
	public GameObject textobject;
	private int coin;
	// Start is called before the first frame update
	void Start()
    {
		ring_Count = textobject.GetComponent<TextMeshProUGUI>();
		ring_Count.text = coin.ToString("  0");
		coin = 0;
	}

	void OnTriggerEnter(Collider hit)
	{
		if (hit.CompareTag("player_car"))
		{
			int coin = +1;
			ring_Count.text = coin.ToString("  0") ;
			Destroy(this.gameObject);
		}
	}
}
