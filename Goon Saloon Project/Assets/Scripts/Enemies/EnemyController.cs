using System.Collections;
using UnityEngine;
using Window;

namespace Enemies
{
    public class EnemyController : MonoBehaviour
    {
        public float attackDelay;
        public float reloadTime;
        public float plankRemoveRate;
        public float moveSpeed;
        public WindowController window;

        private bool isActive;
        private Vector3 spawnPosition;

        private float countDown;
        
        
        private void Start()
        {
            spawnPosition = transform.position;
        }
        
        public void SetActive(bool active)
        {
            if (active)
            {
                StartCoroutine(UpdateMove());
            }
            else
            {
                transform.position = spawnPosition;
            }
            
            isActive = active;
            gameObject.SetActive(active);
        }

        private IEnumerator UpdateMove()
        {
            //Move Towards the target Window
            while (isActive && Vector3.Distance(transform.position, window.transform.position) > 10)
            {
                transform.position = Vector3.MoveTowards(transform.position, window.transform.position, moveSpeed * Time.deltaTime);
                yield return new WaitForSeconds(Time.deltaTime);
            }
            
            if(isActive)
                StartCoroutine(UpdateRemovingPlanks());
        }
        
        private IEnumerator UpdateRemovingPlanks()
        {
            while (isActive && window.barricadedPlanks > 0)
            {
                yield return new WaitForSeconds(plankRemoveRate);
                window.RemovePlank();
            }
            
            if(isActive)
                StartCoroutine(UpdateShooting());
        }
        
        private IEnumerator UpdateShooting()
        {
            while (isActive)
            {
                yield return new WaitForSeconds(attackDelay);
                
                //Shoot Player
                
                yield return new WaitForSeconds(reloadTime);

            }
        }

       

        public void ShotByPlayer()
        {
            SetActive(false);
        }
        
    }
}