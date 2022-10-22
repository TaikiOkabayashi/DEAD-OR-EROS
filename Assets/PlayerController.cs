using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField]
    public float Speed = 1;
    [SerializeField]
    public  int HP = 2;
    public static float attack = 10;
    public string hashFullSpeed;
    public string hashIsGround;
    public string hashSpeed;
    public int hashAttackType;
    public int hashAttack;
    public int hashDamage;
    [SerializeField]
    Animator Animator;
    Animation spped;


    // Start is called before the first frame update
     void Awake()
    {
        //TryGetComponent(out Animator);

    }

    private void Start()
    {
        //TryGetComponent(out Animator);
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        float x = Input.GetAxis("Horizontal") * Speed;
        float z = Input.GetAxis("Vertical") * Speed;
        rb.AddForce(x, 0, z);
        if(Input.GetButtonDown("Jump"))
        {
            HP--;
            Debug.Log("HPは" + HP);
        }
        if (HP <= 1)
        {
            Debug.Log("服がはじけた");
        }

    }

    private void LateUpdate()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            Vector3 dir = rb.velocity;
            dir.y = 0;
            transform.forward = dir;
        }

        if (Animator)
            Apply(rb.velocity.magnitude, 0, true, 0);
    }

    public void Apply(float speed, float fullSpeed, bool isGround, int hp)
    {
        //Animatorのパラメーターを更新する
        Animator.SetInteger(HP, hp);
        Animator.SetBool(hashIsGround, isGround);
        Animator.SetFloat(hashFullSpeed, fullSpeed, 1.0f, Time.deltaTime);

        Animator.SetFloat(hashSpeed, speed, 0.1f, Time.deltaTime);
    }

    public void Attack(AttackType typeID)
    {
        Animator.SetInteger(hashAttackType, (int)typeID);
        Animator.SetTrigger(hashAttack);
    }
    public void Damage()
    {
        Animator.SetTrigger(hashDamage);
    }

    public interface ICharacterAnimationParam
    {
        void Apply(float speed, float fallSpeed, bool isGround, int hp);

        void Attack(AttackType type);
        void Damage();
    }

    public enum AttackType : int
    {
        ShortAttack = 0,
        NormalAttack = 1,
        PowerAttack =2
    }
}