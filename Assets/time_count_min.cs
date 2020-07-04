using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class time_count_min : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI Time_Count;//タイマーテキスト
	private float countup;
	private int min;

	void Start()
    {
		countup = 0f;
		min = 0;
		
	}

    void Update()
    {
	
		if (FlagManager.Instance.flags[1] == true)
		
		countup += Time.deltaTime;

		int min = (int)(countup * 1000 % 100);
		Time_Count.text = min.ToString("00");
	
	}
}
