using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Countdown : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _textCount_3;
	[SerializeField] private TextMeshProUGUI _textCount_2;
	[SerializeField] private TextMeshProUGUI _textCount_1;
	[SerializeField] private TextMeshProUGUI _Count_go;
	[SerializeField] private TextMeshProUGUI touchStart;
	bool isCalledOnce = false;
	float seconds;

	void Start()
	{
		GameObject gameObject = GameObject.Find("Text (TMP)");
		isCalledOnce = true;
	}

	private void Update()
	{
		seconds += Time.deltaTime;

		if (seconds >= 3)
		{
			isCalledOnce = false;
		}
	}

	public void OnClick()
	{

		if (!isCalledOnce)
		{
			//Debug.Log("タッチしてるでやんす");
			StartCoroutine("CountdownCoroutine");//ダウン開始
			isCalledOnce = true;
		}
	}

	IEnumerator CountdownCoroutine()
	{
		touchStart.gameObject.SetActive(false);
		_textCount_3.gameObject.SetActive(true);
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