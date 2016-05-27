using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Common.Logging;
using LightDataInterface.Core;

namespace LightDataInterface.Extra.WebApi
{
    public class UnitOfWorkAttribute: Attribute, IActionFilter
    {
        private static readonly ILog Log = LogManager.GetLogger<UnitOfWorkAttribute>();

        /// <summary>
        /// DataSession name, if not provided the default DataSession will be used.
        /// </summary>
        public string DataName { get; set; }

        #region Implementation of IFilter

        public bool AllowMultiple => false;

        #endregion

        #region Implementation of IExceptionFilter

        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IFilter
        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            var dataSessionHolder = (DataSessionHolder) actionContext.Request.GetDependencyScope().GetService(typeof (DataSessionHolder));
            var dataSession = dataSessionHolder.GetByName(DataName);
            Log.Debug("Creating unit of work.");
            using (var unitOfWork = dataSession.CreateUnitOfWork())
            {
                unitOfWork.AutoCommit = false;
                var ret = await continuation();
                Log.Debug("Commiting unit of work.");
                unitOfWork.Commit();
                return ret;
            }
        }

        #endregion
    }
}
