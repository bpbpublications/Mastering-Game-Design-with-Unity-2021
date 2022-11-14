using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // the Rigid Body physics component of this object
    // since we'll be accessing it a lot, we're store it as a member
    private Rigidbody _rigidBody;

    [SerializeField, Tooltip("How much accelleration is applied to this object when directional input is received.")]
    private float _movementAcceleration = 2;

    [SerializeField, Tooltip("The maximum velocity of this object - keeps the player from moving too fast.")]
    private float _movementVelocityMax = 2;

    [SerializeField, Tooltip("How quickly we slow down when no dirction input is received.")]
    private float _movementFriction = 0.05f;

    [SerializeField, Tooltip("Upwards force applied when Jump key is pressed.")]
    private float _jumpVelocity = 20;

    [SerializeField, Tooltip("Additional gravitation pull.")]
    private float _extraGravity = 40;

    //public bool _canReceiveInput = true;

    [SerializeField, Tooltip("The bullet projectile to fire.")]
    private GameObject _bulletToSpawn;

    Vector3 _curFacing = new Vector3(1, 0, 0);


    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // get the current speed from the RigidBody physics component
        // grabbing this ensures we retain the gravity speed
        Vector3 curSpeed = _rigidBody.velocity;

        // check to see if any of the keyboard arrows are being pressed
        // if so, adjust the speed of the player
        // also store the facing based on the keys being pressed
        if (Input.GetKey(KeyCode.RightArrow))
        {
            curSpeed.x += (_movementAcceleration * Time.deltaTime);
            _curFacing.x = 1;
            _curFacing.z = 0;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            curSpeed.x -= (_movementAcceleration * Time.deltaTime);
            _curFacing.x = -1;
            _curFacing.z = 0;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            curSpeed.z += (_movementAcceleration * Time.deltaTime);
            _curFacing.z = 1;
            _curFacing.x = 0;

        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            curSpeed.z -= (_movementAcceleration * Time.deltaTime);
            _curFacing.z = -1;
            _curFacing.x = 0;
        }

        // if both left and right keys are depressed or pressed, apply friction
        if (Input.GetKey(KeyCode.LeftArrow) == Input.GetKey(KeyCode.RightArrow))
        {
            curSpeed.x -= (_movementFriction * curSpeed.x);
        }

        // if both up and down keys are depressed or pressed, apply friction
        if (Input.GetKey(KeyCode.UpArrow) == Input.GetKey(KeyCode.DownArrow))
        {
            curSpeed.z -= (_movementFriction * curSpeed.z);
        }

        // does the player want to jump?
        if ( Input.GetKeyDown(KeyCode.Space) && Mathf.Abs( curSpeed.y ) < 1 )
            curSpeed.y += _jumpVelocity;
        else
            curSpeed.y -= _extraGravity * Time.deltaTime;

        // fire the weapon?
        if ( Input.GetKeyDown(KeyCode.Return) )
        {
            GameObject newBullet = Instantiate(_bulletToSpawn, transform.position, Quaternion.identity);
            Bullet bullet = newBullet.GetComponent<Bullet>();
            if (bullet)
            {

                bullet.SetDirection( new Vector3( _curFacing.x, 0f, _curFacing.z ) );
            }
        }


        // apply the max speed
        curSpeed.x = Mathf.Clamp(curSpeed.x, _movementVelocityMax * -1, _movementVelocityMax);
        curSpeed.z = Mathf.Clamp(curSpeed.z, _movementVelocityMax * -1, _movementVelocityMax);

        // adjust the velocity of this object's physics component
        _rigidBody.velocity = curSpeed;

    }

    void OnTriggerEnter(Collider collider)
    {
        // did we collide with a PickupItem?
        if (collider.gameObject.GetComponent<PickUpItem>())
        {
            // get component returned a valid PickupItem
            // so let that item know it was grabbed by this gameObject
            PickUpItem collisionItem = collider.gameObject.GetComponent<PickUpItem>();
            collisionItem.onPickedUp(this.gameObject);
        }
    }

}
