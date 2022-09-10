﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Domains
{
    public class BaseEntity
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
