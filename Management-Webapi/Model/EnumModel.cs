using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Management_Webapi.Model
{
    public enum Priority
    {
        [Description("Low")]
        Low = 0,
        [Description("Medium")]
        Medium = 1,
        [Description("High")]
        High = 2
    }
    public enum ResponseError
    {
        [Description("No err")]
        NoError,

        #region Error Base
        [Description("Data null")]
        NotData = 30000,

        [Description("Request Null")]
        RequestNull = 30001,

        [Description("Unauthorized")]
        Unauthorized = 401,

        [Description("Reqs Invalid")]
        InvalidReqs = 400
        #endregion

    }
    public enum Sort
    {
        [Description("No Sort")]
        NoSort = 0,
        [Description("Priority")]
        Priority = 1,
    }
    public enum Filter
    {
        [Description("No Filter")]
        NoSort = 0,
        [Description("Priority")]
        Priority = 1,
    }
}
