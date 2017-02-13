using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {

    public Transform main_camera_position;
    public Transform borracho_position;
    public float borracho_sensibility;
    public float borracho_speed;

    // Use this for initialization
    void Awake () { 
        #if UNITY_ANDROID
        Input.gyro.enabled = true;
        #endif
    }

    // Update is called once per frame
    void Update () {
	
	}

    void FixedUpdate()
    {
        main_camera_position.localPosition = new Vector3(0, borracho_position.localPosition.y+3.0f, -20);
    }
}
