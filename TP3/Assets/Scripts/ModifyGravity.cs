using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyGravity : MonoBehaviour
{
    private float baseGravity = -1.981f;
    [SerializeField]
    public float gravityReduction = 0.5f;
    bool _UpsideDown { get; set; }
    Animator _Anim { get; set; }
    Rigidbody _Rb { get; set; }
    Camera _MainCamera { get; set; }

    void Awake()
    {
        _Anim = GetComponent<Animator>();
        _Anim.SetBool("Open_Anim", true);
        _Rb = gameObject.GetComponent<Rigidbody>();
        _MainCamera = Camera.main;
    }
    void OnEnable()
    {
        _Rb.useGravity = false;
    }
    void Update()
    {
    
        if(Input.GetKeyDown(KeyCode.LeftShift)) SwapGravity();

        bool reduction = false;
        if(Input.GetKey(KeyCode.CapsLock)) reduction = true;
        GravityReduction(reduction);

    }

        void SwapGravity()
    {
        baseGravity *= -1;
        gameObject.GetComponent<RobotController>().InvertGravity();
        Vector3 swap = new Vector3(0, 0, 180);
        if (baseGravity > 0 && !_UpsideDown && _Anim.GetComponent<RobotController>()._Grounded)
        {
            _UpsideDown = true;
            transform.position += Vector3.up;
            transform.Rotate(swap);
        }
        else if (baseGravity < 0 && _UpsideDown)
        {
            _UpsideDown = false;
            transform.position += Vector3.down;
            transform.Rotate(-swap);
        }
    }


    void GravityReduction(bool reduction)
    {
        if (reduction)
        {
            Vector3 gravity = baseGravity * gravityReduction * Vector3.up;
            _Rb.AddForce(gravity, ForceMode.Acceleration);
        }
        else
        {
            Vector3 gravity = baseGravity * Vector3.up;
            _Rb.AddForce(gravity, ForceMode.Acceleration);
        }
    }

    void CheckFall()
    {
        if(_Rb.position.y <= -10 || _Rb.position.y >= 20)
        {
            GameObject robot = GameObject.Find("robotSphere");
            Destroy(robot);
            LevelManager.instance.respawn();
        }
    }

}
