using System.Collections;
using UnityEngine;


namespace Runtime.BT.Data
{
    [System.Serializable]
    public class TestGoogleSheetTable : GoogleSheetTable
    {
        public int index;
        public string name;
        public float num;
        public Vector3 position;
        public int[] arr;
    }
}