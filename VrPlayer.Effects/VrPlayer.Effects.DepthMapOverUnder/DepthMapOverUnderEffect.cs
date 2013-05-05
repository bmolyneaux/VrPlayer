using System;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace VrPlayer.Effects.DepthMapOverUnder
{
    [Export(typeof(ShaderEffect))]
    public class DepthMapOverUnderEffect : ShaderEffect
    {
        public static readonly DependencyProperty InputProperty =
            RegisterPixelShaderSamplerProperty("Input", typeof(DepthMapOverUnderEffect), 0);
        public Brush Input
        {
            get { return ((Brush)(GetValue(InputProperty))); }
            set { SetValue(InputProperty, value); }
        }    
        
        public static readonly DependencyProperty MaxOffsetProperty =
            DependencyProperty.Register("MaxOffset", typeof(double), typeof(DepthMapOverUnderEffect), new UIPropertyMetadata(0D, PixelShaderConstantCallback(0)));
        public double MaxOffset
        {
            get { return ((double)(GetValue(MaxOffsetProperty))); }
            set { SetValue(MaxOffsetProperty, value); }
        }

        public DepthMapOverUnderEffect()
        {
            var pixelShader = new PixelShader();
            pixelShader.UriSource = new Uri(string.Format(
                "pack://application:,,,/{0};component/{1}",
                "VrPlayer.Effects.DepthMapOverUnder",
                "DepthMapOverUnderEffect.ps"), UriKind.RelativeOrAbsolute);
            PixelShader = pixelShader;

            UpdateShaderValue(InputProperty);
            UpdateShaderValue(MaxOffsetProperty);
        }
    }
}
