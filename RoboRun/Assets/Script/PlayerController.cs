using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rigid;
    Animator anim;
    public int health = 3;
    public bool isSlide;
    public bool isRush;
    public bool isInvicible;
    private float invicibleTIme = 1f;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        ChangeSpeed(1f);
        isSlide = false;
        isRush = false;
        isInvicible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Translate(Vector3.left * 1.5f);
            anim.SetTrigger("MoveLeft");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Translate(Vector3.right * 1.5f);
            anim.SetTrigger("MoveRight");
        }
        //transform.Translate(Input.GetAxisRaw("Horizontal"), 0, 0);

        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(Slide());
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(Rush());
        }
    }

    private void ChangeSpeed(float speed)
    {
        GameManager.Instance.SetSpeed(speed);
    }

    //슬라이드
    IEnumerator Slide()
    {
        Debug.Log("Slide 코루틴 실행");
        isSlide = true;
        anim.SetTrigger("isSlide");

        yield return new WaitForSeconds(1.5f / GameManager.Instance.gameSpeed);    //게임이 빨라지면 슬라이드 하는 시간도 감소

        isSlide= false;

        yield break;
    }

    //돌진 (질주)
    IEnumerator Rush()
    {
        Debug.Log("Rush 코루틴 실행");
        isRush = true;
        anim.SetBool("isRush", true);
        GameManager.Instance.StartCoroutine("RushSpeed");

        yield return new WaitForSeconds(1f / GameManager.Instance.gameSpeed);    //게임이 빨라지면 돌진 하는 시간도 감소

        isRush = false;
        anim.SetBool("isRush", false);

        yield break;
    }

    void GetHit()
    {
        StartCoroutine(GracePeriod());
        anim.SetTrigger("getHit");
        health--;
    }

    IEnumerator GracePeriod()
    {
        Debug.Log("GracePeriod 코루틴 실행");
        isInvicible = true;

        yield return new WaitForSeconds(invicibleTIme);    //여기는 게임 속도와 관계없이 고정

        isInvicible = false;

        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "VerticalWall")
        {
            if (!isInvicible)
            {
                GetHit();
            }
        }
        if (other.tag == "HorizontalWall")
        {
            if (!isInvicible && !isSlide)
            {
                GetHit();
            }
        }
    }
}
