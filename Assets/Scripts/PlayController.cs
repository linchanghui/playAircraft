using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;
    private Rigidbody rb;
    public GameObject shot;
    public GameObject shotSpwan;
    public AudioSource weaponPlayer;

    public float fireRate = 0.5F;
    private float nextFire = 0.0F;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        weaponPlayer = GetComponent<AudioSource>();
    }


    void Update() {
      

        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpwan.transform.position, shotSpwan.transform.rotation);
            weaponPlayer.Play();
        }
    }
    void FixedUpdate()
    {   
        //获取键盘的方向键
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        //方向乘以速度
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
            //范围限制，防止飞出摄像机视野
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        //飞行时的机身左右偏移
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}