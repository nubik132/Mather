﻿using Mather.Data.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mather.Data.Tasks
{
    public interface ILogable
    {
        public LogElement GetLog();
    }
}