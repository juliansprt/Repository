using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class ExportAttribute : Attribute
    {
        #region  Constructor

        public ExportAttribute()
        {
            this.InstanceType = InstanceType.SingleInstance;
        }

        public ExportAttribute(InstanceType instanceType)
        {
            this.InstanceType = instanceType;
        }

        #endregion

        #region Properties

        public InstanceType InstanceType { get; set; }

        #endregion
    }

    public enum InstanceType
    {
        SingleInstance,
        InstancePerDependency,
        InstancePerRequest
    }
}
