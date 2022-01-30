using Microsoft.AspNetCore.Mvc;

namespace Suchmasy.ViewComponents
{
    public class LeadingTitleViewComponent : ViewComponent
    {


        public async Task<IViewComponentResult> InvokeAsync(string title, string description)
        {
            var lt = new LeadingTitleObject()
            {
                Title = title,
                Description = description,
            };

            return View(lt);
        }
    }

    public class LeadingTitleObject
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
