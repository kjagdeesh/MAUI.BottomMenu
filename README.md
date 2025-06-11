# 📱 MAUI Bottom Menu Styles (Android & iOS) 

A cross-platform .NET MAUI sample project showcasing multiple customizable bottom navigation menu styles tailored for both Android and iOS platforms. This project demonstrates how to create and switch between various bottom menu designs using reusable components, MVVM architecture, and platform-specific enhancements.

---

## ✨ Features
- 🎨 Fully customizable menu items: icons, labels, active state, colors.
- 🔄 Dynamic navigation between tabs using MVVM pattern.
- 🧱 Component-based architecture for easy reuse and extension.

---

## 📦 Bindable Properties

### ItemsSource

- **Type**: `ObservableCollection<MenuItem>`
- **Description**: The collection of menu items displayed in the navigation bar.

### SelectedIndex

- **Type**: `int`
- **Default**: `0`
- **Description**: The index of the currently selected item.

### BarBackgroundColor

- **Type**: `Color`
- **Default**: `Colors.Black`
- **Description**: The background color of the entire navigation bar.

### BarHeight

- **Type**: `double`
- **Default**: `60.0`
- **Description**: The height of the navigation bar in device-independent units.

### BarCornerRadius

- **Type**: `CornerRadius`
- **Default**: `0`
- **Description**: Radius for rounding the corners of the navigation bar.

### BarMargin

- **Type**: `Thickness`
- **Default**: `new Thickness(0)`
- **Description**: Margin around the entire navigation bar.

### IconHeight

- **Type**: `double`
- **Default**: `60.0`
- **Description**: Height of each icon in the navigation items.

### UnselectedIconColor

- **Type**: `Color`
- **Default**: `Colors.White`
- **Description**: Color used for unselected icons.

### SelectedIconColor

- **Type**: `Color`
- **Default**: `Colors.Black`
- **Description**: Color used for the selected icon.

### SelectedItemBackgroundColor

- **Type**: `Color`
- **Default**: `Colors.Yellow`
- **Description**: Background color of the selected menu item.

### SelectedItemBorderColor

- **Type**: `Color`
- **Default**: `Colors.Transparent`
- **Description**: Border color of the selected menu item.

### IsSelectedItemRequiredExtraSpace

- **Type**: `bool`
- **Default**: `false`
- **Description**: Determines if extra space is rendered around the selected item.

### IsShowDot

- **Type**: `bool`
- **Default**: `false`
- **Description**: Whether to show a dot indicator above the selected item.

### DotColor

- **Type**: `Color`
- **Default**: `Colors.Transparent`
- **Description**: The color of the dot indicator (if enabled).

### IsShowLine

- **Type**: `bool`
- **Default**: `false`
- **Description**: Whether to show a line indicator below the selected item.

### LineColor

- **Type**: `Color`
- **Default**: `Colors.Transparent`
- **Description**: Color of the line shown beneath the selected item.

### LineMargin

- **Type**: `Thickness`
- **Default**: `new Thickness(0)`
- **Description**: Margin around the line indicator.

### LineHorizontalOptions

- **Type**: `LayoutOptions`
- **Default**: `Center`
- **Description**: Horizontal alignment of the line indicator.

### LineVerticalOptions

- **Type**: `LayoutOptions`
- **Default**: `Center`
- **Description**: Vertical alignment of the line indicator.

### ExtraSpaceColor

- **Type**: `Color`
- **Default**: `Colors.Transparent`
- **Description**: Background color for the optional extra space rendered around the selected item.

### IsShowTitle

- **Type**: `bool`
- **Default**: `false`
- **Description**: Whether to display the title (label) for the selected item.

### UnselectedItemBorderColor

- **Type**: `Color`
- **Default**: `Colors.Transparent`
- **Description**: Border color for unselected menu items.

### SelectedItemOffset

- **Type**: `double`
- **Default**: `-30.0`
- **Description**: Vertical offset for the selected item to give a "lifted" effect.

### NotchDepthFactor

- **Type**: `double`
- **Default**: `1.6`
- **Description**: Controls how deep the notch is below the selected item. Higher values result in deeper U-shapes.

---

## 📘 MenuItem Model

Each item in the `ItemsSource` should be a `MenuItem` object:

```csharp
public class MenuItem
{
    public string Glyph { get; set; }
    public string Image { get; set; }
    public string FontFamily { get; set; }
    public string Name { get; set; }
    public ICommand Command { get; set; }
}
```

---

## ⚙️ Behavior

- **Selected item**: Highlighted with custom colors, offset, and optional dot/line/title.
- **Rebuilds UI**: When collection or selection changes.
- **Fully customizable**: All aspects of layout, colors, spacing, and behavior can be styled via bindable properties.

---

## ✅ Usage Example

### XAML

```xml
<ctrls:BottomNavigationBar
    Grid.Row="1"
    BarHeight="70"
    IconHeight="50"
    NotchDepthFactor="0"
    SelectedItemOffset="0"
    BarCornerRadius="20"
    BarMargin="20,0,20,40"
    IsSelectedItemRequiredExtraSpace="False"
    SelectedItemBorderColor="Transparent"
    BarBackgroundColor="White"
    SelectedItemBackgroundColor="#fce7b1"
    UnselectedIconColor="#3d4455"
    SelectedIconColor="#e3a402"
    ItemsSource="{Binding MenuItems}"
    SelectedIndex="{Binding SelectedTabIndex, Mode=TwoWay}" 
/>
```

### ViewModel

```csharp
MenuItems1 = new ObservableCollection<BottomNavigationBar.MenuItem>
{
    new() { Image = "home", Command = new Command(() => ExecuteTab(0)), Name = "Home" },
    new() { Image = "calendar", Command = new Command(() => ExecuteTab(1)), Name = "Events" },
    new() { Image = "setting", Command = new Command(() => ExecuteTab(2)), Name = "Setting" },
    new() { Image = "cart", Command = new Command(() => ExecuteTab(3)), Name = "Cart" }
};
```
<img src="https://github.com/kjagdeesh/MAUI.BottomMenu/blob/master/images/1.png" width=600/><br><br>

`**Note: To explore the different menu styles, check out the images in the Images folder.**`
