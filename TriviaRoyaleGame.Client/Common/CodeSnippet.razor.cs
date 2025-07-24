using Microsoft.AspNetCore.Components;

namespace SquadManagement.FrontEnd.Client.Components.Common
{
    public partial class CodeSnippet
    {
        [Parameter] public RenderFragment? ChildContent { get; set; }
    }
}
