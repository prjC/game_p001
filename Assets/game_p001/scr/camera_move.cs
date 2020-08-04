using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_move : MonoBehaviour
{

	[SerializeField]  GameObject player;   //プレイヤー情報格納用
	[SerializeField] private Vector3 offset;      //相対距離取得用
	[SerializeField] private Vector3 offset_start;   //開幕のスタートムーヴ
	[SerializeField] private Vector3 camera_pos;      //相対距離取得用
	[SerializeField] private Vector3 player_pos;      //相対距離取得用
	[SerializeField] private 　float smoothTime = 0.1f;
	[SerializeField] private bool isCalledOnce = false;
	Vector3 velocity = Vector3.zero;

	void Start ()
	{
		if (FlagManager.Instance.flags[8] == false)
		{
			isCalledOnce = true;
		}
	}
	
	void Update()
	{
		if (isCalledOnce == true)
		{
			smoothTime += Time.deltaTime * 0.3f;
		transform.position = Vector3.Lerp(offset_start, camera_pos, smoothTime);
			if (transform.position == camera_pos)
			{
				offset = transform.position - player.transform.position;
				FlagManager.Instance.flags[13] = true;
				isCalledOnce = false;
			}
		}

		if (FlagManager.Instance.flags[12] == true)
		{
			smoothTime += Time.deltaTime * 0.3f;
			transform.position = Vector3.Lerp(offset_start, camera_pos, smoothTime);
			if (transform.position == camera_pos)
			{
				offset = transform.position - player.transform.position;
				FlagManager.Instance.flags[12] = false;
				FlagManager.Instance.flags[13] = true;
			}
		}

		if (FlagManager.Instance.flags[1] == true)
		{
			var camera_move_now = transform.position;
			//新しいトランスフォームの値を代入する
			player_pos = player.transform.position + offset;
			var camera_move_pos = Vector3.Slerp(transform.position , player_pos, 0.1f);

			camera_move_now.z = camera_move_pos.z;
			transform.position = camera_move_now;
		}
	}
}