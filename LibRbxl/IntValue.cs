﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRbxl
{
    public class IntValue : Instance
    {
        public override string ClassName => "IntValue";

        public int Value { get; set; }
    }
}
