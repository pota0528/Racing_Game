using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어와 충돌하면 아이템 제거 (또는 다른 로직 추가)
            GameManager.Instance.gas += 10;
            Destroy(gameObject);
        }
    }
}
