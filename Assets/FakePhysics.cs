using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePhysics : MonoBehaviour
{
    [SerializeField] float mass = 1;
    CharaterController charscript;
    Vector3 lastFrameVelocity = Vector3.zero;
    Vector3 acceleration = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        charscript = GetComponent<CharaterController>();
    }
    private void Update()
    {
        AcclerationEstimation();
    }

    void AcclerationEstimation()
    {
        acceleration = charscript.desiredMoveDirection;
    }

    //callback that charactercontroller has when touching a collider
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //get the rigidbody
        Rigidbody bodyrb = hit.collider.attachedRigidbody;

        //if it was null, there wasnt one, if it was kinematic we dont wanna move it
        if (bodyrb == null || bodyrb.isKinematic)
        {
            return;
        }

        //The direction the CharacterController was moving in when the collision occured.
        Vector3 movedir = hit.moveDirection;

        //add force but relative to a position
        bodyrb.AddForceAtPosition(movedir  * acceleration.magnitude * mass, hit.point);
    }
}
