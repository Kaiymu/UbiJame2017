using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Ubijam2017 {
    public class PlayerMovement : MonoBehaviour {

        public float smoothTime = 1F;
        private Vector3 velocity = Vector3.zero;

        public float speed;
        
        Vector2 directionVel = Vector2.zero;
        private Vector2 lastGoingDirection = Vector2.zero;

        private Vector3 _rotationVector;

        private void FixedUpdate()
        {
            if(InputManager.Instance.IsGoingHorizontal() || InputManager.Instance.IsGoingVertical())
            {
                lastGoingDirection = GoingDirection();
                transform.position = Vector3.SmoothDamp(transform.position, GoingDirection(), ref velocity, smoothTime);
            } else
            {
                transform.position = Vector3.SmoothDamp(transform.position, lastGoingDirection, ref velocity, smoothTime);
            }

            //Vector3 forwardVector = Quaternion.Euler(GoingDirection()) * Vector3.forward;
            //transform.Rotate(forwardVector);

            PlayerRotation();
        }

        private Vector2 GoingDirection()
        {
            if(InputManager.Instance.IsGoingHorizontal())
            {
                directionVel.x = transform.position.x + (InputManager.Instance.GoHorizontal() * speed * Time.deltaTime);
            }

            // Horizontal
            if(InputManager.Instance.IsGoingVertical())
            {
                directionVel.y = transform.position.y + (InputManager.Instance.GoVertical() * speed * Time.deltaTime);
            }

            return directionVel;
        }

        private void PlayerRotation()
        {
        }
    }

}