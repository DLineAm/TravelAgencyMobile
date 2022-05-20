using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using App3.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(Picker), typeof(CustomPickerRenderer))]
namespace App3.Droid.Renderers
{
    
    public class CustomPickerRenderer : PickerRenderer
    {
        private Context context;
        public CustomPickerRenderer(Context context) : base(context)
        {
            this.context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            var defaultColor = Color.Black;
            if (Control == null || e.NewElement == null) return;
            //for example ,change the line to red:
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                Control.BackgroundTintList = ColorStateList.ValueOf(defaultColor);
            else
                Control.Background.SetColorFilter(defaultColor, PorterDuff.Mode.SrcAtop);
        }
    }
}