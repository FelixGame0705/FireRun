using DG.Tweening;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WeaponBase : MonoBehaviour
{
    [SerializeField] protected Collider2D ColliderWeapon;
    [SerializeField] protected WeaponConfig WeaponConfigSetting;
    [SerializeField] protected Transform TargetAttack;
    [SerializeField] protected Transform Parent;
    [SerializeField] protected Animator WeaponAnimator;
    [SerializeField] protected GameObject Model;
    [SerializeField] protected GameObject Body;

    public ATTACK_DURATION AttackDuration;
    public bool[] isStates = new bool[4];

    // Start is called before the first frame update
    void Start()
    {
        Parent = transform.parent;
        WeaponAnimator.runtimeAnimatorController = WeaponConfigSetting.AnimatorWeapon;
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
        Vector3 direction = TargetAttack.position - transform.position;
        direction.z = 0;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (rotateDirectionX > 0) angle += 180f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, WeaponConfigSetting.AngularVelocityReturn * Time.deltaTime);
    }

    public void CheckDirectionRotate()
    {
        rotateDirectionX = transform.position.x > TargetAttack.position.x ? 1 : -1;
        rotateDirectionY = transform.position.y > TargetAttack.position.y ? 1 : -1;
    }

    public void Flip()
    {
        if (CheckNullTarget()) return;
        Model.transform.localScale = transform.position.x > TargetAttack.position.x ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
        CheckDirectionRotate();
    }
    #endregion

    public bool CheckNullTarget()
    {
        if (TargetAttack == null) return true;
        return false;
    }

    public float GetRangeWeapon()
    {
        return WeaponConfigSetting.RangeEnemyAttack;
    }

    public void SetTargetForAttack(Transform target)
    {
        this.TargetAttack = target;
    }

    virtual public bool CanPerformAttack()
    {
        return !WeaponAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
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
        return this.AttackDuration == attackDuration;
    }
}
