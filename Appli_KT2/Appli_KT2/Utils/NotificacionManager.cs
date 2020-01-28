using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_KT2.Utils
{
    public interface INotificacionManager
    {
        event EventHandler NoticationReceived;
        void Initialize();

        int ScheduleNotification(string title, string message);

        void ReceiveNotification(string title, string message);
    }
}
