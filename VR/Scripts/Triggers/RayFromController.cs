using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LostThoughtStudios.DemterGift.PhysicsTriggers
{
    public class RayFromController : MonoBehaviour
    {
        [SerializeField]
        private GameObject PrivateKeyText;

        [SerializeField]
        private Button SaveButton;

        [SerializeField]
        private PrivateToken MyToken;
        // Start is called before the first frame update
        void Start()
        {
            SaveButton.onClick.AddListener(OnSaveButtonClick);
        }

        // Update is called once per frame
        void Update()
        {
            if (OVRInput.Get(OVRInput.Button.One))
            {
                PrivateKeyText.GetComponent<TMP_InputField>().text = UniClipboard.GetText();

            }
        }
        void OnSaveButtonClick()
        {
            MyToken.PrivateKey = PrivateKeyText.GetComponent<TMP_InputField>().text;

            StartCoroutine(LoadYourAsyncScene());
        }
        IEnumerator LoadYourAsyncScene()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}