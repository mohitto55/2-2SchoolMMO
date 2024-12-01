using Runtime.DB.Model;
using UnityEngine;

namespace Runtime.DB
{
    [System.Serializable]
    public class Configuration : IModel
    {
    
        /* Sound Setting */
        [SerializeField]
        private int m_masterSoundValue = 10;
        public int MasterSoundValue
        {
            get => m_masterSoundValue;
            set
            {
                m_masterSoundValue = value;
            }
        
        }

        [SerializeField]
        private int m_sfxSoundValue = 10;
        public int SFXSoundValue
        {
            get => m_sfxSoundValue;
            set
            {
                m_sfxSoundValue = value;
            }
        }

        [SerializeField]
        private int m_bgmSoundValue = 10;
        public int BGMSoundValue
        {
            get => m_bgmSoundValue;
            set
            {
                m_bgmSoundValue = value;
            }
        }

        /* Graphic Setting */
        private int m_resolution;

        /* Key Bind */
    }
}
