namespace BottomMenu.Views;

public partial class BottomMenuFive : ContentPage
{
	public BottomMenuFive()
	{
		InitializeComponent();
        BindingContext = new ViewModel.MainViewModel(Navigation);

        codeSnippet.Text = "<ctrls:BottomNavigationBar\r\n    Grid.Row=\"1\"\r\n    BarHeight=\"70\"\r\n    IconHeight=\"50\"\r\n    SelectedItemOffset=\"15\"\r\n    NotchDepthFactor=\"1.3\"\r\n    IsSelectedItemRequiredExtraSpace=\"True\"\r\n    SelectedItemBorderColor=\"Transparent\"\r\n    BarBackgroundColor=\"White\"\r\n    SelectedItemBackgroundColor=\"#f9c545\"\r\n    UnselectedIconColor=\"#3d4455\"\r\n    SelectedIconColor=\"#3d4455\"\r\n    BarCornerRadius=\"20,20,0,0\"\r\n    ExtraSpaceColor = \"White\"\r\n    ItemsSource=\"{Binding MenuItems1}\"\r\n    SelectedIndex=\"{Binding SelectedTabIndex, Mode=TwoWay}\" />";

    }
}