using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using WZWVAPI;

namespace SterkeFroulju.Models
{
    public class Pagina : DataObject
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime TimeStamp { get; set; }
        public int PostedByID { get; set; }
        public bool Published { get; set; }

        public User PostedBy
        {
            get
            {
                return UserHandler.UH.GetObjectByID(PostedByID) as User;
            }
        }

        public Pagina[] MenuPaginas { get; private set; }

        public Pagina(int ID, string Title, string Body, DateTime TimeStamp, int postedByID, bool Published) : base(ID)
        {
            this.Title = Title;
            this.Body = Body;
            this.TimeStamp = TimeStamp;
            this.PostedByID = postedByID;
            this.Published =Published;

            if (ID == 0)
            {
                this.TimeStamp = DateTime.Now;

                try
                {
                    this.PostedByID = UserHandler.GetLoggedInUser().ID;
                }
                catch
                {
                    this.PostedByID = 1;
                }

                PaginaHandler.instance.AddObject(this);
            }
        }
    }
}