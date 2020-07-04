using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleAds : MonoBehaviour
{
	private BannerView bannerView;

	void Start()
	{

		// アプリID
		string appId = "ca-app-pub-7430206802272744~7252719853";

		// Initialize the Google Mobile Ads SDK.
		MobileAds.Initialize(appId);

		this.RequestBanner();
	}

	private void RequestBanner()
	{

		// 広告ユニットID これはテスト用
		string adUnitId = "ca-app-pub-3940256099942544/6300978111";

		// Create a 320x50 banner at the top of the screen.
		this.bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder()
		  .AddTestDevice("5AA5D7F7015CF1A2C5747DF577005BAD")
		  .Build();
		　
		// Load the banner with the request.
		this.bannerView.LoadAd(request);

	}

}