﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp
{
    public interface IClientRepository
    {
      public Client GetById(int clientId);
    }
}
