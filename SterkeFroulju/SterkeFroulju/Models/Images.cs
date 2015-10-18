using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WZWVAPI;

namespace SterkeFroulju.Models
{
    public class Image : DataObject
    {
        public string FilePath { get; private set; }
        public string FileComment { get; private set; }
        public DateTime TimePosted { get; private set; }
        public int UserID { get; private set; }

        public Image(int ID, string FilePath, string FileComment, DateTime TimePosted, int UserID)
            : base(ID)
        {
            this.FilePath = FilePath;
            this.FileComment = FileComment;
            this.TimePosted = TimePosted;
            this.UserID = UserID;

            if (ID == 0)
            {
                ImageHandler.instance.AddObject(this);
            }
        }
    }
}