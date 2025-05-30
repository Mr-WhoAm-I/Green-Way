using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class PropsAltar : MonoBehaviour
    {
        public List<SpriteRenderer> runes;
        public float lerpSpeed = 3.0f;

        private Color[] curColors;
        private Color[] targetColors;

        private void Awake()
        {
            int count = runes.Count;
            curColors = new Color[count];
            targetColors = new Color[count];

            for (int i = 0; i < count; i++)
            {
                curColors[i] = runes[i].color;
                targetColors[i] = runes[i].color;
                targetColors[i].a = 0.0f; // все символы выключены изначально
            }
        }

        public void ActivateRune(int index)
        {
            if (index >= 0 && index < targetColors.Length)
            {
                targetColors[index].a = 1.0f;
            }
        }

        private void Update()
        {
            for (int i = 0; i < runes.Count; i++)
            {
                curColors[i] = Color.Lerp(curColors[i], targetColors[i], lerpSpeed * Time.deltaTime);
                runes[i].color = curColors[i];
            }
        }
    }
}
