using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;
    public float jumpVelocity = 5f;
    public float distanceToGround = 0.1f;
    public LayerMask groundMask;
    float _hInput;
    float _vInput;
    Rigidbody _rigidbody;
    CapsuleCollider _col;
    void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
        _col = this.GetComponent<CapsuleCollider>();
       
    }

    // Update is called once per frame
    void Update()
    {
        _hInput = Input.GetAxis("Horizontal") * rotateSpeed;
        _vInput = Input.GetAxis("Vertical") * moveSpeed;
        if (IsGround() && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("jump");
            _rigidbody.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }
        float speed = Mathf.Max(Mathf.Abs(_vInput), Mathf.Abs(_hInput));
        this.GetComponent<Animator>().SetFloat("speed", speed);
    }
    bool IsGround()
    {
        Vector3 endPosition = new Vector3(_col.bounds.center.x,
            _col.bounds.center.y, _col.bounds.center.z);     

        return Physics.CheckCapsule(_col.bounds.center, endPosition, distanceToGround,
            groundMask, QueryTriggerInteraction.Ignore); ;
    }


    private void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * _hInput * Time.deltaTime;
        Quaternion deltaRotation = Quaternion.Euler(rotation);
        _rigidbody.MovePosition(this.transform.position +
            this.transform.forward * _vInput * Time.fixedDeltaTime);
        _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);

    }


}
