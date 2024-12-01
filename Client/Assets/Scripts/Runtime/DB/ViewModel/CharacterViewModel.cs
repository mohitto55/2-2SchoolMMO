using Runtime.DB.Model;
using UnityEngine;

namespace Runtime.DB.ViewModel
{
    [System.Serializable]
    public class CharacterViewModel : ViewModel<CharacterModel>
    {
        public float AxisX
        {
            get => m_model.axisX;
            set
            {
                if (Mathf.Approximately(m_model.axisX, value)) return;
                
                m_model.axisX = value; 
                OnPropertyChanged();
            }
        }
        public float AxisY
        {
            get => m_model.axisY;
            set
            {
                if (Mathf.Approximately(m_model.axisY, value)) return;
                
                m_model.axisY = value; 
                OnPropertyChanged();
            }
        }
        
        public CharacterViewModel(CharacterModel model) : base(model)
        {
            
        }
    }
}