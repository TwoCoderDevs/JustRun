using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace DynamicRagdoll.Demo {

    /*
        spawns the characters in teh scene
        and rains cubes on top of them...
    */
    public class DemoSceneController : MonoBehaviour
    {


        public float maxRainObjects = 10;
        public Vector2 rainSizeRange = new Vector2(1, 10);
        public GameObject rainObject;
        public float rainFrequency = 3;
        List<Transform> currentRain = new List<Transform>();
        public GameObject cube;
        public int numberOfCubes = 5;

        //public GameObject joystick;

        //Spawn points
        //List<Transform> spawnPoints = new List<Transform>();

        public GameObject[] enemy;
        public Transform[] spawnPoints;
        //public int maxSpawn = 5;
        public float playRadius = 50;
        public float spawnFrequency = 3;
        int currentSpawned;
        float lastSpawn, lastRain;
        public Characters characters;
        public GameObject tutorial;

        public List<GameObject> enemyCleanList = new List<GameObject>();

        void SpawnBot () {
            GameObject g = Instantiate(enemy[(int)Random.Range(0, enemy.Length - 1)], 
            spawnPoints[(int)Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
            //g.GetComponentInChildren<AIControl>().playRadius = playRadius;
            currentSpawned++;
            enemyCleanList.Add(g);
        }

        private void Awake() {
            
            if(characters.firstRunGame){
                spawnEnemy = false;
            tutorial.SetActive(true);
            StartCoroutine(DisableTut());
        }
        else{
            tutorial.SetActive(false);
        }

            SpawnCubes();
            //StartCoroutine(sp());                     
        }

        IEnumerator DisableTut(){
        yield return new WaitForSeconds(2.5f);
        tutorial.SetActive(false);
        characters.firstRunGame = false;
        spawnEnemy = true;
    }

        IEnumerator sp(){
            yield return new WaitForSeconds(2f);
            SpawnBot();   
        }

        private void SpawnCubes(){
            for(int i = 0; i < numberOfCubes; i++){
                Instantiate(cube, 
                new Vector3(Random.Range(-45, 45), Random.Range(5, 15), Random.Range(-45, 45)), Quaternion.identity);
            }
        }

        bool spawnEnemy = true;
        public IEnumerator CleanEnemy(){

            spawnEnemy = false;
            foreach(GameObject g in enemyCleanList)
            Destroy(g);

            enemyCleanList.RemoveAll(x => x == null);

            yield return new WaitForSeconds(8f);

            spawnEnemy = true;


        }

        void Rain () {

            Vector3 position = new Vector3(Random.Range(-45, 45), Random.Range(20, 40), Random.Range(-45, 45));
            Quaternion rotation = Quaternion.Euler(Random.value * 360f, Random.value * 360f, Random.value * 360f);
            
            if (currentRain.Count < maxRainObjects) {
                currentRain.Add(Instantiate(rainObject, position, rotation).transform);
            }
            else {
                if (maxRainObjects > 0) {

                    Transform t = currentRain[Random.Range(0, currentRain.Count)];
                    t.position = position;
                    t.rotation = rotation;

                    t.localScale = Vector3.one * Random.Range(rainSizeRange.x, rainSizeRange.y);
                    
                    Rigidbody rb = t.GetComponent<Rigidbody>();
                    rb.mass = t.localScale.x * 100;
                    //fast movign object
                    //rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                    
                }

            }
        }

        void Update()
        {      

            if(!spawnEnemy) return; 


             
            if (Time.time - lastSpawn >= spawnFrequency) {
                SpawnBot();
                lastSpawn = Time.time;
            }
        

            if (Time.time - lastRain >= rainFrequency) {
                Rain();
                lastRain = Time.time;
            }
        }
    }
}
