using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class time_count_sec : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI Time_Count_sec;//タイマーテキスト
	[SerializeField] private TextMeshProUGUI Time_Count_min;//タイマーテキスト
	private float countup;
	private int second;
	private int min;
	public GameObject pleyer_time_count_sec;
	public GameObject pleyer_time_count_min;

	void Start()
	{
		countup = 0f;
		second = 0;
		min = 0;
	}

	void Update()
	{
		if (FlagManager.Instance.flags[1] == true) //自のアクティブなオブジェクトのみ
		{
			countup += Time.deltaTime;
	//		Debug.Log(countup += Time.deltaTime);
			int second = (int)(countup );
			Time_Count_sec.text = second.ToString("00") + ".";

			int min = (int)(countup * 1000%1000/10);
			Time_Count_min.text = min.ToString("00");
		}
	}
}