using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_move : MonoBehaviour
{

	[SerializeField]  GameObject player;   //プレイヤー情報格納用
    private Vector3 offset;      //相対距離取得用
	float smoothTime = 0.1f;
	Vector3 velocity = Vector3.zero;

	void Start () {
        
        offset = transform.position - player.transform.position;
 
	}
	
	void FixedUpdate() {

		if (FlagManager.Instance.flags[1] == true)
		{
			//新しいトランスフォームの値を代入する
			transform.position = Vector3.Slerp(transform.position , player.transform.position + offset, 0.3f);
		}
	}
}