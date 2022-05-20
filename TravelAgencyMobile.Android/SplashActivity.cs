using System;
using System.Threading.Tasks;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Com.Airbnb.Lottie;

namespace TravelAgencyMobile.Droid
{
    [Activity(Theme = "@style/Theme.Splash",
        MainLauncher = true,
        NoHistory = true)]
    public class SplashActivity : Activity, Animator.IAnimatorListener
    {
        private LottieAnimationView _animationView;
        private bool _isAnimated;

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.splash);

            _animationView = FindViewById<LottieAnimationView>(Resource.Id.animation_view);
            _animationView.AddAnimatorListener(this);
        }

        public void OnAnimationCancel(Animator? animation)
        {
            
        }

        public void OnAnimationEnd(Animator? animation)
        {
            
            //StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }

        public void OnAnimationRepeat(Animator? animation)
        {
            if (!_isAnimated)
            {
                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
                _isAnimated = true;
            }
            
        }

        public void OnAnimationStart(Animator? animation)
        {
           
        }

    }
}