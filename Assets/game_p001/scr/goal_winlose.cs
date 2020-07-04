using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goal_winlose: MonoBehaviour
{
	public GameObject win;
	public GameObject lose;


	void Start()
    {
		
	}

	void OnTriggerEnter(Collider other)

	{
		if (other.CompareTag("player_car"))
		{
			FlagManager.Instance.flags[1] = false;//ゲーム中フラグを折る

			if (FlagManager.Instance.flags[2] == false)
			{
			FlagManager.Instance.flags[2] = true;//勝敗判定フラグ
			Debug.Log("プレイヤーの勝ち");
			win.SetActive(true);
			}
		}
		if (other.CompareTag("Enemy"))
		{
			if (FlagManager.Instance.flags[2] == false)
			{
				Debug.Log("敵の勝ち");
			lose.SetActive(true);
			FlagManager.Instance.flags[2] = true;//勝敗判定フラグ
			}
		}
	}
}
