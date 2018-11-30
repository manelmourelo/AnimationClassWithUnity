using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlagMove : MonoBehaviour {

    public GameObject[] SendGoal;
    //public LayerMask mask;
    public int mouse_button = 0;

    Vector3 prevPos;

    public NavMeshAgent agent;
    public Animator animator;
    public GameObject destiny;

    private void Start()
    {
        prevPos = destiny.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        prevPos = destiny.transform.position;
        if (Input.GetMouseButton(mouse_button))
        {
            RaycastHit hit;
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(r, out hit, 10000.0f) == true)
            {
                //hit.point.y = prevPos.y;
                destiny.transform.position = hit.point;
            }
        }
        agent.SetDestination(destiny.transform.position);

        if (transform.position.x != destiny.transform.position.x && transform.position.z != destiny.transform.position.z)
        {
            animator.SetFloat("Speed", agent.desiredVelocity.magnitude);
            animator.SetFloat("AngularSpeed", Vector3.SignedAngle(transform.forward, agent.desiredVelocity, transform.up));
        }
        else
        {
            animator.SetFloat("Speed", 0);
            animator.SetFloat("AngularSpeed", 0);
        }

    }

    private void OnAnimatorMove()
    {
        agent.velocity = animator.deltaPosition / Time.deltaTime;
        agent.transform.rotation = animator.rootRotation;
    }
    
}
