using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour {

    [SerializeField] private PlatesCounter platesCounter;

    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;

    private List<GameObject> platesVisualGameObjectList;

    private void Awake() {
        platesVisualGameObjectList = new List<GameObject>();
    }

    private void Start () {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawnerd;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e) {
        GameObject plateGameObject = platesVisualGameObjectList[platesVisualGameObjectList.Count - 1];
        platesVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlatesCounter_OnPlateSpawnerd(object sender, EventArgs e) {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        float plateOffsetY = .1f; // next plate higher
        plateVisualTransform.localPosition = new Vector3 (0, plateOffsetY * platesVisualGameObjectList.Count, 0);

        platesVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
}
