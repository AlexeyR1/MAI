﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.Models.Abstract;
using System.Text.Json.Serialization;

namespace TrackingTasksProgressSystem.Models
{
    public class ProblemAttachment : BaseAttachment
    {
        public ProblemAttachment(string name, byte[] data, DateTime createdAt) : base(name, data, createdAt) { }
    }
}