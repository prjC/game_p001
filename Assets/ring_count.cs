using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//リングの数を数える
public class ring_count : MonoBehaviour
{
	[SerializeField] private GameObject player;//プレイヤーの位置取得
	[SerializeField] private GameObject enemy;//敵の位置取得
	
	[SerializeField] private float ring_speed;//リングゲット時の移動速度
	[SerializeField] private float ring_get_speed;
	[SerializeField] private Vector3 Pos_offset;

	[SerializeField] bool is_get_ring_pl = false;//どちらが獲得したか
	[SerializeField] bool is_get_ring_ene = false;

	private Vector3 Ring_Pos;
	private Vector3 Player_Pos;
	private Vector3 Enemy_Pos;

	void Start()
    {
		is_get_ring_pl = false;//
		is_get_ring_ene = false;
		
		player = GameObject.Find("player");
		enemy = GameObject.FindGameObjectWithTag("Enemy");

		Player_Pos = player.transform.position;
		Enemy_Pos = enemy.transform.position;
		
	}

	void OnTriggerEnter(Collider other)//コインを獲得できるのはプレイヤーか敵
	{

		if (other.CompareTag("player_car"))
		{
			is_get_ring_pl = true;
			GetComponent<BoxCollider>().enabled = false;
		}
		else if (other.CompareTag("Enemy"))
		{
			is_get_ring_ene = true;
			GetComponent<BoxCollider>().enabled = false;
		}

	}
	void Update()
	{
		if (is_get_ring_pl == true)
		{
			
			Ring_Pos = transform.position;
			Player_Pos = player.transform.position;

			ring_get_speed += Time.deltaTime * ring_speed;
			transform.position = Vector3.Lerp(Ring_Pos, Player_Pos + Pos_offset, ring_get_speed);
		
		}

		if (is_get_ring_ene == true)
		{

			Ring_Pos = transform.position;
			Enemy_Pos = enemy.transform.position;

			ring_get_speed += Time.deltaTime * ring_speed;
			transform.position = Vector3.Lerp(Ring_Pos, Enemy_Pos + Pos_offset, ring_get_speed);

		}
	}

}
