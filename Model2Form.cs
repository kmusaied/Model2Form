using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Collections;
using System.Drawing;
using EasyWebControls;


namespace EasyWebControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:Model2Form runat=server></{0}:Model2Form>")]
    public class Model2Form : WebControl
    {

        #region Events
        public event EventHandler PreFormGeneate;
        public event FieldEventHandler LookupControlCreated;
        public event FieldEventHandler ControlCreated;
        public event FieldEventHandler LabelCreated;
        public event FieldEventHandler ValidationCreated;
        public event EventHandler LookupPostback;
        public event ServerValidateEventHandler ServerValidation;
        public event EventHandler PostFormGeneate;
        public event EventHandler SaveClick;
        public event EventHandler CancelClick;
        public event EventHandler DeleteClick;
        
        protected void OnPreFormGeneate(object sender, EventArgs e)
        {
            if (PreFormGeneate != null)
            {
                PreFormGeneate(sender, e);
            }
        }
        protected void OnLookupControlCreated(object sender, FieldEventArgs e)
        {
            if (LookupControlCreated != null)
            {
                LookupControlCreated(sender, e);
            }
        }
        protected void OnControlCreated(object sender, FieldEventArgs e)
        {
            if (ControlCreated != null)
            {
                ControlCreated(sender, e);
            }
        }
        protected void OnLabelCreated(object sender, FieldEventArgs e)
        {
            if (LabelCreated != null)
            {
                LabelCreated(sender, e);
            }
        }
        protected void OnValidationCreated(object sender, FieldEventArgs e)
        {
            if (ValidationCreated != null)
            {
                ValidationCreated(sender, e);
            }
        }
        protected void OnLookupPostback(object sender, EventArgs e)
        {
            if (LookupPostback != null)
            {
                LookupPostback(sender, e);
            }
        }
        protected void OnServerValidation(object sender, ServerValidateEventArgs e)
        {
            if (ServerValidation != null)
            {
                ServerValidation(sender, e);
            }
        }

        protected void OnPostFormGeneate(object sender, EventArgs e)
        {
            if (PostFormGeneate != null)
            {
                PostFormGeneate(sender, e);
            }
        }
        protected void OnSaveClick(object sender, EventArgs e)
        {
            if (SaveClick != null)
            {
                SaveClick(sender, e);
            }
        }
        protected void OnDeleteClick(object sender, EventArgs e)
        {
            if (DeleteClick != null)
            {
                DeleteClick(sender, e);
            }
        }
        protected void OnCancelClick(object sender, EventArgs e)
        {
            if (CancelClick != null)
            {
                CancelClick(sender, e);
            }
        }

       
        #endregion

        #region Properties

        public int FormControlStartIndex { get { return 1; } }
        [Browsable(true)]
        public string KeyPropertyName
        {
            get
            {
                return ViewState["KeyPropertyName"] as string;
            }
            set
            {
                ViewState["KeyPropertyName"] = value;
            }
        }
        [Browsable(true)]
        public string SaveCaption
        {
            get
            {
                object obj = ViewState["SaveCaption"];
                if (obj == null)
                    return "Save";
                else return obj.ToString();
            }
            set
            {
                ViewState["SaveCaption"] = value;
            }
        }

        [Browsable(true)]
        public string DeleteCaption
        {
            get
            {
                object obj = ViewState["DeleteCaption"];
                if (obj == null)
                    return "Delete";
                else return obj.ToString();
            }
            set
            {
                ViewState["CancelCaption"] = value;
            }
        }

        [Browsable(true)]
        public string CancelCaption
        {
            get
            {
                object obj = ViewState["CancelCaption"];
                if (obj == null)
                    return "Cancel";
                else return obj.ToString();
            }
            set
            {
                ViewState["CancelCaption"] = value;
            }
        }

        [Browsable(true)]
        public string TitleCssClass
        {
            get
            {
                object obj = ViewState["TitleCssClass"];
                if (obj == null)
                    return "txt_title";
                else return obj.ToString();
            }
            set
            {
                ViewState["TitleCssClass"] = value;
            }
        }

        

        public string BtnsCssClass
        {
            get
            {
                object obj = ViewState["BtnsCssClass"];
                if (obj == null)
                    return "btn";
                else return obj.ToString();
            }
            set
            {
                ViewState["BtnsCssClass"] = value;
            }
        }

        [Browsable(false)]
        public object KeyPropertyValue
        {
            get
            {
                return GetPropertyValue(KeyPropertyName);
            }
        }

        [Browsable(false)]
        public object DataSource
        {
            get
            {
                return ViewState["ObjectInfo"];
            }
            set
            {
                ViewState["ObjectInfo"] = value;
            }
        }

        [Browsable(false)]
        public object UpdatedDataSource
        {
            get
            {
                object updatedObject = DataSource;
                PropertyInfo[] properties = updatedObject.GetType().GetProperties();
                foreach (PropertyInfo pi in properties)
                {
                    //Checking if the property is a control based attribue
                    var contAtt = (from cusAtt in pi.GetCustomAttributes(true) where cusAtt.GetType().BaseType == typeof(ControlAttributeBase) select cusAtt).SingleOrDefault();
                    if (contAtt == null)
                    {
                        //assigning a default value to the 
                        //#TODO# this methods shuold match the default attribte creation in geneate control function
                        contAtt = new TextBoxAttribute();
                    }
                    string controlName = pi.Name; //string.Format("{0}_{1}", pi.Name, contAtt.GetType().Name);
                    Control control = this.FindControl(controlName);
                    if (control == null)
                        continue;
                    string bindingProperty = contAtt.GetType().GetProperty("BindingProperty").GetValue(contAtt, null).ToString();
                    object newValue = control.GetType().GetProperty(bindingProperty).GetValue(control, null);
                    if (newValue == "" && pi.PropertyType.Name == "Int32")
                    {
                        newValue = "-1";
                    }
                    pi.SetValue(updatedObject, Convert.ChangeType(newValue, pi.PropertyType), null);
                }
                return updatedObject;
            }
        }

        public Opertion Mode
        {
            get
            {
                object obj = ViewState["Mode"];
                if (obj == null)
                    return Opertion.insert;
                else
                    return (Opertion)obj;
            }
            set
            {
                ViewState["Mode"] = value;
            }
        }

        public bool ShowSaveButton
        {
            get
            {
                object obj = ViewState["ShowSaveButton"];
                if (obj == null)
                    return true;
                else
                    return (bool)obj;
            }
            set
            {
                ViewState["ShowSaveButton"] = value;
            }
        }

        public bool ShowCancelButton
        {
            get
            {
                object obj = ViewState["ShowCancelButton"];
                if (obj == null)
                    return true;
                else
                    return (bool)obj;
            }
            set
            {
                ViewState["ShowCancelButton"] = value;
            }
        }

       
        #endregion

        #region public Methods
        private Table table;
        /// <summary>
        /// This is the main method where we generate the from using refelection
        /// this method requied to set the follwoing properties <typeparamref name="ObjectInfo"/> and <typeparamref name="Mode"/>
        /// </summary>
        public void Generate()
        {
            //firing the event
            OnPreFormGeneate(this, EventArgs.Empty);
            table = new Table();
            /* Beging adding heading to the form*/
            object[] classAttributes = DataSource.GetType().GetCustomAttributes(false);
            //quering for the caption attribute 
            var cAtt = (from ca in classAttributes where ca.GetType() == typeof(CaptionAttribute) select ca).SingleOrDefault();

            string className = "";
            if (cAtt != null)
                className = cAtt.GetType().GetField("Text").GetValue(cAtt).ToString();
            else
                className = DataSource.GetType().Name;

            // code for header  style

            Table htable = new Table();
            htable.CellPadding = 0;
            htable.CellSpacing = 0;
            htable.Width = Unit.Parse("100%");
            htable.BorderWidth = Unit.Parse("0px");
            TableRow hRow = new TableRow();
            TableCell hCell = new TableCell();
            LiteralControl ltTitle = new LiteralControl("<nobr>" + className + "</nobr>");
            hCell.CssClass = TitleCssClass;
            hCell.Wrap = true;
            hCell.Controls.Add(ltTitle);
            hRow.Cells.Add(hCell);

            TableCell cellh1 = new TableCell();
            cellh1.CssClass = "head1";
            hRow.Cells.Add(cellh1);

            TableCell cellh2 = new TableCell();
            cellh2.CssClass = "head2";
            hRow.Cells.Add(cellh2);

            TableCell cellh3 = new TableCell();
            cellh3.CssClass = "head3";
            hRow.Cells.Add(cellh3);
            htable.Rows.Add(hRow);

            TableCell headerCell = new TableCell();
            //headerCell.ColumnSpan = 2;
            headerCell.Controls.Add(htable);
            TableRow headerRow = new TableRow();
            headerRow.Cells.Add(headerCell);
            //table.Rows.Add(headerRow);

            // * finish code for header style



            /*finish adding heading to the form*/

            /* Adding Result message lable*/
            TableCell rscell = new TableCell();
            rscell.HorizontalAlign = HorizontalAlign.Right;
            rscell.ColumnSpan = 2;
            Label rsLable = new Label();

            rsLable.ID = "lblRsMessage";
            rsLable.Visible = false;
            rscell.Controls.Add(rsLable);

            TableRow rsrow = new TableRow();
            rsrow.Cells.Add(rscell);


            table.Rows.Add(rsrow);
            /* Finish Result message lable*/

            /* Adding Validation Summary*/
            TableCell vscell = new TableCell();
            ValidationSummary validationSummary = new ValidationSummary();
            vscell.Controls.Add(validationSummary);

            TableRow vsrow = new TableRow();
            vsrow.Cells.Add(vscell);


            //table.Rows.Add(vsrow);
            /* Finish Validation Summary*/


            //getting the properties of the object passed in ObjectInfo Property
            PropertyInfo[] properties = DataSource.GetType().GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                //checking for Hidden Attribute to hide the property from generation
                object[] hiddenAttribues = pi.GetCustomAttributes(typeof(HiddenAttribute), false);
                if (hiddenAttribues.Length != 0)
                {
                    HiddenAttribute hAtt = (HiddenAttribute)hiddenAttribues[0];//only first one
                    bool formHidden = (bool)hAtt.GetType().GetField("Form").GetValue(hAtt);
                    if (formHidden)
                        continue;
                }

                /* begin handling cascad dropdowns */
                stkProperties = new Stack<PropertyInfo>();
                GetCascadLookups(pi);
                if (stkProperties.Count != 0)
                {
                    while (stkProperties.Count != 0)
                    //for (int i = 0; i <= stkProperties.Count; i++)
                    {
                        PropertyInfo lpi = stkProperties.Pop();
                        AddFieldToMainTable(lpi);
                    }

                }
                /* End handling cascad dropdowns */

                AddFieldToMainTable(pi);

            }
            /*Begin Adding Commands to the Geneated Form */
            TableRow CommandRow = new TableRow();
            TableCell CommandCell = new TableCell();
            //add blank cell to right form 
            TableCell BlankCell = new TableCell();
            CommandRow.Cells.Add(BlankCell);

            //Buttons Generation in a function
            CommandCell.Controls.Add(GetButtons());

            CommandRow.Cells.Add(CommandCell);

            table.Rows.Add(CommandRow);

            this.Controls.Clear();
            /*Finish Adding Commands to the Geneated Form */
            //firing the post genreation event

            OnPostFormGeneate(table, EventArgs.Empty);

            //* split controls in tow tables header and container for style

            Table allTables = new Table();
            allTables.Rows.Add(headerRow);
            allTables.CellSpacing = 0;
            allTables.Width = Unit.Parse("100%");

            TableCell contentCell = new TableCell();
            table.CssClass = "border_td_content";
            table.Width = Unit.Parse("100%");
            contentCell.Controls.Add(table);
            TableRow contentRow = new TableRow();
            contentRow.Cells.Add(contentCell);
            allTables.Rows.Add(contentRow);

            //*finish split controls in tow tables header and container for style

            this.Controls.Add(allTables);

        }




        public new void DataBind()
        {
            Generate();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Geneates the label from the Caption Attribute
        /// </summary>
        /// <param name="pi">Property to geneate label</param>
        /// <returns></returns>
        private Control GetLabel(PropertyInfo pi)
        {
            object[] attributes = pi.GetCustomAttributes(false);
            //quering for the caption attribute 
            var att = (from ca in attributes where ca.GetType() == typeof(CaptionAttribute) select ca).SingleOrDefault();
            Control LabelControls = new Control();

            if (att == null)
                //Assign the propety name as a caption
                att = new CaptionAttribute(pi.Name);

            PropertyInfo typePropertyInfo = att.GetType().GetProperty("LabelTypeName");
            if (typePropertyInfo != null)
            {
                string typeName = typePropertyInfo.GetValue(att, null).ToString();
                Control control = (Control)System.Activator.CreateInstance(Type.GetType(typeName));
                FieldInfo[] fields = att.GetType().GetFields();
                foreach (FieldInfo fld in fields)
                {
                    string fieldName = fld.Name;

                    object fieldValue = fld.GetValue(att);
                    PropertyInfo controlProperty = control.GetType().GetProperty(fieldName);

                    if (controlProperty != null)
                        controlProperty.SetValue(control, fieldValue, null);
                }
                //firing the event
                OnLabelCreated(control, new FieldEventArgs(pi.Name, pi.PropertyType));
                LabelControls.Controls.Add(control);
            }

            return LabelControls;

        }

        private Control GetControl(PropertyInfo pi, out Control vControls)
        {
            object[] attributes = pi.GetCustomAttributes(true);
            Control _Controls = new Control();
            Control _vControls = new Control();

            //finding out control attribue
            int ControlAttributesCount = (from a in attributes where a.GetType().BaseType == typeof(ControlAttributeBase) || a.GetType().BaseType.BaseType == typeof(ControlAttributeBase) select a).Count();

            if (ControlAttributesCount == 0)
            {
                //adding default attributes
                ArrayList defaultAttributes = new ArrayList(attributes);
                TextBoxAttribute textbox = new TextBoxAttribute();
                defaultAttributes.Add(textbox);
                string controlName = pi.Name; //String.Format("{0}_TextBoxAttribute", pi.Name);

                //////RequiredAttribute vrAtt = new RequiredAttribute();
                //////defaultAttributes.Add(vrAtt);

                switch (pi.PropertyType.Name)
                {
                    case "Int32":
                        RegExpAttribute veAtt = new RegExpAttribute();
                        veAtt.ValidationExpression = @"\d";
                        defaultAttributes.Add(veAtt);
                        break;
                    case "String":
                        break;
                    default:
                        break;
                }
                attributes = defaultAttributes.ToArray();
            }

            foreach (Attribute att in attributes)
            {
                //checking validation
                bool isValidation = false;
                if (att.GetType().BaseType == typeof(ValidationAttributeBase))
                    isValidation = true;
                /* Begin Validation Controls Geneation */
                if (isValidation)
                {
                    //General Validator Control
                    string ControlType = att.GetType().GetProperty("ControlType").GetValue(att, null).ToString();
                    Control ValidationControl = (Control)System.Activator.CreateInstance(Type.GetType(ControlType));

                    //binding
                    BindAttributeFieldsToProperties(ValidationControl, att);

                    if (att.GetType() == typeof(CustomValidatorAttribute))
                    {
                        string ServerValidationEventName = GetFieldValue(att, "ServerValidationEventName").ToString();
                        ValidationControl.GetType().GetEvent(ServerValidationEventName).AddEventHandler(ValidationControl, new ServerValidateEventHandler(ServerValidation));
                        //Assigning ControlToValidate Property with the propertyName
                        if (pi.PropertyType != typeof(DateTime))
                        {
                            string ControlToValidatePropertyName = GetFieldValue(att, "ControlToValidatePropertyName").ToString();
                            ValidationControl.GetType().GetProperty(ControlToValidatePropertyName).SetValue(ValidationControl, pi.Name, null);
                        }
                    }
                    else
                    {
                        //Assigning ControlToValidate Property with the propertyName
                        string ControlToValidatePropertyName = GetFieldValue(att, "ControlToValidatePropertyName").ToString();
                        ValidationControl.GetType().GetProperty(ControlToValidatePropertyName).SetValue(ValidationControl, pi.Name, null);
                    }



                    //firing the event
                    OnValidationCreated(ValidationControl, new FieldEventArgs(pi.Name, pi.PropertyType));
                    _vControls.Controls.Add(ValidationControl);
                }
                /* Finish Validation Controls Geneation */
                else
                {   // generic control
                    PropertyInfo typePropertyInfo = att.GetType().GetProperty("ControlTypeName");
                    if (typePropertyInfo != null)
                    {
                        string typeName = typePropertyInfo.GetValue(att, null).ToString();
                        Control control = (Control)System.Activator.CreateInstance(Type.GetType(typeName));
                        control.ID = pi.Name;//+ "_" + att.GetType().Name;

                        //binding fields
                        BindAttributeFieldsToProperties(control, att);

                        string bindingPropertyName = att.GetType().GetProperty("BindingProperty").GetValue(att, null).ToString();
                        PropertyInfo controlBindingProperty = control.GetType().GetProperty(bindingPropertyName);
                        //DataControls
                        bool isDataControl = (bool)att.GetType().GetField("IsDataControl").GetValue(att);
                        if (isDataControl)
                        {
                            bool AutoPostBack = (bool)control.GetType().GetProperty("AutoPostBack").GetValue(control, null);
                            if (AutoPostBack)
                            {
                                string IndexChangeEventName = att.GetType().GetField("IndexChangeEventName").GetValue(att).ToString();
                                control.GetType().GetEvent(IndexChangeEventName).AddEventHandler(control, new EventHandler(LookupPostback));
                            }
                            //add cleared property for clear data from control or no
                            FieldInfo fld = att.GetType().GetField("Cleared");
                            object clear = fld.GetValue(att);
                            ((ListControl)control).Attributes["Clear"] = clear.ToString();

                            //add hide property to hide any control after form created
                            FieldInfo fldhide = att.GetType().GetField("Hide");
                            object hide = fldhide.GetValue(att);
                            ((ListControl)control).Attributes["hide"] = hide.ToString();

                            //firing event
                            OnLookupControlCreated(control, new FieldEventArgs(pi.Name, pi.PropertyType));

                            //seting select value after event 
                            if (Mode == Opertion.update)
                                controlBindingProperty.SetValue(control, Convert.ChangeType(pi.GetValue(DataSource, null), control.GetType().GetProperty(bindingPropertyName).PropertyType), null);

                            //Do Config
                            att.GetType().GetMethod("CustomConfig").Invoke(att, new object[] { this, control, att });


                            _Controls.Controls.Add(control);
                        }
                        else
                        {
                            //DirectBinding Property
                            controlBindingProperty.SetValue(control, Convert.ChangeType(pi.GetValue(DataSource, null), control.GetType().GetProperty(bindingPropertyName).PropertyType), null);

                            //firing the event
                            OnControlCreated(control, new FieldEventArgs(pi.Name, pi.PropertyType));

                            //Do Config
                            att.GetType().GetMethod("CustomConfig").Invoke(att, new object[] { this, control, att });


                            _Controls.Controls.Add(control);
                        }
                    }
                }
            }
            vControls = _vControls;
            return _Controls;
        }
        /// <summary>
        /// Geneates the Command Buttons (Save , Delete , Cancel) in the bottom of the form
        /// </summary>
        /// <remarks>
        /// if the <typeparamref name="Mode"/> is <typeparamref name="Insert"/> delete button will be hidden
        /// </remarks>
        /// <returns>Control With an arry of Buttons</returns>
        private Control GetButtons()
        {
            Control _controls = new Control();
            Button SaveButton = new Button();
            Button CancelButton = new Button();
            Button DeleteButton = new Button();
            //Button BackButton = new Button();

            SaveButton.Text = SaveCaption;
            SaveButton.Click += SaveClick;
            SaveButton.CssClass = BtnsCssClass;

            DeleteButton.Text = DeleteCaption;
            DeleteButton.Click += DeleteClick;
            DeleteButton.CssClass = BtnsCssClass;

            CancelButton.Text = CancelCaption;
            CancelButton.Click += CancelClick;
            CancelButton.CausesValidation = false;
            CancelButton.CssClass = BtnsCssClass;

         
            if (ShowSaveButton)
                _controls.Controls.Add(SaveButton);
            if (ShowCancelButton)
                _controls.Controls.Add(CancelButton);
            if (Mode == Opertion.update)
                _controls.Controls.Add(DeleteButton);
            
            return _controls;
        }

        Stack<PropertyInfo> stkProperties;
        private void GetCascadLookups(PropertyInfo pi)
        {
            object[] atts = pi.GetCustomAttributes(false);
            foreach (object a in atts)
            {
                if (a.GetType().Name == new ParentPropertyAttribute().GetType().Name)
                {
                    string typeName = a.GetType().GetField("TypeName").GetValue(a).ToString();
                    string propertyName = a.GetType().GetField("Name").GetValue(a).ToString();
                    PropertyInfo property = Type.GetType(typeName).GetProperty(propertyName);
                    stkProperties.Push(property);
                    GetCascadLookups(property);
                }
            }
        }

        private void AddFieldToMainTable(PropertyInfo pi)
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();

            //the label will be generated in a function
            cell.Controls.Add(GetLabel(pi));
            
            row.Cells.Add(cell);

            cell = new TableCell();
            //the control will be generated in a function
            Control vControls = new Control();
            Control _controls = GetControl(pi, out vControls);
            //add controls 
            cell.Controls.Add(_controls);
            //add br control to show error message in next line
            LiteralControl ltbr = new LiteralControl("<br />");
            cell.Controls.Add(ltbr);
            // add validation control
            cell.Controls.Add(vControls);
            row.Cells.Add(cell);
            table.Rows.Add(row);
        }


        #endregion

        #region Helper Methods

        /// <summary>
        /// binding attribute fields as properties
        /// </summary>
        /// <example>
        /// TextBoxAttribute.TextMode ==> TextBox.TextMode
        /// </example>
        /// <param name="control">Target Control</param>
        /// <param name="att">Attribute</param>
        /// <param name="fields">Attribute Fields</param>
        private void BindAttributeFieldsToProperties(Control control, Attribute att)
        {

            foreach (FieldInfo fld in att.GetType().GetFields())
            {
                string fieldName = fld.Name;
                object fieldValue = fld.GetValue(att);
                PropertyInfo controlProperty = control.GetType().GetProperty(fieldName);
                if (controlProperty != null)
                    controlProperty.SetValue(control, Convert.ChangeType(fieldValue, controlProperty.PropertyType), null);
            }
        }


        private object GetFieldValue(Attribute att, string fieldName)
        {
            return att.GetType().GetField(fieldName).GetValue(att);
        }

        public object GetPropertyValue(string propertyName)
        {
            return DataSource.GetType().GetProperty(propertyName).GetValue(UpdatedDataSource, null);
        }

        public Control GetControl(string propertyName)
        {
            return this.Controls[0].FindControl(propertyName);
        }

        

        #endregion


        protected override void Render(HtmlTextWriter writer)
        {
            if (DesignMode)
            {
                writer.Write(ID);
            }
            base.Render(writer);
        }



    }

}