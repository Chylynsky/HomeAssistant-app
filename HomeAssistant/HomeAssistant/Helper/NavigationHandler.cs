using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HomeAssistant.Helper
{
    class NavigationHandler
    {
        private readonly uint AnimationLength = 225U;

        private Stack<ContentView> viewStack;

        private Grid contentGrid;

        private ContentView mainContentView;

        public ContentView Content
        {
            get
            {
                return mainContentView;
            }
        }

        public NavigationHandler(ContentView mainView)
        {
            viewStack = new Stack<ContentView>();
            viewStack.Push(mainView);

            contentGrid = new Grid();
            contentGrid.Children.Add(mainView);

            mainContentView = new ContentView();
            mainContentView.Content = contentGrid;
        }

        public async Task NavigateToAsync(ContentView contentView)
        {
            ContentView currentView = viewStack.Peek();

            contentView.TranslationX = currentView.Width;
            viewStack.Push(contentView);

            contentGrid.Children.Add(contentView);

            await Task.WhenAll(
                currentView.TranslateTo(-60.0, 0.0, AnimationLength, Easing.SinOut),
                contentView.TranslateTo(0.0, 0.0, AnimationLength, Easing.SinOut));

            contentGrid.Children.RemoveAt(0);
        }

        public async Task NavigateBackAsync()
        {
            ContentView currentView = viewStack.Pop();
            ContentView previousView = viewStack.Peek();

            contentGrid.Children.Insert(0, previousView);

            await Task.WhenAll(
                previousView.TranslateTo(0.0, 0.0, AnimationLength, Easing.SinOut),
                currentView.TranslateTo(currentView.Width, 0.0, AnimationLength, Easing.SinOut));

            contentGrid.Children.RemoveAt(1);
        }

        public async Task NavigateToMainViewAsync()
        {
            ContentView currentView = viewStack.Peek();

            while (viewStack.Count != 1)
            {
                viewStack.Pop();
            }

            viewStack.Push(currentView);
            await NavigateBackAsync();
        }
    }
}
