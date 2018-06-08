using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace vyger.Web.Controllers
{
    public abstract class BaseController<TController> : Controller
        where TController : Controller
    {
        #region Methods

        public void AddFlashMessage(FlashMessageType type, string message)
        {
            object messages = null;

            if (!TempData.TryGetValue("FlashMessage", out messages))
            {
                messages = new FlashMessageCollection();

                TempData["FlashMessage"] = messages;
            }

            (messages as FlashMessageCollection).Add(new FlashMessage(type, message));
        }

        #endregion
    }
}