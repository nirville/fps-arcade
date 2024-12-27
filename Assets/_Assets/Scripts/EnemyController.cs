using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IAttack
{
    [SerializeField] float moveSpeed = 4f;
     [SerializeField] List<Transform> targetPoints;

    public Animator animator;
    public bool isHumanFound = false;
    CharacterController characterController;
    Transform humanPlayer;
    Transform currentTarget;
    private int currentIndex;

    void Start() {
         characterController = GetComponent<CharacterController>();
         humanPlayer = GameManager.instance.humanPlayer;

        currentIndex = 1;
        currentTarget = targetPoints[currentIndex];
        foreach (var t in targetPoints)
        {
            t.parent = null;
        }
    }

    void Update() 
    {
        if(currentTarget == humanPlayer) 
        {
             MoveTo(humanPlayer);
        }
        else
        {
           if(targetPoints != null) 
            Patrol();
        }
    }

    public void Patrol() {
        if (Vector3.Distance(transform.position, currentTarget.position) > 0.1f)
        {
            MoveTo(currentTarget);
        }

        else 
        {
            currentIndex++;

            if (currentIndex >= targetPoints.Count)
                currentIndex = 0;

            currentTarget = targetPoints[currentIndex];
        }
    }

    void MoveTo(Transform target)
    {
        Vector3 dir = target.position - transform.position;
        dir.y = 0;
        dir.Normalize();
        characterController.Move(dir * moveSpeed * Time.deltaTime);

        Quaternion targetRot = Quaternion.LookRotation(dir);
        if (Quaternion.Angle(transform.rotation, targetRot) > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, moveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            currentTarget = humanPlayer;
            isHumanFound = true;
            Attack();
        }
    }

    public void Attack() {
        animator.Play("enemy");
    }

    internal void Die() {
        isHumanFound = false;
        Destroy(gameObject, .2f);
    }
}