using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//初期パラメーターまとめ用なら使える
//今回はいらなそう、RPG級のデータ量なら必要
[CreateAssetMenu]
public class Player_Status_Data : ScriptableObject
{
	public List<Player_Status> PlayerStatusList = new List<Player_Status>();
}

[System.Serializable]
public class Player_Status
{
	[SerializeField]
	int TopSpeed_lv_Current = 1;

	public int topSpeed_lv_Current
	{
		get
		{
			return TopSpeed_lv_Current;
		}
	}
}