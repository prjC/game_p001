using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
	[SerializeField] float fadeSpeed = 0.01f;
	[SerializeField] float red, green, blue, alfa;
	[SerializeField] float fadein_alfa = 0.0f;
	[SerializeField] float fadeout_alfa = 0.0f;

	public bool isFadeIn = false;
	public bool isFadeOut = false;
	Image fadeImage;                //透明度を変更するパネルのイメージ

	void Start()
	{
		//isFadeOut = true;
		//GetComponent<Image>().enabled = true; //ブラックスクリーンon 
		//GetComponent<Image>().color = new Color(255, 0, 0, 0.5f);
		fadeImage = GetComponent<Image>();
		red = fadeImage.color.r;
		green = fadeImage.color.g;
		blue = fadeImage.color.b;
		alfa = fadeImage.color.a;
	}

	void Update()
	{
		if (isFadeIn == true)
		{
			StartFadeIn();
		}

		if (isFadeOut == true)
		{
			StartFadeOut();
		}

	}
	void StartFadeIn()
	{
		fadeImage.enabled = true;  // a)パネルの表示をオンにする
		alfa -= fadeSpeed;         // b)不透明度を徐々に下げる
		SetAlpha();               // c)変更した透明度をパネルに反映する
		if (alfa <= fadein_alfa)
		{             
			isFadeIn = false;
		}
	}

	void StartFadeOut()
	{
		fadeImage.enabled = true;  // a)パネルの表示をオンにする
		alfa += fadeSpeed;         // b)不透明度を徐々にあげる
		SetAlpha();               // c)変更した透明度をパネルに反映する

		if (alfa >= fadeout_alfa)
		{          
			 isFadeOut = false;
		//	Debug.Log("ふぇーっどあい");
		}
	}
	void SetAlpha()
	{
		fadeImage.color = new Color(red, green, blue, alfa);
	}

	public void SetFadeIn()
	{
		isFadeIn = true;
	}

	public void SetFadeOut()
	{
		isFadeOut = true;
	}

	public void SetFadeIn_false()
	{
		isFadeIn = false;
	}

	public void SetFadeOut_false()
	{
		isFadeOut = false;
	}
}