namespace BottomMenu.Views;

public partial class BottomMenuThree : ContentPage
{
	public BottomMenuThree()
	{
		InitializeComponent();
        BindingContext = new ViewModel.MainViewModel(Navigation);
        codeSnippet.Text = "<ctrls:BottomNavigationBar\r\n    Grid.Row=\"1\"\r\n    BarHeight=\"70\"\r\n    IconHeight=\"50\"\r\n    NotchDepthFactor=\"0\"\r\n    SelectedItemOffset=\"0\"\r\n    IsSelectedItemRequiredExtraSpace=\"False\"\r\n    SelectedItemBorderColor=\"Transparent\"\r\n    BarBackgroundColor=\"White\"\r\n    SelectedItemBackgroundColor=\"#f9c545\"\r\n    UnselectedIconColor=\"#3d4455\"\r\n    SelectedIconColor=\"#3d4455\"\r\n    ItemsSource=\"{Binding MenuItems1}\"\r\n    SelectedIndex=\"{Binding SelectedTabIndex, Mode=TwoWay}\" />";
    }
}