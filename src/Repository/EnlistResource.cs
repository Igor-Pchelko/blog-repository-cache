using System;
using System.Transactions;

namespace Repository
{
    public class EnlistResource : IEnlistmentNotification
    {
        private readonly Action _onCommit;
 
        public EnlistResource(Action onCommit)
        {
            _onCommit = onCommit;
        }
 
        public void Prepare(PreparingEnlistment preparingEnlistment)
        {
            preparingEnlistment.Prepared();
        }
 
        public void Commit(Enlistment enlistment)
        {
            _onCommit();
            enlistment.Done();
        }
 
        public void Rollback(Enlistment enlistment)
        {
            enlistment.Done();
        }
 
        public void InDoubt(Enlistment enlistment)
        {
            enlistment.Done();
        }
    }
}