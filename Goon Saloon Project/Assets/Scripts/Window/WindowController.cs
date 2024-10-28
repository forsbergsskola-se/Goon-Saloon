using System;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace Window
{
    public class WindowController : MonoBehaviour
    {
        [HideInInspector]public int barricadedPlanks;
        public Transform[] planks;

        public GameObject plankPrefab;

        private void Start()
        {
            foreach (var plank in planks)
            {
                if (plank.gameObject.activeSelf)
                    barricadedPlanks++;
            }
        }

        public void AddPlank()
        {
            var plank = planks.FirstOrDefault(p => !p.gameObject.activeSelf);

            if (plank != null)
            {
                plank.gameObject.SetActive(true);
                barricadedPlanks++;
            }
        }

        public void RemovePlank()
        {
            var plank = planks.FirstOrDefault(p => p.gameObject.activeSelf);

            if (plank != null)
            {
                plank.gameObject.SetActive(false);
                barricadedPlanks--;

                Instantiate(plankPrefab, plank.transform.position, quaternion.identity);
            }
        }
        
    }
}