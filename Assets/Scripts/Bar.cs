using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Bar
    {
        public GameObject barObject;
        public Image effect;
        public Image blood;
        public Text text;
        public float hp = 100;
        private float maxHp = 100;
        private float hurtSpeed = 0.005f;
        public Bar(GameObject bar, String title)
        {
            barObject = bar;
            effect = barObject.transform.Find("Effect").GetComponent<Image>();
            blood = barObject.transform.Find("Blood").GetComponent<Image>();
            text = barObject.transform.Find("Text").GetComponent<Text>();
            text.text = title;
        }
        public void Update()
        {
            blood.fillAmount = hp / maxHp;
            if (effect.fillAmount > blood.fillAmount)
            {
                effect.fillAmount -= hurtSpeed;
            }
            else
            {
                effect.fillAmount = blood.fillAmount;
            }
        }

    }
}
