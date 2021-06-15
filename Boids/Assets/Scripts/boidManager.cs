using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boidManager : MonoBehaviour
{

    public GameObject boidPrefab;
    //public GameObject target;
    public List<BoidBehavior> listofBoids = new List<BoidBehavior>();

    [Header("Boid Stats")]
    public int numberOfBoids;
    public float maxSpeed, separationDist;
    public float avoidanceRadius;

    [Range(0, 1f)]public float weight_alignment;
    [Range(0, 1f)]public float weight_cohesion;
    [Range(0, 1f)]public float weight_separation;
    [Range(0, 1f)]public float weight_seeking;
    [Range(0, 1f)]public float weight_avoidance;


    
    //[Range(0f, 1f)]public float weight_avoidance;

    [Header ("Collisions")]
    public LayerMask Obstacle;
    public float boundsRadius =.27f;
    public float avoidCollisionWeight = 10;
    public float collisionAvoidDst = 5;
    public List<GameObject> obstacles = new List<GameObject>();
    //list gameobject of obstacles
    //populate with obstacles (findw/tag in start)
    


    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < numberOfBoids; i++){
            CreateBoid();
        }

        //obstacles.Add(new Obstacle)
        
    }

    // public Vector3 position {
    //     get { return this.transform.position; }
    // }

    void CreateBoid()
    {
        //spawn boid
        GameObject newBoidObject = Instantiate(boidPrefab, this.transform.position, Quaternion.identity);
        BoidBehavior newBoid = newBoidObject.GetComponent<BoidBehavior>();
       
       newBoid.myManager = this;
        listofBoids.Add(newBoid);
        newBoid.transform.parent = this.transform;
        
        newBoid.speed = maxSpeed* Random.Range(0.8f, 1.2f);
    }

    // Vector3 calculateObstacleAvoidance()
    // {
    //     bool obstacleNear = Physics.CheckSphere(position, avoidanceRadius, Obstacle);
    //     if(obstacleNear){
    //         //go away
    //     }
    // }
}
