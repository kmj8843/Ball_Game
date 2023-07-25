using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    public float jumpPower;
    public int itemCount;
    bool isJump;
    Rigidbody rigid;
    AudioSource catchItemAudio;

    private void Awake()
    {
        isJump = false;
        rigid = GetComponent<Rigidbody>();
        catchItemAudio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            isJump = true;
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            isJump = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            itemCount++;
            catchItemAudio.Play();

            ItemCan item = other.GetComponent<ItemCan>();
            item.gameObject.SetActive(false);
        }
    }
}
