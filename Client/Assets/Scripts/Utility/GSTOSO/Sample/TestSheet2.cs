using System.Collections;
using UnityEngine;


namespace Runtime.BT.Data
{
    [System.Serializable]
    public class TestSheet2 : GoogleSheetTable
    {
        public int index;
        public string name;
        public Vector3 position;
        public int[] arr;
        public string[] arrString;
    }
}