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

        public Transform[] characterTransforms;
        
        
        private void Start()
        {
            spawnPosition = transform.position;
        }
        
        public void SetActive(bool active)
        {
            foreach (var character in characterTransforms)
                character.gameObject.SetActive(false);
            
            isActive = active;
            gameObject.SetActive(active);
            
            if (active)
            {
                StartCoroutine(UpdateMove());
                
                int index = Random.Range(0, characterTransforms.Length);
                characterTransforms[index].gameObject.SetActive(true);
            }
            else
            {
                transform.position = spawnPosition;
            }
        }

        private IEnumerator UpdateMove()
        {
            //Move Towards the target Window
            while (isActive && Vector3.Distance(transform.position, window.transform.position) > 1.75f)
            {
                Vector3 targetPos = new Vector3(window.transform.position.x, transform.position.y, window.transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                
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