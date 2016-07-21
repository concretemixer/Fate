using UnityEngine;
using System.Collections;

namespace Fate
{

    public class Conversation : MonoBehaviour
    {
        public enum State
        {
            Idle,
            Speaking,
            Waiting
        }

        private string currentId = "";
        private int currentStep = 0;


        public Scenario scenario;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public virtual void StartDialog(string id)
        {
        }

        public virtual void SelectAnswer(string id, int idx)
        {
        }
    }
}
