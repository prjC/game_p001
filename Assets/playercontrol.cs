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
	public GameObject swipe;//tuto
	public GameObject speed_gauge_Main;
	//プレイヤー
	public GameObject player;
	//ミニゲーム用オブジェクト
	public GameObject start_bar;
	public GameObject start_bar_point;
	public Image Great;
	public Image start_touch;
	//ギアゲージ用オブジェクト
	public Image Great_speed;
	public TextMeshProUGUI Gear_1st;
	public TextMeshProUGUI Gear_2st;
	public TextMeshProUGUI Gear_3st;
	public TextMeshProUGUI Gear_top;
	//カスタマイズボタン
	public GameObject customize;
	//エフェクト
	public GameObject get_eff;
	public GameObject smoke;
	//タッチアニメ
	public　Image　touch_anim;
	//滞在ステージ
	[SerializeField] private TextMeshProUGUI stage_Count;

	public float max_speed = 999.0f;
	public float Current_speed = 0f;
	[SerializeField] private TextMeshProUGUI ring_Count;//リングに反映
	[SerializeField] private GameObject custom_fill;//誤タッチ防止

	[SerializeField] private float speed = 0f;
	//現在のステータス
	[SerializeField] private float max_speed_1st = 0f;//1速の初期最大速度
	[SerializeField] private float max_speed_2st = 0f;//2速の初期最大速度
	[SerializeField] private float max_speed_3st = 0f;//3速の初期最大速度
	[SerializeField] private float max_speed_4st = 0f;//4速の初期最大速度
	[SerializeField] private float max_speed_rowspeed_1st = 0f;//加速度が可変する速度
	[SerializeField] private float max_speed_rowspeed_2st = 0f;
	[SerializeField] private float max_speed_rowspeed_3st = 0f;
	[SerializeField] private float max_speed_rowspeed_4st = 0f;
	[SerializeField] private float acceleration_1st = 0f;//1速の加速度
	[SerializeField] private float acceleration_2st = 0f;//2速の加速度
	[SerializeField] private float acceleration_3st = 0f;//3速の加速度
	[SerializeField] private float acceleration_4st = 0f;//4速の加速度
	[SerializeField] private float acceleration_rowspeed_1st = 0f;
	[SerializeField] private float acceleration_rowspeed_2st = 0f;
	[SerializeField] private float acceleration_rowspeed_3st = 0f;
	[SerializeField] private float acceleration_rowspeed_4st = 0f;
	[SerializeField] private float acceleration_start = 0f;//スタート時の加速度
	[SerializeField] private float Deceleration_speed = 0.1f;//減速速度
	[SerializeField] private float fillAmount_1st = 0.0f;
	[SerializeField] private float fillAmount_2st = 0.0f;
	[SerializeField] private float fillAmount_3st = 0.0f;
	[SerializeField] private float fillAmount_4st = 0.0f;
	//初期ステータス
	[SerializeField] private float max_speed_1st_base = 0f;//1速の初期最大速度
	[SerializeField] private float max_speed_2st_base = 0f;//2速の初期最大速度
	[SerializeField] private float max_speed_3st_base = 0f;//3速の初期最大速度
	[SerializeField] private float max_speed_4st_base = 0f;//4速の初期最大速度
	[SerializeField] private float acceleration_1st_base = 0f;//1速の加速度
	[SerializeField] private float acceleration_2st_base = 0f;//2速の加速度
	[SerializeField] private float acceleration_3st_base = 0f;//3速の加速度
	[SerializeField] private float acceleration_4st_base = 0f;//4速の加速度
	[SerializeField] private float acceleration_start_base = 0f;//スタート時の加速度
	[SerializeField] private float fillAmount_1st_base = 0.0f;
	[SerializeField] private float fillAmount_2st_base = 0.0f;
	[SerializeField] private float fillAmount_3st_base = 0.0f;
	[SerializeField] private float fillAmount_4st_base = 0.0f;
	[SerializeField] private float Start_dash_fillAmount_base = 0f;
	[SerializeField] private float Start_dash_speed_base = 0f;

	//その他
	[SerializeField] private float fillAmount_down = 0.0f;
	[SerializeField] private float player_time = 0.0f;//時間のカウント
	[SerializeField] int gear;

	//レベル確認用
	[SerializeField] private int max_speed_lv;//1速の最大速度LV											  
	[SerializeField] private int acceleration_lv;//1速の加速度LV
	[SerializeField] private int start_dash_lv;//スタート時の加速度レベル
	[SerializeField] private int fillAmount_lv;//ゲージの増加速度

	//リング
	[SerializeField] private int Current_ring;//リングの所持数			  
	[SerializeField] private int get_ring;//リングの加算数

	//滞在ステージ
	[SerializeField] private int Current_stage;

	//レベルアップ計算
	[SerializeField] private int player_max_lv;
	[SerializeField] private int player_max_lv_base = 5;
	
	[SerializeField]  bool is_start_dash_R = false;//スタートダッシュゲーム用フラグ
	[SerializeField]  bool is_start_dash_L = false;//スタートダッシュゲーム用フラグ
	[SerializeField]  bool is_Accel = false;//アクセル状態？
	[SerializeField]  bool is_change_fillAmount_to_2st = false;//ギアチェンジの制御
	[SerializeField]  bool is_change_fillAmount_to_3st = false;
	[SerializeField]  bool is_change_fillAmount_to_4st = false;
	[SerializeField]  bool is_change_gear_2st = false;
	[SerializeField]  bool is_change_gear_3st = false;
	[SerializeField]  bool is_change_gear_4st = false;
	[SerializeField]  bool is_change_gear_great_2st = false;//スピードゲージのタイミング成功
	[SerializeField]　bool is_change_gear_great_3st = false;//スピードゲージのタイミング成功
	[SerializeField]  bool is_change_gear_great_4st = false;//スピードゲージのタイミング成功

	//SE 関連
	[SerializeField] public AudioClip sound1;
	[SerializeField] public AudioClip sound2;
	[SerializeField] public AudioClip sound3;
	[SerializeField] public AudioClip sound4;
	[SerializeField] public AudioClip sound5;
	[SerializeField] public AudioClip sound6;
	[SerializeField] public AudioClip sound7;
	[SerializeField] bool is_oneshot_se01 = true;
	[SerializeField] bool is_once_se_row01 = true;
	[SerializeField] bool is_once_se_row02 = true;
	[SerializeField] bool is_once_se_high01 = true;
	[SerializeField] bool is_once_se_high02 = true;
	[SerializeField] bool is_once_se_idle = true;
	[SerializeField] bool is_once_se_coin = true;
	[SerializeField] bool is_once_se_coin_get = true;

	[SerializeField] AudioSource audioSource;
	[SerializeField] AudioSource audioSource02;
	[SerializeField] AudioSource audioSource03;
	[SerializeField] AudioSource audioSource04;
	[SerializeField] AudioSource audioSource05;

	private Vector3 touchStartPos;
	private Vector3 touchEndPos;
	string Direction;
	Rigidbody rb;

	//スタートダッシュ用ミニゲーム
	[SerializeField] bool is_Start_bar_in=  false;
	[SerializeField] private float start_bar_speed;
	[SerializeField] private Vector2 Start_bar_point_start;
	[SerializeField] private Vector2 Start_bar_point_end;
	[SerializeField] private Vector2 Start_bar_point;
	[SerializeField] private float Start_bar_now_r;
	[SerializeField] private float Start_bar_now_l;
	[SerializeField] private float Start_dash_great = 0f;
	[SerializeField] private float Start_dash_good = 0f;
	[SerializeField] bool is_Start_dash_great = false;//アクセルを離した状態
	[SerializeField] bool is_Start_dash_good = false;//ギアゲージのリセット
	[SerializeField] private float Start_dash_fillAmount = 0f;
	[SerializeField] private float Start_dash_speed = 0f;
	[SerializeField] private float Start_point = 0f;
	[SerializeField] private float Start_point_s = 0f;
	[SerializeField] private float acceleration_start_point = 0f;

	//タッチアニメーション座標
	[SerializeField] private Vector2 touch_point;

	void Start()
	{
		//audioSource03.Stop();
		//audioSource02.loop = !audioSource02.loop;

		touch_point = touch_anim.GetComponent<RectTransform>().anchoredPosition; //タッチで反応

		GameObject.FindGameObjectWithTag("item_coin");

		FlagManager.Instance.flags[0] = true;
		rb = GetComponent<Rigidbody>();
	
		display_speed.text = Current_speed.ToString("000");//初期化
		Current_speed = 0;//初期化
		speed = 0;//初期化
		gear = 1;//初期化
		image.fillAmount = 0;//初期化

		is_start_dash_R = true;//初期でRを入れておく
		is_start_dash_L = false;//初期でRを入れておく
		is_Accel = false;//アクセルのフラグ
		FlagManager.Instance.flags[7] = true;//スタート時に現在のレベル再計算

		is_change_fillAmount_to_2st = false;
		is_change_fillAmount_to_3st = false;
		is_change_fillAmount_to_4st = false;

		is_change_gear_2st = false;
		is_change_gear_3st = false;
		is_change_gear_4st = false;

		speed_gauge_Main.GetComponent <image_blink>().enabled = false; //明滅スクリプトにアクセス

		Current_ring = player_status_manager.Instance.Player_ring;//リングの取得
		ring_Count.text = Current_ring.ToString("  0");//テキストに反映

		Current_stage = player_status_manager.Instance.Player_stage;//ステージの取得
		var stage_max = player_status_manager.Instance.Player_stage_max;//ステージの取得
		var Game_cl = player_status_manager.Instance.Game_clear;//クリアフラグ

		if (Current_stage < stage_max)
		{
			
			stage_Count.text = Current_stage.ToString("  0");
			if(Game_cl==1)
			{
				stage_Count.color = new Color(1.0f, 0.35f, 0.35f, 1.0f);
			}

		}
		else  if (Current_stage == stage_max)
		{
			stage_Count.text = Current_stage.ToString("     Last");
		}
	}

	void Update()
	{

		Flick(); //フリック操作
		fillAmount_control();//ゲージのコントロール
		display_speed.text = Current_speed.ToString("000"); //速度2Dに情報を渡す
		player_time += Time.deltaTime;
		//Debug.Log(touch_point);

		//if (is_Start_bar_in ==true)
		//{
		//StartCoroutine("start_bar_in");//スタートバーイン
		//is_Start_bar_in = false;
		//}
		if (FlagManager.Instance.flags[2] == true)
		{
			audioSource02.volume = 0.05f;
			audioSource03.volume = 0.05f;
		}

		if (FlagManager.Instance.flags[13] == true && FlagManager.Instance.flags[14] == false)
		{
			FlagManager.Instance.flags[13] = false;
		    start_bar.gameObject.SetActive(true);
		}
	}

	void FixedUpdate()
	{
		Current_speed = rb.velocity.magnitude * 60 * 60 / 1000;//現在の速度を計算

		if (FlagManager.Instance.flags[5] == true)
		{
			start_dash_game();
			custom_fill.gameObject.SetActive(true);
			customize.GetComponent<Animation>().enabled = true;
		}

		if (FlagManager.Instance.flags[1] == true)
		{
			Se_control();//SEコントロール

			if (Input.GetMouseButtonUp(0))
			{
				//Debug.Log("離した瞬間");
				is_Accel = false;
				smoke.gameObject.SetActive(false);
			}

			if (Input.GetMouseButton(0))
			{
				Accel(); //アクセル
				is_Accel = true;
				audioSource.Stop();
				smoke.gameObject.SetActive(true);
			}

		}

		if (FlagManager.Instance.flags[3] == true)
		{
			Accel_gorl(); //アクセル
		}

		if (is_Accel == false)
		{
			if (0 <= speed)
			{
				rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed -= Deceleration_speed);//自然減速処理
			}
		}

		if(FlagManager.Instance.flags[7] == true)
		{
			Lvup();
			MaxLV();
		}

	void Lvup()
		{
			Debug.Log("レベルアップ計算を忘れずに修正");
			max_speed_lv = player_status_manager.Instance.TopSpeed_lv;//レベルの取得
			acceleration_lv = player_status_manager.Instance.Acceleration_lv;//加速度LV
			start_dash_lv = player_status_manager.Instance.StartDash_lv;//スタート時の加速度レベル
			fillAmount_lv = player_status_manager.Instance.Torque_lv;//ゲージの増加速度


			//現在のステータス
			max_speed_1st = max_speed_1st_base + max_speed_lv * 1.5f;
			max_speed_2st = max_speed_2st_base + max_speed_lv * 3;
			max_speed_3st = max_speed_3st_base + max_speed_lv * 6;
			max_speed_4st = max_speed_4st_base + max_speed_lv * 10;
			acceleration_1st = acceleration_1st_base + (start_dash_lv + acceleration_lv) * 0.004f;
			acceleration_2st = acceleration_2st_base + (start_dash_lv + acceleration_lv) * 0.006f;
			acceleration_3st = acceleration_3st_base + acceleration_lv * 0.015f;
			acceleration_4st = acceleration_4st_base + acceleration_lv * 0.020f;
			fillAmount_1st = fillAmount_1st_base + fillAmount_lv * 0.001f;
			fillAmount_2st = fillAmount_2st_base + fillAmount_lv * 0.001f;
			fillAmount_3st = fillAmount_3st_base + fillAmount_lv * 0.001f;
			fillAmount_4st = fillAmount_4st_base + fillAmount_lv * 0.001f;

			Start_dash_speed = acceleration_start_base + start_dash_lv * 0.02f;
			Start_dash_fillAmount = Start_dash_fillAmount_base + start_dash_lv * 0.02f;

			//低トルク状態
			max_speed_rowspeed_1st = max_speed_1st * 0.5f;
			max_speed_rowspeed_2st = max_speed_2st * 0.6f;
			max_speed_rowspeed_3st = max_speed_3st * 0.7f;
			max_speed_rowspeed_4st = max_speed_4st * 0.8f;
			acceleration_rowspeed_1st = acceleration_1st * 0.2f;
			acceleration_rowspeed_2st = acceleration_2st * 0.2f;
			acceleration_rowspeed_3st = acceleration_3st * 0.2f;
			acceleration_rowspeed_4st = acceleration_4st * 0.2f;

			FlagManager.Instance.flags[7] = false;
		}

	void MaxLV()
		{

			Current_stage = player_status_manager.Instance.Player_stage;
			player_status_manager.Instance.TopSpeed_lv_max = player_max_lv_base + Current_stage / 2;
			player_status_manager.Instance.Acceleration_lv_max = player_max_lv_base + Current_stage / 2;
			player_status_manager.Instance.StartDash_lv_max = player_max_lv_base + Current_stage / 2;
			player_status_manager.Instance.Torque_lv_max = player_max_lv_base + Current_stage / 2;
			player_status_manager.Instance.Player_lv_max = player_max_lv_base + Current_stage / 2;
		}

	void Accel()
		{
			switch (gear)
			{
				case 1:

					if (Current_speed < max_speed_1st)
					{
						if (is_Start_dash_great == true)
						{
							speed += Start_dash_speed;
							rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
						}
						else if (is_Start_dash_great == false)
						{
			
							if (0.4f >= image.fillAmount)
							{
							speed += acceleration_rowspeed_1st + acceleration_start_point;
							rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
							}
							else if (0.4f <= image.fillAmount)
							{				
							speed += acceleration_1st;
							rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
							}
						}
						
					}
					else if (max_speed_1st <= Current_speed)
					{
						//Debug.Log("最高速");
						speed -= Deceleration_speed;
						rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
					}
				
					break;
				case 2:
					if (Current_speed < max_speed_2st)
					{
						if (is_change_gear_great_2st == true)
						{
							//Debug.Log("スタートダッシュ");
							speed += acceleration_2st;
							rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
						}
						else if (is_change_gear_great_2st == false)
						{
							//Debug.Log("スタートダッシュ");
							if (0.4f >= image.fillAmount)
							{
								speed += acceleration_rowspeed_2st;
								rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
							}
							else if (0.4f <= image.fillAmount)
							{
								speed += acceleration_2st;
								rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
							}
						}
					}
					else if (max_speed_2st <= Current_speed)
					{
						speed -= Deceleration_speed;
						rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
					}
					break;
				case 3:
					if (Current_speed < max_speed_3st)
					{
						if (is_change_gear_great_3st == true)
						{
							//Debug.Log("スタートダッシュ");
							speed += acceleration_3st;
							rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
						}
						else if (is_change_gear_great_3st == false)
						{
							if (0.4f >= image.fillAmount)
							{
								speed += acceleration_rowspeed_3st;
								rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
							}
							else if (0.4f <= image.fillAmount)
							{
								speed += acceleration_3st;
								rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
							}
						}
					}
					break;
				case 4:

					if (Current_speed < max_speed_4st)
					{
						if (is_change_gear_great_4st == true)
						{
							//Debug.Log("スタートダッシュ");
							speed += acceleration_4st;
							rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
						}
						else if (is_change_gear_great_4st == false)
						{
							if (0.4f >= image.fillAmount)
							{
								speed += acceleration_rowspeed_4st;
								rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
							}
							else if (0.4f <= image.fillAmount)
							{
								speed += acceleration_4st;
								rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
							}
						}
					}
					break;
			}
		}
	}

	void Accel_gorl()//ウィイニングラン
	{
		if (Current_speed < max_speed_2st)
		{
			speed += acceleration_2st;
			rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
		}
		else if (Current_speed > max_speed_2st)
		{
			rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed -= Deceleration_speed);//自然減速処理			
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
		else if (Mathf.Abs(directionY) > Mathf.Abs(directionX))
		{
			if (30 < directionY)
			{
				//上向きにフリック
				Direction = "right";
				Debug.Log("向きにフリック");
				Gearchange();
			}
			else if (-30 > directionY)
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
		if (is_change_gear_2st == true && 0.4f <= image.fillAmount && gear == 1)
		{
			gear = 2;
			speed_gauge_Main.GetComponent<image_blink>().enabled = true; //ゲージ明滅スクリプトon
			player_time = 0;//演出時間を更新
			is_change_fillAmount_to_2st = true;

			if ( 0.68f <= image.fillAmount && image.fillAmount <= 0.75f )
			{
				audioSource05.PlayOneShot(sound7, 0.4f);
				StartCoroutine("great_gear_change");//ゲームの制限時間
				is_change_gear_great_2st = true;
			}
		}
		if (is_change_gear_3st == true && 0.4f <= image.fillAmount && gear == 2)
		{
			gear = 3;
			speed_gauge_Main.GetComponent<image_blink>().enabled = true; //ゲージ明滅スクリプトon
			player_time = 0;//演出時間を更新
			is_change_fillAmount_to_3st = true;

			if (0.68f <= image.fillAmount && image.fillAmount <= 0.75f)
			{
				audioSource05.PlayOneShot(sound7, 0.4f);
				StartCoroutine("great_gear_change");//ゲームの制限時間
				is_change_gear_great_3st = true;
			}
		}
		if (is_change_gear_4st == true && 0.4f <= image.fillAmount && gear == 3)
		{
			gear = 4;
			speed_gauge_Main.GetComponent<image_blink>().enabled = true; //ゲージ明滅スクリプトon
			player_time = 0;//演出時間を更新
			is_change_fillAmount_to_4st = true;

			if (0.68f <= image.fillAmount && image.fillAmount <= 0.75f)
			{
				audioSource05.PlayOneShot(sound7, 0.4f);
				StartCoroutine("great_gear_change");//ゲームの制限時間
				is_change_gear_great_4st = true;
			}
		}

	}
	IEnumerator great_gear_change()
	{
		Great_speed.gameObject.SetActive(true);//ナイスギアチェン評価
		yield return new WaitForSeconds(1.0f);
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
				{
					image.fillAmount += fillAmount_1st;
					is_change_gear_2st = true;
					if (1.0f <= image.fillAmount)
					{
						gear = 2;
						speed_gauge_Main.GetComponent<image_blink>().enabled = true; //ゲージ明滅スクリプトon
						player_time = 0;//演出時間を更新
						is_change_fillAmount_to_2st = true;
					}
					if (0.4f <= image.fillAmount)
					{
						swipe.gameObject.SetActive(true);
					}
					else if (0.4f > image.fillAmount)
					{
						swipe.gameObject.SetActive(false);
					}
				}
				break;
			case 2:
				if (player_time >= 0.4f && is_change_fillAmount_to_2st == true)//ゲージのリセット
				{
					
					if (image.fillAmount == 0)
					{
						Great_speed.gameObject.SetActive(false);
						is_change_gear_3st = true;
						is_change_gear_2st = false;
						is_change_fillAmount_to_2st = false;
						is_Start_dash_great = false;
						Gear_1st.gameObject.SetActive(false);
						Gear_2st.gameObject.SetActive(true);
					}

					if (is_Accel == true)
					{
						image.fillAmount -= fillAmount_down * 3;
					}
					else if (is_Accel == false)
					{
						image.fillAmount -= fillAmount_down * 40;
					}
				}

				if (player_time >= 0.4f && is_change_fillAmount_to_2st == false)
				{
					if (is_Accel == false || Current_speed <= max_speed_rowspeed_2st  )
					{
						//Debug.Log("rowspeed");
						image.fillAmount -= fillAmount_down;
					}

					else if (is_Accel == true && max_speed_rowspeed_2st < Current_speed)
					{
						image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);//再上昇で明滅やめ
						speed_gauge_Main.GetComponent<image_blink>().enabled = false;
						image.fillAmount += fillAmount_2st;
						if (1.0f <= image.fillAmount)
						{
							gear = 3;
							speed_gauge_Main.GetComponent<image_blink>().enabled = true; //ゲージ明滅スクリプトon
							player_time = 0;//演出時間を更新
							is_change_fillAmount_to_3st = true;
						}
						if (0.4f <= image.fillAmount)
						{
							swipe.gameObject.SetActive(true);
						}
						else if (0.4f > image.fillAmount)
						{
							swipe.gameObject.SetActive(false);
						}
					}
				}
			break;
			case 3:
				if (player_time >= 0.4f && is_change_fillAmount_to_3st == true)//ゲージのリセット
				{
					image.fillAmount -= fillAmount_down * 40;

					if (image.fillAmount == 0)
					{
						Great_speed.gameObject.SetActive(false);
						is_change_gear_great_2st = false;
						is_change_gear_4st = true;
						is_change_gear_3st = false;
						is_change_fillAmount_to_3st = false;
						Gear_2st.gameObject.SetActive(false);
						Gear_3st.gameObject.SetActive(true);
					}

					if (is_Accel == true)
					{
						image.fillAmount -= fillAmount_down * 3;
					}
					else if (is_Accel == false)
					{
						image.fillAmount -= fillAmount_down * 40;
					}
				}

				if (player_time >= 0.4f && is_change_fillAmount_to_3st == false)
				{
					if (is_Accel == false || Current_speed <= max_speed_rowspeed_3st)
					{
						//Debug.Log("rowspeed");
						image.fillAmount -= fillAmount_down;
					}

					else if (is_Accel == true && max_speed_rowspeed_3st < Current_speed)
					{
						image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);//再上昇で明滅やめ
						speed_gauge_Main.GetComponent<image_blink>().enabled = false;
						image.fillAmount += fillAmount_3st;
						if (1.0f <= image.fillAmount)
						{
							gear = 4;
							speed_gauge_Main.GetComponent<image_blink>().enabled = true; //ゲージ明滅スクリプトon
							player_time = 0;//演出時間を更新
							is_change_fillAmount_to_4st = true;
						}
						if (0.6f <= image.fillAmount)
						{
							swipe.gameObject.SetActive(true);
						}
						else if (0.6f > image.fillAmount)
						{
							swipe.gameObject.SetActive(false);
						}
					}
				}
			break;
			case 4:
				if (player_time >= 0.4f && is_change_fillAmount_to_4st == true)//ゲージのリセット
				{
					image.fillAmount -= fillAmount_down * 40;

					if (image.fillAmount == 0)
					{
						Great_speed.gameObject.SetActive(false);
						is_change_gear_great_3st = false;
						is_change_gear_4st = false;
						is_change_fillAmount_to_4st = false;
						Gear_3st.gameObject.SetActive(false);
						Gear_top.gameObject.SetActive(true);
					}

					if (is_Accel == true)
					{
						image.fillAmount -= fillAmount_down * 3;
					}
					else if (is_Accel == false)
					{
						image.fillAmount -= fillAmount_down * 40;
					}
				}

				if (player_time >= 0.4f && is_change_fillAmount_to_4st  == false)
				{
					if (is_Accel == false || Current_speed <= max_speed_rowspeed_4st)
					{
						//Debug.Log("rowspeed");
						image.fillAmount -= fillAmount_down;
					}

					else if (is_Accel == true && max_speed_rowspeed_4st < Current_speed)
					{
						image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);//再上昇で明滅やめ
						speed_gauge_Main.GetComponent<image_blink>().enabled = false;
						image.fillAmount += fillAmount_4st;
					}
				}
			break;
		}
	}
	void start_dash_game()//ゲージのコントロール
	{
		if (is_oneshot_se01 == true)
		{
			audioSource.PlayOneShot(sound2,0.4f);
			StartCoroutine("se_idle");
			is_oneshot_se01 = false;
		}
		Start_bar_point = start_bar_point.GetComponent<RectTransform>().anchoredPosition;
		start_touch.gameObject.SetActive(true);//ミニゲームバーを表示

		if (is_start_dash_R == true)
		{
			Start_bar_now_r += Time.deltaTime * start_bar_speed;
			start_bar_point.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(Start_bar_point_start, Start_bar_point_end, Start_bar_now_r);

			if (Start_bar_point.x == Start_bar_point_end.x)
			{
				Start_bar_now_l = 0f;
				is_start_dash_R = false;
				is_start_dash_L = true;
			}
		}

		if (is_start_dash_L == true)
		{
			Start_bar_now_l += Time.deltaTime * start_bar_speed;
			start_bar_point.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(Start_bar_point_end, Start_bar_point_start, Start_bar_now_l);

			if (Start_bar_point.x == Start_bar_point_start.x)
			{
				Start_bar_now_r = 0f;
				is_start_dash_R = true;
				is_start_dash_L = false;
			}
		}
		//StartCoroutine("start_game_end");//ゲームの制限時間
	}

	IEnumerator se_idle()
	{
		Debug.Log("SE再生");
		yield return new WaitForSeconds(1.2f);
		audioSource.Play();
		audioSource.volume = 0.2f;
		//audioSource.loop = !audioSource.loop;
	}

	IEnumerator start_game_end()
	{
		yield return new WaitForSeconds(4.0f);
		on_click_start_game_timeend();
		start_bar.gameObject.SetActive(false);//ミニゲームバーを非表示
	}

	public void on_click_start_game()//ゲームの決定
	{
		start_touch.gameObject.SetActive(false);
		//ebug.Log("暴発している");

		if (FlagManager.Instance.flags[6] == true)
		{
			is_start_dash_L = false;
		　　is_start_dash_R = false;
			FlagManager.Instance.flags[5] = false;
			FlagManager.Instance.flags[6] = false;
			Start_bar_point = start_bar_point.GetComponent<RectTransform>().anchoredPosition;
			//Debug.Log(Start_bar_point);
			Start_point = Start_bar_point.x;
			Start_point_s = Mathf.CeilToInt(Start_point);
			acceleration_start_point = (acceleration_1st - acceleration_rowspeed_1st) * (250f - Start_point_s)/ 250 ;
			//Debug.Log(acceleration_start_point);

			if (Start_bar_point.x <= 8 && Start_bar_point.x >= -8)
			{
				audioSource05.PlayOneShot(sound7, 0.4f);
				//Debug.Log("sugoi");
				is_Start_dash_great = true;
				Great.gameObject.SetActive(true);
			}
		}
		StartCoroutine("start_game_end");//ゲームの制限時間
	}

	public void on_click_start_game_timeend()//ゲームの決定
	{

			is_start_dash_L = false;
			is_start_dash_R = false;
			FlagManager.Instance.flags[5] = false;
			FlagManager.Instance.flags[6] = false;
			Start_bar_point = start_bar_point.GetComponent<RectTransform>().anchoredPosition;
	    	//Debug.Log("jikanngire");
	}
	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == "item_coin")
		{
			Debug.Log("imakoko");
			audioSource04.PlayOneShot(sound1, 0.4f);
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


	void Se_control()
	{
		switch (gear)
		{
			case 1:
				if (is_once_se_row01 == true)
				{
					is_once_se_row01 = false;
					audioSource02.Play();
					audioSource02.volume = 0.2f;
					// audioSource02.loop = !audioSource02.loop;
				
				}
				break;
			case 2:
				if (is_once_se_row02 == true)
				{
					is_once_se_row02 = false;
					//audioSource03.volume = 0.2f;
					audioSource02.volume = 0.3f;
					// audioSource02.loop = !audioSource02.loop;
				}
				break;
			case 3:
				if (is_once_se_high01 == true)
				{
					is_once_se_high01 = false;
				 	audioSource02.Stop();
					//audioSource02.volume = 0.4f;
					audioSource03.Play();
					audioSource03.volume = 0.15f;
				}
				break;
			case 4:
				if (is_once_se_high02 == true)
				{
					is_once_se_high02 = false;
					//audioSource02.volume = 0.5f;
					audioSource03.volume = 0.24f;
				}
				break;
		}
	}
}