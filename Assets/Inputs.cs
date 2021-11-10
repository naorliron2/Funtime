using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public Vector2 direction { get { return new Vector2(horizontal, vertical); } }

    public float horizontalRaw;
    public float verticalRaw;
    public Vector2 directionraw { get { return new Vector2(horizontalRaw, verticalRaw); } }

    public bool Jump;
    [SerializeField] KeyCode JumpKey;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal"); 
        vertical = Input.GetAxis("Vertical");

        horizontalRaw = Input.GetAxisRaw("Horizontal");
        verticalRaw = Input.GetAxisRaw("Vertical");

        Jump = Input.GetKeyDown(JumpKey);
    }
}
