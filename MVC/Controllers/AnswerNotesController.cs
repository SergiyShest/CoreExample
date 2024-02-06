using DAL.Core;
using DAL;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Entity.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using NPOI.SS.Formula.Functions;
using Org.BouncyCastle.Utilities;
using static NpgsqlTypes.NpgsqlTsQuery;
using MVC.Models;

namespace Entity.Controllers
{   [Authorize]
    public class AnswerNotesController : GenericCardController<AnswerNote>
    {

     public AnswerNotesController(IHttpContextAccessor httpContextAccessor):base(httpContextAccessor) { }


        public override async Task<IActionResult> Get(int? id, string? note)
        {
            string json = base.GetJsonSafe(() =>
            {
                var model = getModel(id);
                return  model.AnswerNodes;
            });

            return Content(json, "application/json");
        }

        public  async Task<IActionResult> Set(int? id)
        {
            string json = base.GetJsonSafe(() =>
            {
                var body = base.Body();
                var noteOb = JsonConvert.DeserializeObject<AnswerNote>(body);
                var model = getModel(id);
                if (noteOb.Id != null)
                { model.EditNote((int)noteOb.Id, noteOb.Note);return "Item updated"; }
                else { model.AddNote(noteOb.Note);return "Item added"; }
            });

            return Content(json, "application/json");
        }

        public  async Task<IActionResult> Delete(int? id,int? noteId)
        {
            string json = base.GetJsonSafe(() =>
            {
                if (noteId == null) throw new ArgumentNullException(nameof(noteId));
                var model = getModel(id);
                model.DeleteNote((int)noteId);
                return "Item deleted";
            });

            return Content(json, "application/json");
        }

        private static AnswerNodesModel getModel(int? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            var model = new AnswerNodesModel((int)id);
            return model;
        }
    }
}