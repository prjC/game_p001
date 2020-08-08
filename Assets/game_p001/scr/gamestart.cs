using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamestart : MonoBehaviour
{
	GameObject anotherObject;
	[SerializeField] private FadeController anotherScript;
	[SerializeField] int Current_player_stage;

	bool is_start_fadein = false;
	float seconds;

	void Start()
	{
		
		GameObject anotherObject = GameObject.Find("fade_panel");
		anotherScript = anotherObject.GetComponent<FadeController>();
		//	fade_Panel = GameObject.Find(fade_panel);
		is_start_fadein = true;
		Current_player_stage = player_status_manager.Instance.Player_stage;//レベルの取得
		if (is_start_fadein == true)
		{
			SetFadein();
		}

	}
	void Update()
	{
		//if(Current_player_stage==0)
		//{
		//Debug.Log("0だった？");
		//Current_player_stage = 1;
		//player_status_manager.Instance.Player_stage = 1;
		//}

		seconds += Time.deltaTime;

		if (seconds >= 5)
		{
			is_start_fadein = false;
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
	}

	private void LoadScene()
	{
		if (seconds >= 10)
		{
			SceneManager.LoadScene(Current_player_stage);
		}
	}
}
