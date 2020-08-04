using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class time_count_sec : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI Time_Count_sec = null;//タイマーテキスト
	[SerializeField] private TextMeshProUGUI Time_Count_min = null;//タイマーテキスト
	[SerializeField] private GameObject canvas2D = null;
	[SerializeField] private GameObject pleyer_time_count_sec = null;
	[SerializeField] private GameObject pleyer_time_count_min = null;
	private float countup;
	private int second;
	private int min;
	

	void Start()
	{
		canvas2D = GameObject.Find("Canvas_2D");
		pleyer_time_count_sec = canvas2D.transform.Find("Time_base/Player_Time_Count_sec").gameObject;
		pleyer_time_count_min = canvas2D.transform.Find("Time_base/Player_Time_Count_min").gameObject;
		Time_Count_sec = pleyer_time_count_sec.GetComponent<TextMeshProUGUI>();
		Time_Count_min = pleyer_time_count_min.GetComponent<TextMeshProUGUI>();

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