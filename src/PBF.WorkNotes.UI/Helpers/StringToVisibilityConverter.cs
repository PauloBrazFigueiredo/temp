namespace PBF.WorkNotes.UI.Helpers;

public class StringToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
            return Visibility.Collapsed;
        return value?.ToString() switch
        {
            "Done" => Visibility.Visible,
            "Active" => Visibility.Collapsed,
            _ => Visibility.Visible
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is Visibility visibility && visibility == Visibility.Visible;
    }
}
