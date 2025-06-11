using BottomMenu.ViewModel;

namespace BottomMenu.Views;

public partial class BottomMenuEleven : ContentPage
{
	public BottomMenuEleven()
	{
		InitializeComponent();
        BindingContext = new ViewModel.MainViewModel(Navigation);
        codeSnippet.Text = "<ctrls:BottomNavigationBar\r\n    Grid.Row=\"1\"\r\n    BarHeight=\"70\"\r\n    IconHeight=\"50\"\r\n    SelectedItemOffset=\"0\"\r\n    NotchDepthFactor=\"1.1\"\r\n    IsSelectedItemRequiredExtraSpace=\"True\"\r\n    SelectedItemBorderColor=\"#f9c545\"\r\n    BarBackgroundColor=\"White\"\r\n    SelectedItemBackgroundColor=\"Transparent\"\r\n    UnselectedIconColor=\"#3d4455\"\r\n    SelectedIconColor=\"#f9c545\"\r\n    BarCornerRadius=\"20,20,0,0\"\r\n    IsShowDot=\"False\"\r\n    ExtraSpaceColor = \"White\"\r\n    ItemsSource=\"{Binding MenuItems1}\"\r\n    SelectedIndex=\"{Binding SelectedTabIndex, Mode=TwoWay}\" />";
    }
}