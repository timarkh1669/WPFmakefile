﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.WPFMakefile.TopologicalSort
{
    interface IGraphVertex<T>
    {
        List<T> OutEdges { get; }
    }
}
