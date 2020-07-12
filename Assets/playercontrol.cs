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


	public float max_speed = 999.0f;
	public float Current_speed = 0f;
	[SerializeField] private float speed = 0f;
	[SerializeField] private float max_speed_1st = 0f;//1速の初期最大速度
	[SerializeField] private float max_speed_2st = 0f;//2速の初期最大速度
	[SerializeField] private float max_speed_3st = 0f;//3速の初期最大速度
	[SerializeField] private float max_speed_4st = 0f;//4速の初期最大速度
	[SerializeField] private float max_speed_rowspeed_1st = 0f;//加速度が可変する速度
	[SerializeField] private float max_speed_rowspeed_2st = 0f;
	[SerializeField] private float max_speed_rowspeed_3st = 0f;
	[SerializeField] private float max_speed_rowspeed_st = 0f;
	[SerializeField] private float acceleration_1st = 0f;//1速の加速度
	[SerializeField] private float acceleration_2st = 0f;//2速の加速度
	[SerializeField] private float acceleration_3st = 0f;//3速の加速度
	[SerializeField] private float acceleration_4st = 0f;//4速の加速度
	[SerializeField] private float acceleration_rowspeed_1st = 0f;//ギアの初速
	[SerializeField] private float acceleration_rowspeed_2st = 0f;
	[SerializeField] private float acceleration_rowspeed_3st = 0f;
	[SerializeField] private float acceleration_start = 0f;//スタート時の加速度
	[SerializeField] private float Deceleration_speed = 0.1f;//減速速度
	[SerializeField] private float fillAmount_1st = 0.0f;
	[SerializeField] private float fillAmount_2st = 0.0f;
	[SerializeField] private float fillAmount_3st = 0.0f;
	[SerializeField] private float fillAmount_4st = 0.0f;
	[SerializeField] private float fillAmount_down = 0.0f;
	[SerializeField] private float player_time = 0.0f;//時間のカウント
	[SerializeField] int gear;

	//レベル確認用
	[SerializeField] private int max_speed_lv;//1速の最大速度LV
												  
	[SerializeField] private int acceleration_lv;//1速の加速度LV
	
	[SerializeField] private int acceleration_start_lv;//スタート時の加速度レベル

	[SerializeField] private int fillAmount_lv;//ゲージの増加速度

	public AudioClip sound1;

	bool is_start_dash = false;//1速のフラグ
	[SerializeField]  bool is_Accel = false;//アクセルを離した状態
	[SerializeField]  bool is_Lvup = false;//アクセルを離した状態
	[SerializeField]  bool is_2st_fillAmount = false;//アクセルを離した状態
	AudioSource audioSource;

	private Vector3 touchStartPos;
	private Vector3 touchEndPos;
	string Direction;
	Rigidbody rb;

	void Start()
	{
				//レベルの取得
		max_speed_lv = player_status_manager.Instance.speed_1st_lv　+5;

		audioSource = GetComponent<AudioSource>();
		FlagManager.Instance.flags[0] = true;
		rb = GetComponent<Rigidbody>();
		//image = speed_gauge_Main.GetComponent<Image>();//UIゲージを取得
		//display_speed = display_Speed.GetComponent<TextMeshProUGUI>();//UIのスピード表示を取得
	
		display_speed.text = Current_speed.ToString("000");//初期化
		Current_speed = 0;//初期化
		speed = 0;//初期化
		gear = 1;//初期化
		image.fillAmount = 0;//初期化

		bool is_start_dash = false;
		is_Accel = false;//アクセルのフラグ
		is_Lvup = true;//スター時に現在のレベル再計算
		is_2st_fillAmount = false;//スター時に現在のレベル再計算
		
		speed_gauge_Main.GetComponent <image_blink>().enabled = false; //明滅スクリプトにアクセス

		
	}


	void Update()
    {
		Flick(); //フリック操作
		fillAmount_control();//ゲージのコントロール
		player_time += Time.deltaTime;

		Current_speed = rb.velocity.magnitude * 60 * 60 / 1000;//現在の速度を計算
		display_speed.text = Current_speed.ToString("000"); //速度2Dに情報を渡す


		if (FlagManager.Instance.flags[0] == true)
		{
			audioSource.loop = !audioSource.loop;
			//		is_1st = true;//開始時は必ず1速なので立てておく
		}

		if (FlagManager.Instance.flags[1] == true)
		{

			if (Input.GetMouseButtonUp(0))
			{
				//Debug.Log("離した瞬間");
				is_Accel = false;
			}

			if (Input.GetMouseButton(0))
			{
				Accel(); //アクセル
				is_Accel = true;
			}

		}

		if(is_Accel == false)
		{
			if (0 <= speed)
			{
				rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed -= Deceleration_speed);//自然減速処理
			}
		}

		if(is_Lvup == true)
		{
			Lvup();
		}

	void Lvup()
		{
			Debug.Log("レベルアップ計算");
			max_speed_1st = max_speed_1st + max_speed_lv * 10;
			max_speed_2st = max_speed_2st + max_speed_lv * 10;
			max_speed_3st = max_speed_3st + max_speed_lv * 10;
			max_speed_4st = max_speed_4st + max_speed_lv * 10;
			max_speed_rowspeed_1st = max_speed_1st;
			max_speed_rowspeed_2st = max_speed_2st * 0.8f;
			max_speed_rowspeed_3st = max_speed_3st * 0.8f;
			is_Lvup = false;
		}

	void Accel()
		{
			
			switch (gear)
			{
				case 1:

					if (Current_speed < max_speed_rowspeed_1st)
					{
						speed += acceleration_rowspeed_1st;
						rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
					}
					else if (max_speed_rowspeed_1st <= Current_speed && Current_speed < max_speed_1st)
					{
						speed += acceleration_1st;
						rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
					}
					break;
				case 2:

					if (Current_speed < max_speed_rowspeed_2st)
					{
						rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed += acceleration_rowspeed_2st);
					}
					else if (max_speed_rowspeed_2st <= Current_speed && Current_speed < max_speed_2st)
					{
						rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed += acceleration_2st);
					}
					break;
				case 3:
					if (Current_speed < max_speed_rowspeed_3st)
					{
						rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed += acceleration_rowspeed_3st);
					}
					else if (max_speed_rowspeed_3st <= Current_speed && Current_speed < max_speed_3st)
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
		if (gear == 1 && 0.6f < image.fillAmount)
		{
			gear = 2;
			speed_gauge_Main.GetComponent<image_blink>().enabled = true; //ゲージ明滅スクリプトon
			player_time = 0;//演出時間を更新
		}
		if (gear == 2 && is_2st_fillAmount == true)
		{
			gear = 3;
			speed_gauge_Main.GetComponent<image_blink>().enabled = true; //ゲージ明滅スクリプトon
			player_time = 0;//演出時間を更新
		}

	}
	void fillAmount_control()//ゲージのコントロール
	{
		switch (gear)
		{
			case 1:
				if (is_Accel == false)//アクセル離した状態
				{
					image.fillAmount -= fillAmount_down;
				}
				else if (is_Accel == true)//アクセル踏んでる時にゲージ上昇
			　　//else　if (is_Accel == true && max_speed_rowspeed_1st < Current_speed)//アクセル踏んでる時
				{
					image.fillAmount += fillAmount_1st;
				}
				break;
			case 2:
				if (player_time > 1)
				{
					if (is_Accel == false || Current_speed <= max_speed_rowspeed_2st  )
					{
						image.fillAmount -= fillAmount_down;
					}

					else if (is_Accel == true && max_speed_rowspeed_2st < Current_speed)
					{
						is_2st_fillAmount = true;
						image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);//再上昇で明滅やめ
						speed_gauge_Main.GetComponent<image_blink>().enabled = false;
						image.fillAmount += fillAmount_2st;
					}
				}
			break;
			case 3:
				if (player_time > 1)
				{
					if (is_Accel == false || Current_speed <= max_speed_rowspeed_3st)
					{
						image.fillAmount -= fillAmount_down;
					}

					else if (is_Accel == true && max_speed_rowspeed_3st < Current_speed)
					{
						image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);//処理を抜けたら元に戻す
						speed_gauge_Main.GetComponent<image_blink>().enabled = false;
						image.fillAmount += fillAmount_3st;
					}
				}
			break;
		}
	}
}