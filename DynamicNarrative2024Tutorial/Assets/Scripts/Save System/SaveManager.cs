using System;
using UnityEditor;
using UnityEngine;

namespace Save_System
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager Instance;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                Instance = this;
            }
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
        }

        public void Save()
        {
            var saveData = ScriptableObject.CreateInstance<SaveData_SO>();

            if (!PlayerStatusController.Instance)
            {
                return;
            }
            
            saveData.position = PlayerStatusController.Instance.transform.position;
            saveData.energy = PlayerStatusController.Instance.floatingEnergy;
            AssetDatabase.CreateAsset(saveData, "Assets/Scripts/Save System/Resources/SaveData.asset");
            EditorUtility.SetDirty(saveData);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public void Load()
        {
            var saveData = AssetDatabase.LoadAssetAtPath<SaveData_SO>("Assets/Scripts/Save System/Resources/SaveData.asset");

            if (!saveData)
            {
                return;
            }
            
            PlayerStatusController.Instance.transform.position = saveData.position;
            PlayerStatusController.Instance.floatingEnergy = saveData.energy;
            PlayerStatusController.Instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
