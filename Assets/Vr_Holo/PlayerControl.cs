using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float lookSensitivity;


    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX = 0;


    [SerializeField]
    private Camera theCamera;
    private Rigidbody myRigid;

    bool isActive = false;
    public GameObject gameManager;


    // Use this for initialization
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
    }




    // Update is called once per frame
    void Update()
    {
        Debug.Log(isActive);
        if (isActive)
        {
            Move();
            //CameraRotation();
            //CharacterRotation();
        }

    }

    public void Gogogo()
    {
        Debug.Log("started");
        isActive = true;
    }

    public void End()
    {
        isActive = false;
    }

    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed;
        _velocity = Quaternion.Euler(0, theCamera.transform.eulerAngles.y - 90, 0) * _velocity;

        if (_velocity.magnitude != 0)
        {
            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().loop = true;
        }
        else
            GetComponent<AudioSource>().Stop();

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }


    private void CharacterRotation()
    {
        // 좌우 캐릭터 회전
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));

    }

    private void CameraRotation()
    {
        // 상하 카메라 회전
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "EndZone")
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<CapsuleCollider>().enabled = false;
            gameManager.SendMessage("Clear");
        }
        else if (other.gameObject.tag == "Ghost")
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<CapsuleCollider>().enabled = false;
            gameManager.SendMessage("GameOver");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
