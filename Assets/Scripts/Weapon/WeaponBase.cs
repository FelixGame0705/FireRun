using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [SerializeField] protected Collider2D ColliderWeapon;
    [SerializeField] protected WeaponConfig WeaponConfigSetting;
    [SerializeField] protected Transform TargetAttack;
    [SerializeField] protected Transform Parent;
    [SerializeField] protected Animator WeaponAnimator;
    [SerializeField] protected GameObject Model;
    [SerializeField] protected GameObject Body;

    public ATTACK_STAGE PlayerAttackStage;
    protected bool[] IsStates = new bool[4];

    // Start is called before the first frame update
    void Start()
    {
        Parent = transform.parent;
        WeaponAnimator.runtimeAnimatorController = WeaponConfigSetting.AnimatorWeapon;
        IsStates[0] = true;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (CheckCurrentStateActive(ATTACK_STAGE.DURATION) != true)
        {
            Rotate();
        }
        if (CheckCurrentStateActive(ATTACK_STAGE.START)) 
        {
            Debug.Log(RotateDirectionY);
            Flip();
        }

    }

    public bool CheckIsAttack(ATTACK_STAGE attackStage)
    {
        return IsStates[(int)attackStage];
    }

    #region Rotate flip

    public int RotateDirectionX = 1;
    public int RotateDirectionY = 1;
    public void Rotate()
    {
        if (CheckNullTarget()) return;
        Vector3 direction = TargetAttack.position - transform.position;
        direction.z = 0;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (RotateDirectionX > 0) angle += 180f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, WeaponConfigSetting.AngularVelocityReturn * Time.deltaTime);
    }

    public void CheckDirectionRotate()
    {
        RotateDirectionX = transform.position.x > TargetAttack.position.x ? 1 : -1;
        RotateDirectionY = transform.position.y > TargetAttack.position.y ? 1 : -1;
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

    virtual public void SetStateAttacking(ATTACK_STAGE currentState, ATTACK_STAGE stateContinue)
    {

    }

    virtual public bool CanPerformAttackState()
    {
        return false;
    }

    virtual public bool CheckCurrentStateActive(ATTACK_STAGE attackStage)
    {
        return this.PlayerAttackStage == attackStage;
    }
}
