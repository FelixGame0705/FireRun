using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCrewUtils;

public class Bullet : MonoBehaviour
{
    private Vector3 _shootDir;
    private int _damage;
    public void SetUp(Vector3 shootDir)
    {
        this._shootDir = shootDir;
        transform.eulerAngles = new Vector3(0, 0, Utils.Instance.GetAnglesFromVector(shootDir));
        StartCoroutine(DelayReturnPool());
    }

    IEnumerator DelayReturnPool()
    {
        yield return new WaitForSeconds(5f);
        GamePlayController.Instance.GetBulletPool().ReturnObjectToPool(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveSpeed = 10f;
        transform.position += _shootDir* moveSpeed*Time.deltaTime;
    }

    public void SetDamage(int damage)
    {
        this._damage = damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy target = collision.gameObject.GetComponent<Enemy>();
        if (target != null)
        {
            Debug.Log("Take Damage");
            target.TakeDamage(_damage);
            target.isHurt = true;
            GamePlayController.Instance.GetBulletPool().ReturnObjectToPool(gameObject);
        }
    }
}
