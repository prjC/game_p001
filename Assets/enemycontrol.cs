using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemycontrol : MonoBehaviour
{

	[SerializeField] private GameObject get_eff;

	[SerializeField] private float Enemy_Current_speed = 0f;
	[SerializeField] private float Enemy_speed = 0f;
	[SerializeField] private float Enemy_torque = 0f;//ギアコントロールの入れ物
	[SerializeField] int Ene_gear;
	[SerializeField] private float Ene_max_speed_1st = 0f;//1速の最大速度
	[SerializeField] private float Ene_max_speed_2st = 0f;//2速の最大速度
	[SerializeField] private float Ene_max_speed_3st = 0f;//3速の最大速度
	[SerializeField] private float Ene_max_speed_4st = 0f;
	[SerializeField] private float Ene_max_speed_rowspeed_1st = 0f;//加速度が可変する速度
	[SerializeField] private float Ene_max_speed_rowspeed_2st = 0f;
	[SerializeField] private float Ene_max_speed_rowspeed_3st = 0f;
	[SerializeField] private float Ene_max_speed_rowspeed_4st = 0f;
	[SerializeField] private float Ene_acceleration_1st = 0f;//1速の加速度
	[SerializeField] private float Ene_acceleration_2st = 0f;//2速の加速度
	[SerializeField] private float Ene_acceleration_3st = 0f;//3速の加速度
	[SerializeField] private float Ene_acceleration_4st = 0f;//3速の加速度
	[SerializeField] private float Ene_acceleration_rowspeed_1st = 0f;//ギアの初速
	[SerializeField] private float Ene_acceleration_rowspeed_2st = 0f;
	[SerializeField] private float Ene_acceleration_rowspeed_3st = 0f;
	[SerializeField] private float Ene_acceleration_rowspeed_4st = 0f;
	[SerializeField] private float Ene_acceleration_start = 0f;//スタート時の加速度
	[SerializeField] private float Ene_torque_1st = 0f;//内部的なギアコントロール
	[SerializeField] private float Ene_torque_2st = 0f;
	[SerializeField] private float Ene_torque_3st = 0f;
	[SerializeField] private float Ene_torque_4st = 0f;
	[SerializeField] private float Ene_Deceleration_speed = 0.1f;//減速速度
	[SerializeField] float Enemy_AI_range_gear_2st;
	[SerializeField] float Enemy_AI_range_gear_3st;
	[SerializeField] float Enemy_AI_range_gear_4st;
	[SerializeField] int start_dash_num;
	[SerializeField] bool is_Enemy_change_gear_great_2st = false;//スピードゲージのタイミング成功
	[SerializeField] bool is_Enemy_change_gear_great_3st = false;//スピードゲージのタイミング成功
	[SerializeField] bool is_Enemy_change_gear_great_4st = false;//スピードゲージのタイミング成功

	//敵のレベルと基礎値
	[SerializeField] int start_dash_success;
	[SerializeField] float Enemy_AI_range_max;
	[SerializeField] float Enemy_AI_range_min;
	[SerializeField] private float Ene_max_speed_lv = 0f;//1速の最大速度
	[SerializeField] private float Ene_acceleration_lv = 0f;//1速の最大速度
	[SerializeField] private float Ene_torque_lv = 0f;//1速の最大速度
	[SerializeField] private float Ene_start_dash_lv = 0f;//1速の最大速度

	[SerializeField] private float Ene_max_speed_1st_base = 0f;//2速の最大速度
	[SerializeField] private float Ene_max_speed_2st_base = 0f;//3速の最大速度
	[SerializeField] private float Ene_max_speed_3st_base = 0f;
	[SerializeField] private float Ene_max_speed_4st_base = 0f;
	[SerializeField] private float Ene_acceleration_1st_base = 0f;//1速の加速度
	[SerializeField] private float Ene_acceleration_2st_base = 0f;//2速の加速度
	[SerializeField] private float Ene_acceleration_3st_base = 0f;//3速の加速度
	[SerializeField] private float Ene_acceleration_4st_base = 0f;
	[SerializeField] private float Ene_torque_1st_base = 0f;//内部的なギアコントロール
	[SerializeField] private float Ene_torque_2st_base = 0f;
	[SerializeField] private float Ene_torque_3st_base = 0f;
	[SerializeField] private float Ene_torque_4st_base = 0f;
	[SerializeField] private float Ene_start_dash_base = 0f;

	Rigidbody ene_rb;

	void Start()
    {
		int game_clear = player_status_manager.Instance.Game_clear;

		if (game_clear == 1)
		{
			Ene_max_speed_lv = Ene_max_speed_lv  + 20;
			Ene_start_dash_lv = Ene_start_dash_lv + 20;
			Ene_torque_lv = Ene_torque_lv + 20;
			Ene_acceleration_lv = Ene_acceleration_lv + 20;
		}
		ene_rb = GetComponent<Rigidbody>();
		Ene_gear = 1;
		enemy_status(); //アクセル
		enemy_renge(); 
	}
	void enemy_renge()
	{
		start_dash_num = Random.Range(0, 100);
		Enemy_AI_range_gear_2st = Random.Range(Enemy_AI_range_min, Enemy_AI_range_max);
		Enemy_AI_range_gear_3st = Random.Range(Enemy_AI_range_min, Enemy_AI_range_max);
		Enemy_AI_range_gear_4st = Random.Range(Enemy_AI_range_min, Enemy_AI_range_max);

		if (0.7f <= Enemy_AI_range_gear_2st && Enemy_AI_range_gear_2st <= 0.8f)
		{
			is_Enemy_change_gear_great_2st = true;
		}
		if (0.7f <= Enemy_AI_range_gear_3st && Enemy_AI_range_gear_3st <= 0.8f)
		{
			is_Enemy_change_gear_great_3st = true;
		}
		if (0.7f <= Enemy_AI_range_gear_4st && Enemy_AI_range_gear_4st <= 0.8f)
		{
			is_Enemy_change_gear_great_4st = true;
		}
	}
	void enemy_status()
	{
			Ene_max_speed_1st = Ene_max_speed_1st_base + Ene_max_speed_lv * 1;
			Ene_max_speed_2st = Ene_max_speed_2st_base + Ene_max_speed_lv * 3;
			Ene_max_speed_3st = Ene_max_speed_3st_base + Ene_max_speed_lv * 5;
			Ene_max_speed_4st = Ene_max_speed_4st_base + Ene_max_speed_lv * 7;
			Ene_acceleration_1st = Ene_acceleration_1st_base + (Ene_start_dash_lv + Ene_acceleration_lv) * 0.005f;
			Ene_acceleration_2st = Ene_acceleration_2st_base + (Ene_start_dash_lv + Ene_acceleration_lv) * 0.006f;
			Ene_acceleration_3st = Ene_acceleration_3st_base + Ene_acceleration_lv * 0.015f;
			Ene_acceleration_4st = Ene_acceleration_4st_base + Ene_acceleration_lv * 0.020f;
			Ene_torque_1st =  Ene_torque_1st_base +  Ene_torque_lv* 0.001f;
			Ene_torque_2st =  Ene_torque_2st_base +  Ene_torque_lv* 0.001f;
			Ene_torque_3st =  Ene_torque_3st_base +  Ene_torque_lv* 0.001f;
			Ene_torque_4st =  Ene_torque_4st_base +  Ene_torque_lv* 0.001f;
			Ene_acceleration_start = Ene_start_dash_base + Ene_start_dash_lv * 0.01f;//スタート時の加速度

		//低トルク状態
		Ene_max_speed_rowspeed_1st = Ene_max_speed_1st * 0.5f;
			Ene_max_speed_rowspeed_2st = Ene_max_speed_2st * 0.6f;
			Ene_max_speed_rowspeed_3st = Ene_max_speed_3st * 0.7f;
			Ene_max_speed_rowspeed_4st = Ene_max_speed_4st * 0.8f;
			Ene_acceleration_rowspeed_1st = Ene_acceleration_1st * 0.2f;
			Ene_acceleration_rowspeed_2st = Ene_acceleration_2st * 0.2f;
			Ene_acceleration_rowspeed_3st = Ene_acceleration_3st * 0.2f;
			Ene_acceleration_rowspeed_4st = Ene_acceleration_4st * 0.2f;
	}

	void FixedUpdate()
    {
		Enemy_Current_speed = ene_rb.velocity.magnitude * 60 * 60 / 1000;

		if (FlagManager.Instance.flags[1] == true)
		{
			Accel(); //アクセル
			gear(); //アクセル
		}
		
		if (FlagManager.Instance.flags[2] == true)
		{
			Accel(); //アクセル
		}
	}

	void Accel()
	{

		switch (Ene_gear)
		{
		case 1:
			if (Ene_max_speed_1st > Enemy_Current_speed)
			{
				if (start_dash_success < start_dash_num)
				{
					if (Enemy_torque <= 0.4f)
					{
						Enemy_speed += Ene_acceleration_rowspeed_1st;
						ene_rb.velocity = new Vector3(ene_rb.velocity.x, ene_rb.velocity.y, Enemy_speed);
					}
					else if (0.4f < Enemy_torque)
					{
						Enemy_speed += Ene_acceleration_1st;
						ene_rb.velocity = new Vector3(ene_rb.velocity.x, ene_rb.velocity.y, Enemy_speed);

					}
				}
				else if (start_dash_num <= start_dash_success)
				{
					if (Ene_max_speed_1st > Enemy_Current_speed)
					{
						Enemy_speed += Ene_acceleration_start;
						ene_rb.velocity = new Vector3(ene_rb.velocity.x, ene_rb.velocity.y, Enemy_speed);
					}
				}
			}
			else if (Ene_max_speed_1st <= Enemy_Current_speed)
			{
				Enemy_speed -= Ene_Deceleration_speed;
				ene_rb.velocity = new Vector3(ene_rb.velocity.x, ene_rb.velocity.y, Enemy_speed);
			}
			break;
		case 2:
			if (Ene_max_speed_2st > Enemy_Current_speed)
			{
				if (is_Enemy_change_gear_great_2st == false)
				{
					if (Ene_torque_2st <= 0.4f)
					{
						Enemy_speed += Ene_acceleration_rowspeed_2st;
					ene_rb.velocity = new Vector3(ene_rb.velocity.x, ene_rb.velocity.y, Enemy_speed);
					}
					else if (0.4f < Ene_torque_2st)
					{
						Enemy_speed += Ene_acceleration_2st;
						ene_rb.velocity = new Vector3(ene_rb.velocity.x, ene_rb.velocity.y, Enemy_speed);
					}
				}
				else if (is_Enemy_change_gear_great_2st == true)
				{
					Enemy_speed += Ene_acceleration_2st;
					ene_rb.velocity = new Vector3(ene_rb.velocity.x, ene_rb.velocity.y, Enemy_speed);
				}
			}
			else if (Ene_max_speed_2st <= Enemy_Current_speed)
			{
				Enemy_speed -= Ene_Deceleration_speed;
				ene_rb.velocity = new Vector3(ene_rb.velocity.x, ene_rb.velocity.y, Enemy_speed);
			}
				break;
		case 3:
				if (Ene_max_speed_3st > Enemy_Current_speed)
				{
					if (is_Enemy_change_gear_great_3st == false)
					{
						if (Ene_torque_3st <= 0.4f)
						{
							Enemy_speed += Ene_acceleration_rowspeed_3st;
							ene_rb.velocity = new Vector3(ene_rb.velocity.x, ene_rb.velocity.y, Enemy_speed);
						}
						else if (0.4f < Ene_torque_3st)
						{
							Enemy_speed += Ene_acceleration_3st;
							ene_rb.velocity = new Vector3(ene_rb.velocity.x, ene_rb.velocity.y, Enemy_speed);
						}
					}
					else if (is_Enemy_change_gear_great_3st == true)
					{
						Enemy_speed += Ene_acceleration_3st;
						ene_rb.velocity = new Vector3(ene_rb.velocity.x, ene_rb.velocity.y, Enemy_speed);
					}
				}
				else if (Ene_max_speed_3st <= Enemy_Current_speed)
				{
					Enemy_speed -= Ene_Deceleration_speed;
					ene_rb.velocity = new Vector3(ene_rb.velocity.x, ene_rb.velocity.y, Enemy_speed);
				}
				break;
		case 4:
				if (Ene_max_speed_4st > Enemy_Current_speed)
				{
					if (is_Enemy_change_gear_great_4st == false)
					{
						if (Ene_torque_4st <= 0.4f)
						{
							Enemy_speed += Ene_acceleration_rowspeed_4st;
							ene_rb.velocity = new Vector3(ene_rb.velocity.x, ene_rb.velocity.y, Enemy_speed);
						}
						else if (0.4f < Ene_torque_4st)
						{
							Enemy_speed += Ene_acceleration_4st;
							ene_rb.velocity = new Vector3(ene_rb.velocity.x, ene_rb.velocity.y, Enemy_speed);
						}
					}
					else if (is_Enemy_change_gear_great_4st == true)
					{
						Enemy_speed += Ene_acceleration_4st;
						ene_rb.velocity = new Vector3(ene_rb.velocity.x, ene_rb.velocity.y, Enemy_speed);
					}
				}
				else if (Ene_max_speed_4st <= Enemy_Current_speed)
				{
					Enemy_speed -= Ene_Deceleration_speed;
					ene_rb.velocity = new Vector3(ene_rb.velocity.x, ene_rb.velocity.y, Enemy_speed);
				}
				break;
		}
	}

	void gear()
	{

		switch (Ene_gear)
		{
		case 1:
			Enemy_torque += Ene_torque_1st;

				if (Enemy_AI_range_gear_2st <= Enemy_torque)
				{
					Ene_gear = 2;
					Enemy_torque = 0;
				}
			break;
		case 2:
			Enemy_torque += Ene_torque_2st;

				if (Enemy_AI_range_gear_3st <= Enemy_torque)
				{
					
					Ene_gear = 3;
					Enemy_torque = 0;
				}
			break;
		case 3:
			Enemy_torque += Ene_torque_3st;

				if (Enemy_AI_range_gear_4st <= Enemy_torque)
				{
					Ene_gear = 4;
					Enemy_torque = 0;

				}
			break;
	
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("item_coin"))
		{
			StartCoroutine("Ring_get_eff");
		}
	}
	IEnumerator Ring_get_eff()
	{
		get_eff.gameObject.SetActive(true);

		yield return new WaitForSeconds(1f);

		get_eff.gameObject.SetActive(false);
		yield return null;
	}
}
