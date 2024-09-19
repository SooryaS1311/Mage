using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator _animator;

    public CharacterController characterController;
    public bool walking;
    public float speed;
    public bool isAttacking;
    public bool isRunning;
    public Transform _transform;
    public Transform _projectile_Spawner;
    public GameObject _projectile_Prefab;
    public float time = 2f;

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 movementNormalized = Vector3.zero;
        if (walking == true)
        {

            Vector3 movement = new Vector3(h, 0, v);
            movementNormalized = movement;
            if (movementNormalized != Vector3.zero)
            {
                Quaternion desiredRotation = Quaternion.LookRotation(movementNormalized, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, speed * Time.deltaTime);
            }
            characterController.Move(movementNormalized * speed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            _animator.SetTrigger("run");
            _animator.ResetTrigger("walk");
            characterController.Move(movementNormalized * (speed * 6) * Time.deltaTime);
            isRunning = true;
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _animator.SetTrigger("walk");
            _animator.ResetTrigger("Idle");

            walking = true;
            isAttacking = false;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            _animator.ResetTrigger("walk");
            _animator.SetTrigger("Idle");
            walking = false;
            isAttacking = true;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            _animator.SetTrigger("attack");
            _animator.ResetTrigger("walk");
            isAttacking = true;
            // Instantiate(_projectile_Prefab, _projectile_Spawner);
            StartCoroutine(FireBallShooter());
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            _animator.ResetTrigger("attack");
            _animator.SetTrigger("Idle");
            isAttacking = false;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            _animator.SetTrigger("block");
            _animator.ResetTrigger("walk");
            isAttacking = true;

        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            _animator.ResetTrigger("block");
            _animator.SetTrigger("Idle");
            isAttacking = false;
        }
        // if (walking == true)
        // {
        //     if (Input.GetKeyDown(KeyCode.LeftShift))
        //     {
        //         characterController.Move(movementNormalized * (speed * 6) * Time.deltaTime);
        //         _animator.SetTrigger("run");
        //         _animator.ResetTrigger("walk");
        //         isRunning = true;
        //     }
        //     if (Input.GetKeyUp(KeyCode.LeftShift))
        //     {
        //         _animator.SetTrigger("walk");
        //         _animator.ResetTrigger("run");
        //         characterController.Move(movementNormalized * speed * Time.deltaTime);
        //         isRunning = false;
        //     }
        // }
        IEnumerator FireBallShooter()
        {

            yield return new WaitForSeconds(time);
            GameObject FireBall=Instantiate(_projectile_Prefab,_projectile_Spawner);
            FireBall.transform.parent = null;


        }
    }
}