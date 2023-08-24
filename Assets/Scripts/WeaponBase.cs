using DG.Tweening;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WeaponBase : MonoBehaviour
{
    [SerializeField] protected Collider2D collider2D;
    [SerializeField] protected WeaponConfig weaponConfig;
    [SerializeField] protected Transform targetAttack;
    [SerializeField] protected Transform _parent;
    [SerializeField] protected Animator animator;
    [SerializeField] protected GameObject model;
    [SerializeField] protected GameObject body;

    public ATTACK_DURATION attackDuration;
    public bool[] isStates = new bool[4];

    // Start is called before the first frame update
    void Start()
    {
        _parent = transform.parent;
        animator.runtimeAnimatorController = weaponConfig.AnimatorWeapon;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (isStates[(int)ATTACK_DURATION.DURATION] != true)
        {
            
            Rotate();

        }
        if (isStates[(int)ATTACK_DURATION.START])
        {
            Debug.Log(rotateDirectionY);
            Flip();
        }

    }

    #region Rotate flip

    public int rotateDirectionX = 1;
    public int rotateDirectionY = 1;
    public void Rotate()
    {
        if (CheckNullTarget()) return;
        Vector3 direction = targetAttack.position - transform.position;
        direction.z = 0;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (rotateDirectionX > 0) angle += 180f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, weaponConfig.AngularVelocityReturn * Time.deltaTime);
    }

    public void CheckDirectionRotate()
    {
        rotateDirectionX = transform.position.x > targetAttack.position.x ? 1 : -1;
        rotateDirectionY = transform.position.y > targetAttack.position.y ? 1 : -1;
    }

    public void Flip()
    {
        if (CheckNullTarget()) return;
        model.transform.localScale = transform.position.x > targetAttack.position.x ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
        CheckDirectionRotate();
    }
    #endregion

    public bool CheckNullTarget()
    {
        if (targetAttack == null) return true;
        return false;
    }

    public float GetRangeWeapon()
    {
        return weaponConfig.RangeEnemyAttack;
    }

    public void SetTargetForAttack(Transform target)
    {
        this.targetAttack = target;
    }

    virtual public bool CanPerformAttack()
    {
        return !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
    }

    virtual public void AttackMachanism(Transform target)
    {
        //write in subclass
    }

    virtual public void SetStateAttacking(ATTACK_DURATION currentState, ATTACK_DURATION stateContinue)
    {

    }

    virtual public bool CanPerformAttackState()
    {
        return false;
    }

    virtual public bool GetCurrentStateActive(ATTACK_DURATION attackDuration)
    {
        return this.attackDuration == attackDuration;
    }
}
