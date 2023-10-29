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
        else { Debug.LogWarning("���ӸŴ����� �ΰ� �̻� ����"); Destroy(Instance); }
    }

    void Start()
    {
        //�÷��̾� anim �ʱ�ȭ
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void SetSpeed(float speed)
    {
        gameSpeed = speed;
        Debug.Log("speed ���� : " + gameSpeed);

        //�÷��̾� anim MotionSpeed
        playerAnim.SetFloat("Speed", speed);
    }

    IEnumerator RushSpeed()
    {
        Debug.Log("���� �ӵ� ����");
        float tempSpeed = gameSpeed;
        gameSpeed *= 1.5f;

        while (true)
        {
            yield return new WaitForSeconds(0.2f / tempSpeed);  //������ �������� ���� �ϴ� �ð��� ����
            gameSpeed *= 0.9f;
            if (gameSpeed <= tempSpeed) break;
        }
        gameSpeed = tempSpeed;

        yield break;
    }
}
