// Code sourced from this blog: https://chrissainty.com/creating-a-custom-validation-message-component-for-blazor-forms/

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Microsoft.AspNetCore.Components.Forms
{
    public class BS4ValidationMessageBase<TValue> : ComponentBase, IDisposable
    {
        private FieldIdentifier _fieldIdentifier;

        [CascadingParameter] EditContext EditContext { get; set; }
        [Parameter] public Expression<Func<TValue>> For { get; set; }
        [Parameter] public string Class { get; set; }

        protected IEnumerable<string> ValidationMessages => EditContext.GetValidationMessages(_fieldIdentifier);

        protected override void OnInitialized()
        {
            _fieldIdentifier = FieldIdentifier.Create(For);
            EditContext.OnValidationStateChanged += HandleValidationStateChanged;
        }

        private void HandleValidationStateChanged(object sender, ValidationStateChangedEventArgs eventArgs)
        {
            StateHasChanged();
        }

        public void Dispose()
        {
            EditContext.OnValidationStateChanged -= HandleValidationStateChanged;
        }
    }
}
