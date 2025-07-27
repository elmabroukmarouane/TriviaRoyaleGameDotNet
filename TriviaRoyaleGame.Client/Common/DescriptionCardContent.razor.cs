using Microsoft.AspNetCore.Components;

namespace CATEGORYManagement.FrontEnd.Client.Components.Common
{
    public partial class DescriptionCardContent
    {
        [Parameter] 
        public RenderFragment? ChildContent { get; set; }
    }
}
