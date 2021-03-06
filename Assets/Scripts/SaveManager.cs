﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


namespace Assets.Scripts {
    public class SaveManager : MonoBehaviour {

        public InputField fileName;
        public GameObject saveText;
        public InputField width;
        public InputField length;

        /// <summary>
        /// Save the current playground and upload it to the server
        /// </summary>
        /// <param name="withAnimation">Play the animation on saveText</param>
        public void SavePlayground(bool withAnimation = true) {
            SaveFile save = MakeSaveFile();
            //Debug.Log(saveJSON);
            StartCoroutine(DataManager.Instance.SavePlayground(fileName.text, save));
            if (withAnimation) {
                saveText.SetActive(true);
                saveText.GetComponent<Animation>().Play();
            }
        }

        /// <summary>
        /// Creates a JSON object to be uploaded to the server
        /// </summary>
        /// <returns>SaveFile object which can be encoded into JSON</returns>
        public SaveFile MakeSaveFile() {

            GameObject[] models = GameObject.FindGameObjectsWithTag("Models");
            List<ModelData> modelDatas = new List<ModelData>();

            string strToRemove = "(Clone)";

            foreach (GameObject model in models) {
                string newName = model.name.Replace(strToRemove, "");
                ModelData data = new ModelData(newName, model.transform.position, model.transform.rotation, model.transform.localScale);
                modelDatas.Add(data);
            }
            GameObject[] photos = GameObject.FindGameObjectsWithTag("PhotoObject");
            List<PhotoData> photoDatas = new List<PhotoData>();

            //Debug.Log("Photos" + photos[0].ToString());

            // IN HERE WE SHOULD CREATE A LIST OF PHOTOS TEX AND PASS THAT TO DATAMANAGER WITH THE SAME NAME AS PHOTO.NAME
            foreach (GameObject photo in photos) {
                Texture2D image = photo.GetComponent<SpriteRenderer>().sprite.texture;
                PhotoData data = new PhotoData(photo.transform.position, photo.transform.rotation, photo.transform.localScale, photo.name, image);
                photoDatas.Add(data);
            }

            int widthValue = 80;
            int lengthValue = 40;

            if (width.text != "")
                widthValue = int.Parse(width.text);
            if (length.text != "")
                lengthValue = int.Parse(length.text);


            SaveFile newSave = new SaveFile(widthValue, lengthValue, modelDatas.ToArray(), photoDatas.ToArray());
            //Debug.Log(newSave.ToString());
            return newSave;
        }


    }
}
