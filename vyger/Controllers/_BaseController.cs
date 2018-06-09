using System.Web.Mvc;

namespace vyger.Controllers
{
    public abstract partial class BaseController : Controller
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