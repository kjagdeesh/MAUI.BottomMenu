using BottomMenu.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BottomMenu.ViewModel
{
    /// <summary>
    /// ViewModel for demonstrating usage of the <see cref="BottomNavigationBar"/> component.
    /// Manages multiple sets of menu items and the selected tab index.
    /// </summary>
    public class MainViewModel : BindableObject
    {
        int _selectedTabIndex;

        /// <summary>
        /// ViewModel for demonstrating usage of the <see cref="BottomNavigationBar"/> component.
        /// Manages multiple sets of menu items and the selected tab index.
        /// </summary>
        public ObservableCollection<BottomNavigationBar.MenuItem> MenuItems1 { get; }

        /// <summary>
        /// Gets a collection of menu items styled with FontAwesome glyphs (for Menu Bar 2).
        /// </summary>
        public ObservableCollection<BottomNavigationBar.MenuItem> MenuItems2 { get; }

        /// <summary>
        /// Gets a smaller set of glyph-based items (for Menu Bar 3).
        /// </summary>
        public ObservableCollection<BottomNavigationBar.MenuItem> MenuItems3 { get; }

        /// <summary>
        /// Gets or sets the index of the currently selected tab.
        /// This is bound to the <see cref="BottomNavigationBar.SelectedIndex"/> property.
        /// </summary>
        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set
            {
                if (_selectedTabIndex == value) return;
                _selectedTabIndex = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// A command to navigate back to the previous page.
        /// </summary>
        public ICommand NavigatePreviousPage { get; }

        /// <summary>
        /// Navigation service to manage page stack operations.
        /// </summary>
        public INavigation navigation { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// Populates the different sets of menu items and sets up commands.
        /// </summary>
        /// <param name="navigation">The navigation service to use for backward navigation.</param>
        public MainViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            NavigatePreviousPage = new Command(() => navigation.PopAsync());

            // Third menu configuration using glyphs (3 tabs)
            MenuItems3 = new ObservableCollection<BottomNavigationBar.MenuItem>
            {
                new() {
                    Glyph = "\uf015",
                    FontFamily = "FontAwesome",
                    Command = new Command(()=> ExecuteTab(0))
                },
                new() {
                    Glyph = "\uf002",
                    FontFamily = "FontAwesome",
                    Command = new Command(()=> ExecuteTab(1))
                },
                new() {
                    Glyph = "\uf013",
                    FontFamily = "FontAwesome",
                    Command = new Command(()=> ExecuteTab(2))
                }
            };

            // Second menu configuration using glyphs (4 tabs)
            MenuItems2 = new ObservableCollection<BottomNavigationBar.MenuItem>
            {
                new() {
                    Glyph = "\uf015",
                    FontFamily = "FontAwesome",
                    Command = new Command(()=> ExecuteTab(0))
                },
                new() {
                    Glyph = "\uf002",
                    FontFamily = "FontAwesome",
                    Command = new Command(()=> ExecuteTab(1))
                },
                new() {
                    Glyph = "\uf013",
                    FontFamily = "FontAwesome",
                    Command = new Command(()=> ExecuteTab(2))
                },
                new() {
                    Glyph = "\uf1ea",
                    FontFamily = "FontAwesome",
                    Command = new Command(()=> ExecuteTab(3))
                }
            };

            // First menu configuration using images and titles
            MenuItems1 = new ObservableCollection<BottomNavigationBar.MenuItem>
            {
                new() {
                    Image = "home",
                    Command = new Command(()=> ExecuteTab(0)),
                    Name = "Home"
                },
                new() {
                    Image = "calendar",
                    Command = new Command(()=> ExecuteTab(1)),
                    Name = "Events"
                },
                new() {
                    Image = "setting",
                    Command = new Command(()=> ExecuteTab(2)),
                    Name = "Setting"
                },
                new() {
                    Image = "cart",
                    Command = new Command(()=> ExecuteTab(3)),
                    Name = "Cart"
                }
            };

            SelectedTabIndex = 0;
        }

        /// <summary>
        /// Executes the logic when a tab is selected.
        /// Updates <see cref="SelectedTabIndex"/> and optionally displays an alert.
        /// </summary>
        /// <param name="idx">The index of the tab that was selected.</param>
        void ExecuteTab(int idx)
        {
            SelectedTabIndex = idx;
            // example action:
            Application.Current.MainPage.DisplayAlert("Tapped", $"Tab {idx} tapped", "OK");
        }
    }
}
