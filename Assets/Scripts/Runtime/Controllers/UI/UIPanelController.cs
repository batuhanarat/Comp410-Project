using System.Collections.Generic;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Controllers.UI
{
    public class UIPanelController: MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<Transform> layers = new List<Transform>();

        #endregion

        #endregion
        
        private void OnCloseAllPanels()
        {
            foreach (var layer in layers)
            {
                if (layer.childCount <= 0) return;
                
                Destroy(layer.GetChild(0).gameObject);
            }
        }
        
        private void OnOpenPanel(UIPanelTypes panelType, int value)
        {
            OnClosePanel(value);
            Instantiate(Resources.Load<GameObject>($"Screens/{panelType}Panel"), layers[value]);
        }
        
        private void OnClosePanel(int value)
        {
            if (layers[value].childCount <= 0) return;
            
            Destroy(layers[value].GetChild(0).gameObject);
        }
        
        
    }
}