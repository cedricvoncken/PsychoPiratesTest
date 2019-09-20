using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
//Always use the UnityEngine.AI namespace when using navigation components
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

    /* We need a reference to the Camera to use Raycasting.
     * When we click on the level a ray needs to be casted from the camera to the mouse
     * position. This will be used to calculate the destination for the agent to
     * navigate to.
     */
    public Camera cam;

    public int Health;

    public int potions;

    public GameObject Light;
    /* We need a reference to the agent to update the destination to navigate
     * to.
     */
    public NavMeshAgent agent;

    //We don't need the Start() function in this script.

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Shoot a ray from camera to mousclick position
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            //Variable to store the hit position
            RaycastHit hit;

            //Check if the ray hits any object and store the position in hit
            if (Physics.Raycast(ray, out hit))
            {
                //Move agent to destination (the Navmesh Agent compnent is handling the movement)
                agent.SetDestination(hit.point);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && potions >= 1)
        {
            Instantiate(Light, transform);
            potions = potions -1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            Health--;
        }

        if (collision.gameObject.tag == "potion")
        {
            potions++;
            Destroy(collision.gameObject);
        }

        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "potion")
        {
            potions++;
            Destroy(other.gameObject);
        }
    }
}
