﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModeler
{
    public interface IMessenger
    {
        void SendMessage(string content, string title);
    }
}
