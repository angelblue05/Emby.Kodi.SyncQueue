﻿using NanoApi.JsonFile;
using System;

namespace Emby.Kodi.SyncQueue.Entities
{
    public class ItemRec
    {
        //Status 0 = Added
        //Status 1 = Updated
        //Status 2 = Removed

        [PrimaryKey]
        public int Id { get; set; }
        public string ItemId { get; set; }
        public long LastModified { get; set; }
        public int Status { get; set; }
    }
}