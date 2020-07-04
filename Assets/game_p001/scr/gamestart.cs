using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamestart : MonoBehaviour
{

	float seconds;
	GameObject anotherObject;
	[SerializeField] private FadeController anotherScript;

	void Start()
	{
		GameObject anotherObject = GameObject.Find("fade_panel");
		anotherScript = anotherObject.GetComponent<FadeController>();
		//	fade_Panel = GameObject.Find(fade_panel);

	}
	void Update()
	{
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

	private void LoadScene()
	{
		if (seconds >= 10)
		{
			SceneManager.LoadScene("stage01");
		}
	}
}
