using Unity.VisualScripting;
using UnityEngine;



    public class Movement : MonoBehaviour
    {
        public float walkSpeed = 8f;
        public float sprintSpeed = 14f;
        public float maxVelocityChange = 10f;

        [Space]
        public float jumpHeight = 8f;
        public float airControl = 0.5f;

        private Vector2 input;
        private Rigidbody rb;

        private bool sprinting;
        private bool jumping;

        private bool grounded = false;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            input = new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal"), UnityEngine.Input.GetAxisRaw("Vertical"));
            input.Normalize();

            sprinting = Input.GetButton("Sprint");
            jumping = Input.GetButton("Jump");
        }

    private void OnTriggerStay(Collider other)
    {
        grounded = true;
    }

    void FixedUpdate()
        {   
        if(grounded)
        {   
            if(jumping)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
            }
            else if(input.magnitude > 0.5f)
            {
                rb.AddForce(CalculateMovement(sprinting ? sprintSpeed : walkSpeed), ForceMode.VelocityChange);
            }
            else
            {
                var velocity1 = rb.velocity;
                velocity1 = new Vector3(velocity1.x * 0.2f * Time.fixedDeltaTime, velocity1.y, velocity1.z * 0.2f * Time.fixedDeltaTime);
                rb.velocity = velocity1;
            }
        }
        else
        {
             if (input.magnitude > 0.5f)
            {
                rb.AddForce(CalculateMovement(sprinting ? sprintSpeed * airControl : walkSpeed * airControl), ForceMode.VelocityChange);
            }
            else
            {
                var velocity1 = rb.velocity;
                velocity1 = new Vector3(velocity1.x * 0.2f * Time.fixedDeltaTime, velocity1.y, velocity1.z * 0.2f * Time.fixedDeltaTime);
                rb.velocity = velocity1;
            }

        }
        grounded = false;
            
        }

        Vector3 CalculateMovement(float _speed)
        {
            Vector3 targetVelocity = new Vector3(input.x, 0, input.y);
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= _speed;

            Vector3 velocity = rb.velocity;

            if (input.magnitude > 0.5f)
            {
                Vector3 velocityChange = targetVelocity - velocity;
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                velocityChange.y = 0f;

                return velocityChange;
            }
            else
            {
                return Vector3.zero;
            }
        }
    }


