using Xamarin.Forms;

namespace HomeAssistant.ViewModel
{
    /// <summary>
    /// Interface for View Models that require background selection at runtime.
    /// </summary>
    public interface IThemedViewModelBase
    {
        ImageSource Background { get; set; }
    }
}
