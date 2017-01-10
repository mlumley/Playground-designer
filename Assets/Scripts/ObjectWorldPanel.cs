﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectWorldPanel : MonoBehaviour {

    protected static ObjectWorldPanel _instance;

    public static ObjectWorldPanel Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType(typeof(ObjectWorldPanel)) as ObjectWorldPanel;

                if (_instance == null) {
                    Debug.LogError("Warning: there should always be an instance of ObjectWorldPanel in the scene.");
                }
            }

            return _instance;
        }
    }

    public GameObject DeleteButton;
    public GameObject Slider;
    private Transform target = null;
    public Vector3 DeleteButtonOffset;
    public Vector3 SliderOffset;

    public void SetTarget(Transform targetTrans) {
        target = targetTrans;

        if (target) {
            DeleteButton.SetActive(true);
            if(target.gameObject.tag == "PhotoObject" || target.gameObject.GetComponent<ObjectInfo>().info.MainCategory == "Landscape") {
                Slider.SetActive(true);
                Slider.GetComponent<Slider>().value = target.localScale.x;
            }
        }
        else {
            DeleteButton.SetActive(false);
            Slider.SetActive(false);
        }
    }
    void Update() {
        if (target) {
            DeleteButton.transform.position = Camera.main.WorldToScreenPoint(target.position + DeleteButtonOffset);
            Slider.transform.position = Camera.main.WorldToScreenPoint(target.position + SliderOffset);
        }
    }
}
