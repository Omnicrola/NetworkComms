using System;
using System.Windows;

namespace NetworkClient
{
    public class UiDispatcher
    {
        public void Dispatch(Action action)
        {
            Application.Current.Dispatcher.BeginInvoke(action);

        }
    }
}