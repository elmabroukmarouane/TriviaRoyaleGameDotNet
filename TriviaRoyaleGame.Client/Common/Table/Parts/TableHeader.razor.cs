using Microsoft.AspNetCore.Components;

namespace SquadManagement.FrontEnd.Client.Components.Common.Table.Parts
{
    public partial class TableHeader
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
