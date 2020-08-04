using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goal_winlose: MonoBehaviour
{
	public GameObject win;
	public GameObject lose;

	void OnTriggerEnter(Collider other)

	{
		if (other.CompareTag("player_car"))
		{
			FlagManager.Instance.flags[1] = false;//ゲーム中フラグを折る

			if (FlagManager.Instance.flags[2] == false)
			{
			Debug.Log("プレイヤーの勝ち");
			win.SetActive(true);
			FlagManager.Instance.flags[3] = true;//勝利フラグ
			FlagManager.Instance.flags[2] = true;//勝敗判定後フラグ
			player_status_manager.Instance.Save();
			}
		}
		if (other.CompareTag("Enemy"))
		{
			FlagManager.Instance.flags[1] = false;//ゲーム中フラグを折る

			if (FlagManager.Instance.flags[2] == false)
			{
				Debug.Log("敵の勝ち");
			lose.SetActive(true);
			FlagManager.Instance.flags[4] = true;//敗北フラグ
			FlagManager.Instance.flags[2] = true;//勝敗判定フラグ
			player_status_manager.Instance.Save();
			}
		}
	}
}
