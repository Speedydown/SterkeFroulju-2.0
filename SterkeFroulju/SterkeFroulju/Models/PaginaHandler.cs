using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WZWVAPI;

namespace SterkeFroulju.Models
{
    public class PaginaHandler : DataHandler
    {
        private static readonly Field TitleField = new Field("Title", typeof(string), 350);
        private static readonly Field BodyField = new Field("Body", typeof(string), 5000);
        private static readonly Field TimeStampField = new Field("TimeStamp", typeof(DateTime), 1);
        private static readonly Field PostedByIDField = new Field("PostedByID", typeof(int), 1);
        private static readonly Field PublishedField = new Field("Published", typeof(bool), 1);

        public static readonly PaginaHandler instance = new PaginaHandler();

        private PaginaHandler()
            : base("Paginas", new Field[]
            {
                TitleField,
                BodyField,
                TimeStampField,
                PostedByIDField,
                PublishedField
            }, typeof(Pagina))
        {

        }

        public Pagina GetPageByURLOrDefault(string URL)
        {
            List <Pagina> Paginas = base.GetObjectByFieldsAndSearchQuery(new Field[] { TitleField }, URL, true, 0, OrderBy.ASC, TitleField).Cast<Pagina>().ToList();
            
            if (Paginas.Count > 0)
            {
                return Paginas.First();
            }

            return GetObjectByID(1) as Pagina;
        }

        public List<Pagina> GetPaginaList(bool PublishedOnly = true)
        {
            if (PublishedOnly)
            {
                return base.GetObjectsByChildObjectID(PublishedField, 1, 0, OrderBy.ASC, TitleField).Cast<Pagina>().ToList();
            }
            else
            {
                return base.GetObjectList(0, OrderBy.ASC, TitleField).Cast<Pagina>().ToList();
            }
        }

        public void DeletePagina(int ID)
        {
            if (ID == 1)
            {
                return;
            }

            base.DeleteObjectByID(ID);
        }
    }
}