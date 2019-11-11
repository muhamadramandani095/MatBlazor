﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MatBlazor
{
    public abstract class BaseMatInputTextComponent<T> : BaseMatInputElementComponent<T>
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> IconOnClick { get; set; }

        [Parameter]
        public EventCallback<FocusEventArgs> OnFocus { get; set; }

        [Parameter]
        public EventCallback<FocusEventArgs> OnFocusOut { get; set; }

        [Parameter]
        public EventCallback<KeyboardEventArgs> OnKeyPress { get; set; }

        [Parameter]
        public EventCallback<KeyboardEventArgs> OnKeyDown { get; set; }

        [Parameter]
        public EventCallback<KeyboardEventArgs> OnKeyUp { get; set; }

        [Parameter]
        public EventCallback<ChangeEventArgs> OnInput { get; set; }


        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public string Icon { get; set; }

        [Parameter]
        public bool IconTrailing { get; set; }

        [Parameter]
        public bool Box { get; set; }

        [Parameter]
        public bool TextArea { get; set; }

        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        /// <summary>
        /// When true, it specifies that an input field is read-only.
        /// </summary>
        [Parameter]
        public bool ReadOnly { get; set; }


        protected virtual bool InputTextReadOnly()
        {
            return ReadOnly;
        }

        [Parameter]
        public bool FullWidth { get; set; }

        [Parameter]
        public bool Required { get; set; }

        [Parameter]
        public string HelperText { get; set; }

        [Parameter]
        public bool HelperTextPersistent { get; set; }

        [Parameter]
        public bool HelperTextValidation { get; set; }

        [Parameter]
        public string PlaceHolder { get; set; }

        [Parameter]
        public bool HideClearButton { get; set; }

        [Parameter]
        public string Type { get; set; } = "text";



        protected virtual EventCallback<KeyboardEventArgs> OnKeyDownEvent()
        {
            return this.OnKeyDown;
        }


        /// <summary>
        /// Css class of input element
        /// </summary>
        [Parameter]
        public string InputClass
        {
            get => _inputClass;
            set
            {
                _inputClass = value;
            }
        }

        /// <summary>
        /// Style attribute of input element
        /// </summary>
        [Parameter]
        public string InputStyle { get; set; }

        private string _value;
        private string _inputClass;

        protected ClassMapper LabelClassMapper = new ClassMapper();
        protected ClassMapper InputClassMapper = new ClassMapper();
        protected ClassMapper HelperTextClassMapper = new ClassMapper();


        protected virtual RenderFragment GetChildContent()
        {
            return ChildContent;
        }

        protected BaseMatInputTextComponent()
        {
            ClassMapper
                .Add("mdc-text-field")
                .Add("_mdc-text-field--upgraded")
                .If("mdc-text-field--with-leading-icon", () => this.Icon != null && !this.IconTrailing)
                .If("mdc-text-field--with-trailing-icon", () => this.Icon != null && this.IconTrailing)
                .If("mdc-text-field--box", () => !this.FullWidth && this.Box)
                .If("mdc-text-field--dense", () => Dense)
                .If("mdc-text-field--outlined", () => !this.FullWidth && this.Outlined)
                .If("mdc-text-field--disabled", () => this.Disabled)
                .If("mdc-text-field--fullwidth", () => this.FullWidth)
                .If("mdc-text-field--fullwidth-with-leading-icon",
                    () => this.FullWidth && this.Icon != null && !this.IconTrailing)
                .If("mdc-text-field--fullwidth-with-trailing-icon",
                    () => this.FullWidth && this.Icon != null && this.IconTrailing)
                .If("mdc-text-field--textarea", () => this.TextArea);

            LabelClassMapper
                .Add("mdc-floating-label")
                .If("mat-floating-label--float-above-outlined", () => Outlined && !string.IsNullOrEmpty(CurrentValueAsString))
                .If("mdc-floating-label--float-above", () => !string.IsNullOrEmpty(CurrentValueAsString));

            InputClassMapper
                .Get(() => this.InputClass)
                .Get(() => this.FieldClass)
                .Add("mat-text-field-input")
                .Add("mdc-text-field__input")
                .If("_mdc-text-field--upgraded", () => !string.IsNullOrEmpty(CurrentValueAsString))
                .If("mat-hide-clearbutton", () => this.HideClearButton);

            HelperTextClassMapper
                .Add("mdc-text-field-helper-text")
                .If("mdc-text-field-helper-text--persistent", () => HelperTextPersistent)
                .If("mdc-text-field-helper-text--validation-msg", () => HelperTextValidation);

            CallAfterRender(async () =>
            {
                await JsInvokeAsync<object>("matBlazor.matTextField.init", Ref);
            });
        }
    }
}