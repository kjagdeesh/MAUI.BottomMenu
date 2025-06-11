using BottomMenu.ViewModel;
using BottomMenu.Views;

namespace BottomMenu
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel(Navigation);
        }

        private void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new BottomMenuOne());
        }

        private void TapGestureRecognizer_Tapped_2(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new BottomMenuTwo());
        }

        private void TapGestureRecognizer_Tapped_3(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new BottomMenuThree());
        }

        private void TapGestureRecognizer_Tapped_4(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new BottomMenuFour());
        }
        private void TapGestureRecognizer_Tapped_5(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new BottomMenuFive());
        }

        private void TapGestureRecognizer_Tapped_6(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new BottomMenuSix());
        }

        private void TapGestureRecognizer_Tapped_7(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new BottomMenuSeven());
        }

        private void TapGestureRecognizer_Tapped_8(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new BottomMenuEight());
        }

        private void TapGestureRecognizer_Tapped_9(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new BottomMenuNine());
        }

        private void TapGestureRecognizer_Tapped_10(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new BottomMenuTen());
        }

        private void TapGestureRecognizer_Tapped_11(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new BottomMenuEleven());
        }

        private void TapGestureRecognizer_Tapped_12(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new BottomMenuTwelve());
        }

        private void TapGestureRecognizer_Tapped_13(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new BottomMenuThirteen());
        }
    }

}
