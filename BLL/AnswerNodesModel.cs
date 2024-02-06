
using DAL.Core;
using Entity;

namespace MVC.Models
{
    public class AnswerNodesModel
    {
        protected UnitOfWork uow = new UnitOfWork();

        public int AnswerId{ get; }

        #region AnswerNodes
        List<AnswerNote> _answerNodes;
        public List<AnswerNote> AnswerNodes
        {
            get
            {
                if (_answerNodes == null)
                {
                    _answerNodes = uow.GetRepository<AnswerNote>().Where(x => x.AnswerId == AnswerId).OrderBy(x=>x.Id).ToList();
                }
                return _answerNodes;
            }
        }

        #endregion

        public AnswerNodesModel(int answerId)
        {
            AnswerId = answerId;

        }

        public void AddNote(string note)
        {
            var r = uow.GetRepository<AnswerNote>();
            var an = new AnswerNote() { AnswerId = AnswerId, Note = note, Cdate = DateTime.Now, Ldate = DateTime.Now };
            r.Create(an);
            r.Save();
            _answerNodes = null;
        }

        public void EditNote(int id, string note)
        {
            var repos = uow.GetRepository<AnswerNote>();
            var r = repos.FirstOrDefault(x=>x.Id==id,false);
            r.Note = note;
            repos.Save();
        }

        public void DeleteNote(int deletedNodeId)
        {
           var repos = uow.GetRepository<AnswerNote>();
            repos.Delete(deletedNodeId);
            repos.Save();
        }
    }
}
