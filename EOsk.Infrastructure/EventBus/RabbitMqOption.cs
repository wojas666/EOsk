﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOsk.Infrastructure.EventBus
{
    public class RabbitMqOption
    {
        public string ConnectionString { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

    }
}
