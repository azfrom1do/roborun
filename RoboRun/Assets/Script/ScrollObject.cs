using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObject : MonoBehaviour
{
    public float speed;
    void Start()
    {
        //***�ҷ����� �ڵ� ���� �ʿ�***//
        //speed = GameManager.Instance.gameSpeed;
    }

    
    void Update()
    {
        transform.Translate(Vector3.back * GameManager.Instance.gameSpeed * Time.deltaTime * 10f);
    }
}
