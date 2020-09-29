using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HomeAssistant.Helper
{
    class NavigationHandler
    {
        private Stack<ContentView> viewStack;

        public ContentView CurrentView
        {
            get
            {
                return viewStack.Peek();
            }
        }

        public NavigationHandler(ContentView mainView)
        {
            viewStack = new Stack<ContentView>();
            viewStack.Push(mainView);
        }

        public async Task NavigateTo(ContentView contentView)
        {
            if (contentView == mainView)
            {
                return;
            }


        }

        public async Task NavigateBack()
        {

        }

        public async Task NavigateToMainView()
        {

        }
    }
}
