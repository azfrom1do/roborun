using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Animator playerAnim;
    public static GameManager Instance;
    public float gameSpeed;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Debug.LogWarning("게임매니저가 두개 이상 존재"); Destroy(Instance); }
    }

    void Start()
    {
        //플레이어 anim 초기화
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void SetSpeed(float speed)
    {
        gameSpeed = speed;
        Debug.Log("speed 변경 : " + gameSpeed);

        //플레이어 anim MotionSpeed
        playerAnim.SetFloat("Speed", speed);
    }

    IEnumerator RushSpeed()
    {
        Debug.Log("질주 속도 세팅");
        float tempSpeed = gameSpeed;
        gameSpeed *= 1.5f;

        while (true)
        {
            yield return new WaitForSeconds(0.2f / tempSpeed);  //게임이 빨라지면 돌진 하는 시간도 감소
            gameSpeed *= 0.9f;
            if (gameSpeed <= tempSpeed) break;
        }
        gameSpeed = tempSpeed;

        yield break;
    }
}
