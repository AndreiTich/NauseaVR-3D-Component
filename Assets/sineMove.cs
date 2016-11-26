using UnityEngine;
using System.Collections;
using SocketIO;
using System;


public class sineMove : MonoBehaviour {
	private GameObject head;
	private GameObject left_eye;
	private GameObject go;
	private SocketIOComponent socket;


	// Use this for initialization
	void Start () {
		go = GameObject.Find("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();

		head = GameObject.Find("GvrMain");
		left_eye = GameObject.Find("Main Camera Left");
		socket.On("reset", reset);
		socket.On("intensity", setIntensity);
		socket.On("deltaEyes", setEyeDisplace);
	}
	float intensity = 0.2f;
	float eyedisplace = 0f;
	float time = 0f;

	float massageSocket(string a){
		string splitString = a.Split(new string[] { ":" }, StringSplitOptions.None)[1];
		splitString = splitString.Substring(0, splitString.Length-1);
		return float.Parse(splitString);
	}

	void setIntensity (SocketIO.SocketIOEvent ev) {
		//json = JsonUtility.FromJson<data>(ev.data);
		string amountstr = string.Format("{0}", ev.data);
		intensity = massageSocket (amountstr);
	}

	void setEyeDisplace (SocketIO.SocketIOEvent ev) {
		string amountstr = string.Format("{0}", ev.data);
		eyedisplace = massageSocket (amountstr);
	}

	void reset (SocketIO.SocketIOEvent ev) {
		time = 0f;
		intensity = 0.2f;
		eyedisplace = 0f;
		head.transform.rotation = Quaternion.identity;
	}

	// Update is called once per frame
	void Update () {
		time = time + (float)Time.deltaTime;


		head.transform.Rotate (0, 0, Mathf.Sin(Time.fixedTime*3)*intensity);//head.transform.position.x+Mathf.Sin(Time.deltaTime));
		head.transform.Rotate (0, 0, Mathf.Sin(Time.fixedTime)*intensity);
		left_eye.transform.Rotate (0, 0, Mathf.Sin(Time.fixedTime)*eyedisplace);
		// head.transform.Rotate(0, Time.deltaTime, 0);
		// left_eye.transform.Rotate(0, Time.deltaTime, 0);
	}


}
