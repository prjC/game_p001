using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Speed_UI : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI speed_object;
	private playercontrol playerControl;

	void Start()
    {
		playerControl = GetComponent<playercontrol>();
	}

    // Update is called once per frame
    void Update()
    {
	//	Debug.Log(rb.velocity.magnitude); //速度計測
	 //	speed_object.text = "速度: " + _rigidbody.velocity.magnitude;
	}
}
