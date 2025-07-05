namespace PBF.WorkNotes.UI.Helpers;

public class StringToSymbolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return  value?.ToString() switch
        {
            "Done" =>"✓",
            "Active" => string.Empty,
            _ => string.Empty
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
