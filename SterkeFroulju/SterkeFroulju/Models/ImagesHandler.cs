using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WZWVAPI;

namespace SterkeFroulju.Models
{
    public class ImageHandler : DataHandler
    {
        private static readonly Field FilePathField = new Field("FilePath", typeof(string), 250);
        private static readonly Field FileCommentField = new Field("FileComment", typeof(string), 750);
        private static readonly Field TimePostedField = new Field("TimePosted", typeof(DateTime), 1);
        private static readonly Field UserIDField = new Field("UserID", typeof(int), 1);

        public static readonly ImageHandler instance = new ImageHandler();

        private ImageHandler() : base("Images", new Field[]
            {
                FilePathField,
                FileCommentField,
                TimePostedField,
                UserIDField
            }, typeof(Image))
        {

        }

        public List<Image> GetImageList()
        {
            return base.GetObjectList(0, OrderBy.ASC, FilePathField).Cast<Image>().ToList();
        }
    }
}