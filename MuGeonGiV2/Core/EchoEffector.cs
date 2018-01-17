using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CSCore;
using CSCore.DMO.Effects;
using CSCore.Streams.Effects;
using Newtonsoft.Json;

namespace MuGeonGiV2.Core
{
    public class EchoEffector : Effector
    {
        [JsonProperty]
        public float RightDelay
        {
            get => _rightDelay;
            set
            {
                if (_effect != null)
                {
                    _effect.RightDelay = value;
                }
                _rightDelay = value;
            }
        }

        [JsonProperty]
        public float WetDryMix
        {
            get => _wetDryMix;
            set
            {
                if (_effect != null)
                {
                    _effect.WetDryMix = value;
                }
                _wetDryMix = value;
            }
        }
        [JsonProperty]
        public float Feedback
        {
            get => _feedback;
            set
            {
                if (_effect != null)
                {
                    _effect.Feedback = value;
                }
                _feedback = value;
            }
        }
        [JsonProperty]
        public float LeftDelay
        {
            get => _leftDelay;
            set
            {
                if (_effect != null)
                {
                    _effect.LeftDelay = value;
                }
                _leftDelay = value;
            }
        }
        [JsonProperty]
        public int PanDelay
        {
            get => _panDelay;
            set
            {
                if (_effect != null)
                {
                    _effect.PanDelay = value == 1;
                }
                _panDelay = value;
            }
        }

        private float _rightDelay;
        private float _wetDryMix;
        private float _feedback;
        private float _leftDelay;
        private int _panDelay;

        [JsonProperty]
        public const float WetDryMixMin = 0;
        [JsonProperty]
        public const float WetDryMixMax = 100;
        [JsonProperty]
        public const float RightDelayMin = 1;
        [JsonProperty]
        public const float RightDelayMax = 2000;
        [JsonProperty]
        public const float LeftDelayMin = 1;
        [JsonProperty]
        public const float LeftDelayMax = 2000;
        [JsonProperty]
        public const float FeedbackMin = 0;
        [JsonProperty]
        public const float FeedbackMax = 100;
        [JsonProperty]
        public const int PanDelayMax = 1;
        [JsonProperty]
        public const int PanDelayMin = 0;

        private DmoEchoEffect _effect;

        public EchoEffector()
        {
            RightDelay = DmoEchoEffect.RightDelayDefault;
            WetDryMix = DmoEchoEffect.WetDryMixDefault;
            Feedback = DmoEchoEffect.FeedbackDefault;
            LeftDelay = DmoEchoEffect.LeftDelayDefault;
            PanDelay = DmoEchoEffect.PanDelayDefault ? 1 : 0;
        }
        public override IWaveSource AppendSource(IWaveSource source)
        {
            _effect = new DmoEchoEffect(source)
            {
                RightDelay = RightDelay,
                WetDryMix = WetDryMix,
                Feedback = Feedback,
                LeftDelay = LeftDelay,
                PanDelay = (PanDelay == 1),
            };

            return _effect;
        }
    }
}
