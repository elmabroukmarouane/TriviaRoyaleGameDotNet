using Microsoft.AspNetCore.Components;

namespace CATEGORYManagement.FrontEnd.Client.Components.Common.Table
{
    public partial class TableContent
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
