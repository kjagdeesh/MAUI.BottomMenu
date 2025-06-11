namespace BottomMenu.Views;

public partial class BottomMenuSix : ContentPage
{
	public BottomMenuSix()
	{
		InitializeComponent();
        BindingContext = new ViewModel.MainViewModel(Navigation);

        codeSnippet.Text = "<ctrls:BottomNavigationBar\r\n    Grid.Row=\"1\"\r\n    BarHeight=\"70\"\r\n    IconHeight=\"70\"\r\n    SelectedItemOffset=\"10\"\r\n    NotchDepthFactor=\"1.1\"\r\n    IsSelectedItemRequiredExtraSpace=\"True\"\r\n    SelectedItemBorderColor=\"Transparent\"\r\n    BarBackgroundColor=\"White\"\r\n    SelectedItemBackgroundColor=\"Transparent\"\r\n    UnselectedIconColor=\"#3d4455\"\r\n    SelectedIconColor=\"#f9c545\"\r\n    BarCornerRadius=\"20,20,0,0\"\r\n    IsShowDot=\"True\"\r\n    DotColor=\"#f9c545\"\r\n    ExtraSpaceColor = \"White\"\r\n    ItemsSource=\"{Binding MenuItems1}\"\r\n    SelectedIndex=\"{Binding SelectedTabIndex, Mode=TwoWay}\" />";
    }
}