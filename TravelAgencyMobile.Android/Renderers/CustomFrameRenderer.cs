using Android.Content;
using TravelAgencyMobile.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using FrameRenderer = Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer;

[assembly: ExportRenderer(typeof(Frame), typeof(CustomFrameRenderer))]
namespace TravelAgencyMobile.Droid.Renderers
{
    public class CustomFrameRenderer : FrameRenderer
    {
        private Context _context;

        public CustomFrameRenderer(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            var frame = e.NewElement as Frame;

            if(frame == null) return;

            if (frame.HasShadow)
            {
                Elevation = 30.0f;
                TranslationZ = 0.0f;
                SetZ(30f);
            }
        }
    }
}