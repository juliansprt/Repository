using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.UnitOfWork
{
    public class WorkContext
    {

        public UnitOfWork CurrentUnitOfWork { get; private set; }

        public virtual bool IsUnitOfWorkRunning => this.CurrentUnitOfWork != null &&
            this.CurrentUnitOfWork.Status == UnitOfWorkStatus.Running;


        public UnitOfWorkExecution BeginUnitOfWork(string key,
            IApplicationDatabaseContext applicationDatabaseContext,
            bool useTransaction)
        {
            var execution = new UnitOfWorkExecution(key);
            execution.OnEnd(() => this.CurrentUnitOfWork = null);
            this.CurrentUnitOfWork = execution.Begin(applicationDatabaseContext, useTransaction);
            return execution;
        }
    }
}
