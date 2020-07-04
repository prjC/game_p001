using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playercontrol : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI display_speed;//タイマーテキスト
	public GameObject display_Speed;
	public Image image;//スピードゲージ
	public GameObject speed_gauge_Main;//スピードゲージ
	public GameObject player;

	public float speed = 0f;
	public float max_speed = 999.0f;
	public float Current_speed = 0f;

	[SerializeField] 　public float max_speed_1st = 0f;//1速の最大速度
	[SerializeField] 　public float max_speed_2st = 0f;//2速の最大速度
	[SerializeField] 　public float max_speed_3st = 0f;//3速の最大速度
	[SerializeField]　 public float max_speed_rowspeed_1st = 0f;//加速度が可変する速度
	[SerializeField]   public float max_speed_rowspeed_2st = 0f;
	[SerializeField]   public float max_speed_rowspeed_3st = 0f;
	[SerializeField]   public float acceleration_1st = 0f;//1速の加速度
	[SerializeField]   public float acceleration_2st = 0f;//2速の加速度
	[SerializeField]   public float acceleration_3st = 0f;//3速の加速度
	[SerializeField]   public float acceleration_rowspeed_1st = 0f;//ギアの初速
	[SerializeField]   public float acceleration_rowspeed_2st = 0f;
	[SerializeField]   public float acceleration_rowspeed_3st = 0f;
	[SerializeField] 　public float acceleration_start = 0f;//スタート時の加速度
	[SerializeField]   int gear;
	[SerializeField]   public float Deceleration_speed = 0.1f;//減速速度
	[SerializeField] 　public float fillAmount_1st = 0.0f;
	[SerializeField]　 public float fillAmount_2st = 0.0f;
	[SerializeField]   public float fillAmount_3st = 0.0f;
	[SerializeField]   public float fillAmount_down = 0.0f;
	public AudioClip sound1;

	bool is_start_dash = false;//1速のフラグ
	[SerializeField]  bool is_move = false;//アクセルを離した状態
	AudioSource audioSource;

	private Vector3 touchStartPos;
	private Vector3 touchEndPos;
	string Direction;
	Rigidbody rb;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		FlagManager.Instance.flags[0] = true;
		rb = GetComponent<Rigidbody>();
		//image = speed_gauge_Main.GetComponent<Image>();//UIゲージを取得
		//display_speed = display_Speed.GetComponent<TextMeshProUGUI>();//UIのスピード表示を取得
	
		display_speed.text = Current_speed.ToString("000");
		Current_speed = 0;
		max_speed_rowspeed_1st = max_speed_1st * 0.7f;
		max_speed_rowspeed_2st = max_speed_2st * 0.8f;
		max_speed_rowspeed_3st = max_speed_3st * 0.8f;
		gear = 1;
		image.fillAmount = 0;//初期化

		//開始時にフラグを初期化
		bool is_start_dash = false;
		is_move = true;

	}


	void Update()
    {
		Flick(); //フリック操作
		Current_speed = rb.velocity.magnitude * 60 * 60 / 1000;//現在の速度を計算
		display_speed.text = Current_speed.ToString("000"); //速度2Dに情報を渡す

		fillAmount_control();

		if (FlagManager.Instance.flags[0] == true)
		{
			audioSource.loop = !audioSource.loop;
			//		is_1st = true;//開始時は必ず1速なので立てておく
		}

		if (FlagManager.Instance.flags[1] == true)
		{

			if (Input.GetMouseButtonUp(0))
			{
				Debug.Log("離した瞬間");
				is_move = true;
			}

			if (Input.GetMouseButton(0))
			{
				Accel(); //アクセル
				is_move = false;
			}

		}

		if(is_move == true)
		{
			if (0 <= speed)
			{
				rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed -= Deceleration_speed);//自然減速処理
			}
		}

	void Accel()
		{
			
			switch (gear)
			{
				case 1:

					if (Current_speed < max_speed_rowspeed_1st)
					{
						rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed += acceleration_rowspeed_1st);
					}
					else if (Current_speed < max_speed_1st)
					{
						rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed += acceleration_1st);
					}
					break;
				case 2:

					if (Current_speed < max_speed_rowspeed_2st)
					{
						rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed += acceleration_rowspeed_2st);
					}
					else if (Current_speed < max_speed_2st)
					{
						rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed += acceleration_2st);
					}
					break;
				case 3:
					if (Current_speed < max_speed_rowspeed_3st)
					{
						rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed += acceleration_rowspeed_3st);
					}
					else if (Current_speed < max_speed_3st)
					{
						rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed += acceleration_3st);
					}
					break;
			}
		}
	}

	void Flick()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			touchStartPos = new Vector3(Input.mousePosition.x,
									Input.mousePosition.y,
									Input.mousePosition.z);
		}

		if (Input.GetKeyUp(KeyCode.Mouse0))
		{
			touchEndPos = new Vector3(Input.mousePosition.x,
									  Input.mousePosition.y,
									  Input.mousePosition.z);
			GetDirection();
		}
	}
	void GetDirection()
	{
		float directionX = touchEndPos.x - touchStartPos.x;
		float directionY = touchEndPos.y - touchStartPos.y;
		string Direction;

		if (Mathf.Abs(directionY) < Mathf.Abs(directionX))
		{
			if (30 < directionX)
			{
				//右向きにフリック
				Direction = "right";
			//	Debug.Log("右向きにフリック");
				Gearchange();
			}
			else if (-30 > directionX)
			{
				//左向きにフリック
				Direction = "left";
				//Debug.Log("左向きにフリック");
				Gearchange();
			}
		}
	}
	void Gearchange()//ギアを変えるんだってばよ
	{
		if (gear == 1 && max_speed_rowspeed_1st< Current_speed)
		{
			gear = 2;
		}
		if (gear == 2 && max_speed_rowspeed_2st < Current_speed)
		{
			gear = 3;
		}

	}
	void fillAmount_control()
	{
		switch (gear)
		{
			case 1:
				if (is_move == true )//アクセル離した状態
				{
					image.fillAmount -= fillAmount_down;
				}
				if (is_move == false && max_speed_rowspeed_1st < Current_speed)//アクセル踏んでる時
				{
					image.fillAmount += fillAmount_1st;
				}
				break;
			case 2:
				if (is_move == true || Current_speed <= max_speed_rowspeed_2st  )
				{
					image.fillAmount -= fillAmount_down;
				}

				if (is_move == false && max_speed_rowspeed_2st < Current_speed)
				{
					image.fillAmount += fillAmount_2st;
				}
				break;
			case 3:
				if (is_move == true || Current_speed <= max_speed_rowspeed_3st)
				{
					image.fillAmount -= fillAmount_down;
				}

				if (is_move == false && max_speed_rowspeed_3st < Current_speed)
				{
					image.fillAmount += fillAmount_3st;
				}
				break;
		}
	}
}