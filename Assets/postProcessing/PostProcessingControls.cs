using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace Group8.FinalsFrenzy
{
    public class PostProcessingControls : MonoBehaviour
    {
        [SerializeField] private Volume postProcessingVolume;
        [SerializeField] private bool VolOn;

        [Header("Post Processing Profiles")]
        [SerializeField] private VolumeProfile postProfileMain;

        [Header("Post Processing Effects")]
        private Bloom _bloom;
        public Slider BloomSlider;
        private LiftGammaGain _LGG;
        public Slider LGGSlider;



        private void Start() {
            postProfileMain.TryGet(out _bloom);
            postProfileMain.TryGet(out _LGG);
        }



        public void MainPostProcess() {
            postProcessingVolume.profile = postProfileMain;
        }


        public void AdjustBloom()
        {
            _bloom.intensity.value = BloomSlider.value / 50;
            
        }

        public void AdjustGamma()
        {
            float gammaSetting = (LGGSlider.value - 50) / 50 + 0.06f;
            _LGG.gamma.Override(new Vector4(gammaSetting, gammaSetting, gammaSetting, gammaSetting));
        }
    }
}
