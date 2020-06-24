using UnityEngine;
using System.Collections;

using Game.Combat;

namespace DynamicRagdoll.Demo {
    public class PlayerControl : MonoBehaviour {

        //public GameObject cam;

        public VariableJoystick variableJoystick;

        public CharacterMovement controlledCharacter;
		
        // Shooting shooting;
        CameraHandler camFollow;
        Camera cam;

        void Awake () {
            camFollow = GetComponent<CameraHandler>();
            cam = GetComponent<Camera>();

        }

        void Start () {


            controlledCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>(); 
            ragdollController = controlledCharacter.GetComponent<RagdollController>();

            camFollow.GetRagdollController(ragdollController);
        }

        public RagdollController ragdollController;
        
        void Update() {
            

            if (controlledCharacter) {

                if (!controlledCharacter.disableExternalMovement)
                {
                    float h = variableJoystick.Horizontal;
                    float v = variableJoystick.Vertical;

                    var forward = cam.transform.forward;
                    var right = cam.transform.right;

                    forward.y = 0;
                    right.y = 0;

                    forward.Normalize();
                    right.Normalize();

                    Vector3 dir = forward * v + right * h;

                    controlledCharacter.transform.rotation = Quaternion.Slerp(controlledCharacter.transform.rotation, 
                    Quaternion.LookRotation(dir), Time.deltaTime * 20);

                    controlledCharacter.SetMovementSpeed(Mathf.Clamp01(Mathf.Abs(v) + Mathf.Abs(h)));
                }
            }
        }
        
    }
}