using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBoid : MonoBehaviour
{
        public Vector3 velocity;
        public float speed;
        public Vector3 acceleration;
        public Vector3 force;
        public float maxSpeed = 5;
        public float maxForce = 10;
    
        public float mass = 1;
    
        public bool seekEnabled = true;
        public Transform seekTargetTransform;
        public Vector3 seekTarget;
    
        public bool arriveEnabled = false;
        public Transform arriveTargetTransform;
        public Vector3 arriveTarget;
        public float slowingDistance = 10;
        public Path path;
        public int counter = 0;

        public bool pursueEnabled = false;
        public FishBoid pursueTarget;

        public bool followPathEnabled = false;
        
    
        public void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, transform.position + velocity);
    
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + acceleration);
    
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + force * 10);
    
            if (arriveEnabled)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(arriveTargetTransform.position, slowingDistance);
            }
    
        }
    
        // Start is called before the first frame update
        void Start()
        {
            
        }
    
        public Vector3 Seek(Vector3 target)
        {
            Vector3 toTarget = target - transform.position;
            Vector3 desired = toTarget.normalized * maxSpeed;
    
            return (desired - velocity);
        } 
    
        public Vector3 Arrive(Vector3 target)
        {
            Vector3 toTarget = target - transform.position;
            float dist = toTarget.magnitude;
            float ramped = (dist / slowingDistance) * maxSpeed;
            float clamped = Mathf.Min(ramped, maxSpeed);
            Vector3 desired = (toTarget / dist) * clamped;
    
            return desired - velocity;
        }
        
        public Vector3 Pursue(FishBoid pursueTarget)
        {
            float dist = Vector3.Distance(pursueTarget.transform.position, transform.position);

            float time = dist / maxSpeed;

            Vector3 pursueTargetPos = pursueTarget.transform.position + pursueTarget.velocity * time;

            return Seek(pursueTargetPos);
        }
    
        public Vector3 CalculateForce()
        {
            Vector3 f = Vector3.zero;
            if (seekEnabled)
            {
                if (seekTargetTransform != null)
                {
                    seekTarget = seekTargetTransform.position;
                }
                f += Seek(seekTarget);
            }
    
            if (arriveEnabled )
            {
                if (arriveTargetTransform != null)
                {
                    arriveTarget = arriveTargetTransform.position;                
                }
                f += Arrive(arriveTarget);
            }
            
            if (pursueEnabled)
            {
                f += Pursue(pursueTarget);
            }

            if (followPathEnabled)
            {
                seekEnabled = false;
                arriveEnabled = false;
                f += followPath();
            }
    
            return f;
        }

        Vector3 followPath()
        {
            Vector3 f = Vector3.zero;
            
            f += Seek(path.waypoints[counter]);
            f += Arrive(path.waypoints[counter]);


            if (Vector3.Distance( path.waypoints[counter],transform.position) < 0.5f)
            {
                counter = (counter+1) % path.waypoints.Count;
            }

            return f;

        }
    
        // Update is called once per frame
        void Update()
        {
            force = CalculateForce();
            //force = followPath();
            acceleration = force / mass;
            velocity = velocity + acceleration * Time.deltaTime;
            transform.position = transform.position + velocity * Time.deltaTime;
            speed = velocity.magnitude;
            if (speed > 0)
            {
                transform.forward = velocity;
            }        
        }
}
