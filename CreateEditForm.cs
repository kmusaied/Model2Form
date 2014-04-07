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

namespace CustomUIBuilder
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:CreateEditForm runat=server></{0}:CreateEditForm>")]
    public class CreateEditForm : WebControl
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
        public object UpdatedDataSource
        {
            get
            {
                object updatedObject = DataSource;
                PropertyInfo[] properties = updatedObject.GetType().GetProperties();
                foreach (PropertyInfo pi in properties)
                {
                    //Checking if the property is a control based attribue
                    var contAtt = (from cusAtt in pi.GetCustomAttributes(true) where cusAtt.GetType().BaseType == typeof(ControlAttribute) select cusAtt).SingleOrDefault();
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
        #endregion

        #region public Methods
        /// <summary>
        /// This is the main method where we generate the from using refelection
        /// this method requied to set the follwoing properties <typeparamref name="ObjectInfo"/> and <typeparamref name="Mode"/>
        /// </summary>
        public void Generate()
        {
            //firing the event
            OnPreFormGeneate(this, EventArgs.Empty);
            Table table = new Table();
            /* Beging adding heading to the form*/
            object[] classAttributes = DataSource.GetType().GetCustomAttributes(false);
            //quering for the caption attribute 
            var cAtt = (from ca in classAttributes where ca.GetType().Name == "CaptionAttribute" select ca).SingleOrDefault();

            string className = "بيانات";
            if (cAtt != null)
                className = cAtt.GetType().GetField("Text").GetValue(cAtt).ToString();

            TableRow hRow = new TableRow();
            TableCell hCell = new TableCell();
            LiteralControl ltTitle = new LiteralControl("<h2>" + className + "</h2>");
            hCell.Controls.Add(ltTitle);

            hRow.Cells.Add(hCell);
            table.Rows.Add(hRow);
            /*finish adding heading to the form*/

            //getting the properties of the object passed in ObjectInfo Property
            PropertyInfo[] properties = DataSource.GetType().GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                //checking for Hidden Attribute to hide the property from generation
                object[] hiddenAttribues = pi.GetCustomAttributes(typeof(HiddenAttribute), false);
                if (hiddenAttribues.Length != 0)
                {
                    continue;
                }

                TableRow row = new TableRow();
                TableCell cell = new TableCell();

                //the label will be generated in a function
                cell.Controls.Add(GetLabel(pi));
                row.Cells.Add(cell);

                cell = new TableCell();
                //the control will be generated in a function
                cell.Controls.Add(GetControl(pi));
                row.Cells.Add(cell);
                table.Rows.Add(row);
            }
            /*Begin Adding Commands to the Geneated Form */
            TableRow CommandRow = new TableRow();
            TableCell CommandCell = new TableCell();

            //Buttons Generation in a function
            CommandCell.Controls.Add(GetButtons());

            CommandRow.Cells.Add(CommandCell);
            table.Rows.Add(CommandRow);
            this.Controls.Add(table);
            /*Finish Adding Commands to the Geneated Form */
            //firing the post genreation event
            OnPostFormGeneate(this, EventArgs.Empty);
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

        private Control GetControl(PropertyInfo pi)
        {
            object[] attributes = pi.GetCustomAttributes(true);
            Control _Controls = new Control();

            //finding out control attribue
            int ControlAttributesCount = (from a in attributes where a.GetType().BaseType == typeof(ControlAttribute) || a.GetType().BaseType.BaseType == typeof(ControlAttribute) select a).Count();

            if (ControlAttributesCount == 0)
            {
                //adding default attributes
                ArrayList defaultAttributes = new ArrayList(attributes);
                TextBoxAttribute textbox = new TextBoxAttribute();
                defaultAttributes.Add(textbox);
                string controlName = pi.Name; //String.Format("{0}_TextBoxAttribute", pi.Name);

                RequiredAttribute vrAtt = new RequiredAttribute();
                defaultAttributes.Add(vrAtt);

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
                if (att.GetType().BaseType == typeof(ValidationAttribute))
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
                    }
                    //Assigning ControlToValidate Property with the propertyName
                    string ControlToValidatePropertyName = GetFieldValue(att, "ControlToValidatePropertyName").ToString();
                    ValidationControl.GetType().GetProperty(ControlToValidatePropertyName).SetValue(ValidationControl, pi.Name, null);

                    //firing the event
                    OnValidationCreated(ValidationControl, new FieldEventArgs(pi.Name, pi.PropertyType));
                    _Controls.Controls.Add(ValidationControl);
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

                            //firing event
                            OnLookupControlCreated(control, new FieldEventArgs(pi.Name, pi.PropertyType));

                            //seting select value after event 
                            if (Mode == Opertion.update)
                                controlBindingProperty.SetValue(control, Convert.ChangeType(pi.GetValue(DataSource, null), control.GetType().GetProperty(bindingPropertyName).PropertyType), null);

                            _Controls.Controls.Add(control);
                        }
                        else
                        {
                            //DirectBinding Property
                            controlBindingProperty.SetValue(control, Convert.ChangeType(pi.GetValue(DataSource, null), control.GetType().GetProperty(bindingPropertyName).PropertyType), null);

                            //firing the event
                            OnControlCreated(control, new FieldEventArgs(pi.Name, pi.PropertyType));
                            _Controls.Controls.Add(control);
                        }
                    }
                }
            }

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

            SaveButton.Text = "حفظ";
            SaveButton.Click += SaveClick;

            DeleteButton.Text = "حذف";
            DeleteButton.Click += DeleteClick;

            CancelButton.Text = "إلغاء الأمر";
            CancelButton.Click += CancelClick;

            _controls.Controls.Add(SaveButton);
            _controls.Controls.Add(CancelButton);
            if (Mode == Opertion.update)
                _controls.Controls.Add(DeleteButton);
            return _controls;

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

        private object GetPropertyValue(PropertyInfo pi)
        {
            return null;
        }
        private object GetFieldValue(Attribute att , string fieldName)
        {
            return att.GetType().GetField(fieldName).GetValue(att);
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
