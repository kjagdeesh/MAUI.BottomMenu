namespace BottomMenu.Views;

public partial class BottomMenuEight : ContentPage
{
	public BottomMenuEight()
	{
		InitializeComponent();
        BindingContext = new ViewModel.MainViewModel(Navigation);
        codeSnippet.Text = "<ctrls:BottomNavigationBar\r\n    Grid.Row=\"1\"\r\n    BarHeight=\"70\"\r\n    IconHeight=\"50\"\r\n    IsShowTitle=\"True\"\r\n    NotchDepthFactor=\"0\"\r\n    SelectedItemOffset=\"0\"\r\n    BarCornerRadius=\"20,20,0,0\"\r\n    BarMargin=\"0\"\r\n    IsSelectedItemRequiredExtraSpace=\"False\"\r\n    SelectedItemBorderColor=\"Transparent\"\r\n    BarBackgroundColor=\"White\"\r\n    SelectedItemBackgroundColor=\"#fce7b1\"\r\n    UnselectedIconColor=\"#3d4455\"\r\n    SelectedIconColor=\"#e3a402\"\r\n    ItemsSource=\"{Binding MenuItems1}\"\r\n    SelectedIndex=\"{Binding SelectedTabIndex, Mode=TwoWay}\" />";
    }
}