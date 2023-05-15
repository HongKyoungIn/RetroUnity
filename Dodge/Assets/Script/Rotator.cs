using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
    public float rotationSpeed = 60f;
    public float rotateRateMin = 1f; // 최소 회전 주기 (초)
    public float rotateRateMax = 20f; // 최대 회전 주기 (초)
    private float rotateRate; // 회전 주기 (초)
    private float timeAfterRotate;
    private bool rotateRight;
    private bool shouldRotate = true; // 회전 여부를 나타내는 변수 추가

    void Start() {
        timeAfterRotate = 0f;
        rotateRate = Random.Range(rotateRateMin, rotateRateMax);
        rotateRight = true;
    }

    void Update() {
        if (shouldRotate) {
            timeAfterRotate += Time.deltaTime;
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);

            if (timeAfterRotate >= rotateRate) {
                timeAfterRotate = 0f;
                rotateRight = !rotateRight;

                if (rotateRight == true) {
                    rotationSpeed = 60f;
                }
                else {
                    rotationSpeed = -60f;
                }

                rotateRate = Random.Range(rotateRateMin, rotateRateMax);
            }
        }
    }

    // 회전을 멈추는 메서드 추가
    public void StopRotation() {
        shouldRotate = false;
    }
}
