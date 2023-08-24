using System.Collections.Generic;
using UnityEngine;

public class PlayerUpDownController : MonoBehaviour
{
    public STATE_OF_CHARACTER StateLife;
    protected float m_MoveX;
    protected float m_MoveY;
    
    protected float MoveSpeed = 6f;
    protected float MaxHealth;
    protected float HealingRate;
    protected float Acceleration = 1.3f;

    protected Rigidbody2D m_Rigidbody;
    [SerializeField] protected CapsuleCollider2D m_CapsuleCollider;
    [SerializeField] protected CircleCollider2D m_CircleCollider;
    [SerializeField] protected Animator m_Animator;
    [SerializeField] List<Collider2D> enemiesCollider;
    [SerializeField] ContactFilter2D contactFilter2D;
    [SerializeField] LayerMask layerAttack;
    [SerializeField] GameObject body;
    [SerializeField] private WeaponBase[] weapons;

    Transform _nearestTransform;
    private void Start()
    {
        contactFilter2D.SetLayerMask(layerAttack);
        _nearestTransform = transform;
    }
    protected void Update()
    {
        CheckInput();
        FindEnemy();
        AttackEnemy();
    }

    public void CheckInput()
    {
        m_MoveX = Input.GetAxis("Horizontal") * Acceleration;
        m_MoveY = Mathf.Min(Input.GetAxis("Vertical") * Acceleration, 1);

        if(m_MoveX == 0 && m_MoveY == 0)
        {
            m_Animator.Play("Idle");
        }
        else
        {
            m_Animator.Play("Run");
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * MoveSpeed * m_MoveY * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.up * MoveSpeed * m_MoveY * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Flip(true);
            transform.Translate(Vector2.right * MoveSpeed * m_MoveX * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Flip(false);
            transform.Translate(Vector2.right * MoveSpeed * m_MoveX * Time.deltaTime);
        }
    }

    private void Flip(bool isFlip)
    {
        body.transform.localScale = new Vector3(isFlip ? -1 : 1, 1, 1);
    }

    private void FindEnemy()
    {
        m_CircleCollider.OverlapCollider(contactFilter2D, enemiesCollider);
    }

    public List<GameObject> GetEnemies()
    {
        List<GameObject> enemies = new List<GameObject>();
        foreach(Collider2D e in enemiesCollider)
        {
            enemies.Add(e.gameObject);
        }
        return enemies;
    }

    public Transform FindEnemyNearest()
    {
        float distanceNearest = m_CircleCollider.radius;
        foreach (GameObject e in GetEnemies())
        {
            if(distanceNearest > Vector2.Distance(transform.position, e.transform.position))
            {
                _nearestTransform = e.transform;
                distanceNearest = Vector2.Distance(transform.position, e.transform.position);
            }
        }
        return _nearestTransform;
    }

    public float GetDistanceNearest()
    {
        float distanceNearest = m_CircleCollider.radius;
        foreach (GameObject e in GetEnemies())
        {
            if (distanceNearest > Vector2.Distance(transform.position, e.transform.position))
            {
                _nearestTransform = e.transform;
                distanceNearest = Vector2.Distance(transform.position, e.transform.position);
            }
        }
        return distanceNearest;
    }

    public WeaponSystem weaponSystem;
    public bool isInRangeAttack = false;

    public bool CheckInRangeAttack(int index)
    {    
        if (weaponSystem.weapons[index].GetRangeWeapon() >= GetDistanceNearest())
            return true;
        return false;
    }


    public void AttackEnemy()
    {
        for(int i = 0; i < weaponSystem.weapons.Count; i++)
        {
            weaponSystem.ExcuteAttack(FindEnemyNearest(), CheckInRangeAttack(i));
        }
    }
}
