using Microsoft.AspNetCore.Components;

namespace SquadManagement.FrontEnd.Client.Components.Common.Table
{
    public partial class TableContent
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
