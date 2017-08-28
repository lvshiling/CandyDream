﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisRecord : MonoBehaviour {

    public Transform[] compareObjs;

    public Transform[] becomparedObjs;  // pos, controller

    public string disFilePath;

	static int maxWriteSize = 10000;
	[SerializeField]
	string[] contents;
	int contentIdx;

    // Use this for initialization
    void Start () {
		contents = new string[maxWriteSize+becomparedObjs.Length * compareObjs.Length];
		contentIdx = 0;

		string[] header = new string[6];
		header [0] = "pos to chair dis";
		header [1] = "pos to table dis";
		header [2] = "pos to audience dis";
		header [3] = "controller to chair dis";
		header [4] = "controller to table dis";
		header [5] = "controller to audience dis";
		WriteToFile.writeheader(Application.dataPath + "/record/" + disFilePath, header);
//		WriteToFile.writeheader(Application.dataPath + "/record/" + disFilePath, header);
	}
	
	// Update is called once per frame
	void Update () {
//        string[] contents = new string[becomparedObjs.Length * compareObjs.Length];
        for (int j = 0; j < becomparedObjs.Length; j++)
        {
            for (int i = 0; i < compareObjs.Length; i++)
            {
                float dis = Vector3.Distance(compareObjs[i].position, becomparedObjs[j].position);
//                contents[j * compareObjs.Length + i] = dis.ToString();
				contents[contentIdx++] = dis.ToString();
            }
        }
		contents [contentIdx-1] += "\n"; 
		if (contentIdx >= maxWriteSize) {
			print ("dis " + Time.time);
			WriteToFile.write2csv(Application.dataPath + "/record/" + disFilePath, contents);
			contentIdx = 0;
			print ("dis " + Time.time);
		}
//        WriteToFile.write2csv(Application.dataPath + "/record/" + disFilePath, contents);
    }

	void OnApplicationQuit(){
		if (contentIdx > 0) {
			WriteToFile.write2csv(Application.dataPath + "/record/" + disFilePath, contents);
			contentIdx = 0;
		}
	}

	void OnApplicationPause(){
		if (contentIdx > 0) {
			WriteToFile.write2csv (Application.dataPath + "/record/" + disFilePath, contents);
			contentIdx = 0;
		}
	}
}
