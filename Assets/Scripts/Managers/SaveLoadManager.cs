//***************************************************************************************
// Writer: Stylish Esper
// Last Updated: April 2024
// Description: Save/load example for sample game.
//***************************************************************************************

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Esper.ESave.Example
{
    public class SaveLoadManager: MonoBehaviour
    {
        // Const data IDs 
        private const string playerPositionDataKey = "PlayerPosition";
        private const string eqDataKey = "eq";

        [SerializeField]
        private CharacterController characterController;

        private SaveFileSetup saveFileSetup;
        private SaveFile saveFile;
        [SerializeField] Button SaveButton;
        private Vector3 prevPlayerPosition;

        private void Start()
        {
            // Get save file
            saveFileSetup = GetComponent<SaveFileSetup>();
            saveFile = saveFileSetup.GetSaveFile();

            // Load game
            LoadGame();

            SaveButton.onClick.AddListener(() => SaveGame());
        }

        private void Update()
        {
            prevPlayerPosition = characterController.transform.position;
        }

        /// <summary>
        /// Loads the game.
        /// </summary>
        public void LoadGame()
        {
            // Temporarily disable character controller so it doesn't override the position
            characterController.enabled = false;

            if (saveFile.HasData(playerPositionDataKey))
            {
                // Get Vector3 from a special method because Vector3 is not savable data
                var savableTransform = saveFile.GetTransform(playerPositionDataKey);
                characterController.transform.CopyTransformValues(savableTransform);
            }

            prevPlayerPosition = characterController.transform.position;

            characterController.enabled = true;

            Debug.Log("Loaded game.");
        }

        /// <summary>
        /// Saves the game.
        /// </summary>
        public void SaveGame()
        {

            saveFile.AddOrUpdateData(playerPositionDataKey, characterController.transform);
            saveFile.Save();

            Debug.Log("Saved game.");
        }
    }
}