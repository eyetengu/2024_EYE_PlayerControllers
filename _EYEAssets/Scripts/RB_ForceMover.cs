using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RB_ForceMover : MonoBehaviour
{    
    Rigidbody m_Rigidbody;

    public float m_Speed = 5f;

    [Header("Jump")]
    public float jump_Thrust = 20f;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _canDoubleJump;

    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        MoveRigidbody();

        
        //JUMP
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //ExplosiveForce();
            AddForceToRB();
            //ApplyForceAtPosition(m_Rigidbody);
        }
    }        

    //Adding Force to RBs
    void AddForceToRB()
    {    
        /*
         Adds a force to the Rigidbody.

        Force is applied continuously along the direction of the force vector. Specifying the ForceMode mode allows the type of force to be changed to an Acceleration, Impulse or Velocity Change.

        The effects of the forces applied with this function are accumulated at the time of the call. The physics system applies the effects during the next simulation run (either after FixedUpdate, or when the script explicitly calls the Physics.Simulate method). Because this function has different modes, the physics system only accumulates the resulting velocity change, not the passed force values. Assuming deltaTime (DT) is equal to the simulation step length (Time.fixedDeltaTime), and mass is equal to the mass of the Rigidbody the force is being applied to, here is how the velocity change is calculated for all the modes:

        ForceMode.Force: Interprets the input as force (measured in Newtons), and changes the velocity by the value of force * DT / mass. The effect depends on the simulation step length and the mass of the body.
        ForceMode.Acceleration: Interprets the parameter as acceleration (measured in meters per second squared), and changes the velocity by the value of force * DT. The effect depends on the simulation step length but doesn't depend on the mass of the body.
        ForceMode.Impulse: Interprets the parameter as an impulse (measured in Newtons per second), and changes the velocity by the value of force / mass. The effect depends on the mass of the body but doesn't depend on the simulation step length.
        ForceMode.VelocityChange: Interprets the parameter as a direct velocity change (measured in meters per second), and changes the velocity by the value of force. The effect doesn't depend on the mass of the body or the simulation step length.
        Force can only be applied to an active Rigidbody. If a GameObject is inactive, AddForce has no effect. Also, the Rigidbody cannot be kinematic.

        By default the Rigidbody's state is set to awake once a force is applied, unless the force is Vector3.zero.

        Additional resources: AddForceAtPosition, AddRelativeForce, AddTorque.

        This example applies a forward force to the GameObject's Rigidbody.
         */

        //if (Input.GetButton("Jump"))
        //{
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
        if(_isGrounded)
        {            
            m_Rigidbody.AddForce(transform.up * jump_Thrust *4);
            _isGrounded= false;
        }

        else if(_isGrounded == false && _canDoubleJump)
        {
            _canDoubleJump = false;
            //dividing the upward thrust by 2 gives a jump and mantle timing(could use an animation)
            m_Rigidbody.AddForce(transform.up * (jump_Thrust/2) * 4);
        }
        //}
    }
    
    void MoveRigidbody()
    {
        //Store user input as a movement vector
        Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //Apply the movement vector to the current position, which is
        //multiplied by deltaTime and speed for a smooth MovePosition
        m_Rigidbody.MovePosition(transform.position + m_Input * Time.deltaTime * m_Speed);
    }

    //COLLISIONS
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            _isGrounded = true;
            _canDoubleJump = true;
        }
        else
        {

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            _isGrounded = false;
        }
        else
        {

        }
    }


}
