using UnityEngine;

namespace Window
{
    public class WindowController : MonoBehaviour
    {
        public int barricadedPlanks;

        public void AddPlank()
        {
            barricadedPlanks++;
        }

        public void RemovePlank()
        {
            barricadedPlanks--;
        }
        
    }
}