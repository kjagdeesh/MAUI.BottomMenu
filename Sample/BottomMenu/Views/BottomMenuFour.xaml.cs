namespace BottomMenu.Views;

public partial class BottomMenuFour : ContentPage
{
	public BottomMenuFour()
	{
		InitializeComponent();
        BindingContext = new ViewModel.MainViewModel(Navigation);
        codeSnippet.Text = "<ctrls:BottomNavigationBar\r\n    Grid.Row=\"1\"\r\n    BarHeight=\"70\"\r\n    IconHeight=\"60\"\r\n    SelectedItemOffset=\"25\"\r\n    NotchDepthFactor=\"1\"\r\n    SelectedItemBorderColor=\"Transparent\"\r\n    BarBackgroundColor=\"White\"\r\n    SelectedItemBackgroundColor=\"#f9c545\"\r\n    UnselectedIconColor=\"#3d4455\"\r\n    SelectedIconColor=\"#3d4455\"\r\n    BarCornerRadius=\"20,20,0,0\"\r\n    ItemsSource=\"{Binding MenuItems2}\"\r\n    SelectedIndex=\"{Binding SelectedTabIndex, Mode=TwoWay}\" />";
    }
}