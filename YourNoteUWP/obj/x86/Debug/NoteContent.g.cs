﻿#pragma checksum "D:\YourNoteUWP\YourNoteUWP\NoteContent.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "662D468B95EF04473487A1468E47B73DF62BF0BAEF36672A9AFF1BA9BFF416F3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YourNoteUWP
{
    partial class NoteContent : 
        global::Windows.UI.Xaml.Controls.UserControl, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private static class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_Controls_Panel_Background(global::Windows.UI.Xaml.Controls.Panel obj, global::Windows.UI.Xaml.Media.Brush value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::Windows.UI.Xaml.Media.Brush) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::Windows.UI.Xaml.Media.Brush), targetNullValue);
                }
                obj.Background = value;
            }
            public static void Set_Windows_UI_Xaml_Controls_TextBox_IsReadOnly(global::Windows.UI.Xaml.Controls.TextBox obj, global::System.Boolean value)
            {
                obj.IsReadOnly = value;
            }
            public static void Set_Windows_UI_Xaml_UIElement_IsTapEnabled(global::Windows.UI.Xaml.UIElement obj, global::System.Boolean value)
            {
                obj.IsTapEnabled = value;
            }
            public static void Set_Windows_UI_Xaml_Controls_TextBox_Text(global::Windows.UI.Xaml.Controls.TextBox obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Text = value ?? global::System.String.Empty;
            }
            public static void Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(global::Windows.UI.Xaml.Controls.ItemsControl obj, global::System.Object value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Object) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Object), targetNullValue);
                }
                obj.ItemsSource = value;
            }
            public static void Set_Windows_UI_Xaml_Controls_TextBlock_Text(global::Windows.UI.Xaml.Controls.TextBlock obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Text = value ?? global::System.String.Empty;
            }
        };

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private class NoteContent_obj3_Bindings :
            global::Windows.UI.Xaml.IDataTemplateExtension,
            global::Windows.UI.Xaml.Markup.IDataTemplateComponent,
            global::Windows.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            INoteContent_Bindings
        {
            private global::YourNoteUWP.Models.User dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);
            private bool removedDataContextHandler = false;

            // Fields for each control that has bindings.
            private global::System.WeakReference obj3;
            private global::Windows.UI.Xaml.Controls.TextBlock obj4;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj4TextDisabled = false;

            public NoteContent_obj3_Bindings()
            {
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 21 && columnNumber == 25)
                {
                    isobj4TextDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 3: // NoteContent.xaml line 15
                        this.obj3 = new global::System.WeakReference((global::Windows.UI.Xaml.Controls.StackPanel)target);
                        break;
                    case 4: // NoteContent.xaml line 17
                        this.obj4 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    default:
                        break;
                }
            }

            public void DataContextChangedHandler(global::Windows.UI.Xaml.FrameworkElement sender, global::Windows.UI.Xaml.DataContextChangedEventArgs args)
            {
                 if (this.SetDataRoot(args.NewValue))
                 {
                    this.Update();
                 }
            }

            // IDataTemplateExtension

            public bool ProcessBinding(uint phase)
            {
                throw new global::System.NotImplementedException();
            }

            public int ProcessBindings(global::Windows.UI.Xaml.Controls.ContainerContentChangingEventArgs args)
            {
                int nextPhase = -1;
                ProcessBindings(args.Item, args.ItemIndex, (int)args.Phase, out nextPhase);
                return nextPhase;
            }

            public void ResetTemplate()
            {
                Recycle();
            }

            // IDataTemplateComponent

            public void ProcessBindings(global::System.Object item, int itemIndex, int phase, out int nextPhase)
            {
                nextPhase = -1;
                switch(phase)
                {
                    case 0:
                        nextPhase = -1;
                        this.SetDataRoot(item);
                        if (!removedDataContextHandler)
                        {
                            removedDataContextHandler = true;
                            (this.obj3.Target as global::Windows.UI.Xaml.Controls.StackPanel).DataContextChanged -= this.DataContextChangedHandler;
                        }
                        this.initialized = true;
                        break;
                }
                this.Update_((global::YourNoteUWP.Models.User) item, 1 << phase);
            }

            public void Recycle()
            {
            }

            // INoteContent_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
            }

            public void DisconnectUnloadedObject(int connectionId)
            {
                throw new global::System.ArgumentException("No unloadable elements to disconnect.");
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                if (newDataRoot != null)
                {
                    this.dataRoot = (global::YourNoteUWP.Models.User)newDataRoot;
                    return true;
                }
                return false;
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::YourNoteUWP.Models.User obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_userId(obj.userId, phase);
                    }
                }
            }
            private void Update_userId(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // NoteContent.xaml line 17
                    if (!isobj4TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj4, obj, null);
                    }
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private class NoteContent_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IDataTemplateComponent,
            global::Windows.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            INoteContent_Bindings
        {
            private global::YourNoteUWP.NoteContent dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.Grid obj5;
            private global::Windows.UI.Xaml.Controls.TextBox obj7;
            private global::Windows.UI.Xaml.Controls.Button obj8;
            private global::Windows.UI.Xaml.Controls.Button obj9;
            private global::Windows.UI.Xaml.Controls.ListView obj10;
            private global::Windows.UI.Xaml.Controls.TextBox obj11;
            private global::Windows.UI.Xaml.Controls.Button obj12;

            // Fields for each event bindings event handler.
            private global::Windows.UI.Xaml.Input.TappedEventHandler obj7Tapped;
            private global::Windows.UI.Xaml.RoutedEventHandler obj8Click;
            private global::Windows.UI.Xaml.RoutedEventHandler obj9Click;
            private global::Windows.UI.Xaml.Input.TappedEventHandler obj11Tapped;
            private global::Windows.UI.Xaml.RoutedEventHandler obj12Click;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj5BackgroundDisabled = false;
            private static bool isobj7IsReadOnlyDisabled = false;
            private static bool isobj7IsTapEnabledDisabled = false;
            private static bool isobj7TextDisabled = false;
            private static bool isobj10ItemsSourceDisabled = false;
            private static bool isobj11IsReadOnlyDisabled = false;
            private static bool isobj11IsTapEnabledDisabled = false;
            private static bool isobj11TextDisabled = false;

            private NoteContent_obj1_BindingsTracking bindingsTracking;

            public NoteContent_obj1_Bindings()
            {
                this.bindingsTracking = new NoteContent_obj1_BindingsTracking(this);
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 34 && columnNumber == 11)
                {
                    isobj5BackgroundDisabled = true;
                }
                else if (lineNumber == 153 && columnNumber == 13)
                {
                    isobj7IsReadOnlyDisabled = true;
                }
                else if (lineNumber == 154 && columnNumber == 13)
                {
                    isobj7IsTapEnabledDisabled = true;
                }
                else if (lineNumber == 157 && columnNumber == 13)
                {
                    isobj7TextDisabled = true;
                }
                else if (lineNumber == 156 && columnNumber == 13)
                {
                    this.obj7.Tapped -= obj7Tapped;
                }
                else if (lineNumber == 130 && columnNumber == 21)
                {
                    this.obj8.Click -= obj8Click;
                }
                else if (lineNumber == 97 && columnNumber == 21)
                {
                    this.obj9.Click -= obj9Click;
                }
                else if (lineNumber == 119 && columnNumber == 37)
                {
                    isobj10ItemsSourceDisabled = true;
                }
                else if (lineNumber == 56 && columnNumber == 17)
                {
                    isobj11IsReadOnlyDisabled = true;
                }
                else if (lineNumber == 57 && columnNumber == 17)
                {
                    isobj11IsTapEnabledDisabled = true;
                }
                else if (lineNumber == 61 && columnNumber == 17)
                {
                    isobj11TextDisabled = true;
                }
                else if (lineNumber == 60 && columnNumber == 17)
                {
                    this.obj11.Tapped -= obj11Tapped;
                }
                else if (lineNumber == 68 && columnNumber == 17)
                {
                    this.obj12.Click -= obj12Click;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 5: // NoteContent.xaml line 34
                        this.obj5 = (global::Windows.UI.Xaml.Controls.Grid)target;
                        break;
                    case 7: // NoteContent.xaml line 140
                        this.obj7 = (global::Windows.UI.Xaml.Controls.TextBox)target;
                        this.obj7Tapped = (global::System.Object p0, global::Windows.UI.Xaml.Input.TappedRoutedEventArgs p1) =>
                        {
                            this.dataRoot.ContentOfNoteTapped(p0, p1);
                        };
                        ((global::Windows.UI.Xaml.Controls.TextBox)target).Tapped += obj7Tapped;
                        break;
                    case 8: // NoteContent.xaml line 127
                        this.obj8 = (global::Windows.UI.Xaml.Controls.Button)target;
                        this.obj8Click = (global::System.Object p0, global::Windows.UI.Xaml.RoutedEventArgs p1) =>
                        {
                            this.dataRoot.NoteDeleteButtonClick();
                        };
                        ((global::Windows.UI.Xaml.Controls.Button)target).Click += obj8Click;
                        break;
                    case 9: // NoteContent.xaml line 94
                        this.obj9 = (global::Windows.UI.Xaml.Controls.Button)target;
                        this.obj9Click = (global::System.Object p0, global::Windows.UI.Xaml.RoutedEventArgs p1) =>
                        {
                            this.dataRoot.NoteShareButtonClick();
                        };
                        ((global::Windows.UI.Xaml.Controls.Button)target).Click += obj9Click;
                        break;
                    case 10: // NoteContent.xaml line 105
                        this.obj10 = (global::Windows.UI.Xaml.Controls.ListView)target;
                        break;
                    case 11: // NoteContent.xaml line 44
                        this.obj11 = (global::Windows.UI.Xaml.Controls.TextBox)target;
                        this.obj11Tapped = (global::System.Object p0, global::Windows.UI.Xaml.Input.TappedRoutedEventArgs p1) =>
                        {
                            this.dataRoot.TitleOfNoteTapped(p0, p1);
                        };
                        ((global::Windows.UI.Xaml.Controls.TextBox)target).Tapped += obj11Tapped;
                        break;
                    case 12: // NoteContent.xaml line 64
                        this.obj12 = (global::Windows.UI.Xaml.Controls.Button)target;
                        this.obj12Click = (global::System.Object p0, global::Windows.UI.Xaml.RoutedEventArgs p1) =>
                        {
                            this.dataRoot.NoteCloseButtonClick(p0, p1);
                        };
                        ((global::Windows.UI.Xaml.Controls.Button)target).Click += obj12Click;
                        break;
                    default:
                        break;
                }
            }

            // IDataTemplateComponent

            public void ProcessBindings(global::System.Object item, int itemIndex, int phase, out int nextPhase)
            {
                nextPhase = -1;
            }

            public void Recycle()
            {
                return;
            }

            // INoteContent_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.initialized = false;
            }

            public void DisconnectUnloadedObject(int connectionId)
            {
                throw new global::System.ArgumentException("No unloadable elements to disconnect.");
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                this.bindingsTracking.ReleaseAllListeners();
                if (newDataRoot != null)
                {
                    this.dataRoot = (global::YourNoteUWP.NoteContent)newDataRoot;
                    return true;
                }
                return false;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::YourNoteUWP.NoteContent obj, int phase)
            {
                this.bindingsTracking.UpdateChildListeners_(obj);
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_NoteContentBackground(obj.NoteContentBackground, phase);
                        this.Update_ContentOfNoteIsReadOnly(obj.ContentOfNoteIsReadOnly, phase);
                        this.Update_ContentOfNoteIsTapped(obj.ContentOfNoteIsTapped, phase);
                        this.Update_ContentOfNoteText(obj.ContentOfNoteText, phase);
                    }
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_usersToShare(obj.usersToShare, phase);
                    }
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_TitleOfNoteIsReadOnly(obj.TitleOfNoteIsReadOnly, phase);
                        this.Update_TitleOfNoteIsTapped(obj.TitleOfNoteIsTapped, phase);
                        this.Update_TitleOfNoteText(obj.TitleOfNoteText, phase);
                    }
                }
            }
            private void Update_NoteContentBackground(global::Windows.UI.Xaml.Media.SolidColorBrush obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // NoteContent.xaml line 34
                    if (!isobj5BackgroundDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_Panel_Background(this.obj5, obj, null);
                    }
                }
            }
            private void Update_ContentOfNoteIsReadOnly(global::System.Boolean obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // NoteContent.xaml line 140
                    if (!isobj7IsReadOnlyDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBox_IsReadOnly(this.obj7, obj);
                    }
                }
            }
            private void Update_ContentOfNoteIsTapped(global::System.Boolean obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // NoteContent.xaml line 140
                    if (!isobj7IsTapEnabledDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_UIElement_IsTapEnabled(this.obj7, obj);
                    }
                }
            }
            private void Update_ContentOfNoteText(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // NoteContent.xaml line 140
                    if (!isobj7TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBox_Text(this.obj7, obj, null);
                    }
                }
            }
            private void Update_usersToShare(global::System.Collections.ObjectModel.ObservableCollection<global::YourNoteUWP.Models.User> obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // NoteContent.xaml line 105
                    if (!isobj10ItemsSourceDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj10, obj, null);
                    }
                }
            }
            private void Update_TitleOfNoteIsReadOnly(global::System.Boolean obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // NoteContent.xaml line 44
                    if (!isobj11IsReadOnlyDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBox_IsReadOnly(this.obj11, obj);
                    }
                }
            }
            private void Update_TitleOfNoteIsTapped(global::System.Boolean obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // NoteContent.xaml line 44
                    if (!isobj11IsTapEnabledDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_UIElement_IsTapEnabled(this.obj11, obj);
                    }
                }
            }
            private void Update_TitleOfNoteText(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // NoteContent.xaml line 44
                    if (!isobj11TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBox_Text(this.obj11, obj, null);
                    }
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private class NoteContent_obj1_BindingsTracking
            {
                private global::System.WeakReference<NoteContent_obj1_Bindings> weakRefToBindingObj; 

                public NoteContent_obj1_BindingsTracking(NoteContent_obj1_Bindings obj)
                {
                    weakRefToBindingObj = new global::System.WeakReference<NoteContent_obj1_Bindings>(obj);
                }

                public NoteContent_obj1_Bindings TryGetBindingObject()
                {
                    NoteContent_obj1_Bindings bindingObject = null;
                    if (weakRefToBindingObj != null)
                    {
                        weakRefToBindingObj.TryGetTarget(out bindingObject);
                        if (bindingObject == null)
                        {
                            weakRefToBindingObj = null;
                            ReleaseAllListeners();
                        }
                    }
                    return bindingObject;
                }

                public void ReleaseAllListeners()
                {
                    UpdateChildListeners_(null);
                }

                public void PropertyChanged_(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
                {
                    NoteContent_obj1_Bindings bindings = TryGetBindingObject();
                    if (bindings != null)
                    {
                        string propName = e.PropertyName;
                        global::YourNoteUWP.NoteContent obj = sender as global::YourNoteUWP.NoteContent;
                        if (global::System.String.IsNullOrEmpty(propName))
                        {
                            if (obj != null)
                            {
                                bindings.Update_NoteContentBackground(obj.NoteContentBackground, DATA_CHANGED);
                                bindings.Update_ContentOfNoteIsReadOnly(obj.ContentOfNoteIsReadOnly, DATA_CHANGED);
                                bindings.Update_ContentOfNoteIsTapped(obj.ContentOfNoteIsTapped, DATA_CHANGED);
                                bindings.Update_ContentOfNoteText(obj.ContentOfNoteText, DATA_CHANGED);
                                bindings.Update_TitleOfNoteIsReadOnly(obj.TitleOfNoteIsReadOnly, DATA_CHANGED);
                                bindings.Update_TitleOfNoteIsTapped(obj.TitleOfNoteIsTapped, DATA_CHANGED);
                                bindings.Update_TitleOfNoteText(obj.TitleOfNoteText, DATA_CHANGED);
                            }
                        }
                        else
                        {
                            switch (propName)
                            {
                                case "NoteContentBackground":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_NoteContentBackground(obj.NoteContentBackground, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "ContentOfNoteIsReadOnly":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_ContentOfNoteIsReadOnly(obj.ContentOfNoteIsReadOnly, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "ContentOfNoteIsTapped":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_ContentOfNoteIsTapped(obj.ContentOfNoteIsTapped, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "ContentOfNoteText":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_ContentOfNoteText(obj.ContentOfNoteText, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "TitleOfNoteIsReadOnly":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_TitleOfNoteIsReadOnly(obj.TitleOfNoteIsReadOnly, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "TitleOfNoteIsTapped":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_TitleOfNoteIsTapped(obj.TitleOfNoteIsTapped, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "TitleOfNoteText":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_TitleOfNoteText(obj.TitleOfNoteText, DATA_CHANGED);
                                    }
                                    break;
                                }
                                default:
                                    break;
                            }
                        }
                    }
                }
                public void UpdateChildListeners_(global::YourNoteUWP.NoteContent obj)
                {
                    NoteContent_obj1_Bindings bindings = TryGetBindingObject();
                    if (bindings != null)
                    {
                        if (bindings.dataRoot != null)
                        {
                            ((global::System.ComponentModel.INotifyPropertyChanged)bindings.dataRoot).PropertyChanged -= PropertyChanged_;
                        }
                        if (obj != null)
                        {
                            bindings.dataRoot = obj;
                            ((global::System.ComponentModel.INotifyPropertyChanged)obj).PropertyChanged += PropertyChanged_;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 6: // NoteContent.xaml line 76
                {
                    this.NoteMenuOptions = (global::Windows.UI.Xaml.Controls.ListBox)(target);
                }
                break;
            case 7: // NoteContent.xaml line 140
                {
                    this.ContentOfNote = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 8: // NoteContent.xaml line 127
                {
                    this.NoteDeleteButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                }
                break;
            case 9: // NoteContent.xaml line 94
                {
                    this.NoteShareButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                }
                break;
            case 10: // NoteContent.xaml line 105
                {
                    this.UsersToShareView = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)this.UsersToShareView).ItemClick += this.UsersToShareView_ItemClick;
                }
                break;
            case 11: // NoteContent.xaml line 44
                {
                    this.TitleOfNote = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 12: // NoteContent.xaml line 64
                {
                    this.NoteCloseButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            switch(connectionId)
            {
            case 1: // NoteContent.xaml line 1
                {                    
                    global::Windows.UI.Xaml.Controls.UserControl element1 = (global::Windows.UI.Xaml.Controls.UserControl)target;
                    NoteContent_obj1_Bindings bindings = new NoteContent_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                    global::Windows.UI.Xaml.Markup.XamlBindingHelper.SetDataTemplateComponent(element1, bindings);
                }
                break;
            case 3: // NoteContent.xaml line 15
                {                    
                    global::Windows.UI.Xaml.Controls.StackPanel element3 = (global::Windows.UI.Xaml.Controls.StackPanel)target;
                    NoteContent_obj3_Bindings bindings = new NoteContent_obj3_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(element3.DataContext);
                    element3.DataContextChanged += bindings.DataContextChangedHandler;
                    global::Windows.UI.Xaml.DataTemplate.SetExtensionInstance(element3, bindings);
                    global::Windows.UI.Xaml.Markup.XamlBindingHelper.SetDataTemplateComponent(element3, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}

