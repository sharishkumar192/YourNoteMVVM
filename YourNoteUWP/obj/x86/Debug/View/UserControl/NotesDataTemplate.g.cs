﻿#pragma checksum "D:\YourNoteUWP\YourNoteUWP\View\UserControl\NotesDataTemplate.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "351D8EDBCA62A7BC1B5BBC839692589A63309211EFDFAB87E009389722210FC3"
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
    partial class NotesDataTemplate : 
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
        private class NotesDataTemplate_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IDataTemplateComponent,
            global::Windows.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            INotesDataTemplate_Bindings
        {
            private global::YourNoteUWP.NotesDataTemplate dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.Grid obj2;
            private global::Windows.UI.Xaml.Controls.TextBlock obj3;
            private global::Windows.UI.Xaml.Controls.TextBlock obj4;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj2BackgroundDisabled = false;
            private static bool isobj3TextDisabled = false;
            private static bool isobj4TextDisabled = false;

            private NotesDataTemplate_obj1_BindingsTracking bindingsTracking;

            public NotesDataTemplate_obj1_Bindings()
            {
                this.bindingsTracking = new NotesDataTemplate_obj1_BindingsTracking(this);
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 14 && columnNumber == 9)
                {
                    isobj2BackgroundDisabled = true;
                }
                else if (lineNumber == 28 && columnNumber == 13)
                {
                    isobj3TextDisabled = true;
                }
                else if (lineNumber == 42 && columnNumber == 13)
                {
                    isobj4TextDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 2: // View\UserControl\NotesDataTemplate.xaml line 12
                        this.obj2 = (global::Windows.UI.Xaml.Controls.Grid)target;
                        break;
                    case 3: // View\UserControl\NotesDataTemplate.xaml line 21
                        this.obj3 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 4: // View\UserControl\NotesDataTemplate.xaml line 38
                        this.obj4 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
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

            // INotesDataTemplate_Bindings

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
                    this.dataRoot = (global::YourNoteUWP.NotesDataTemplate)newDataRoot;
                    return true;
                }
                return false;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            private bool TryGet_notesTemplate_modifiedDay(out global::System.String val)
            {
                global::YourNoteUWP.Models.Note obj;
                if (TryGet_notesTemplate(out obj) && obj != null)
                {
                    val = obj.modifiedDay;
                    return true;
                }
                else
                {
                    val = default(global::System.String);
                    return false;
                }
            }

            private bool TryGet_notesTemplate(out global::YourNoteUWP.Models.Note val)
            {
                global::YourNoteUWP.NotesDataTemplate obj;
                if (TryGet_(out obj) && obj != null)
                {
                    val = obj.notesTemplate;
                    return true;
                }
                else
                {
                    val = default(global::YourNoteUWP.Models.Note);
                    return false;
                }
            }

            private bool TryGet_(out global::YourNoteUWP.NotesDataTemplate val)
            {
                val = this.dataRoot;
                return true;
            }

            private delegate void InvokeFunctionDelegate(int phase);
            private global::System.Collections.Generic.Dictionary<string, InvokeFunctionDelegate> PendingFunctionBindings = new global::System.Collections.Generic.Dictionary<string, InvokeFunctionDelegate>();

            private void Invoke_M_ShowModifiedTime_4042839057(int phase)
            {
                global::System.String p0;
                if (!TryGet_notesTemplate_modifiedDay(out p0)) { return; }
                global::System.String result = this.dataRoot.ShowModifiedTime(p0);
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // View\UserControl\NotesDataTemplate.xaml line 38
                    if (!isobj4TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj4, result, null);
                    }
                }
            }

            private void CompleteUpdate(int phase)
            {
                var functions = this.PendingFunctionBindings;
                this.PendingFunctionBindings = new global::System.Collections.Generic.Dictionary<string, InvokeFunctionDelegate>();
                foreach (var function in functions.Values)
                {
                    function.Invoke(phase);
                }
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::YourNoteUWP.NotesDataTemplate obj, int phase)
            {
                this.bindingsTracking.UpdateChildListeners_(obj);
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_NoteContentBackground(obj.NoteContentBackground, phase);
                        this.Update_notesTemplate(obj.notesTemplate, phase);
                    }
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_M_ShowModifiedTime_4042839057(phase);
                    }
                }
                this.CompleteUpdate(phase);
            }
            private void Update_NoteContentBackground(global::Windows.UI.Xaml.Media.SolidColorBrush obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // View\UserControl\NotesDataTemplate.xaml line 12
                    if (!isobj2BackgroundDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_Panel_Background(this.obj2, obj, null);
                    }
                }
            }
            private void Update_notesTemplate(global::YourNoteUWP.Models.Note obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_notesTemplate_title(obj.title, phase);
                    }
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_notesTemplate_modifiedDay(obj.modifiedDay, phase);
                    }
                }
            }
            private void Update_notesTemplate_title(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // View\UserControl\NotesDataTemplate.xaml line 21
                    if (!isobj3TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj3, obj, null);
                    }
                }
            }
            private void Update_notesTemplate_modifiedDay(global::System.String obj, int phase)
            {
                if (obj != null)
                {
                    this.Update_M_ShowModifiedTime_4042839057(phase);
                }
            }
            private void Update_M_ShowModifiedTime_4042839057(int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    if (!isobj4TextDisabled)
                    {
                        this.PendingFunctionBindings["M_ShowModifiedTime_4042839057"] = new InvokeFunctionDelegate(this.Invoke_M_ShowModifiedTime_4042839057); 
                    }
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private class NotesDataTemplate_obj1_BindingsTracking
            {
                private global::System.WeakReference<NotesDataTemplate_obj1_Bindings> weakRefToBindingObj; 

                public NotesDataTemplate_obj1_BindingsTracking(NotesDataTemplate_obj1_Bindings obj)
                {
                    weakRefToBindingObj = new global::System.WeakReference<NotesDataTemplate_obj1_Bindings>(obj);
                }

                public NotesDataTemplate_obj1_Bindings TryGetBindingObject()
                {
                    NotesDataTemplate_obj1_Bindings bindingObject = null;
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
                    NotesDataTemplate_obj1_Bindings bindings = TryGetBindingObject();
                    if (bindings != null)
                    {
                        string propName = e.PropertyName;
                        global::YourNoteUWP.NotesDataTemplate obj = sender as global::YourNoteUWP.NotesDataTemplate;
                        if (global::System.String.IsNullOrEmpty(propName))
                        {
                            if (obj != null)
                            {
                                bindings.Update_NoteContentBackground(obj.NoteContentBackground, DATA_CHANGED);
                                bindings.Update_notesTemplate(obj.notesTemplate, DATA_CHANGED);
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
                                case "notesTemplate":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_notesTemplate(obj.notesTemplate, DATA_CHANGED);
                                    }
                                    break;
                                }
                                default:
                                    break;
                            }
                        }
                        bindings.CompleteUpdate(DATA_CHANGED);
                    }
                }
                public void UpdateChildListeners_(global::YourNoteUWP.NotesDataTemplate obj)
                {
                    NotesDataTemplate_obj1_Bindings bindings = TryGetBindingObject();
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
            case 3: // View\UserControl\NotesDataTemplate.xaml line 21
                {
                    this.Titles = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 5: // View\UserControl\NotesDataTemplate.xaml line 30
                {
                    this.Contents = (global::Windows.UI.Xaml.Controls.RichEditBox)(target);
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
            case 1: // View\UserControl\NotesDataTemplate.xaml line 1
                {                    
                    global::Windows.UI.Xaml.Controls.UserControl element1 = (global::Windows.UI.Xaml.Controls.UserControl)target;
                    NotesDataTemplate_obj1_Bindings bindings = new NotesDataTemplate_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                    global::Windows.UI.Xaml.Markup.XamlBindingHelper.SetDataTemplateComponent(element1, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}

