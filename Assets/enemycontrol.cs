using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemycontrol : MonoBehaviour
{
	[SerializeField] private float Enemy_Current_speed = 0f;
	[SerializeField] private float Enemy_speed = 0f;

	[SerializeField] public float Ene_max_speed_1st = 0f;//1速の最大速度
	[SerializeField] public float Ene_max_speed_2st = 0f;//2速の最大速度
	[SerializeField] public float Ene_max_speed_3st = 0f;//3速の最大速度
	[SerializeField] public float Ene_max_speed_rowspeed_1st = 0f;//加速度が可変する速度
	[SerializeField] public float Ene_max_speed_rowspeed_2st = 0f;
	[SerializeField] public float Ene_max_speed_rowspeed_3st = 0f;
	[SerializeField] public float Ene_acceleration_1st = 0f;//1速の加速度
	[SerializeField] public float Ene_acceleration_2st = 0f;//2速の加速度
	[SerializeField] public float Ene_acceleration_3st = 0f;//3速の加速度
	[SerializeField] public float Ene_acceleration_rowspeed_1st = 0f;//ギアの初速
	[SerializeField] public float Ene_acceleration_rowspeed_2st = 0f;
	[SerializeField] public float Ene_acceleration_rowspeed_3st = 0f;
	[SerializeField] public float Ene_acceleration_start = 0f;//スタート時の加速度
	[SerializeField] int Ene_gear;
	[SerializeField] private float Ene_Deceleration_speed = 0.1f;//減速速度
	[SerializeField] private float enemy_time;//

	Rigidbody ene_rb;

	// Start is called before the first frame update
	void Start()
    {
		ene_rb = GetComponent<Rigidbody>();
		Ene_gear = 1;
	}

    // Update is called once per frame
    void Update()
    {
		enemy_time += Time.deltaTime;

		if (FlagManager.Instance.flags[1] == true)
		{
			Enemy_Current_speed = ene_rb.velocity.magnitude * 60 * 60 / 1000;
			Accel(); //アクセル
		}
	}

	void Accel()
	{

		switch (Ene_gear)
		{
		case 1:

			if (Enemy_Current_speed < Ene_max_speed_rowspeed_1st)
			{
				ene_rb.velocity = new Vector3(ene_rb.velocity.x, ene_rb.velocity.y, Enemy_speed += Ene_acceleration_rowspeed_1st);
			}
			else if (Enemy_Current_speed < Ene_max_speed_1st)
			{
				ene_rb.velocity = new Vector3(ene_rb.velocity.x, ene_rb.velocity.y, Enemy_speed += Ene_acceleration_1st);
			}
			break;
		case 2:

			if (Enemy_Current_speed < Ene_max_speed_rowspeed_2st)
			{
				ene_rb.velocity = new Vector3(ene_rb.velocity.x, ene_rb.velocity.y, Enemy_speed += Ene_acceleration_rowspeed_2st);
			}
			else if (Enemy_Current_speed < Ene_max_speed_2st)
			{
				ene_rb.velocity = new Vector3(ene_rb.velocity.x, ene_rb.velocity.y, Enemy_speed += Ene_acceleration_2st);
			}
			break;
		case 3:
			if (Enemy_Current_speed < Ene_max_speed_rowspeed_3st)
			{
				ene_rb.velocity = new Vector3(ene_rb.velocity.x, ene_rb.velocity.y, Enemy_speed += Ene_acceleration_rowspeed_3st);
			}
			else if (Enemy_Current_speed < Ene_max_speed_3st)
			{
				ene_rb.velocity = new Vector3(ene_rb.velocity.x, ene_rb.velocity.y, Enemy_speed += Ene_acceleration_3st);
			}
			break;
		}
	}
}
