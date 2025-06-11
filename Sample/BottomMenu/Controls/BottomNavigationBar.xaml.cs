using CommunityToolkit.Maui.Behaviors;
using Microsoft.Maui.Controls.Shapes;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace BottomMenu.Controls
{
    public partial class BottomNavigationBar : ContentView
    {
        /// <summary>
        /// Indicates whether the page containing the BottomNavigationBar is currently active (loaded).
        /// This flag helps avoid unnecessary rebuilds when the page is not visible.
        /// </summary>
        bool _isPageActive = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="BottomNavigationBar"/> class.
        /// Sets up event handlers for when the view is loaded or unloaded,
        /// and triggers UI rebuilds when the binding context changes.
        /// </summary>
        public BottomNavigationBar()
        {
            InitializeComponent();

            // Event fired when the view is loaded into the visual tree
            this.Loaded += (s, e) =>
            {
                _isPageActive = true;

                // Ensure rebuilds occur when the view is loaded or the BindingContext changes
                Loaded += (_, __) => Rebuild();
                BindingContextChanged += (_, __) => Rebuild();
            };

            // Event fired when the view is removed from the visual tree
            this.Unloaded += (s, e) =>
            {
                _isPageActive = false;
            };
        }

        /// <summary>
        /// Handles changes to the <see cref="ItemsSource"/> property.
        /// Subscribes or unsubscribes to the collection's <see cref="INotifyCollectionChanged.CollectionChanged"/> event
        /// to automatically respond when the contents of the collection are modified (e.g., items added or removed).
        /// Triggers a rebuild of the navigation bar layout.
        /// </summary>
        /// <param name="bind">The bindable object that owns the property.</param>
        /// <param name="oldV">The old value of the property.</param>
        /// <param name="newV">The new value of the property.</param>
        static void OnItemsChanged(BindableObject bind, object oldV, object newV)
        {
            var ctrl = (BottomNavigationBar)bind;
            if (oldV is INotifyCollectionChanged o) o.CollectionChanged -= ctrl.OnCollectionChanged;
            if (newV is INotifyCollectionChanged n) n.CollectionChanged += ctrl.OnCollectionChanged;
            ctrl.Rebuild();
        }

        /// <summary>
        /// Called when the <see cref="ItemsSource"/> collection is changed (e.g., item added or removed).
        /// Triggers a rebuild of the navigation bar layout to reflect the updated collection.
        /// </summary>
        /// <param name="s">The sender (typically the collection).</param>
        /// <param name="e">Details about the collection change event.</param>
        void OnCollectionChanged(object s, NotifyCollectionChangedEventArgs e) => Rebuild();


        /// <summary>
        /// Identifies the <see cref="ItemsSource"/> bindable property.
        /// This property is used to bind a collection of <see cref="MenuItem"/> objects
        /// that will be displayed in the bottom navigation bar.
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(
                nameof(ItemsSource),
                typeof(ObservableCollection<MenuItem>),
                typeof(BottomNavigationBar),
                propertyChanged: OnItemsChanged);

        /// <summary>
        /// Gets or sets the collection of <see cref="MenuItem"/> objects displayed in the navigation bar.
        /// When the collection changes, the layout is automatically rebuilt.
        /// </summary>
        public ObservableCollection<MenuItem> ItemsSource
        {
            get => (ObservableCollection<MenuItem>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="SelectedIndex"/> bindable property.
        /// This property is used to track the index of the currently selected item in the bottom navigation bar.
        /// When the selected index changes, the layout is rebuilt to reflect the selection.
        /// </summary>
        public static readonly BindableProperty SelectedIndexProperty =
            BindableProperty.Create(
                nameof(SelectedIndex),
                typeof(int),
                typeof(BottomNavigationBar),
                0,
                propertyChanged: (_, __, ___) => ((BottomNavigationBar)_)
                    .Rebuild());

        /// <summary>
        /// Gets or sets the index of the currently selected menu item in the <see cref="ItemsSource"/>.
        /// Updating this value will update the visual state of the navigation bar and execute the item's command.
        /// </summary>
        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        /// <summary>
        /// Gets the calculated margin to apply to the items grid within the bottom navigation bar.
        /// If the bottom margin of the bar is greater than 0, horizontal side margins are added
        /// to provide spacing on the left and right. This is typically used when accommodating
        /// devices with gesture navigation or safe area insets.
        /// </summary>
        public Thickness ItemGridMargin
        {
            get
            {
                double bottomMargin = BarMargin.Bottom;
                double leftSideMargin = bottomMargin > 0 ? 30 : 0;
                double rightSideMargin = bottomMargin > 0 ? 30 : 0;
                return new Thickness(leftSideMargin, 0, rightSideMargin, 0);
            }
        }

        /// <summary>
        /// Identifies the <see cref="BarBackgroundColor"/> bindable property.
        /// This property controls the background color of the bottom navigation bar.
        /// </summary>
        public static readonly BindableProperty BarBackgroundColorProperty =
            BindableProperty.Create(
                nameof(BarBackgroundColor),
                typeof(Color),
                typeof(BottomNavigationBar),
                Colors.Black);

        /// <summary>
        /// Gets or sets the background color of the bottom navigation bar.
        /// Defaults to <see cref="Colors.Black"/>.
        /// </summary>
        public Color BarBackgroundColor
        {
            get => (Color)GetValue(BarBackgroundColorProperty);
            set => SetValue(BarBackgroundColorProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="BarHeight"/> bindable property.
        /// This property defines the height of the bottom navigation bar in device-independent units (DIPs).
        /// </summary>
        public static readonly BindableProperty BarHeightProperty =
            BindableProperty.Create(
                nameof(BarHeight),
                typeof(double),
                typeof(BottomNavigationBar),
                60.0);

        /// <summary>
        /// Gets or sets the height of the bottom navigation bar in DIPs.
        /// Default value is <c>60.0</c>.
        /// </summary>
        public double BarHeight
        {
            get => (double)GetValue(BarHeightProperty);
            set => SetValue(BarHeightProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="BarCornerRadius"/> bindable property.
        /// This property defines the corner radius of the bottom navigation bar,
        /// allowing for rounded edges.
        /// </summary>
        public static readonly BindableProperty BarCornerRadiusProperty =
            BindableProperty.Create(
                nameof(BarCornerRadius),
                typeof(CornerRadius),
                typeof(BottomNavigationBar),
                new CornerRadius(0));

        /// <summary>
        /// Gets or sets the corner radius of the bottom navigation bar.
        /// This allows for rounded corners on the top-left and top-right edges.
        /// Default is <c>0</c> (no rounding).
        /// </summary>
        public CornerRadius BarCornerRadius
        {
            get => (CornerRadius)GetValue(BarCornerRadiusProperty);
            set => SetValue(BarCornerRadiusProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="BarMargin"/> bindable property.
        /// This property sets the margin around the entire bottom navigation bar.
        /// </summary>
        public static readonly BindableProperty BarMarginProperty =
            BindableProperty.Create(
                nameof(BarMargin),
                typeof(Thickness),
                typeof(BottomNavigationBar),
                new Thickness(0));

        /// <summary>
        /// Gets or sets the margin around the bottom navigation bar.
        /// This is useful for applying padding or safe area spacing from screen edges.
        /// Default is <c>new Thickness(0)</c>.
        /// </summary>
        public Thickness BarMargin
        {
            get => (Thickness)GetValue(BarMarginProperty);
            set => SetValue(BarMarginProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="IconHeight"/> bindable property.
        /// This property controls the height of icons displayed in the navigation bar.
        /// </summary>
        public static readonly BindableProperty IconHeightProperty =
                BindableProperty.Create(
                    nameof(IconHeight),
                    typeof(double),
                    typeof(BottomNavigationBar),
                    60.0);

        /// <summary>
        /// Gets or sets the height (in device-independent units) of the icons displayed in the bottom navigation bar.
        /// Default is <c>60.0</c>.
        public double IconHeight
        {
            get => (double)GetValue(IconHeightProperty);
            set => SetValue(IconHeightProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="UnselectedIconColor"/> bindable property.
        /// This property sets the color of icons when they are not selected.
        /// </summary>
        public static readonly BindableProperty UnselectedIconColorProperty =
            BindableProperty.Create(
                nameof(UnselectedIconColor),
                typeof(Color),
                typeof(BottomNavigationBar),
                Colors.White);

        /// <summary>
        /// Gets or sets the color of the icons when they are in an unselected state.
        /// Default is <see cref="Colors.White"/>.
        /// </summary>
        public Color UnselectedIconColor
        {
            get => (Color)GetValue(UnselectedIconColorProperty);
            set => SetValue(UnselectedIconColorProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="SelectedIconColor"/> bindable property.
        /// This property sets the color of icons when they are selected.
        /// </summary>
        public static readonly BindableProperty SelectedIconColorProperty =
            BindableProperty.Create(
                nameof(SelectedIconColor),
                typeof(Color),
                typeof(BottomNavigationBar),
                Colors.Black);

        /// <summary>
        /// Gets or sets the color of the icons when they are in a selected state.
        /// Default is <see cref="Colors.Black"/>.
        /// </summary>
        public Color SelectedIconColor
        {
            get => (Color)GetValue(SelectedIconColorProperty);
            set => SetValue(SelectedIconColorProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="SelectedItemBackgroundColor"/> bindable property.
        /// This property sets the background color of the currently selected item in the navigation bar.
        /// </summary>
        public static readonly BindableProperty SelectedItemBackgroundColorProperty =
            BindableProperty.Create(
                nameof(SelectedItemBackgroundColor),
                typeof(Color),
                typeof(BottomNavigationBar),
                Colors.Yellow);

        /// <summary>
        /// Gets or sets the background color of the selected menu item.
        /// Default is <see cref="Colors.Yellow"/>.
        /// </summary>
        public Color SelectedItemBackgroundColor
        {
            get => (Color)GetValue(SelectedItemBackgroundColorProperty);
            set => SetValue(SelectedItemBackgroundColorProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="SelectedItemBorderColor"/> bindable property.
        /// This property sets the border color of the selected item in the navigation bar.
        /// </summary>
        public static readonly BindableProperty SelectedItemBorderColorProperty =
            BindableProperty.Create(
                nameof(SelectedItemBorderColor),
                typeof(Color),
                typeof(BottomNavigationBar),
                Colors.Transparent);

        /// <summary>
        /// Gets or sets the border color of the selected menu item.
        /// Default is <see cref="Colors.Transparent"/>.
        /// </summary>
        public Color SelectedItemBorderColor
        {
            get => (Color)GetValue(SelectedItemBorderColorProperty);
            set => SetValue(SelectedItemBorderColorProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="UnselectedItemBorderColor"/> bindable property.
        /// Sets the border color for unselected items in the navigation bar.
        /// </summary>
        public static readonly BindableProperty UnselectedItemBorderColorProperty =
            BindableProperty.Create(
                nameof(UnselectedItemBorderColor),
                typeof(Color),
                typeof(BottomNavigationBar),
                Colors.Transparent);

        /// <summary>
        /// Gets or sets the border color for unselected menu items.
        /// Default is <see cref="Colors.Transparent"/>.
        /// </summary>
        public Color UnselectedItemBorderColor
        {
            get => (Color)GetValue(UnselectedItemBorderColorProperty);
            set => SetValue(UnselectedItemBorderColorProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="IsSelectedItemRequiredExtraSpace"/> bindable property.
        /// This property determines whether extra visual spacing is added around the selected item.
        /// </summary>
        public static readonly BindableProperty IsSelectedItemRequiredExtraSpaceProperty =
                BindableProperty.Create(
                    nameof(IsSelectedItemRequiredExtraSpace),
                    typeof(bool),
                    typeof(BottomNavigationBar),
                    false);

        /// <summary>
        /// Gets or sets a value indicating whether the selected item should appear with extra visual space.
        /// Useful for highlighting the selected item with more prominence.
        /// Default is <c>false</c>.
        /// </summary>
        public bool IsSelectedItemRequiredExtraSpace
        {
            get => (bool)GetValue(IsSelectedItemRequiredExtraSpaceProperty);
            set => SetValue(IsSelectedItemRequiredExtraSpaceProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="IsShowDot"/> bindable property.
        /// Determines whether a small indicator dot is shown above the selected item.
        /// </summary>
        public static readonly BindableProperty IsShowDotProperty =
        BindableProperty.Create(
            nameof(IsShowDot),
            typeof(bool),
            typeof(BottomNavigationBar),
            false);

        /// <summary>
        /// Gets or sets a value indicating whether to show a small dot above the selected item.
        /// Often used as a visual cue to highlight activity or selection.
        /// Default is <c>false</c>.
        /// </summary>
        public bool IsShowDot
        {
            get => (bool)GetValue(IsShowDotProperty);
            set => SetValue(IsShowDotProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="DotColor"/> bindable property.
        /// This property defines the color of the indicator dot when <see cref="IsShowDot"/> is enabled.
        /// </summary>
        public static readonly BindableProperty DotColorProperty =
                BindableProperty.Create(
                    nameof(DotColor),
                    typeof(Color),
                    typeof(BottomNavigationBar),
                    Colors.Transparent);

        /// <summary>
        /// Gets or sets the color of the dot displayed above the selected item.
        /// Default is <see cref="Colors.Transparent"/> (invisible).
        /// </summary>
        public Color DotColor
        {
            get => (Color)GetValue(DotColorProperty);
            set => SetValue(DotColorProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="IsShowLine"/> bindable property.
        /// Determines whether a horizontal line is displayed below/above the selected item.
        /// </summary>
        public static readonly BindableProperty IsShowLineProperty =
        BindableProperty.Create(
            nameof(IsShowLine),
            typeof(bool),
            typeof(BottomNavigationBar),
            false);

        /// <summary>
        /// Gets or sets a value indicating whether to show a line beneath/above the selected item.
        /// Useful for additional selection indication.
        /// Default is <c>false</c>.
        /// </summary>
        public bool IsShowLine
        {
            get => (bool)GetValue(IsShowLineProperty);
            set => SetValue(IsShowLineProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="LineColor"/> bindable property.
        /// Sets the color of the horizontal selection line when <see cref="IsShowLine"/> is enabled.
        /// </summary>
        public static readonly BindableProperty LineColorProperty =
                BindableProperty.Create(
                    nameof(LineColor),
                    typeof(Color),
                    typeof(BottomNavigationBar),
                    Colors.Transparent);

        /// <summary>
        /// Gets or sets the color of the line shown beneath the selected item.
        /// Default is <see cref="Colors.Transparent"/>.
        /// </summary>
        public Color LineColor
        {
            get => (Color)GetValue(LineColorProperty);
            set => SetValue(LineColorProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="LineMargin"/> bindable property.
        /// Sets the margin around the horizontal line shown beneath the selected item.
        /// </summary>
        public static readonly BindableProperty LineMarginProperty =
            BindableProperty.Create(
                nameof(LineMargin),
                typeof(Thickness),
                typeof(BottomNavigationBar),
                new Thickness(0));

        /// <summary>
        /// Gets or sets the margin around the selection line (when <see cref="IsShowLine"/> is true).
        /// Default is <c>new Thickness(0)</c>.
        /// </summary>
        public Thickness LineMargin
        {
            get => (Thickness)GetValue(LineMarginProperty);
            set => SetValue(LineMarginProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="LineHorizontalOptions"/> bindable property.
        /// Determines the horizontal alignment of the selection line within the item container.
        /// </summary>
        public static readonly BindableProperty LineHorizontalOptionsProperty =
            BindableProperty.Create(
                nameof(LineHorizontalOptions),
                typeof(LayoutOptions),
                typeof(BottomNavigationBar),
                LayoutOptions.Center);

        /// <summary>
        /// Gets or sets the horizontal alignment of the selection line.
        /// Default is <see cref="LayoutOptions.Center"/>.
        /// </summary>
        public LayoutOptions LineHorizontalOptions
        {
            get => (LayoutOptions)GetValue(LineHorizontalOptionsProperty);
            set => SetValue(LineHorizontalOptionsProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="LineVerticalOptions"/> bindable property.
        /// Determines the vertical alignment of the selection line within the item container.
        /// </summary>
        public static readonly BindableProperty LineVerticalOptionsProperty =
            BindableProperty.Create(
                nameof(LineVerticalOptions),
                typeof(LayoutOptions),
                typeof(BottomNavigationBar),
                LayoutOptions.Center);

        /// <summary>
        /// Gets or sets the vertical alignment of the selection line.
        /// Default is <see cref="LayoutOptions.Center"/>.
        /// </summary>
        public LayoutOptions LineVerticalOptions
        {
            get => (LayoutOptions)GetValue(LineVerticalOptionsProperty);
            set => SetValue(LineVerticalOptionsProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="ExtraSpaceColor"/> bindable property.
        /// Sets the background color for the extra space shown around the selected item if enabled.
        /// </summary>
        public static readonly BindableProperty ExtraSpaceColorProperty =
                BindableProperty.Create(
                    nameof(ExtraSpaceColor),
                    typeof(Color),
                    typeof(BottomNavigationBar),
                    Colors.Transparent);

        /// <summary>
        /// Gets or sets the background color of the extra space shown for the selected item
        /// when <see cref="IsSelectedItemRequiredExtraSpace"/> is <c>true</c>.
        /// Default is <see cref="Colors.Transparent"/>.
        /// </summary>
        public Color ExtraSpaceColor
        {
            get => (Color)GetValue(ExtraSpaceColorProperty);
            set => SetValue(ExtraSpaceColorProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="IsShowTitle"/> bindable property.
        /// Determines whether to show the title (text label) of the selected menu item.
        /// </summary>
        public static readonly BindableProperty IsShowTitleProperty =
                BindableProperty.Create(
                    nameof(IsShowTitle),
                    typeof(bool),
                    typeof(BottomNavigationBar),
                    false);

        /// <summary>
        /// Gets or sets a value indicating whether the title of the selected item should be shown
        /// next to its icon.
        /// Default is <c>false</c>.
        /// </summary>
        public bool IsShowTitle
        {
            get => (bool)GetValue(IsShowTitleProperty);
            set => SetValue(IsShowTitleProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="SelectedItemOffset"/> bindable property.
        /// Specifies the vertical offset (in DIPs) to apply to the selected item,
        /// creating a lifted or elevated appearance.
        /// </summary>
        public static readonly BindableProperty SelectedItemOffsetProperty =
            BindableProperty.Create(
                nameof(SelectedItemOffset),
                typeof(double),
                typeof(BottomNavigationBar),
                -30.0);

        /// <summary>
        /// Gets or sets the vertical offset for the selected item in the navigation bar.
        /// A negative value raises the item above the bar.
        /// Default is <c>-30.0</c>.
        /// </summary>
        public double SelectedItemOffset
        {
            get => (double)GetValue(SelectedItemOffsetProperty);
            set => SetValue(SelectedItemOffsetProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="NotchDepthFactor"/> bindable property.
        /// Controls the depth of the circular notch (used for the selected item).
        /// A value of 1.0 creates a semi-circle; higher values create deeper curves (e.g., 1.6–1.8).
        /// 1.0 = semi-circle; >1 = deeper U; try 1.6–1.8 for 60–80% depth
        /// </summary>
        public static readonly BindableProperty NotchDepthFactorProperty =
            BindableProperty.Create(
                nameof(NotchDepthFactor),
                typeof(double),
                typeof(BottomNavigationBar),
                1.6,
                propertyChanged: (_, __, ___) => ((BottomNavigationBar)_)
                    .Rebuild());

        /// <summary>
        /// Gets or sets the depth of the notch that contains the selected item.
        /// Increasing this value creates a more pronounced U-shaped indentation.
        /// Default is <c>1.6</c>.
        /// </summary>
        public double NotchDepthFactor
        {
            get => (double)GetValue(NotchDepthFactorProperty);
            set => SetValue(NotchDepthFactorProperty, value);
        }

        /// <summary>
        /// Represents a single item in the <see cref="BottomNavigationBar"/> control.
        /// Each item can display an icon (glyph or image), a title, and respond to user interaction through a command.
        /// </summary>
        public class MenuItem
        {
            /// <summary>
            /// Gets or sets the glyph (character icon) to display for the menu item.
            /// This is typically used with a font icon (e.g., FontAwesome).
            /// If <see cref="Glyph"/> is set, it takes precedence over the glyph.
            /// </summary>
            public string Glyph { get; set; }

            /// <summary>
            /// Gets or sets the path or resource identifier for the image to display.
            /// If this is set, it will be shown instead of <see cref="Image"/>.
            /// </summary>
            public string Image { get; set; }

            /// <summary>
            /// Gets or sets the font family used to render the <see cref="Glyph"/>.
            /// This should be set when using custom icon fonts.
            /// </summary>
            public string FontFamily { get; set; }

            /// <summary>
            /// Gets or sets the display name or label for the menu item.
            /// This is shown only if <see cref="BottomNavigationBar.IsShowTitle"/> is <c>true</c>.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the command to execute when the item is selected or tapped.
            /// This allows each menu item to perform a specific action.
            /// </summary>
            public ICommand Command { get; set; }
        }


        /// <summary>
        /// Rebuilds the layout of the bottom navigation bar UI based on the current
        /// <see cref="ItemsSource"/>, <see cref="SelectedIndex"/>, and other visual properties.
        /// This method is invoked when items are added/removed, the selection changes,
        /// or the component is reloaded.
        /// </summary>
        /// <remarks>
        /// The method introduces a slight delay to avoid redundant layout calls, ensures execution
        /// on the UI thread, and dynamically creates the item layout including:
        /// - The notch or mask for selected item
        /// - Icon display (glyph or image)
        /// - Optional visual enhancements like dot, line, title
        /// </remarks>
        private async void Rebuild()
        {
            // Delay to debounce rapid layout triggers (e.g., fast binding changes)
            await Task.Delay(200);

            // Skip if the page is not active (e.g., navigated away or unloaded)
            if (!_isPageActive) return;

            // Ensure layout code runs on the UI thread
            MainThread.BeginInvokeOnMainThread(() =>
            {
                // Skip layout updates if ItemsGrid is not ready or not visible
                if (ItemsGrid?.Handler == null || !ItemsGrid.IsVisible)
                    return;

                ItemsGrid.Children.Clear();
                ItemsGrid.ColumnDefinitions.Clear();

                if (ItemsSource == null || ItemsSource.Count == 0)
                    return;

                // Set up columns based on whether title is shown for selected item
                for (int i = 0; i < ItemsSource.Count; i++)
                {
                    if (IsShowTitle && i == SelectedIndex)
                    {
                        ItemsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
                    }
                    else
                    {
                        ItemsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    }
                }

                // Add UI elements for each item
                for (int i = 0; i < ItemsSource.Count; i++)
                {
                    var item = ItemsSource[i];
                    bool isSel = (i == SelectedIndex);
                    int idx = i;

                    // Setup tap gesture for selection
                    var tap = new TapGestureRecognizer
                    {
                        Command = new Command(() =>
                        {
                            SelectedIndex = idx;
                            item.Command?.Execute(null);
                        })
                    };

                    if (isSel)
                    {
                        // Selected item layout
                        var grid = new Grid()
                        {
                            Padding = 0,
                            Margin = 0,
                        };

                        double h = IconHeight;
                        double notchDim = h * NotchDepthFactor;
                        double maskOffset = SelectedItemOffset;

                        double bottomMargin = BarMargin.Bottom;
                        double leftSideMargin = (bottomMargin > 0 && (i == 0)) ? 55 : 0;
                        double rightSideMargin = (bottomMargin > 0 && (i == ItemsSource.Count - 1)) ? 35 : 0;

                        // mask frame (page‐background) to “cut” deeper notch
                        var mask = new Frame
                        {
                            CornerRadius = (float)(notchDim / 2) + (IsSelectedItemRequiredExtraSpace ? 20 : 0),
                            BackgroundColor = IsSelectedItemRequiredExtraSpace ? ExtraSpaceColor : Colors.Transparent,
                            BorderColor = Colors.Transparent,
                            Padding = 0,
                            HasShadow = false,
                            HeightRequest = IsSelectedItemRequiredExtraSpace ? notchDim : h,
                            WidthRequest = IsSelectedItemRequiredExtraSpace ? notchDim : h,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.Fill,
                            TranslationY = -maskOffset,
                            Margin = new Thickness(0, 0, 0, bottomMargin)
                        };

                        mask.GestureRecognizers.Add(tap);

                        // colored circle with icon
                        var circle = new Frame
                        {
                            CornerRadius = (float)(h / 2),
                            BackgroundColor = SelectedItemBackgroundColor,
                            BorderColor = SelectedItemBorderColor,
                            Padding = 0,
                            HasShadow = false,
                            HeightRequest = h,
                            WidthRequest = h,
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                        };

                        // Icon logic: either glyph or image
                        var glyphIcon = new Label
                        {
                            Text = item.Glyph,
                            FontFamily = item.FontFamily,
                            FontSize = 28,
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                            TextColor = UnselectedIconColor,
                        };

                        IconTintColorBehavior iconTintColor = new IconTintColorBehavior()
                        {
                            TintColor = SelectedIconColor
                        };


                        if (string.IsNullOrEmpty(item.Image))
                        {
                            circle.Content = glyphIcon;
                        }
                        else
                        {
                            var imageIcon = new Image
                            {
                                Source = item.Image,
                                WidthRequest = 28,
                                HeightRequest = 28,
                            };
                            imageIcon.Behaviors.Add(iconTintColor);
                            circle.Content = imageIcon;
                        }

                        var dot = new Microsoft.Maui.Controls.Shapes.Path
                        {
                            Fill = DotColor,
                            Stroke = DotColor,
                            Data = new EllipseGeometry()
                            {
                                Center = new Point(4, 4),
                                RadiusX = 4,
                                RadiusY = 4
                            },
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Start,
                        };

                        // Adjust layout for dot appearance
                        if (IsShowDot)
                        {
                            mask.Padding = new Thickness(0, 4, 0, 0);
                            circle.HeightRequest = h - 4;
                            circle.WidthRequest = h - 4;
                            circle.CornerRadius = (float)((h - 4) / 2);
                            circle.Margin = new Thickness(0, 8, 0, 0);
                        }

                        // Optional title layout
                        if (IsShowTitle)
                        {
                            circle.Margin = new Thickness(0);

                            HorizontalStackLayout selectedItemStack = new HorizontalStackLayout()
                            {
                                HorizontalOptions = LayoutOptions.Center,
                                VerticalOptions = LayoutOptions.Center,
                            };

                            if (string.IsNullOrEmpty(item.Image))
                                selectedItemStack.Children.Add(glyphIcon);
                            else
                            {
                                var imageIcon = new Image
                                {
                                    Source = item.Image,
                                    WidthRequest = 28,
                                    HeightRequest = 28,
                                };
                                imageIcon.Behaviors.Add(iconTintColor);
                                selectedItemStack.Children.Add(imageIcon);
                            }


                            double widthInPixels = DeviceDisplay.MainDisplayInfo.Width;

                            circle.WidthRequest = h * 2.5;
                            grid.WidthRequest = h * 2.5;

                            var label = new Label()
                            {
                                Text = item.Name,
                                TextColor = SelectedIconColor,
                                Padding = new Thickness(8, 0, 0, 0),
                                FontSize = 18,
                                FontAttributes = FontAttributes.Bold,
                                VerticalOptions = LayoutOptions.Center
                            };

                            selectedItemStack.Children.Add(label);
                            circle.Content = selectedItemStack;

                            grid.Children.Add(circle);
                            grid.Children.Add(dot);
                        }
                        else
                        {
                            grid.Children.Add(circle);
                            grid.Children.Add(dot);
                        }

                        // Optional underline
                        if (IsShowLine)
                        {
                            var Line = new Border()
                            {
                                WidthRequest = 28,
                                HeightRequest = 5,
                                MaximumHeightRequest = 5,
                                Padding = 0,
                                StrokeShape = new RoundRectangle(),
                                Margin = LineMargin,
                                Stroke = LineColor,
                                BackgroundColor = LineColor,
                                VerticalOptions = LineVerticalOptions,
                                HorizontalOptions = LineHorizontalOptions
                            };
                            grid.Children.Add(Line);
                        }

                        mask.Content = grid;
                        Grid.SetColumn(mask, i);
                        ItemsGrid.Children.Add(mask);
                        ItemsGrid.Margin = ItemGridMargin;
                    }
                    else
                    {
                        // Unselected item layout
                        double bottomMargin = BarMargin.Bottom;
                        double leftSideMargin = (bottomMargin > 0 && (i == 0)) ? 55 : 0;
                        double rightSideMargin = (bottomMargin > 0 && (i == ItemsSource.Count - 1)) ? 35 : 0;
                        // --- unselected: just icon ---
                        var container = new Grid
                        {
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            Margin = new Thickness(0, 0, 0, bottomMargin)
                        };
                        container.GestureRecognizers.Add(tap);

                        if (string.IsNullOrEmpty(item.Image))
                        {
                            var icon = new Label
                            {
                                Text = item.Glyph,
                                FontFamily = item.FontFamily,
                                FontSize = 28,
                                HorizontalOptions = LayoutOptions.Center,
                                VerticalOptions = LayoutOptions.Center,
                                TextColor = UnselectedIconColor
                            };
                            container.Children.Add(icon);
                        }
                        else
                        {
                            IconTintColorBehavior iconTintColor = new IconTintColorBehavior()
                            {
                                TintColor = UnselectedIconColor
                            };

                            var icon = new Image
                            {
                                Source = item.Image,
                                WidthRequest = 28,
                                HeightRequest = 28,
                            };
                            icon.Behaviors.Add(iconTintColor);
                            container.Children.Add(icon);
                        }

                        Grid.SetColumn(container, i);
                        ItemsGrid.Children.Add(container);
                        ItemsGrid.Margin = ItemGridMargin;
                    }
                }
            });
        }
    }
}