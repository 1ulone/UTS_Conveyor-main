using System;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI ttext;
	private float minutes, seconds;

	private void Awake()
	{
		minutes = 2;
		seconds = 0;
		ttext.text = "2:00";  
	}

	public void Update()
	{
		seconds -= Time.deltaTime;
		if (seconds < 0)
		{
			minutes--;
			seconds = 60;
		}
		ttext.text =  minutes.ToString() + ":" + (Math.Round(seconds)==60?"00":Math.Round(seconds).ToString());
	}
}
