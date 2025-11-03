using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Items
{
    public class ItemLayout : MonoBehaviour
    {
        private ItemSetup _currSetup;

        public Image uiIcon;
        public TextMeshProUGUI uiValue;
        public TextMeshProUGUI uiKeyText;

        public void Load(ItemSetup setup)
        {
            _currSetup = setup;
            uiIcon.sprite = _currSetup.icon;
            uiValue.text = _currSetup.soInt.value.ToString();

            var action = FindObjectOfType<ActionLifePack>();
            if (action != null && _currSetup.isUsable)
            {
                uiKeyText.text = action.keyCode.ToString();
            }
            else
            {
                uiKeyText.text = "";
            }


            UpdateUI();
        }

        private void UpdateUI()
        {
            uiIcon.sprite = _currSetup.icon;
        }

        private void Update()
        {
            {
                uiValue.text = _currSetup.soInt.value.ToString();
            }
        }

    }
}
