using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _textCount_3;
	[SerializeField] private TextMeshProUGUI _textCount_2;
	[SerializeField] private TextMeshProUGUI _textCount_1;
	[SerializeField] private TextMeshProUGUI _Count_go;
	[SerializeField] private GameObject touchStart;
	[SerializeField] private GameObject custom_fill;//誤タッチ防止
	[SerializeField] private bool isCalledOnce = false;
	float seconds;

	void Start()
	{
		isCalledOnce = true;
	}

	public void OnClickcount()
	{
		custom_fill.gameObject.SetActive(true);
		if (isCalledOnce == true)
		{
			Debug.Log("1回のみのカウントダウン？");
			StartCoroutine("CountdownCoroutine");//ダウン開始
			isCalledOnce = false;
		
		}
	}

	IEnumerator CountdownCoroutine()
	{
		yield return new WaitForSeconds(1.0f);

		FlagManager.Instance.flags[5] = true;//スタートミニゲーム開始
		touchStart.gameObject.SetActive(false);
		_textCount_3.gameObject.SetActive(true);
		FlagManager.Instance.flags[6] = true;//スタートミニゲーム開始
		yield return new WaitForSeconds(1.0f);

		_textCount_3.gameObject.SetActive(false);
		_textCount_2.gameObject.SetActive(true);
		yield return new WaitForSeconds(1.0f);

		_textCount_2.gameObject.SetActive(false);
		_textCount_1.gameObject.SetActive(true);
		yield return new WaitForSeconds(1.0f);

		_textCount_1.gameObject.SetActive(false);
		_Count_go.gameObject.SetActive(true);

		FlagManager.Instance.flags[1] = true;
		yield return new WaitForSeconds(1.0f);
		_Count_go.gameObject.SetActive(false);
	}
}