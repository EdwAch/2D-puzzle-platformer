using System;
using UnityEngine;

public class PuzzleDoor : MonoBehaviour {

    [SerializeField] private bool startsActive = true;

    private void Start() {
        if (startsActive) {
            DoorAppear();
        } else {
            DoorDisappear();
        }
    }


    public void ToggleDoorAppear() {
        if (gameObject.activeSelf) {
            DoorDisappear();
        } else {
            DoorAppear();
        }
    }
    private void DoorDisappear() {
        gameObject.SetActive(false);
    }
    
    private void DoorAppear() {
        gameObject.SetActive(true);
    }
}
