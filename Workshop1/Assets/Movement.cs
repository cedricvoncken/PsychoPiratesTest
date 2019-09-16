using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed;
    public float jumpHeight;
    private bool isGrounded;


    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHor = Input.GetAxis("Horizontal");
        float moveVer = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHor, 0.0f, moveVer);

        rb.AddForce(movement * speed);

        if (Input.GetKeyDown(KeyCode.Space) & isGrounded)
        {
            rb.AddForce(new Vector3(0, 1, 0) * jumpHeight, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }

    //void OnCollisionStay(Collision collision)
    //{
    //    isGrounded = true;
    //}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "grow")
        {
            transform.localScale += new Vector3(4f, 4f, 4f);
            //Destroy(other);
        }
    }

    IEnumerator GradGrow()
    {
        int i = 0;
        while (i < 10)
        {
            Debug.Log("in the while");
            yield return new WaitForSeconds(0.1f);
            i++;
        }
    }
}
