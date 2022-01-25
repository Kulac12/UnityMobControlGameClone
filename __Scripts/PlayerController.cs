using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 3.0f;
    public float gravity = 9.81f;

    private CharacterController myController;
    // Start is called before the first frame update
    void Start()
    {
        myController = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movementX = Input.GetAxis("Horizontal") * Vector3.right * moveSpeed * Time.deltaTime;
        myController.Move(movementX);
    }
}
