﻿using UnityEngine;
using System.Collections;

public class TimeLabel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<UILabel>().text = Mathf.FloorToInt(GameManager.timeRemaining).ToString();
	}
}
