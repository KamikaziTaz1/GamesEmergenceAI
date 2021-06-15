using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidBehavior : MonoBehaviour
{

    private float avoidanceRadius = 3;
    private float cohesionRadius = 10;
    private Collider[] boids;
    Vector3 velocity;
    public float speed;
    Vector3 cohesion, alignment, separation, targetSeeking, avoidance;
    int separationCount;
    public float separationDist;
    Collider collider;
    LayerMask Obstacle;
    
    public boidManager myManager;
    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(Random.value, Random.value, Random.value);
        this.transform.up = velocity.normalized;

        collider = GetComponent<Collider>();

    }

    // Update is called once per frame
    void Update()
    {
       Vector3 currentPos = this.transform.position; 

       //declare 3 boid behavior vectors
       cohesion = Vector3.zero;
       alignment = this.transform.up;
       separation = Vector3.zero;
       targetSeeking = Vector3.zero;
       avoidance = Vector3.zero;

       boids = Physics.OverlapSphere(transform.position, cohesionRadius);

       foreach(BoidBehavior boid in myManager.listofBoids){
           if(boid == this) continue;//continue will forget the rest of this part of the loop and go to the next one so if it hits itself again it'll skip it

            cohesion += boid.transform.position - this.transform.position;
            alignment += boid.velocity;
            
            Vector3 diffDirec = this.transform.position - boid.transform.position;//just inverting targetseeking
            if(diffDirec.magnitude > 0 && diffDirec.magnitude < myManager.separationDist) //other boid is too close, fuck off
            {
                separation += (diffDirec.normalized/ diffDirec.magnitude)*myManager.separationDist;//if higher than 1, shrinks, if less, increase, then multiply by separationDist
            }
           // Vector3 dis = myManager.target.position;
           
        }


        
        //apply separation in new foreach to obstacles

       targetSeeking = myManager.target.position - this.transform.position;
       cohesion /= myManager.listofBoids.Count;
       alignment /= myManager.listofBoids.Count;
       separation /= myManager.listofBoids.Count;
       avoidance /= myManager.listofBoids.Count;

    //     if(separationCount > 0){
    //         separation = separation/separationCount;
    //     }

    //    velocity += cohesion + separation;
    
        Vector3 newVelocity = Vector3.zero;

        newVelocity += alignment * myManager.weight_alignment;
        newVelocity += cohesion * myManager.weight_cohesion;
        newVelocity += separation * myManager.weight_separation;
        newVelocity += targetSeeking * myManager.weight_seeking;
        newVelocity += avoidance * myManager.weight_avoidance;

        velocity = Vector3.Lerp(velocity, newVelocity, Time.deltaTime*0.2f); //blend existing velocity to new calc

        //if boids are physics objects, change with torque using rb (don't access transform w/o rb)
        this.transform.up = velocity.normalized;

       //move the boid
       this.transform.position = currentPos + velocity * (Time.deltaTime * speed);
    }

    void OnCollisionEnter(Collision other)
    {
       if(other.gameObject.tag == "obstacle") {
           Debug.Log("HitObstacle");
       }
    }

    public float gizmoScale = 2f;
    //add separation and target seeking
    private void OnDrawGizmos()//gizmos works even outside of play mode and in editor mode
    {
        if(Application.isPlaying){
            //current heading
        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position, velocity * gizmoScale);
        //alignment direction
        Gizmos.color = Color.green;
        Gizmos.DrawRay(this.transform.position, alignment * gizmoScale);
        //cohesion
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(this.transform.position, cohesion * gizmoScale);
        //target seeking
        Gizmos.color = Color.white;
        Gizmos.DrawRay(this.transform.position, targetSeeking * gizmoScale);
        //separation
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(this.transform.position, separation * gizmoScale);
        //avoidance
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(this.transform.position, avoidance * gizmoScale);
        }
    }
}
