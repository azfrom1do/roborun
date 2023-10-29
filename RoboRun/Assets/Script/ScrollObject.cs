using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObject : MonoBehaviour
{
    public float speed;
    void Start()
    {
        //***불러오는 코드 수정 필요***//
        //speed = GameManager.Instance.gameSpeed;
    }

    
    void Update()
    {
        transform.Translate(Vector3.back * GameManager.Instance.gameSpeed * Time.deltaTime * 10f);
    }
}
