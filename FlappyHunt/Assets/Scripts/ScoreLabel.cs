﻿using UnityEngine;
using System.Collections;

public class ScoreLabel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<UILabel>().text = GameManager.numKilled.ToString();
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<UILabel>().text = GameManager.numKilled.ToString();
	}
}
