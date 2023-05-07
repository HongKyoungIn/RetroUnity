using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 관련 라이브러리
using UnityEngine.SceneManagement; // 씬 관리 관련 라이브러리

public class GameManager : MonoBehaviour {
    public GameObject gameoverText; // 게임오버 시 활성화할 텍스트 게임 오브젝트
    public Text timeText; // 생존 시간을 표시할 텍스트 컴포넌트
    public Text recordText; // 최고 기록을 표시할 텍스트 컴포넌트

    private float surviveTime; // 생존 시간
    private bool isGameover; // 게임오버 상태

    void Start() {
        // 생존 시간과 게임오버 상태 초기화
        surviveTime = 0;
        isGameover = false;
    }

    void Update() {
        // 게임 오버가 아닌 동안
        if(!isGameover) {
            // 생존 시간 갱신
            surviveTime += Time.deltaTime;
            // 갱신한 생존 시간을 timeText 텍스트 컴포넌트를 이용해 표시
            timeText.text = "Time : " + (int)surviveTime;
        }

        else {
            // 게임 오버 상태에서 R 키를 누른 경우
            if(Input.GetKeyDown(KeyCode.R)) {
                // SampleScene 씬을 로드
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    // 현재 게임을 게임오버 상태로 변경하는 메서드
    public void EndGame() {

        // 현재 상태를 게임오버 상태로 전환
        isGameover = true;
        // 게임오버 텍스트 게임 오브젝트를 활성화
        gameoverText.SetActive(true);

        // 게임오버 시 회전 멈춤
        Rotator rotator = FindObjectOfType<Rotator>();
        rotator.rotationSpeed = 0f;

        // 게임오버 시 탄알생성 멈춤
        /*
        Bullet Spawner(1), Bullet Spawner(2), Bullet Spawner(3)이 멈추지 않는 이유는 여러 개의 Bullet Spawner 오브젝트가 씬에 존재하며, 
        FindObjectOfType<BulletSpawner>() 메소드는 첫 번째 발견된 오브젝트만 반환하기 때문입니다.
        따라서 모든 Bullet Spawner 오브젝트를 찾아서 멈추도록 코드를 수정해야 합니다.
        
        FindObjectsOfType<BulletSpawner>() 메소드를 사용하여 모든 Bullet Spawner 오브젝트를 찾은 후, 
        반복문을 사용하여 각 오브젝트의 Stop() 메소드를 호출하는 것입니다.
        */
        BulletSpawner[] bulletSpawners = FindObjectsOfType<BulletSpawner>();
        foreach(BulletSpawner bulletSpawner in bulletSpawners) {
            bulletSpawner.Stop();
        }

        // BestTime 키로 저장된 이전까지의 최고 기록 가져오기
        float bestTime = PlayerPrefs.GetFloat("BestTime");

        // 이전까지의 최고 기록보다 현재 생존 시간이 더 크면
        if(surviveTime > bestTime) {
            // 최고 기록 값을 현재 생존 시간 값으로 변경
            bestTime = surviveTime;
            // 변경된 최고 기록을 BestTime 키로 저장
            PlayerPrefs.SetFloat("BestTime", bestTime);
        }

        // 최고 기록을 recordText 텍스트 컴포넌트를 이용해 표시
        recordText.text = "Best Time : " + (int)bestTime;
    }
}

