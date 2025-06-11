namespace BottomMenu.Views;

public partial class BottomMenuNine : ContentPage
{
	public BottomMenuNine()
	{
		InitializeComponent();
        BindingContext = new ViewModel.MainViewModel(Navigation);

        codeSnippet.Text = "<ctrls:BottomNavigationBar\r\n    Grid.Row=\"1\"\r\n    BarHeight=\"60\"\r\n    IconHeight=\"50\"\r\n    SelectedItemOffset=\"40\"\r\n    NotchDepthFactor=\"1.3\"\r\n    IsSelectedItemRequiredExtraSpace=\"True\"\r\n    SelectedItemBorderColor=\"Transparent\"\r\n    BarBackgroundColor=\"#ffffff\"\r\n    SelectedItemBackgroundColor=\"#ffffff\"\r\n    UnselectedIconColor=\"#3d4455\"\r\n    SelectedIconColor=\"#f9c545\"\r\n    BarCornerRadius=\"0,0,0,0\"\r\n    ExtraSpaceColor = \"#f9c545\"\r\n    ItemsSource=\"{Binding MenuItems1}\"\r\n    SelectedIndex=\"{Binding SelectedTabIndex, Mode=TwoWay}\" />";
    }
}