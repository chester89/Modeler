﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ViewModeler
{
    public class Messenger: IMessenger
    {
        public void SendMessage(string content, string title)
        {
            MessageBox.Show(content, title);
        }
    }
}
