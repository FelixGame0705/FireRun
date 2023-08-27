using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 shootDir;
    public void SetUp(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        transform.eulerAngles = new Vector3(0, 0, GetAnglesFromVector(shootDir));
        StartCoroutine(DelayReturnPool());
    }

    IEnumerator DelayReturnPool()
    {
        yield return new WaitForSeconds(5f);
        GamePlayController.Instance.GetBulletPool().ReturnObjectToPool(gameObject);
    }

    private float GetAnglesFromVector(Vector3 vectorToCheck)
    {
        float angleInRadians = Mathf.Atan2(vectorToCheck.y, vectorToCheck.x);
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;
        return angleInDegrees;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveSpeed = 100f;
        transform.position +=shootDir* moveSpeed*Time.deltaTime;
    }
}
