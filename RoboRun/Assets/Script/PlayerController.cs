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

    //�����̵�
    IEnumerator Slide()
    {
        Debug.Log("Slide �ڷ�ƾ ����");
        isSlide = true;
        anim.SetTrigger("isSlide");

        yield return new WaitForSeconds(1.5f / GameManager.Instance.gameSpeed);    //������ �������� �����̵� �ϴ� �ð��� ����

        isSlide= false;

        yield break;
    }

    //���� (����)
    IEnumerator Rush()
    {
        Debug.Log("Rush �ڷ�ƾ ����");
        isRush = true;
        anim.SetBool("isRush", true);
        GameManager.Instance.StartCoroutine("RushSpeed");

        yield return new WaitForSeconds(1f / GameManager.Instance.gameSpeed);    //������ �������� ���� �ϴ� �ð��� ����

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
        Debug.Log("GracePeriod �ڷ�ƾ ����");
        isInvicible = true;

        yield return new WaitForSeconds(invicibleTIme);    //����� ���� �ӵ��� ������� ����

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
