﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public static class Factory
    {
        public static IBl Get() { return new BlImplementation.Bl();}
    }
}