﻿using System;
using System.Collections.Generic;

namespace GF.DillyDally.ReadModel.Views.TaskBoard
{
    public class TaskBoardLaneEntity
    {
        public Guid LaneId { get; set; }

        public string Name { get; set; }

        public IList<TaskBoardTaskEntity> Tasks { get; set; }
    }
}