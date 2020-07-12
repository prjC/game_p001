using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamestart : MonoBehaviour
{
	GameObject anotherObject;
	[SerializeField] private FadeController anotherScript;

	bool is_start_fadein = false;
	float seconds;

	void Start()
	{
		GameObject anotherObject = GameObject.Find("fade_panel");
		anotherScript = anotherObject.GetComponent<FadeController>();
		//	fade_Panel = GameObject.Find(fade_panel);
		is_start_fadein = true;

	}
	void Update()
	{
		if (is_start_fadein == true)
		{
			SetFadein();
		}

		seconds += Time.deltaTime;

		if (seconds >= 5)
		{
			Setbool();
			LoadScene();
		}
	}
	private void Setbool()
	{
		anotherScript.SetFadeOut();
	}

	private void SetFadein()
	{
		anotherScript.SetFadeIn();
		is_start_fadein = false;
	}

	private void LoadScene()
	{
		if (seconds >= 10)
		{
			SceneManager.LoadScene("stage01");
		}
	}
}
