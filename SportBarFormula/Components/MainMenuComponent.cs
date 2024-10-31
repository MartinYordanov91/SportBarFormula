using Microsoft.AspNetCore.Mvc;

namespace SportBarFormula.Components;

[ViewComponent(Name = "MainMenu")]
public class MainMenuComponent  : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return await Task.FromResult<IViewComponentResult>(View());
    }

}
