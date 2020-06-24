using UnityEngine;

/*
    just wanders around random points on our map
*/
namespace DynamicRagdoll.Demo {

    [RequireComponent(typeof(CharacterMovement))]
    public class AIControl : MonoBehaviour
    {
        //public float playRadius = 500;
        public float turnSpeed = 5f;
        public Transform destination;
        CharacterMovement characterMovement;

        public Scores scores;

        public bool shouldAddScore;

        void Awake () {
            characterMovement = GetComponent<CharacterMovement>();

            scores = GameObject.FindGameObjectWithTag("Scores").GetComponent<Scores>();
            
        }

        private void Start() {

            characterMovement.SetMovementSpeed (1);
            
        }
            
        void SetPlayer () {
            //maybe run, maybe walk
            
            destination = GameObject.FindGameObjectWithTag("Player").transform;
            //destination = new Vector3(Random.Range(-playRadius, playRadius), 0, Random.Range(-playRadius, playRadius));
        }

        void Update () {
                
            //if(!characterMovement.disableExternalMovement){
                //CheckForArrival();
                SetPlayer();
                TurnToDestination();
            //}

            if(shouldAddScore){

                scores.AddScore(1);
                shouldAddScore = false;

            }
        }

        void TurnToDestination () {
            Vector3 lookDir = destination.position - transform.position;
            lookDir.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), 
            turnSpeed * Time.deltaTime);
        }

    }
}
