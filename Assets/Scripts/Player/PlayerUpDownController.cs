using System.Collections.Generic;
using UnityEngine;

public class PlayerUpDownController : MonoBehaviour
{
    public STATE_OF_CHARACTER StateLife;
    protected float MoveX;
    protected float MoveY;
    
    protected float MoveSpeed = 6f;
    protected float MaxHealth;
    protected float HealingRate;
    protected float Acceleration = 1.3f;

    protected Rigidbody2D RigidbodyPlayer;
    [SerializeField] protected CapsuleCollider2D CapsuleColliderPlayer;
    [SerializeField] protected CircleCollider2D CircleColliderPlayer;
    [SerializeField] protected Animator AnimatorPlayer;
    [SerializeField] private List<Collider2D> _enemiesCollider;
    [SerializeField] private ContactFilter2D _contactFilter2D;
    [SerializeField] private LayerMask _layerAttack;
    [SerializeField] private GameObject _body;
    [SerializeField] private WeaponBase[] _weapons;

    private Transform _nearestTransform;
    private void Start()
    {
        _contactFilter2D.SetLayerMask(_layerAttack);
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
        MoveX = Input.GetAxis("Horizontal") * Acceleration;
        MoveY = Mathf.Min(Input.GetAxis("Vertical") * Acceleration, 1);

        if(MoveX == 0 && MoveY == 0)
        {
            AnimatorPlayer.Play("Idle");
        }
        else
        {
            AnimatorPlayer.Play("Run");
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * MoveSpeed * MoveY * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.up * MoveSpeed * MoveY * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Flip(true);
            transform.Translate(Vector2.right * MoveSpeed * MoveX * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Flip(false);
            transform.Translate(Vector2.right * MoveSpeed * MoveX * Time.deltaTime);
        }
    }

    private void Flip(bool isFlip)
    {
        _body.transform.localScale = new Vector3(isFlip ? -1 : 1, 1, 1);
    }

    private void FindEnemy()
    {
        CircleColliderPlayer.OverlapCollider(_contactFilter2D, _enemiesCollider);
    }

    public List<GameObject> GetEnemies()
    {
        List<GameObject> enemies = new List<GameObject>();
        foreach(Collider2D e in _enemiesCollider)
        {
            enemies.Add(e.gameObject);
        }
        return enemies;
    }

    public Transform FindEnemyNearest()
    {
        float distanceNearest = CircleColliderPlayer.radius;
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
        float distanceNearest = CircleColliderPlayer.radius;
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
